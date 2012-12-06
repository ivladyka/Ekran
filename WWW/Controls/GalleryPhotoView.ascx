<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GalleryPhotoView.ascx.cs" Inherits="GalleryPhotoView" %>
<%@ Register TagPrefix="uc1" TagName="CategoryView" Src="CategoryView.ascx" %>
<%@ Register TagPrefix="uc1" TagName="GalleryView" Src="GalleryView.ascx" %>
<table cellpadding="0" cellspacing="0" border="0" width="100%">

    <tr>
        <td align="center" >
           <uc1:GalleryView id="galleryView" runat="server"></uc1:GalleryView>
        </td>
    </tr>
        <tr>
        <td>
         <div class="PageNameStyle" style="margin-top: 10px; margin-bottom: 10px">   <asp:Label ID="lblTitle" runat="server" CssClass="pageName"></asp:Label></div>
            <uc1:CategoryView id="categoryView" runat="server"></uc1:CategoryView>
            <div class="bottomdottedtop" style="margin-top: 60px"></div>
        </td>
    </tr>
</table>