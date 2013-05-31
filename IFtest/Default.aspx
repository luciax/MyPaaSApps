<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IFtest.Default" EnableViewStateMac="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Hello from the Intel Cloud! &nbsp; Instance ID:&nbsp; <asp:Label runat="server" ID="lblInstance_id" /> &nbsp; Instance Index: &nbsp; <asp:Label runat="server" ID="lblInstanceIndex" />
               &nbsp; 
        <br />
       
        <asp:TextBox ID="txtMachineName" runat="server" Visible="false" Width="797px" Height="31px"></asp:TextBox>
               <br />
        <br />
         <asp:TextBox  ID="txtConnectionInfo" Text="" runat="server" ReadOnly="True" 
                    Width="800px" Rows="10" TextMode="MultiLine" Visible="True" 
                    ToolTip="Select text then use &quot;Ctrl + C&quot; to copy it to the clipboard"  ></asp:TextBox>

               <br />
        connection string:
        <asp:TextBox ID="txtConnectionString" runat="server" Width="797px"></asp:TextBox>
               <br />
               <br />
        <asp:Button ID="Button1" runat="server" Text="Create Table" 
            onclick="Button1_Click" />
            
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" >
        </asp:GridView>
        <asp:Image ID="Image1" runat="server" Height="71px" 
            ImageUrl="~/images/honeybadger_logo.png" Width="127px" />
        
                       <br />
            <asp:Label ID="lblConStr" runat="server"></asp:Label>

    </div>
    </form>
</body>
</html>
