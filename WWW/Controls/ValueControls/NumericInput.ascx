<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NumericInput.ascx.cs" Inherits="Controls_ValueControls_NumericInput" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadNumericTextBox Skin="Telerik"  BorderColor="#b1a9a9"   BackColor="#fbfbfb" Width="300px"
    ID="numInput" runat="server" Culture="Ukrainian (Ukraine)">
<NumberFormat AllowRounding="True"></NumberFormat>
</telerik:RadNumericTextBox>