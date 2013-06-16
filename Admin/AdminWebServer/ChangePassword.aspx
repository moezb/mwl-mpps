<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="AdminWebServer.ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/javascript">
    function checkNewPassLength(sender, args)
	{
	  var textBox = document.getElementById("_newPassBisTextBox");
	  var pass=textBox.value;
	  pass = pass.replace(/^\s+|\s+$/g,""); 
	  if (pass.length<5){
	    args.IsValid = false;
	    return;
	  }
	  else {
	    args.IsValid = true;
	    return;
	  }
	}
    </script>
    <title>Untitled Page</title>
    
</head>
<body  style="background-image: none; color: royalblue; font-family: Tahoma; background-color: transparent">
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
       
        <table style="width: 811px; height: 260px;">
            <tr>
                <td style="width: 202px; height: 49px;">
        <asp:Label ID="Label1" runat="server" Text="Enter the old password" Width="171px"></asp:Label></td>
                <td style="height: 49px; width: 142px;">
                    <asp:TextBox
            ID="_oldpassTextBox" runat="server" MaxLength="10"></asp:TextBox></td>
                <td style="width: 392px; height: 49px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="_oldpassTextBox"
                        ErrorMessage="Required Field"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 202px; height: 44px;">
        <asp:Label ID="Label2" runat="server" Text="Enter your new password" Width="194px"></asp:Label></td>
                <td style="height: 44px; width: 142px;">
                    <asp:TextBox
            ID="_newPassTextBox" runat="server" MaxLength="10"></asp:TextBox></td>
                <td style="width: 392px; height: 44px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="_newPassTextBox"
                        ErrorMessage="Required Field "></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 202px;">
        <asp:Label ID="Label3" runat="server" Text="Retype your new password" Width="192px"></asp:Label></td>
                <td style="width: 142px">
                    <asp:TextBox
            ID="_newPassBisTextBox" runat="server" MaxLength="10"></asp:TextBox></td>
                <td style="width: 392px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="_newPassBisTextBox"
                        ErrorMessage="Required Field "></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="_newPassTextBox"
                        ControlToValidate="_newPassBisTextBox" ErrorMessage="the retyped password is not similar." Width="317px"></asp:CompareValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="checkNewPassLength"
                        ControlToValidate="_newPassBisTextBox" ErrorMessage="Password must be at least 5 characters long." Width="321px"></asp:CustomValidator></td>
            </tr>
        </table>
        <br />
        &nbsp;
        <asp:Label ID="_badPass" runat="server" ForeColor="Red" Text="The password you entered is not valid."
            Width="283px"></asp:Label><br />
        <hr style="width: 334px" />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Submit" /></div>
    </form>
</body>
</html>
