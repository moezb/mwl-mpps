<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MppsSettings.aspx.cs" Inherits="AdminWebServer.MppsSettings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="background-image: none; color: royalblue; font-family: Tahoma; background-color: transparent">
   Edgeteks Modality Worklist SCP &amp; Modality Perfomred Procedure Step SCP &nbsp;
        Admin V0.1<br />
        <br />
        <a href="ChangePassword.aspx">Change Password</a> <a href="MppsSettings.aspx">Mpps Settings</a>
        <a href="MwlSettings.aspx">Mwl Settings</a>
        <br />
        <hr />
        &nbsp;<label ForeColor="Green" Text="MPPS SCP Settings"
            Width="166px"></label><br />
        <br />
    <form id="form1" runat="server">
    <div>
        
        <table style="width: 625px; height: 86px">
            <tr>
                <td style="width: 116px; height: 6px;">
                    <asp:Label ID="Label1" runat="server" Text="Mpps AE Title" Width="193px"></asp:Label></td>
                <td style="width: 171px; height: 6px">
                    <asp:TextBox ID="_MppsAETTextBox" runat="server" MaxLength="15"></asp:TextBox></td>
                <td style="width: 328px; height: 6px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field" ControlToValidate="_MppsAETTextBox"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 116px">
                    <asp:Label ID="Label2" runat="server" Text="Port"></asp:Label></td>
                <td style="width: 171px">
                    <asp:TextBox ID="_MppsListeningPort" runat="server"></asp:TextBox></td>
                <td style="width: 328px">
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="_MppsListeningPort"
                        CultureInvariantValues="True" ErrorMessage="Invalid port  number." MaximumValue="49151"
                        MinimumValue="1024" Type="Integer"></asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_MppsListeningPort"
                        ErrorMessage="Required Field "></asp:RequiredFieldValidator></td>
            </tr>
        </table>
        <br />
    
    </div>
        <asp:Label ID="Label3" runat="server" Text="*You need to restart the MPPS service to apply new settings."
            Visible="False"></asp:Label><br />
        <hr style="width: 334px" />
        <asp:Button ID="Button1" runat="server" Text="Submit" />
    </form>
</body>
</html>
