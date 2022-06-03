using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace E_CommerceBackEnd
{
    public partial class CategoriesAndMarquesControl : UserControl
    {
        private static int latest_cate_id;
        private static int latest_mar_id;
        public CategoriesAndMarquesControl()
        {
            InitializeComponent();
        }

        private void ChercherCategorie(object sender, EventArgs e)
        {
            var validationError = Utils.ValidateTextBoxs(txtNom);

            if(validationError != string.Empty)
            {
                Utils.MessageBox("Nom est obligatoire", MessageErrorLevel.ValidationError, null);
                return;
            }

            SqlCommand cmd = new SqlCommand("select * from categories where nom like @search");
            cmd.Parameters.AddWithValue("@search", string.Format("%{0}%", txtNom.Text));
            try
            {
                var dt = DataAccess.getData(cmd);
                if(dt.Rows.Count > 0)
                {
                    categorieDataGridView.DataSource = DataAccess.getData(cmd);
                    categorieDataGridView.AutoResizeColumn(1);
                    txtNom.Clear();
                }
                else
                {
                    Utils.MessageBox("Aucun categorie trouvé", MessageErrorLevel.Info, null);
                }
            }
            catch (Exception ex)
            {
                Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
            }
            finally
            {
                latest_cate_id = 0;

            }
        }

        private void SupprimmerCategorie(object sender, EventArgs e)
        {
            if(latest_cate_id != 0)
            {
                DialogResult res = MessageBox.Show("Êtes-vous sûr de vouloir supprimer la categorie?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if(res == DialogResult.OK)
                {
                    try
                    {
                        DataAccess.setData(new SqlCommand("delete from categories where id=" + latest_cate_id.ToString()));
                        Utils.MessageBox("Le categorie a été supprimer", MessageErrorLevel.Info, null);

                    }
                    catch (Exception ex)
                    {
                        Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                    }
                    finally
                    {
                        latest_cate_id = 0;
                        categorieDataGridView.DataSource = null;
                        txtNom.Clear();
                    }
                }
            }
            else
            {
                Utils.MessageBox("Aucun categorie sélectionné", MessageErrorLevel.ValidationError, null);
            }
        }

        private void CategorieCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                int id = int.Parse(categorieDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());

                if (latest_cate_id != id)
                {
                    try
                    {
                        var dt = DataAccess.getData(new SqlCommand("select * from categories where id=" + id.ToString()));
                        if (dt.Rows.Count > 0)
                        {
                            txtNom.Text = dt.Rows[0]["nom"].ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                    }
                }

                latest_cate_id = id;

            }
        }

        private void ModifierCategorie(object sender, EventArgs e)
        {
            if(latest_cate_id != 0)
            {
                var validateError = Utils.ValidateTextBoxs(txtNom);

                if(validateError != string.Empty)
                {
                    Utils.MessageBox(validateError, MessageErrorLevel.ValidationError, null);
                    return;
                }
             
                SqlCommand cmd = new SqlCommand("update categories set nom=@nom where id=@id; select * from categories where id=@id");
                cmd.Parameters.AddWithValue("@nom", txtNom.Text);
                cmd.Parameters.AddWithValue("@id", latest_cate_id);

                try
                {
                    categorieDataGridView.DataSource =  DataAccess.getData(cmd);
                    Utils.MessageBox("Le categorie a été modifié", MessageErrorLevel.Info, null);
                }
                catch (Exception ex)
                {
                    Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                }
                finally
                {
                    latest_cate_id = 0;
                    txtNom.Clear();
                }
            }
            else
            {
                Utils.MessageBox("Aucun categorie sélectionné", MessageErrorLevel.ValidationError, null);

            }
        }

        private void AjouterCategorie(object sender, EventArgs e)
        {
           
            var validationError = Utils.ValidateTextBoxs(txtNom);

            if(validationError != string.Empty)
            {
                Utils.MessageBox(validationError, MessageErrorLevel.ValidationError, null);
                return;
            }

            var cmd = new SqlCommand("insert into categories(nom) values(@nom);");
            cmd.Parameters.AddWithValue("@nom", string.Join("-", txtNom.Text.ToLower().Trim().Split(' ')));
            try
            { 
                DataAccess.setData(cmd);
                txtNom.Clear();
                Utils.MessageBox("Le categorie a été ajouter", MessageErrorLevel.Info, null);
            }
            catch (Exception ex)
            {
                Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
            }
        }


        private void AjouterMarque(object sender, EventArgs e)
        {
            var validationError = Utils.ValidateTextBoxs(txtMarqueNom);
            if(validationError != string.Empty)
            {
                Utils.MessageBox(validationError, MessageErrorLevel.ValidationError, null);
                return;
            }

            if(marquePicBox.Image != null)
            {
                Console.WriteLine("hoooooooola");
                var cmd = new SqlCommand("insert into marques values (@nom,@photo);");
                cmd.Parameters.AddWithValue("@nom", txtMarqueNom.Text);
                cmd.Parameters.AddWithValue("@photo", File.ReadAllBytes(marquePicBox.ImageLocation));

                try
                {
                    DataAccess.setData(cmd);

                    marquePicBox.Image = null;
                    txtMarqueNom.Clear();

                    Utils.MessageBox("La marque a été ajouter", MessageErrorLevel.Info, null);
                }catch(Exception ex)
                {
                    Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                }

            }
            else
            {
                Utils.MessageBox("La marque photo est obligatoire", MessageErrorLevel.ValidationError, null);
            }

        }

        private void SupprimmerMarque(object sender, EventArgs e)
        {
            if(latest_mar_id != 0)
            {
                DialogResult res = MessageBox.Show("Êtes-vous sûr de vouloir supprimer la marque?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(res == DialogResult.OK)
                {
                    try
                    {
                        DataAccess.setData(new SqlCommand("delete from marques where id=" + latest_mar_id.ToString()));
                        Utils.MessageBox("La marque a été supprimer", MessageErrorLevel.Info, null);

                    }
                    catch (Exception ex)
                    {
                        Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                    }
                    finally{
                        latest_mar_id = 0;
                        marqueDataGridView.DataSource = null;
                        marquePicBox.Image = null;
                        txtMarqueNom.Clear();
                    }
                }
            }
            else
            {
                Utils.MessageBox("Aucun marque sélectionné", MessageErrorLevel.ValidationError, null);
            }
        }

        private void ModifierMarque(object sender, EventArgs e)
        {
            if(latest_mar_id != 0)
            {
                SqlCommand cmd;

                if(marquePicBox.ImageLocation != null)
                {
                    cmd = new SqlCommand("update marques set nom=@nom, photo=@photo where id=@id");
                    cmd.Parameters.AddWithValue("@nom", txtMarqueNom.Text);
                    cmd.Parameters.AddWithValue("@photo", File.ReadAllBytes(marquePicBox.ImageLocation));
                }
                else
                {
                    cmd = new SqlCommand("update marques set nom=@nom where id=@id");
                    cmd.Parameters.AddWithValue("@nom", txtMarqueNom.Text);
                }
                cmd.Parameters.AddWithValue("@id", latest_mar_id);

                try
                {
                    DataAccess.setData(cmd);
                    Utils.MessageBox("La marque a été modifier", MessageErrorLevel.Info, null);
                }
                catch (Exception ex)
                {
                    Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                }
                finally
                {
                    txtMarqueNom.Clear();
                    marquePicBox.Image = null;
                    latest_mar_id = 0;
                }
            }
            else
            {
                Utils.MessageBox("Aucun marque sélectionné", MessageErrorLevel.ValidationError, null);
            }
        }

        private void ChercherMarque(object sender, EventArgs e)
        {
            string validationError = Utils.ValidateTextBoxs(txtMarqueNom);

            if(validationError != string.Empty)
            {
                Utils.MessageBox(validationError, MessageErrorLevel.ValidationError, null);
                return;
            }
            var cmd = new SqlCommand("select id,nom from marques where nom like @search");
            cmd.Parameters.AddWithValue("@search", string.Format("%{0}%", txtMarqueNom.Text));
            try
            {
                var dt = DataAccess.getData(cmd);
                if(dt.Rows.Count > 0)
                {
                    marqueDataGridView.DataSource = dt;
                    marqueDataGridView.AutoResizeColumn(1);
                    txtMarqueNom.Clear();

                }
                else
                {
                    Utils.MessageBox("Aucun marque trouvé", MessageErrorLevel.Info, null);
                }

            }
            catch(Exception ex)
            {
                Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
            }
            finally
            {
                latest_mar_id = 0;
            }
        }

        private void MarqueCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(marqueDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());

            if(latest_mar_id != id)
            {
                try
                { 
                    var dt = DataAccess.getData(new SqlCommand("select id,nom,photo from marques where id=" + id.ToString()));
                   
                    txtMarqueNom.Text = dt.Rows[0]["nom"].ToString();
                    marquePicBox.Image = Image.FromStream(new MemoryStream((byte[])dt.Rows[0]["photo"]));
                    
                    

                }catch(Exception ex)
                {
                    Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                }
            }
            latest_mar_id = id;
        }

        private void ImportMarqueImage(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Title = "Selectionner image",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                marquePicBox.Image = Image.FromFile(dialog.FileName);
                marquePicBox.ImageLocation = dialog.FileName;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
