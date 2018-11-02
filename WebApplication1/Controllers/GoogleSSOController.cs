using GoogleSSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class GoogleSSOController : Controller
    {
        // GET: GoogleSSO
        public ActionResult Index()
        {
            //get code from queryString
            var code = Request.QueryString["code"];
            //Response.Write("<br/> code: " + code);

            //get token from code
            var token =  Utility.GetTokenFromCode(code,
                "709970692634-racas8jbr9ta34put8lg51pps952ol2n.apps.googleusercontent.com",
                "94Z4YU0lXLi5bd3vG9trP1N0",
                "http://localhost:63577/GoogleSSO");

            var UserInfoResult = Utility.GetUserInfo(token.access_token);
            //Response.Write("email:" + UserInfoResult.email);
            var DisplayJSON = Newtonsoft.Json.JsonConvert.SerializeObject(
                UserInfoResult, Newtonsoft.Json.Formatting.Indented);
            DisplayJSON = DisplayJSON.Replace("\n", "<br/>");

            dynamic ret = new { DisplayJSON = DisplayJSON, token = token.access_token };
            return View(ToExpando(ret));
        }

        public  System.Dynamic.ExpandoObject ToExpando( object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = new System.Web.Routing.RouteValueDictionary(anonymousObject);
            IDictionary<string, object> expando = new System.Dynamic.ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (System.Dynamic.ExpandoObject)expando;
        }
    }
}
