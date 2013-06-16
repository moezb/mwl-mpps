<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AdminMain.aspx.cs" Inherits="AdminWebServer.Admin" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css/hstyle.css" />
    <!-- include core functions plus the table control -->
    <script type="text/javascript" src="js/hcore.js"></script>
    <script type="text/javascript" src="js/htree.js"></script>
    <title>Untitled Page</title>
</head>
<body style="background-image: none; color: royalblue; font-family: Tahoma; background-color: transparent">
    EdgeTeks Modality Worklist SCP &amp; Modality Perfomred Procedure Step SCP &nbsp;
    Admin V0.1<br />
    <br />
    <a href="ChangePassword.aspx">Change Password</a>
    <a href="MppsSettings.aspx">Mpps Settings</a>
    <a href="MwlSettings.aspx">Mwl Settings</a>
    <hr />
    <br />
    <br />
    <%--<table visible="false" cellpadding="0" cellspacing="0" border="1" style="width: 17%; height: 69px">
        <tr>
            <td id="treeParent">

                <script type="text/javascript">
function createTree()
	{
	var d = 
		[
		{text : "Root", expanded : true, children :
			[
			{text: "Modality Worklist SCP Settings"}
			,
			{text: "Modality Performed procedure Step SCP Settings"}
			,
			{text: "Change Admin Password"}
			,
			{text: "Logout"}
			]
		}
		];

	var o = 
		{
		//allowMultipleSelection : true,
		visibleWidth : HCore_DYNAMIC,
		visibleHeight : HCore_DYNAMIC,
		// HCore_DYNAMIC is a special value that indicates the tree
		// should expand to fill the space available. Note: 
		// the HTML tag that is the parent to the table must have a 
		// size in order for this to work
		scroll: HTree_SCROLLBARS_ALWAYS
		// last can also be 'never' to remove the scrollbar
		};
	
	var tree = new HTree( new HTreeModel( d ), o );
	tree.handleDoubleClick = function( node )
		{
		//alert( node.getText() + " double clicked" ); 
		}
	tree.handleSelectionChange = function( node )
		{
		//alert( node.getText() + " selection changed" ); 
		// pausing here prevents double click from working
		}
	return tree;	
	}
	var treeControl = createTree();
                </script>

                <script type="text/javascript">
    treeControl.render( "treeParent" );
                </script>

            </td>            
        </tr>       
        </table>    --%>
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server" Height="1px" Width="782px">
        Edgeteks Modality Worklist SCP &amp; Modality Perfomred Procedure Step SCP &nbsp;
        Admin V0.1</asp:Panel>
</body>
</html>
