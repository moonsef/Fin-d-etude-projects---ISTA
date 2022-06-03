using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace E_CommerceBackEnd
{
    public partial class PromoControl : UserControl
    {
        static int latest_redu_id;
        static int latest_redu_pro_id;
        static List<int> ids = new List<int>();

        public PromoControl()
        {
            InitializeComponent();
        }

        private void PromoControlLoad(object sender, EventArgs e)
        {

        }

        private void ReductionMontantChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtReductionMontant.Text, "[^0-9]"))
            {

                int tstDbl;
                if (!int.TryParse(txtReductionMontant.Text, out tstDbl))
                {
                    txtReductionMontant.Text = txtReductionMontant.Text.Remove(txtReductionMontant.Text.Length - 1);
                }
            }

        }


        private void PromoControlVisibleChanged(object sender, EventArgs e)
        {
            PromoControlLoad(sender, e);
        }

        private void ChercherReduction(object sender, EventArgs e)
        {
            var validationError = Utils.ValidateTextBoxs(txtReductionNom);

            if(validationError != string.Empty)
            {
                Utils.MessageBox("Nom est oligatoire", MessageErrorLevel.ValidationError, null);
                return;
            }

            var cmd = new SqlCommand("select * from reductions where nom like @search");
            cmd.Parameters.AddWithValue("@search", string.Format("%{0}%", txtReductionNom.Text));
            try
            {
                var dt = DataAccess.getData(cmd);
                if(dt.Rows.Count > 0)
                {
                    reductionDataGridView.DataSource = dt;
                    reductionDataGridView.AutoResizeColumn(0);
                    reductionDataGridView.AutoResizeColumn(2);
                    txtReductionNom.Clear();
                }
                else
                {
                    Utils.MessageBox("Aucun reduction trouvé", MessageErrorLevel.Info, null);
                }

            }catch(Exception ex)
            {
                Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
            }


        }

        private void AjouterReduction(object sender, EventArgs e)
        {
            var validationError = Utils.ValidateTextBoxs(txtReductionNom, txtReductionMontant);
            if(validationError != string.Empty)
            {
                Utils.MessageBox(validationError, MessageErrorLevel.ValidationError, null);
                return;
            }

            if(int.Parse(txtReductionMontant.Text) > 100 || int.Parse(txtReductionMontant.Text) < 1)
            {
                Utils.MessageBox("Le pourcentage incorrect. (1-100)", MessageErrorLevel.ValidationError, null);
                return;
            }
           
            var cmd = new SqlCommand("insert into reductions values(@nom,@redu_pours);");
            cmd.Parameters.AddWithValue("@nom", txtReductionNom.Text);
            cmd.Parameters.AddWithValue("@redu_pours", int.Parse(txtReductionMontant.Text.Trim()));

            try
            {
                DataAccess.setData(cmd);
                txtReductionNom.Clear();
                txtReductionMontant.Clear();
                Utils.MessageBox("Reduction ajouter!", MessageErrorLevel.Info, null);

            }
            catch (Exception ex)
            {
                Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
            }
            finally
            {
                cmd.Dispose();
            }
        }

        private void ReductionCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {

           
                int id = int.Parse(reductionDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());

                if(latest_redu_id != id)
                {
                    var cmd = new SqlCommand("select * from reductions where id=" + id.ToString());
                    try
                    {
                        var dt = DataAccess.getData(cmd);
                        txtReductionNom.Text = dt.Rows[0]["nom"].ToString();
                        txtReductionMontant.Text = dt.Rows[0]["reduction_montant"].ToString();

                    }catch(Exception ex)
                    {
                        Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                    }
                }

                latest_redu_id = id;

            }
        }

        private void SupprimerReduction(object sender, EventArgs e)
        {
            if(latest_redu_id != 0)
            {
                DialogResult dialog = MessageBox.Show("Êtes-vous sûr de vouloir supprimer reduction?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(dialog == DialogResult.OK)
                {
                    try
                    {
                        DataAccess.setData(new SqlCommand("delete from reductions where id=" + latest_redu_id.ToString()));
                        reduProduitDataGridView.DataSource = null;
                        produitDataGridView.DataSource = null;
                        Utils.MessageBox("Reduction a été supprimer", MessageErrorLevel.Info, null);

                    }catch(Exception ex)
                    {
                        Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                    }
                    finally
                    {
                        latest_redu_id = 0;
                        reductionDataGridView.DataSource = null;
                        txtReductionMontant.Clear();
                        txtReductionNom.Clear();
                    }
                }
            }
            else
            {
                Utils.MessageBox("Aucun redution sélectionné", MessageErrorLevel.ValidationError, null);
            }
        }

        private void ModifierRedution(object sender, EventArgs e)
        {
            if(latest_redu_id != 0)
            {
                var validationError = Utils.ValidateTextBoxs(txtReductionMontant, txtReductionNom);
                if(validationError != string.Empty)
                {
                    Utils.MessageBox(validationError, MessageErrorLevel.ValidationError, null);
                    return;
                }
                var cmd = new SqlCommand("update reductions set nom=@nom, reduction_montant=@redu_mon where id=@id;");
                cmd.Parameters.AddWithValue("@nom", txtReductionNom.Text);
                cmd.Parameters.AddWithValue("@redu_mon", double.Parse(txtReductionMontant.Text));
                cmd.Parameters.AddWithValue("@id", latest_redu_id);

                try
                {
                    DataAccess.setData(cmd);
                    Utils.MessageBox("Reduction modifier.", MessageErrorLevel.Info, null);

                }catch(Exception ex)
                {
                    Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                }
                finally
                {
                    txtReductionMontant.Clear();
                    txtReductionNom.Clear();
                    reductionDataGridView.DataSource = null;
                }
            }
            else
            {
                Utils.MessageBox("Aucun redution sélectionné", MessageErrorLevel.ValidationError, null);
            }
        }


        private void ChercherReductionProduit(object sender, EventArgs e)
        {
            var validationError = Utils.ValidateTextBoxs(txtReduProNom);

            if(validationError != string.Empty)
            {
                Utils.MessageBox("Nom reduction est obligatoire", MessageErrorLevel.ValidationError, null);
                return;
            }

            var cmd = new SqlCommand(@"select rep.id as 'id', pr.nom as 'produit nom', re.nom as 'reduction nom', rep.date_debut as 'reduction date debut', rep.date_fin as 'reduction date fin'
                                        from reductions re join reduction_produits rep
                                        on re.id= rep.reduction_id
                                        join produits pr on pr.id=rep.produit_id
                                        where pr.nom like @search"
                                    );
            cmd.Parameters.AddWithValue("@search", string.Format("%{0}%", txtReduProNom.Text));

            try
            {
                var dt = DataAccess.getData(cmd);

                if(dt.Rows.Count > 0)
                {
                    reduProduitDataGridView.DataSource = dt;
                    reduProduitDataGridView.AutoResizeColumn(0);
                    reduProduitDataGridView.AutoResizeColumn(2);
                    txtReduProNom.Clear();
                }
                else
                {
                    Utils.MessageBox("Aucun produit reduction trouvé", MessageErrorLevel.Info, null);
                }

            }catch(Exception ex)
            {
                Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
            }
        }

        private void RecutionProduitCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                int id = int.Parse(reduProduitDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());

              

                if(latest_redu_pro_id != id)
                {

                    var cmd = new SqlCommand(@"select re.nom , rep.date_debut , rep.date_fin
                                                from reductions re join reduction_produits rep
                                                on re.id= rep.reduction_id
                                                where rep.id=" + id.ToString()
                                            );
                    try
                    {
                        var dt = DataAccess.getData(cmd);


                        txtReductionNom.Text = dt.Rows[0]["nom"].ToString();
                        dateTimeDebut.Value = DateTime.Parse(dt.Rows[0]["date_debut"].ToString());
                        dateTimeFin.Value = DateTime.Parse(dt.Rows[0]["date_fin"].ToString());

                    }
                    catch(Exception ex)
                    {
                        Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                    }

                }

                latest_redu_pro_id = id;

            }
        }

        private void SupprimerReductionProduit(object sender, EventArgs e)
        {
            if(latest_redu_pro_id != 0)
            {
                DialogResult dialog = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce produit en réduction?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if(dialog == DialogResult.OK)
                {
                    try
                    {

                        DataAccess.setData(new SqlCommand("delete from reduction_produits where id=" + latest_redu_pro_id.ToString()));
                        Utils.MessageBox("Produit en réduction a été supprimer", MessageErrorLevel.Info, null);

                    }catch(Exception ex)
                    {
                        Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                    }
                    finally
                    {
                        txtReduProNom.Clear();
                        reduProduitDataGridView.DataSource = null;
                        latest_redu_pro_id = 0;
                    }
                }
                  
            }
            else
            {
                Utils.MessageBox("Aucun produit en réduction été sélectionné", MessageErrorLevel.ValidationError, null);
            }
        }

        private void ModifierRedutionProduit(object sender, EventArgs e)
        {
            if(latest_redu_pro_id != 0)
            {
                if(DateTime.Compare(dateTimeDebut.Value, dateTimeFin.Value) > 0)
                {
                    Utils.MessageBox("Date debut doit être inférieur à date fin", MessageErrorLevel.ValidationError, null);
                    return;
                }

                var cmd = new SqlCommand("update reduction_produits set date_debut=@date_debut, date_fin=@date_fin where id=@id");
                cmd.Parameters.AddWithValue("@id", latest_redu_pro_id);
                cmd.Parameters.AddWithValue("@date_debut", dateTimeDebut.Value);
                cmd.Parameters.AddWithValue("@date_fin", dateTimeFin.Value);
                try
                {
                    DataAccess.setData(cmd);
                    Utils.MessageBox("Produit en réduction a été modifié", MessageErrorLevel.Info, null);

                }catch(Exception ex)
                {
                    Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                }
                finally
                {
                    latest_redu_pro_id = 0;
                    txtReduProNom.Clear();
                }
            }
            else
            {
                Utils.MessageBox("Aucun produit en réduction été sélectionné", MessageErrorLevel.ValidationError, null);
            }
        }

        private void AjouterReductionProduit(object sender, EventArgs e)
        {
            if(latest_redu_id == 0)
            {
                Utils.MessageBox("Aucun reduction été sélectionné", MessageErrorLevel.ValidationError, null);
                return;
            }

            if(DateTime.Compare(dateTimeDebut.Value, dateTimeFin.Value) > 0)
            {
                Utils.MessageBox("Date debut doit être inférieur à date fin", MessageErrorLevel.ValidationError, null);
                return;
            }


            if(ids.Count == 0)
            {
                Utils.MessageBox("Au moins sélectionner un produit", MessageErrorLevel.ValidationError, null);
                return;
            }

            for(int i=0; i < ids.Count; i++)
            {
                var cmd = new SqlCommand("insert into reduction_produits values(@pro_id, @redu_id,@date_debut,@date_fin)");
                cmd.Parameters.AddWithValue("@pro_id", ids[i]);
                cmd.Parameters.AddWithValue("@redu_id", latest_redu_id);
                cmd.Parameters.AddWithValue("@date_debut", dateTimeDebut.Value);
                cmd.Parameters.AddWithValue("@date_fin", dateTimeFin.Value);

                try
                {
                    DataAccess.setData(cmd);
                    if(i == ids.Count - 1)
                    {
                        Utils.MessageBox(string.Format("{0} ajouteé", ids.Count > 1 ? "Produits" : "Produit"), MessageErrorLevel.Info, null);
                        produitDataGridView.DataSource = null;
                    }

                }catch(Exception ex) {
                    Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                }

            }


        }

        private void ChercherProduit(object sender, EventArgs e)
        {
            string validationError = Utils.ValidateTextBoxs(txtProduitNom);
            if (validationError != string.Empty)
            {
                Utils.MessageBox("Produit nom est obligatoire", MessageErrorLevel.ValidationError, null);
                return;
            }
            SqlCommand cmd = new SqlCommand("select * from produits where nom like @search and quantity > 0 and id not in (select produit_id from reduction_produits where date_fin > GETDATE())");
            cmd.Parameters.AddWithValue("@search", string.Format("%{0}%", txtProduitNom.Text));

            try
            {
                DataTable dt = CleanDataTableFromUnusedColumns(DataAccess.getData(cmd));
                if (dt.Rows.Count > 0)
                {
                    produitDataGridView.DataSource = dt;
                    produitDataGridView.AutoResizeColumn(0);
                    produitDataGridView.ClearSelection();
                    ids.Clear();

                }
                else
                {
                    Utils.MessageBox("Aucun produit trouvé!", MessageErrorLevel.Info, null);
                }
            }
            catch (Exception ex)
            {
                Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
            }
            
        }

        private DataTable CleanDataTableFromUnusedColumns(DataTable dt)
        {
            dt.Columns.Remove("categorie_id");
            dt.Columns.Remove("marque_id");
            return dt;
        }


        private void produitDataGridViewSelectionChanged(object sender, EventArgs e)
        {
            ids.Clear();
            foreach(DataGridViewCell row in produitDataGridView.SelectedCells)
            {
                var id = int.Parse(produitDataGridView.Rows[row.RowIndex].Cells[0].Value.ToString());
                ids.Add(id);
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtReduProNom_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimeDebut_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimeFin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
   
}
