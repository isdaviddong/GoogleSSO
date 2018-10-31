using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class SSOcallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //get code
            var code = Request.QueryString["code"];
            Response.Write("<br/> code: " + code);
            var token = GetTokenFromCode(code, 
                "709970692634-racas8jbr9ta34put8lg51pps952ol2n.apps.googleusercontent.com",
                "",
                "http://localhost:63577/SSOcallback.aspx");
            Response.Write("<br/>token: " + token);
        }

        private string GetTokenFromCode(string code , string ClientId, string ClientSecret, string redirect_uri)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Clear();
                //wc.Headers.Add("Content-Type", "application/json");

                var data = new System.Collections.Specialized.NameValueCollection();
                data["grant_type"] = "authorization_code";
                data["code"] = code;
                data["redirect_uri"] = redirect_uri;
                data["client_id"] = ClientId;
                data["client_secret"] = ClientSecret;
                //post
                byte[] bResult = wc.UploadValues("https://www.googleapis.com/oauth2/v4/token", data);
                //get result
                string JSON = System.Text.Encoding.UTF8.GetString(bResult);
                //parsing JSON
               // var GetTokenFromCodeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTokenFromCodeResult>(JSON);
                return JSON;
            }
            catch (WebException ex)
            {
                using (var reader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    throw new Exception("GetToeknFromCode: " + responseText, ex);
                }
            }
        }
    }
}