using System;
using System.Windows.Forms;

namespace E_CommerceBackEnd
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            produitsControl1.Hide();
            categoriesAndMarquesControl1.Hide();
            promoControl1.Hide();
            feedBackControl1.Hide();
            ordersControl1.Hide();
            SidePanel.Visible = false;
            
        }
        private void CloseApp(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Êtes-vous sûr de vouloir quitter?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(res == DialogResult.OK)
                Close();
        }

        private void MinimizeApp(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void CategorieAndMarqueButtonClicked(object sender, EventArgs e)
        {
            SidePanel.Visible = true;

            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            produitsControl1.Hide();
            promoControl1.Hide();
            feedBackControl1.Hide();
            ordersControl1.Hide();


            categoriesAndMarquesControl1.Show();

        }

        private void ProduitButtonClicked(object sender, EventArgs e)
        {
            SidePanel.Visible = true;
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
            promoControl1.Hide();
            feedBackControl1.Hide();
            ordersControl1.Hide();


            categoriesAndMarquesControl1.Hide();
            produitsControl1.Show();
        }

        private void PromoButtonClicked(object sender, EventArgs e)
        {
            SidePanel.Visible = true;
            SidePanel.Height = button3.Height;
            SidePanel.Top = button3.Top;
            categoriesAndMarquesControl1.Hide();
            feedBackControl1.Hide();
            ordersControl1.Hide();

            produitsControl1.Hide();
            promoControl1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SidePanel.Visible = true;
            SidePanel.Height = button4.Height;
            SidePanel.Top = button4.Top;
            categoriesAndMarquesControl1.Hide();
            produitsControl1.Hide();
            promoControl1.Hide();
            ordersControl1.Hide();

            feedBackControl1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SidePanel.Visible = true;
            SidePanel.Height = button5.Height;
            SidePanel.Top = button5.Top;
            categoriesAndMarquesControl1.Hide();
            produitsControl1.Hide();
            promoControl1.Hide();
            feedBackControl1.Hide();
            ordersControl1.Show();

        }
    }
}
