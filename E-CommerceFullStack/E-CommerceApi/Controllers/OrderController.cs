using E_CommerceApi.Helpers;
using E_CommerceApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_CommerceApi.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        [Route("create")]
        public IHttpActionResult PostOrder([FromBody] List<Order> orders)
        {
            int i = 0;
            int order_id = 0;

            foreach (Order order in orders)
            {


                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                
            var cmd = new SqlCommand("select id from clients where uuid=@id");
            cmd.Parameters.AddWithValue("@id", order.ClientUuid);

            var dt = DataAccess.GetData(cmd, out string err1);

            if (err1 != string.Empty)
                return BadRequest(err1);

            if (dt.Rows.Count == 0)
                return BadRequest("Tu n'a pas authentifié");



            cmd = new SqlCommand("select id, prix, quantity from produits where nom like @search ");
            cmd.Parameters.AddWithValue("@search", string.Format("%{0}%", order.ProduitName));


             dt = DataAccess.GetData(cmd, out string err2);

            if (err2 != string.Empty)
                return BadRequest(err2);

            if (dt.Rows.Count == 0)
                return BadRequest("Produit n'exists pas!");

            int produit_id = int.Parse(dt.Rows[0]["id"].ToString());
            int produit_prix = int.Parse(dt.Rows[0]["prix"].ToString());
            int produit_quantity = int.Parse(dt.Rows[0]["quantity"].ToString());

            if (produit_quantity == 0)
                return BadRequest("Le produit est rupture de stock! ");


            cmd = new SqlCommand("select r.reduction_montant as 'discount' from reduction_produits rp join reductions r on r.id=rp.reduction_id where rp.produit_id=@id");
            cmd.Parameters.AddWithValue("@id", produit_id);


            dt = DataAccess.GetData(cmd, out string err3);

            if (err3 != string.Empty)
                return BadRequest(err3);

            int discount = 0;

            if (dt.Rows.Count > 0)
            {
                discount = int.Parse(dt.Rows[0]["discount"].ToString());

            }



            if(i == 0)
            {

                cmd = new SqlCommand("insert into orders values(@id,@date); SELECT @@IDENTITY AS id;");
                cmd.Parameters.AddWithValue("@id", order.ClientUuid);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                dt = DataAccess.GetData(cmd, out string err4);
                order_id = int.Parse(dt.Rows[0]["id"].ToString());

                if (err4 != string.Empty)
                    return BadRequest(err4);

                i++;
            }


            cmd = new SqlCommand("insert into order_items values(@quantity,@prix,@discount, @orde_id, @pro_id);");
            cmd.Parameters.AddWithValue("@quantity", order.Quantity);
            cmd.Parameters.AddWithValue("@prix", produit_prix);
            cmd.Parameters.AddWithValue("@discount", discount);
            cmd.Parameters.AddWithValue("@orde_id", order_id);
            cmd.Parameters.AddWithValue("@pro_id", produit_id);

            DataAccess.SetData(cmd, out string err5);

            if (err5 != string.Empty)
                return BadRequest(err5 + order_id.ToString());


            cmd = new SqlCommand("update produits set quantity = quantity - @quantity where id=@id");
            cmd.Parameters.AddWithValue("@quantity", order.Quantity);
            cmd.Parameters.AddWithValue("@id", produit_id);

            DataAccess.SetData(cmd, out string err6);

            if (err6 != string.Empty)
                return BadRequest(err6);
            }


            return Ok();
        }
    }
}
