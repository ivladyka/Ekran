<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GalleryView.ascx.cs" Inherits="GalleryView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div id="gallery" runat="server">
</div>
<script language="javascript">
    Galleria.loadTheme('galleria/themes/classic/galleria.classic.min.js');
    $("#<%=gallery.ClientID%>").galleria({
        width: 900,
        height: 620,
        autoplay: 5000,
        showImagenav: false,
        imagePosition: '0 0',
        transitionSpeed: 1000,
    });
</script>