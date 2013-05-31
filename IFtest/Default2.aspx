<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default2.aspx.cs" Inherits="IFtest.Default1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Hello from Iron Foundry Micro Cloud! (modified1) &nbsp; Instance ID:&nbsp; <asp:Label runat="server" ID="lblInstance_id" /> &nbsp; Instance Index: &nbsp; <asp:Label runat="server" ID="lblInstanceIndex" />
        <br />
        Connection String: <asp:Label runat="server" ID="lblConStr" />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" Visible="False" />
        <br />
        <br /> 
        <asp:LinkButton ID="lbDBpage" runat="server" PostBackUrl="~/DBPage.aspx">Open DB page</asp:LinkButton>
               <br />  <asp:Image ID="Image1" runat="server" Height="71px" 
            ImageUrl="~/images/honeybadger_logo.png" Width="127px" />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
