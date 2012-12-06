<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Відновлення паролю.</title>
    <link href="office_style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="100%">
    <tr>
        <td align="center" valign="middle">
        <table cellpadding="3" cellspacing="10"  border="0" class="logintable">
        <tr>
            <td class="pageName" colspan="3">Забули пароль? 
            </td>
        </tr>
        <tr id="trPasswordSend1" runat="server">
            <td colspan="2">Відновлення паролю. Пароль буде послано на ваш E-mail.</td>
        </tr>
        <tr id="trPasswordSend2" runat="server">
            <td colspan="2" align="center">
                <asp:Label id="lbNotFound" runat="server" ForeColor="Red" Visible="False" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr id="trPasswordSend3" runat="server">
            <td colspan="2">
                 <asp:Button ID="btnSendPassword" runat="server" OnClick="btnSendPassword_Click" Text="Надіслати" Width="100px" Height="30px" CssClass="formbutton1" ToolTip="Надіслати"/>
                </td>
            </tr>
        <tr id="trPasswordSent" runat="server" Visible="False">
            <td colspan="2" align="center">
                <asp:Label id="lblPasswordSent" runat="server" ForeColor="Green" Font-Bold="true" Text="Пароль посланий на ваш E-mail." Font-Size="16pt"></asp:Label><br />
                <asp:HyperLink ID="hlBackLogin" runat="server" NavigateUrl="Login.aspx" Text="Повернутись назад."></asp:HyperLink>
            </td>
        </tr>
        </table>
        </td>
    </tr>
</table>
    </form>
</body>
</html>
