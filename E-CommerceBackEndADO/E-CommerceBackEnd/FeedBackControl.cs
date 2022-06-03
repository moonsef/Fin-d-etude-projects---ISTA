using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_CommerceBackEnd
{
    public partial class FeedBackControl : UserControl
    {
        public FeedBackControl()
        {
            InitializeComponent();
        }

        private void FeedBackControl_Load(object sender, EventArgs e)
        {
            feedBackGridView.DataSource = DataAccess.getData(new System.Data.SqlClient.SqlCommand("select * from feedbacks order by id"));
            feedBackGridView.AutoResizeColumn(4);
        }

        private void FeedBackControl_VisibleChanged(object sender, EventArgs e)
        {
            FeedBackControl_Load(sender, e);
        }
    }
}
