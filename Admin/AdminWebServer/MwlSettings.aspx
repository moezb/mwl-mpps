<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MwlSettings.aspx.cs" Inherits="AdminWebServer.MwlSettings" %>

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
        
        <table style="width: 798px; height: 86px">
            <tr>
                <td style="width: 116px; height: 6px">
                    <asp:Label ID="Label1" runat="server" Text="Mwl AE Title" Width="193px"></asp:Label></td>
                <td style="width: 171px; height: 6px">
                    <asp:TextBox ID="_MwlAETTextBox" runat="server" MaxLength="15"></asp:TextBox></td>
                <td style="width: 1431px; height: 6px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field" ControlToValidate="_MwlAETTextBox"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 116px; height: 12px;">
                    <asp:Label ID="Label2" runat="server" Text="Port"></asp:Label></td>
                <td style="width: 171px; height: 12px;">
                    <asp:TextBox ID="_MwlListeningPort" runat="server"></asp:TextBox></td>
                <td style="width: 1431px; height: 12px;">
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="_MwlListeningPort"
                        CultureInvariantValues="True" ErrorMessage="Invalid port  number." MaximumValue="49151"
                        MinimumValue="1024" Type="Integer"></asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_MwlListeningPort"
                        ErrorMessage="Required Field "></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 116px">
                    <asp:Label ID="Label6" runat="server" Text="Max Responses To Send to SCU per session"
                        Width="329px"></asp:Label></td>
                <td style="width: 171px">
                    <asp:TextBox ID="_maxResponsesTextBox" runat="server"></asp:TextBox></td>
                <td style="width: 1431px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_maxResponsesTextBox"
                        ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="_maxResponsesTextBox"
                        CultureInvariantValues="True" ErrorMessage="Enter a number between 1 and 100."
                        MaximumValue="100" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
            </tr>
            <tr>
                <td style="width: 116px">
                    <asp:Label ID="Label5" runat="server" Text="Response Buffer Capacity"></asp:Label></td>
                <td style="width: 171px">
                    <asp:TextBox ID="_bufferCapacityTextBox" runat="server"></asp:TextBox></td>
                <td style="width: 1431px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="_bufferCapacityTextBox"
                        ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="_bufferCapacityTextBox"
                        CultureInvariantValues="True" ErrorMessage="Enter a number between 1 and 100."
                        MaximumValue="100" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="Label3" runat="server" Text="*You need to restart the mwl service to apply new settings."
            Visible="False"></asp:Label><br />
        <hr style="width: 334px" />
        <asp:Button ID="Button1" runat="server" Text="Submit" />
    
    </div>
    </form>
</body>
</html>
