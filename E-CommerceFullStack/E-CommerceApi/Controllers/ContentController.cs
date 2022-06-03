using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace E_CommerceApi.Controllers
{
    [RoutePrefix("")]
    public class ContentController : ApiController
    {
        [Route("login")]
        public HttpResponseMessage GetLogin()
        {
            return GetPageByPath("login");
        }

        [Route("profile")]
        public HttpResponseMessage GetProfile()
        {
            return GetPageByPath("profile");
        }

        [Route("register")]
        public HttpResponseMessage GetRegister()
        {
            return GetPageByPath("register");
        }

        [Route("")]
        public HttpResponseMessage GetIndex()
        {
            return GetPageByPath("index");
        }

        [Route("produits")]
        public HttpResponseMessage GetProduits()
        {
            return GetPageByPath("produits");
        }

        [Route("produits/produit")]
        public HttpResponseMessage GetOProduits()
        {
            return GetPageByPath("produit");
        }

        [Route("contact")]
        public HttpResponseMessage GetContact()
        {
            return GetPageByPath("contact");
        }

        private HttpResponseMessage GetPageByPath(string name)
        {
            var fileContents = File.ReadAllText(HttpContext.Current.Server.MapPath(string.Format("~/Content/pages/{0}.html", name)));
            var response = new HttpResponseMessage
            {
                Content = new StringContent(fileContents),
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
