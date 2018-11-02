using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoogleSSO;

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
                var token = Utility.GetTokenFromCode(code,
                    "709970692634-racas8jbr9ta34put8lg51pps952ol2n.apps.googleusercontent.com",
                    "94Z4YU0lXLi5bd3vG9trP1N0",
                    "http://localhost:63577/SSOcallback.aspx");
                //把取得的user Token放入Textbox(測試，千萬別在正式環境這樣做)
                this.TextBoxToken.Text = token.access_token;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var UserInfoResult = Utility.GetUserInfo(this.TextBoxToken.Text);
            //Response.Write("email:" + UserInfoResult.email);
            var DisplayJSON = Newtonsoft.Json.JsonConvert.SerializeObject(
                UserInfoResult, Newtonsoft.Json.Formatting.Indented);
            DisplayJSON = DisplayJSON.Replace("\n", "<br/>");
            Response.Write("<br/><br/>Formated JSON : <br/>" + DisplayJSON);
        }
    }

}