using E_CommerceApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace E_CommerceApi.Controllers
{
    [RoutePrefix("api/clients")]
    public class ClientController : ApiController
    {
        [Route("client")]
        public IHttpActionResult PostClient([FromBody] string phoneUuid)
        {
            var session = HttpContext.Current.Session["uuid"];
            if (session != null || phoneUuid != null)
            {
                var cmd = new SqlCommand("select nom, prenom, telephone, adresse, mot_de_passe from clients where uuid=@uuid");
                cmd.Parameters.AddWithValue("@uuid", session != null ? session.ToString() : phoneUuid);
                var dt = DataAccess.GetData(cmd, out string message);

                if (message != string.Empty)
                    return BadRequest(message);

                if (dt.Rows.Count == 0)
                    return BadRequest("Invalid client uuid");


                return Ok(new { Nom = dt.Rows[0]["nom"].ToString(), Prenom = dt.Rows[0]["prenom"].ToString(), Adresse = dt.Rows[0]["adresse"].ToString(), Telephone = dt.Rows[0]["telephone"].ToString(), Password=dt.Rows[0]["mot_de_passe"].ToString() });
            }
            return BadRequest("Tu n'es pas authentifié");
        }

        [Route("client/modifier")]
        public IHttpActionResult PostEdit([FromBody] ClientData client)
        {

            var session = HttpContext.Current.Session["uuid"];

            if(session != null || client.PhoneUuid != null)
            { 



                var cmd = new SqlCommand("select id from clients where uuid=@uuid");
                cmd.Parameters.AddWithValue("@uuid", session != null ? session.ToString() : client.PhoneUuid);
                var dt = DataAccess.GetData(cmd, out string message);

                if (message != string.Empty)
                    return BadRequest(message);

                if (dt.Rows.Count == 0)
                    return BadRequest("Invalid client uuid");


                if (!ModelState.IsValid)
                    return BadRequest(ModelState);



                cmd = new SqlCommand("select id from clients where telephone=@tele");
                cmd.Parameters.AddWithValue("@tele", client.Telephone);
                dt = DataAccess.GetData(cmd, out string message1);


                if (message1 != string.Empty)
                    return BadRequest(message1);

                if (dt.Rows.Count > 0)
                    return BadRequest("Telephone existe déjà");


                cmd = new SqlCommand("update clients set nom=@nom, prenom=@prenom, telephone=@telephone, adresse=@adresse, mot_de_passe=@password where uuid=@uuid");
                cmd.Parameters.AddWithValue("@uuid", session != null ? session.ToString() : client.PhoneUuid);
                cmd.Parameters.AddWithValue("@nom", client.Nom);
                cmd.Parameters.AddWithValue("@prenom", client.Prenom);
                cmd.Parameters.AddWithValue("@adresse", client.Adresse);
                cmd.Parameters.AddWithValue("@telephone", client.Telephone);
                cmd.Parameters.AddWithValue("@password", client.Password);
                DataAccess.SetData(cmd, out string error2);

                if (error2 != string.Empty)
                    return BadRequest(error2);

                return Ok();

            }

            return BadRequest("Tu n'es pas authentifié");
        }
    }

    public class ClientData
    {
        [Required(ErrorMessage = "Nom est obligatoire")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Prenom est obligatoire")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Telephone est obligatoire")]
        [RegularExpression(@"(\+212|0)([ \-_/]*)(\d[ \-_/]*){9}", ErrorMessage = "Telephone n'est pas valide")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Adresse est obligatoire")]
        public string Adresse { get; set; }

        [MinLength(6, ErrorMessage = "Le mot de passe doit comporter au moins 6 caractères")]
        public string Password { get; set; }

        public string PhoneUuid { get; set; }

    }
}
