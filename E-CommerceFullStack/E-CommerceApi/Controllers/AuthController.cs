using System;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using E_CommerceApi.Helpers;
using System.Web;
using E_CommerceApi.Models;
using System.Linq;

namespace E_CommerceApi.Controllers
{


    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {

        [Route("logout")]
        public IHttpActionResult PostLogout()
        {
            HttpContext.Current.Session.Clear();
            return Ok();
        }


        [Route("me")]
        public IHttpActionResult PostMe([FromBody] string phoneUuid)
        {
            var session = HttpContext.Current.Session["uuid"];

            if (session != null || phoneUuid != null)
            {
                var cmd = new SqlCommand("select nom, prenom from clients where uuid=@uuid");
                cmd.Parameters.AddWithValue("@uuid", session != null ? session.ToString() : phoneUuid);
                var dt = DataAccess.GetData(cmd, out string message);
                if (message != string.Empty)
                    return BadRequest(message);

                if (dt.Rows.Count == 0)
                    return BadRequest("Invalid client uuid");

                return Ok(new { Prenom= dt.Rows[0]["nom"].ToString(), Nom=dt.Rows[0]["prenom"].ToString() });
            }


            return BadRequest("Tu n'es pas authentifié");
        }

        [Route("login")]
        public IHttpActionResult PostLoginClient([FromBody]LoginData data)
        {
            // validate request data
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cmd = new SqlCommand("select uuid from clients where email=@email and mot_de_passe=@password;");
            cmd.Parameters.AddWithValue("@email", data.Email);
            cmd.Parameters.AddWithValue("@password", data.Password);

            var dt = DataAccess.GetData(cmd, out string message);
            if (message != string.Empty)
                return BadRequest(message);


            if(dt.Rows.Count == 0)
                return BadRequest("Les informations d'identification invalides");


            var uuid = dt.Rows[0]["uuid"].ToString();
            if (data.Phone)
                return Ok(uuid);

            HttpContext.Current.Session["uuid"] = uuid;
            return Ok();

        }

        [Route("register")]
        public IHttpActionResult PostRegisterClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dt = DataAccess.GetData(new SqlCommand(string.Format("select id from clients where email='{0}';", client.Email)), out string message1);
            
            if (message1 != string.Empty)
                return BadRequest(message1);

            if (dt.Rows.Count > 0)
                return BadRequest("Email existe déjà");


            dt = DataAccess.GetData(new SqlCommand(string.Format("select id from clients where telephone='{0}';", client.Telephone)), out string message3);

            if (message3 != string.Empty)
                return BadRequest(message3);

            if (dt.Rows.Count > 0)
                return BadRequest("Telephone existe déjà");

            var cmd = new SqlCommand("insert into clients values(@nom,@prenom,@email,@telephone,@adresse,@password,@uuid);");
            var uuid = Guid.NewGuid().ToString();
            cmd.Parameters.AddWithValue("@nom", client.Nom);
            cmd.Parameters.AddWithValue("@prenom", client.Prenom);
            cmd.Parameters.AddWithValue("@email", client.Email);
            cmd.Parameters.AddWithValue("@telephone", client.Telephone);
            cmd.Parameters.AddWithValue("@adresse", client.Adresse);
            cmd.Parameters.AddWithValue("@password", client.Password);
            cmd.Parameters.AddWithValue("@uuid", uuid);

            DataAccess.SetData(cmd, out string message2);

            if (message2 != string.Empty)
                return BadRequest(message2);


            if (client.Phone)
                return Ok(uuid);

            HttpContext.Current.Session["uuid"] = uuid;
            return Ok();
        }

        [Route("test")]
        [HttpPost]
        public IHttpActionResult PostTest()
        {
            return Ok(HttpContext.Current.Session["uuid"]);
        }

    }

    public class LoginData
    {

        [Required(ErrorMessage = "Mot de passe est obligatoire")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email est obligatoire")]
        [EmailAddress(ErrorMessage = "Email n'est pas valide")]
        public string Email { get; set; }
        public bool Phone { get; set; } = false;
    }
}
