<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AdminWebServer._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-image: none; color: royalblue; font-family: Tahoma; background-color: transparent">
    
        <br />Edgeteks Modality Worklist SCP & Modality Perfomred Procedure Step SCP &nbsp; Admin V0.1<br />
        <br />
        <hr />
        <br />
    <form id="form1" runat="server">
    <div>
        <table style="width: 248px; height: 1px; left: 50%; position: relative; top: 50%;" cellpadding="0" cellspacing="0">
            <tr>               
                <td style="height: 16px; width: 284px;">
                    Enter Admin password :</td>                
            </tr>
            <tr>                
                <td style="height: 22px; width: 284px;">
                    <asp:TextBox ID="_pwTextBox" runat="server" TextMode="Password" style="table-layout: auto" Width="95%" Wrap="False"></asp:TextBox></td>               
            </tr>            
            <tr>
                <td align="center" style="height: 14px; width: 284px;">
        <asp:Button ID="_loginButton" runat="server" Text="Login"
            Width="108px" style="left: 26%; top: -23%; background-color: cornflowerblue; text-align: center; table-layout: auto; cursor: hand;" Font-Bold="True" /></td>
            </tr>
            <tr>
                <td align="center" style="height: 22px; width: 284px;">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password is required to access admin page." ControlToValidate="_pwTextBox" ForeColor="OrangeRed"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="center" style="width: 284px; height: 22px">
                    <asp:Label ID="_badPasslabel" runat="server" Style="color: red" Width="139px"></asp:Label></td>
            </tr>
        </table>
        <br />         
    </div>
    </form>
   
</body>
</html>
