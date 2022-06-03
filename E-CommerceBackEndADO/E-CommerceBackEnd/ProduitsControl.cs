using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace E_CommerceBackEnd
{
    public partial class ProduitsControl : UserControl
    {
        private static int latest_id;
        private List<string> imagesFileNames = new List<string>();

        public ProduitsControl()
        {
            InitializeComponent();
            imageListView.View = View.LargeIcon;
            imageList.ImageSize = new Size(180, 130);
            imageListView.LargeImageList = imageList;
            
        }


        private void AjouterProduct(object sender, EventArgs e)
        {
         
            // validate comboboxs and textboxs
            string textBoxError = Utils.ValidateTextBoxs(txtNom, txtPrix, txtQuantity, txtDescription);
            string comboBoxError = Utils.ValidateComboBoxs(comboEtat, comboCategorie ,comboMarque);

            if (textBoxError != string.Empty)
            {
                Utils.MessageBox(textBoxError,MessageErrorLevel.ValidationError, null);
                return;
            }

            if (comboBoxError != string.Empty)
            {
                Utils.MessageBox(comboBoxError, MessageErrorLevel.ValidationError, null);
                return;
            }

            if(imagesFileNames.Count == 0)
            {
                Utils.MessageBox("Sélectionnez au moins une image", MessageErrorLevel.ValidationError, null);
                return;
            }

            SqlCommand cmd = LoadDataToSqlCommandObject("insert into produits values(@nom, @prix,@description, @quantity ,@etat,@statut,@date ,@cate_id, @mar_id);SELECT @@IDENTITY AS id;");

            try
            {
                int id = int.Parse(DataAccess.getData(cmd).Rows[0]["id"].ToString());

                foreach(string file in imagesFileNames)
                {
                    cmd = new SqlCommand("insert into produit_photos values(@photo,@produit_id);");
                    cmd.Parameters.AddWithValue("@photo", File.ReadAllBytes(file));
                    cmd.Parameters.AddWithValue("@produit_id", id);
                    DataAccess.setData(cmd);
                }

                imagesFileNames.Clear();
                ClearImageListAndListView();
                ClearInputsAndComboBoxs();

                Utils.MessageBox("Produit ajouter!", MessageErrorLevel.Info, null);

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

        private void PrixChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtPrix.Text, "[^0-9]"))
            {

                double tstDbl;
                if (!double.TryParse(txtPrix.Text, out tstDbl))
                {
                    txtPrix.Text = txtPrix.Text.Remove(txtPrix.Text.Length - 1);
                }
            }
           
        }

        private void ModifierProduit(object sender, EventArgs e)
        {
            if(latest_id != 0)
            {
                // validate textboxs
                string textBoxError = Utils.ValidateTextBoxs(txtNom, txtPrix, txtQuantity, txtDescription);

                if (textBoxError != string.Empty)
                {
                    Utils.MessageBox(textBoxError, MessageErrorLevel.ValidationError, null);
                    return;
                }

                SqlCommand cmd = LoadDataToSqlCommandObject("update produits set nom=@nom, prix=@prix,description=@description, quantity=@quantity,etat=@etat,statut=@statut,categorie_id=@cate_id,marque_id=@mar_id where id=" + latest_id.ToString() + "; select * from produits where id="+ latest_id.ToString() + ";");
                try
                {
                    DataTable dt = CleanDataTableFromUnusedColumns(DataAccess.getData(cmd));
                    produitsDataGridView.DataSource = dt;

                    if (imagesFileNames.Count > 0)
                    {
                        foreach (string file in imagesFileNames)
                        {
                            cmd = new SqlCommand("insert into produit_photos values(@photo,@produit_id);");
                            cmd.Parameters.AddWithValue("@photo", File.ReadAllBytes(file));
                            cmd.Parameters.AddWithValue("@produit_id", latest_id);
                            DataAccess.setData(cmd);
                        }

                        imagesFileNames.Clear();
                    }

                    ClearImageListAndListView();
                    ClearInputsAndComboBoxs();
                    Utils.MessageBox("Le produit a été modifié", MessageErrorLevel.Info, null);

                }
                catch(Exception ex)
                {
                    Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                }
                finally
                {
                    cmd.Dispose();
                    latest_id = 0;
                }

            }
            else
            {
                Utils.MessageBox("Aucun produit sélectionné", MessageErrorLevel.ValidationError, null);
            }
        }

        private void QuantityChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtQuantity.Text, "[^0-9]"))
            {
                txtQuantity.Text = txtQuantity.Text.Remove(txtQuantity.Text.Length - 1);
            }
            
        }

        private void SupprimerProduit(object sender, EventArgs e)
        {
            if(latest_id != 0)
            {
                DialogResult msg = MessageBox.Show("Êtes-vous sûr de vouloir supprimer le produit?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(msg == DialogResult.OK)
                {
                    try
                    {
                        DataAccess.setData(new SqlCommand("delete from produits where id="+ latest_id.ToString()));

                        
                        Utils.MessageBox("Produit a ete supprimmer", MessageErrorLevel.Info, null);
                    }
                    catch(Exception ex)
                    {
                        Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                    }
                    finally
                    {
                        imagesFileNames.Clear();

                        ClearImageListAndListView();
                        ClearInputsAndComboBoxs();

                        produitsDataGridView.DataSource = null;
                        latest_id = 0;
                    }
                   
                }
            }
            else
            {
                Utils.MessageBox("Aucun produit sélectionné", MessageErrorLevel.ValidationError, null);

            }
        }

        private void ChercherProduit(object sender, EventArgs e)
        {
            
            string validationError = Utils.ValidateTextBoxs(txtNom);
            if(validationError != string.Empty)
            {
                Utils.MessageBox(validationError, MessageErrorLevel.ValidationError, null);
                return;
            }
            SqlCommand cmd = new SqlCommand("select * from produits where nom like @search");
            cmd.Parameters.AddWithValue("@search", string.Format("%{0}%", txtNom.Text));

            try
            {
                DataTable dt = CleanDataTableFromUnusedColumns(DataAccess.getData(cmd));
                if(dt.Rows.Count > 0)
                {
                    produitsDataGridView.DataSource = dt;
                    imageList.Images.Clear();
                    imageListView.Clear();
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
            finally
            {
                ClearInputsAndComboBoxs();
                latest_id = 0;
            }
        }

        private void ProduitsControlLoad(object sender, EventArgs e)
        {

            // load categories
            comboCategorie.DataSource = DataAccess.getData(new SqlCommand("select id,nom from categories"));
            comboCategorie.DisplayMember = "nom";
            comboCategorie.ValueMember = "id";
            comboCategorie.SelectedIndex = -1;

            // load marques
            comboMarque.DataSource = DataAccess.getData(new SqlCommand("select id,nom from marques"));
            comboMarque.DisplayMember = "nom";
            comboMarque.ValueMember = "id";
            comboMarque.SelectedIndex = -1;

        }

        private void ClearInputsAndComboBoxs()
        {
            txtDescription.Clear();
            txtNom.Clear();
            txtPrix.Clear();
            txtQuantity.Clear();
            comboCategorie.SelectedIndex = -1;
            comboEtat.SelectedIndex = -1;
            comboMarque.SelectedIndex = -1;
        }

        private void ImportImagesButtonClicked(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Multiselect = true,
                Title = "Selectionner images",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach(string file in dialog.FileNames)
                {
                    imagesFileNames.Add(file);
                    LoadImagesFileNamesToListView(dialog.FileNames);
                }
            }
            dialog.Dispose();
        }

        private void ClearImageListAndListView()
        {
            imageList.Images.Clear();
            imageListView.Items.Clear();
        }

        private void LoadImagesFileNamesToListView(string[] files)
        {
            if(imagesFileNames.Count == 0 && latest_id == 0)
                ClearImageListAndListView();

            foreach (string file in files)
            {
                imageList.Images.Add(Image.FromFile(file));
            }

            for (int i= imageList.Images.Count - 1; i < imageList.Images.Count + files.Length; i++)
            {
                ListViewItem item = new ListViewItem
                {
                    ImageIndex = i
                };
                imageListView.Items.Add(item);
            }

            // faites défiler jusqu'à la dernière image ajoutée dans une "ListView"
            imageListView.Items[imageListView.Items.Count - 1].EnsureVisible();

        }

        private SqlCommand LoadDataToSqlCommandObject(string query)
        {
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@nom", txtNom.Text);
            cmd.Parameters.AddWithValue("@prix", txtPrix.Text);
            cmd.Parameters.AddWithValue("@description", txtDescription.Text);
            cmd.Parameters.AddWithValue("@quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@etat", comboEtat.SelectedItem);
            cmd.Parameters.AddWithValue("@statut", int.Parse(txtQuantity.Text) > 0 ? "en stock" : "rupture de stock");
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.Parameters.AddWithValue("@cate_id", comboCategorie.SelectedValue);
            cmd.Parameters.AddWithValue("@mar_id", comboMarque.SelectedValue);
            return cmd;
        }

        private DataTable CleanDataTableFromUnusedColumns(DataTable dt)
        {
            dt.Columns.Remove("categorie_id");
            dt.Columns.Remove("marque_id");
            return dt;
        }

        private void LoadImagesStreamsToListView(DataRowCollection collection)
        {

            ClearImageListAndListView();

            foreach (DataRow row in collection)
            {
                try
                {
                    byte[] image = (byte[])row["photo"];
                    MemoryStream stream = new MemoryStream(image);
                    Image img = Image.FromStream(stream);

                    imageList.Images.Add(img);
                }catch (Exception e)
                {
                    Utils.MessageBox(e.Message, MessageErrorLevel.Error, "Image decoding error");
                }
            }

            if(imageList.Images.Count > 0)
            { 
       
                for (int j = 0; j < imageList.Images.Count; j++)
                {
                    ListViewItem item = new ListViewItem
                    {
                        ImageIndex = j
                    };

                    // Enregistrer l'identifiant de l'image dans la "Item Tag"
                    item.Tag = collection[j]["id"];
                    imageListView.Items.Add(item);
                }
                
            }
        }

        private void ImageListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            if (imageListView.SelectedItems.Count > 0)
            {
                var item = imageListView.SelectedItems[0];
                DialogResult msg = MessageBox.Show("Êtes-vous sûr de vouloir supprimer la photo?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(msg == DialogResult.OK)
                {
                    try
                    {
                        DataAccess.setData(new SqlCommand("delete from produit_photos where id=" + item.Tag.ToString()));
                        item.Remove();
                        Utils.MessageBox("Photo a été supprimmer", MessageErrorLevel.Info, null);
                    }
                    catch (Exception ex)
                    {
                        Utils.MessageBox(ex.Message, MessageErrorLevel.Error, null);
                    }
                }
            }
        }

        private void ProduitsGridViewCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { 

                int id = int.Parse(produitsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());

                // si la dernière ligne sélectionnée est égale à celle sélectionnée.
                // nous arrêtons à nouveau de charger les données
                if (latest_id != id)
                {
                    SqlCommand cmd = new SqlCommand("select * from produits where id=@id");
                    cmd.Parameters.AddWithValue("@id", id);
                    DataTable dt = DataAccess.getData(cmd);


                    if (dt.Rows.Count > 0)
                    {
                        var prix = string.Join("", dt.Rows[0]["prix"].ToString().Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));

                        txtNom.Text = dt.Rows[0]["nom"].ToString();
                        txtPrix.Text = prix;
                        txtQuantity.Text = dt.Rows[0]["quantity"].ToString();
                        txtDescription.Text = dt.Rows[0]["description"].ToString();
                        comboCategorie.SelectedValue = int.Parse(dt.Rows[0]["categorie_id"].ToString());
                        comboMarque.SelectedValue = int.Parse(dt.Rows[0]["marque_id"].ToString());
                        comboEtat.SelectedIndex = dt.Rows[0]["etat"].ToString() == "neuf" ? 0 : 1;
                        cmd = new SqlCommand("select id,photo from produit_photos where produit_id=@id");
                        cmd.Parameters.AddWithValue("@id", dt.Rows[0]["id"].ToString());


                        LoadImagesStreamsToListView(DataAccess.getData(cmd).Rows);

                    }

                }
                // mettre à jour la dernière ligne sélectionnée
                latest_id = id;
            }

        }

        private void ProduitControlVisibleChanged(object sender, EventArgs e)
        {
            ProduitsControlLoad(sender, e);
        }
    }

}
