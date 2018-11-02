using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace GoogleSSO 
{

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

    public class Utility
    {
        public static GetTokenFromCodeResult GetTokenFromCode(string code, string ClientId, string ClientSecret, string redirect_uri)
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

        public static UserInfoResult GetUserInfo(string token)
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

    }
}