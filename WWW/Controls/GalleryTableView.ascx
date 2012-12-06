<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GalleryTableView.ascx.cs" Inherits="GalleryTableView" %>
<%@ Register TagPrefix="uc1" TagName="CategoryView" Src="CategoryView.ascx" %>
<%@ Register TagPrefix="uc1" TagName="GalleryView" Src="GalleryView.ascx" %>
<table cellpadding="0" cellspacing="0" border="0" width="100%">

    <tr>
        <td align="center" >
           <uc1:GalleryView id="galleryView" runat="server"></uc1:GalleryView>
        </td>
    </tr>


</table>
