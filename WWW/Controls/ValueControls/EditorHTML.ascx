<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditorHTML.ascx.cs" Inherits="Controls_ValueControls_EditorHTML" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:radeditor id="editorHTML" runat="server" showsubmitcancelbuttons="false" 
    toolsfile="~/Controls/ValueControls/BasicTools.xml"
    AllowThumbGeneration="True" 
    BackColor="White" BorderColor="#EAEAEA">
    <Content>
</Content>
    <ImageManager MaxUploadFileSize="5242880" UploadPaths="~/Images/Upload" 
        ViewMode="Grid" ViewPaths="~/Images/Upload" />
    <DocumentManager MaxUploadFileSize="5242880" />
    <FlashManager MaxUploadFileSize="5242880" />
    </telerik:radeditor>
    <br />
<asp:RequiredFieldValidator ID="rfvEditorHTML" runat="server" ErrorMessage="Обов'язкове поле." ControlToValidate="editorHTML" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
