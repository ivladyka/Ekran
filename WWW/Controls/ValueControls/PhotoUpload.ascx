<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhotoUpload.ascx.cs" Inherits="Controls_ValueControls_PhotoUpload" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript">

    
</script>
<table cellpadding="10" cellspacing="10">
    <tr>
        <td>
            <asp:Image ID="imgPhoto" runat="server" CssClass="VIKKI_HandCursor"/>
        </td>
        <td>
            <asp:ImageButton ID="imgDelete" runat="server" CssClass="b_style" ToolTip="Видалити зображення" ImageUrl="~/Images/Delete.gif" Visible="false"/>
        </td>
    </tr>
</table>
<telerik:RadAsyncUpload runat="server" ID="auFile" AllowedFileExtensions="jpg" 
    MaxFileInputsCount="1" onfileuploaded="auFile_FileUploaded">
    <Localization Remove="Видалити" Select="Вибрати" />
</telerik:RadAsyncUpload>
<asp:Label ID="lblAllowedExtensions" runat="server" >Виберіть зображення для завантаження (jpg)</asp:Label>
<INPUT id="hdPhotoNameDeleted" type="hidden" name="hdPhotoNameDeleted" runat="server" value=""/>
<telerik:RadWindow ID="radWindow" runat="server" Width="170px"
        Height="460px" Title="Фотохата" 
    NavigateUrl="../ModalDialog.aspx?content=ImageView" 
    VisibleStatusbar="false" ShowContentDuringLoad="false" Behaviors="None" 
    Modal="True" VisibleTitlebar="False" ReloadOnShow="True" Skin="Vista">
</telerik:RadWindow>

