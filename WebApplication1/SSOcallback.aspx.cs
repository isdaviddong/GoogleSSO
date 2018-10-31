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
            if (!this.IsPostBack)
            {
                //get code from queryString
                var code = Request.QueryString["code"];
                //Response.Write("<br/> code: " + code);
                
                //get token from code
                var token = GetTokenFromCode(code,
                    "709970692634-racas8jbr9ta34put8lg51pps952ol2n.apps.googleusercontent.com",
                    "qLajJZ4ZBOYDxc7aEy7jYFLg",
                    "http://localhost:63577/SSOcallback.aspx");
                //把取得的user Token放入Textbox(測試，千萬別在正式環境這樣做)
                this.TextBoxToken.Text = token.access_token;
            }
        }

        private GetTokenFromCodeResult GetTokenFromCode(string code, string ClientId, string ClientSecret, string redirect_uri)
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
                var GetTokenFromCodeResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTokenFromCodeResult>(JSON);
                return GetTokenFromCodeResult;
            }
            catch (WebException ex)
            {
                using (var reader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    throw new Exception("GetTokenFromCode: " + responseText, ex);
                }
            }
        }

        private UserInfoResult GetUserInfo(string token)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                wc.Headers.Clear();
                wc.Headers.Add("Authorization", "Bearer  " + token);

                //get
                string JSON = wc.DownloadString("https://www.googleapis.com/oauth2/v1/userinfo");

                //parsing JSON
                var Result = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfoResult>(JSON);

                return Result;
            }
            catch (WebException ex)
            {
                using (var reader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    throw new Exception("GetUserInfo: " + responseText, ex);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var UserInfoResult = GetUserInfo(this.TextBoxToken.Text);
            //Response.Write("email:" + UserInfoResult.email);
            var DisplayJSON = Newtonsoft.Json.JsonConvert.SerializeObject(
                UserInfoResult, Newtonsoft.Json.Formatting.Indented);
            DisplayJSON = DisplayJSON.Replace("\n", "<br/>");
            Response.Write("<br/><br/>Formated JSON : <br/>" + DisplayJSON);
        }
    }

    public class GetTokenFromCodeResult
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
        public string id_token { get; set; }
    }
    public class UserInfoResult
    {
        public string id { get; set; }
        public string email { get; set; }
        public bool verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string link { get; set; }
        public string picture { get; set; }
        public string locale { get; set; }
    }
}