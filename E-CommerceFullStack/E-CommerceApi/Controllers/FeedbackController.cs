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
    [RoutePrefix("api/feedbacks")]
    public class FeedbackController : ApiController
    {
        [Route("create")]
        public IHttpActionResult PostFeedBack([FromBody]Feedback feedback)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cmd = new SqlCommand("insert into feedbacks values (@name,@email,@subject,@message);");
            cmd.Parameters.AddWithValue("@name", feedback.FullName);
            cmd.Parameters.AddWithValue("@email", feedback.Email);
            cmd.Parameters.AddWithValue("@subject", feedback.Subject);
            cmd.Parameters.AddWithValue("@message", feedback.Message);

            DataAccess.SetData(cmd, out string err);
            if (err != string.Empty)
                return BadRequest(err);

            return Ok();
        }
    }
}
