using E_CommerceApi.Helpers;
using E_CommerceApi.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.Web.Http;

namespace E_CommerceApi.Controllers
{
    [RoutePrefix("api/avis")]
    public class AvisController : ApiController
    {
        [Route("create")]
        public IHttpActionResult PostAvis([FromBody] Avis avis)
        {

            var session = HttpContext.Current.Session["uuid"];

            if (session == null && avis.PhoneUuid == null)
                return BadRequest("Tu n'a pas authentifié");


            if (!ModelState.IsValid || avis == null)
                return BadRequest(ModelState);

            int produitID;
            int clientID;


            var cmd = new SqlCommand("select id from produits where nom=@nom");
            cmd.Parameters.AddWithValue("@nom", avis.ProduitNom);
            var dt = DataAccess.GetData(cmd, out string er);
            if (er != string.Empty)
                return BadRequest(er);

            if (dt.Rows.Count == 0)
                return BadRequest("Ce produit n'est exists pas!");

            produitID = int.Parse(dt.Rows[0]["id"].ToString());


            cmd = new SqlCommand("select id from clients where uuid=@uuid");
            cmd.Parameters.AddWithValue("@uuid", session != null ? session.ToString() : avis.PhoneUuid);
            dt = DataAccess.GetData(cmd, out string message);
            if (message != string.Empty)
                return BadRequest(message);

            if (dt.Rows.Count == 0)
                return BadRequest("Invalid client uuid");

            clientID = int.Parse(dt.Rows[0]["id"].ToString());
            cmd = new SqlCommand("select id from produit_ratings where client_id=@client_id and produit_id=@id");
            cmd.Parameters.AddWithValue("@client_id", clientID);
            cmd.Parameters.AddWithValue("@id", produitID);
            dt = DataAccess.GetData(cmd, out string err);


            if (err != string.Empty)
                return BadRequest(err);

            if (dt.Rows.Count > 0)
                return BadRequest("Vous avez déjà évalué ce produit!");

            

            cmd = new SqlCommand("insert into produit_ratings values(@text,@rating,@date,@id,@client_id)");
            cmd.Parameters.AddWithValue("@text", avis.Text);
            cmd.Parameters.AddWithValue("@rating", avis.AvisEtoile);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.Parameters.AddWithValue("@id", produitID);
            cmd.Parameters.AddWithValue("@client_id", clientID);

            DataAccess.SetData(cmd, out string err2);

            if (err2 != string.Empty)
                return BadRequest(err2);


            return Ok();
        }

        [Route("delete")]
        public IHttpActionResult DeleteAvis([FromBody] DeleteAvis avis)
        {
            var session = HttpContext.Current.Session["uuid"];

            if (session == null && avis.PhoneUuid == null)
                return BadRequest("Tu n'a pas authentifié");


            var cmd = new SqlCommand("select id from clients where uuid=@uuid");
            cmd.Parameters.AddWithValue("@uuid", session != null ? session.ToString() : avis.PhoneUuid);
            var dt = DataAccess.GetData(cmd, out string message);
            if (message != string.Empty)
                return BadRequest(message);

            if (dt.Rows.Count == 0)
                return BadRequest("Invalid client uuid");


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int produitID;
            var hostname = HttpContext.Current.Request.UserHostAddress;
            IPAddress ip = IPAddress.Parse(hostname);

            cmd = new SqlCommand("select id from produits where nom=@nom");
            cmd.Parameters.AddWithValue("@nom", avis.ProduitNom);
            dt = DataAccess.GetData(cmd, out string er);
            if (er != string.Empty)
                return BadRequest(er);

            if (dt.Rows.Count == 0)
                return BadRequest("Ce produit n'est exists pas!");

            produitID = int.Parse(dt.Rows[0]["id"].ToString());

            cmd = new SqlCommand("select id from produit_ratings where ip=@ip and produit_id=@id");
            cmd.Parameters.AddWithValue("@ip", ip.ToString());
            cmd.Parameters.AddWithValue("@id", produitID);
            dt = DataAccess.GetData(cmd, out string err);


            if (err != string.Empty)
                return BadRequest(err);

            if (dt.Rows.Count == 0)
                return BadRequest("Aucun produit avis trouvé !");

            cmd = new SqlCommand("delete from produit_ratings where id=" + dt.Rows[0]["id"].ToString());
            DataAccess.SetData(cmd, out string e);


            if (e != string.Empty)
                return BadRequest(e);


            return Ok();

        }
    }
    public class DeleteAvis
    {
 
        [Required(ErrorMessage = "Le produit nom est obligatoire")]
        public string ProduitNom { get; set; }
        public string PhoneUuid { get; set; }
    }
}
