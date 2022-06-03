using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_CommerceBackEnd
{
    public partial class OrdersControl : UserControl
    {
        private int latest_order_id;
        public OrdersControl()
        {
            InitializeComponent();
        }

        private void OrdersControl_Load(object sender, EventArgs e)
        {
        

            var cmd = new SqlCommand(@"select o.id, c.nom + ' ' + c.prenom as 'nom et prenom', o.order_date,
                    convert(int, sum((ot.list_prix - (ot.list_prix * ot.discount / 100)) * ot.quantity)) as 'total prix order' from orders o
                    join clients c on c.uuid=o.client_uuid
                    join order_items ot on ot.order_id=o.id
                    group by o.id, o.order_date, c.prenom, c.nom
                    order by o.order_date desc");

            orderDataGridView.DataSource = DataAccess.getData(cmd);
            orderDataGridView.AutoResizeColumn(1);
        }

        

        private void OrdersControl_VisibleChanged(object sender, EventArgs e)
        {
            OrdersControl_Load(sender, e);

        }

        private void orderDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;



            int id = int.Parse(orderDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());

            if(latest_order_id != id)
            {
                var query = @"select  p.nom as 'produit nom',  ot.quantity, ot.list_prix, ot.discount,
                        convert(int, (ot.list_prix - (ot.list_prix * ot.discount / 100)) * ot.quantity)  as 'total prix'
                        from orders o
                        join order_items ot on o.id=ot.order_id
                        join produits p on p.id=ot.produit_id
                        where o.id=" + id;

                orderItemsDataGridView.DataSource = DataAccess.getData(new SqlCommand(query));
                orderItemsDataGridView.AutoResizeColumn(1);

            }

            latest_order_id = id;
        }
    }
}
