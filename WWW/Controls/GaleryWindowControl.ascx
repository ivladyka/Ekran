<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GaleryWindowControl.ascx.cs" Inherits="GaleryWindowControl" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript">
    function VIKKI_ShowImageViewWindow(categoryID, photoName, orderID) {
        var oWnd = $find("<%= radWindow.ClientID %>");
        var urlPrefix = "";
        if (window.location.href.indexOf("/Office/") != -1) {
            urlPrefix = "../";
        }
        oWnd.setUrl(urlPrefix + "ModalDialog.aspx?content=ImageView&CategoryID=" + categoryID + "&PhotoName=" + photoName
        + "&OrderID=" + orderID);
        oWnd.show();  
	}
</script>
<telerik:RadWindow ID="radWindow" runat="server" Width="456"
        Height="642px" Title="Екран" 
    NavigateUrl="../ModalDialog.aspx?content=ImageView" 
    VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="None" 
    Modal="True" VisibleTitlebar="False" ReloadOnShow="True" Skin="Outlook" 
    BorderColor="#a1a1a1" BorderStyle="Solid" BorderWidth="1px" >
</telerik:RadWindow>