using E_CommerceApi.Helpers;
using E_CommerceApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_CommerceApi.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategorieController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetCategories()
        {
            var dt = DataAccess.GetData(new SqlCommand("select * from categories order by id"), out string message);
            if (message != string.Empty)
                return BadRequest(message);

            var categories = new List<Categorie>();
            foreach (DataRow row in dt.Rows)
            {
                categories.Add(new Categorie
                {
                    ID = int.Parse(row["id"].ToString()),
                    Nom= row["nom"].ToString()
                });
            }

            return Ok(categories);
        }
    }
}
