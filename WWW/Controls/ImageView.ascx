<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageView.ascx.cs" Inherits="ImageView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">  
    html, body, form  
    {  
      height: 100%;   
      margin: 0;  
      padding: 0; 
    } 

    </style>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script language="javascript">
    var VIKKI_ImageClientWidth = 0;
    function VIKKI_OnLoadModalDialog() {
        window.setTimeout(
            'VIKKI_SetRadWindowAutoSize()', 1300);
        }

    function VIKKI_SetRadWindowAutoSize() {
        var imageObj = $get('<%= imageHolder.ClientID %>');
        var oWnd = VIKKI_GetRadWindow();
        if (oWnd.isClosed()) {
            return;
        }
        if (VIKKI_ImageClientWidth > 0 && VIKKI_ImageClientWidth == imageObj.clientWidth) {
            if ($telerik.isIE) {
                var hoverNav = $get('hoverNav');
                hoverNav.style.height = imageObj.clientHeight;
                hoverNav.style.width = imageObj.clientWidth + 4;
            }
            oWnd.SetWidth(imageObj.clientWidth + 24);
            oWnd.SetHeight(imageObj.clientHeight + 57);
            oWnd.center();
        }
        else 
        {
            window.setTimeout('VIKKI_SetRadWindowAutoSize()', 1300);
        }
        VIKKI_ImageClientWidth = imageObj.clientWidth;
    }
        
    function VIKKI_ImageViewOnResponseEnd(sender, arguments) {
            VIKKI_OnLoadModalDialog();
    }
</script>
</telerik:RadCodeBlock> 
<telerik:RadAjaxPanel id="rapImageView" runat="server" EnableAJAX="True" 
    LoadingPanelID="ralpImageView" ClientEvents-OnResponseEnd="VIKKI_ImageViewOnResponseEnd">
<table cellpadding="2" cellspacing="2" border="0" width="100%" class="VIKKI_ImageBackground">
    <tr>
        <td colspan="2" align="center">
            <asp:Image ID="imageHolder" Style="display: block; text-align: center;" runat="server"/>
            <div id="hoverNav">
                <asp:LinkButton ID="hlPrevLink" runat="server" CssClass="VIKKI_PrevImageLink" 
                    onclick="hlPrevLink_Click"></asp:LinkButton>
                <asp:LinkButton ID="hlNextLink" runat="server" CssClass="VIKKI_NextImageLink" 
                    onclick="hlNextLink_Click"></asp:LinkButton>
            </div>
        </td>
    </tr>
    <tr>
        <td align="left">
            <b><asp:Label ID="ImageCount" runat="server"></asp:Label></b>
        </td>
        <td align="right">
            <asp:imagebutton id="btnCancel" runat="server" commandname="Cancel" causesvalidation="False" ImageUrl="~/Images/Cancel.gif" ToolTip="Закрити"></asp:imagebutton>
        </td>
    </tr>
</table>
<INPUT id="hdPhotoName" type="hidden" name="hdPhotoName" runat="server" value=""/>
</telerik:RadAjaxPanel>
<telerik:RadAjaxLoadingPanel ID="ralpImageView" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>