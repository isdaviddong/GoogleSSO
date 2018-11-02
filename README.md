GoogleSSO
===

## Google Single Sign On with C#

### MVC版本
使用到 index.html 與 GoogleSSOController.cs
注意：測試本範例你必須把index.html以及GoogleSSOController.cs中的所有ClientID與ClientSecret與redirecr_uri換成你自己申請的，否則將無法正確執行("Unauthorized")。 
GCP上'已授權的重新導向 URI'必須註冊 
http://localhost:63577/GoogleSSO

### WebForm版本
使用到 default.aspx 與 SSOcallback.aspx
注意：測試本範例你必須把default.aspx以及SSOcallback.aspx頁面中的所有ClientID與ClientSecret與redirecr_uri換成你自己申請的，否則將無法正確執行("Unauthorized")。 
GCP上'已授權的重新導向 URI'必須註冊 
http://localhost:63577/GoogleSSO
