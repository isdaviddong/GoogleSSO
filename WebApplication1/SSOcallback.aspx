<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SSOcallback.aspx.cs" Inherits="WebApplication1.SSOcallback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            Token: (正式環境請勿將token顯示於畫面上)
            <asp:TextBox ID="TextBoxToken" runat="server" Width="1052px"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get User Info" />

        </div>
    </form>
</body>
</html>
