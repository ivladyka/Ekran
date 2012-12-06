<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Вхід в систему</title>
    <link href="office_style.css" type="text/css" rel="stylesheet"/>
    <script src="js/Scripts.js" type="text/javascript"></script>
    <script language="javascript">
        function VIKKI_RedirectFromLoginPage(url, VS_FX_LVIDValue) {
		VIKKI_SetCookie('VS_FX_LVID', VS_FX_LVIDValue);
		window.location.replace(url);
	}
    </script>

</head>
<body>
<form id="form1" runat="server"> 
<table width="100%" height="100%">
    <tr>
        <td align="center" valign="middle">
            <table cellpadding="3" cellspacing="10"  border="0" class="logintable" id="Table3">
    <tr>
        <td class="pageName" colspan="3">Вхід в систему
        </td>
    </tr>
    <tr>
        <td width="180px;" align="right"> 
            Ім'я корситувача:
        </td>
        <td >
            <asp:textbox id="tbUserName" runat="server" Width="200px" MaxLength="50" 
                TextMode="SingleLine"  CssClass="textBoxStyle"></asp:textbox>
			
        </td>
        <td width="200">
        <asp:RequiredFieldValidator id="rfvUserName" runat="server" ErrorMessage="обов'язкове поле." ControlToValidate="tbUserName"
			    Display="Dynamic"></asp:RequiredFieldValidator>
		</td>
    </tr>
    <tr>
        <td align="right">
            Пароль: </td>
         <td>
            <asp:textbox id="tbPassword" runat="server" Width="200px" MaxLength="50" 
                 TextMode="Password"  CssClass="textBoxStyle"></asp:textbox>
		</td>
		<td>	<asp:RequiredFieldValidator id="rfvPassword" runat="server" ErrorMessage="обов'язкове поле." ControlToValidate="tbPassword"
			    Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
           
        </td>
        <td> <asp:CheckBox id="chkKeepInSystem" runat="server" Text="" BackColor="#FBFBFB" 
                BorderColor="#EAEAEA"></asp:CheckBox>
            Залишатися в системі
        </td>
        <td></td>
    </tr>
    <tr>
        <td colspan="3" align="center">
            <asp:Label id="lbNotFound" runat="server" ForeColor="Red" Visible="False" Text="Логін або пароль введені неправильно"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="right" style="padding-right: 120px">
             <asp:Button id="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Вхід" Width="100px" Height="30px" CssClass="formbutton1" />
       </td>
    </tr>
     <tr>
        <td colspan="3">
            <asp:HyperLink id="hlForgotPassword" runat="server" NavigateUrl="ForgotPassword.aspx" CssClass="colorlink" Text="Забули пароль?"></asp:HyperLink>
        </td>
    </tr>  
</table>
        </td>
    </tr>
</table>
</form>
</body>

