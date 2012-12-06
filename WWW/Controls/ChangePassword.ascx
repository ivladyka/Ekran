<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.ascx.cs" Inherits="ChangePassword" %>
<TABLE id="Table3" class="logintable" style="margin: 10px 50px 50px 100px; vertical-align: middle; text-align: left;" border="0" width="450" align="center" cellpadding="3" cellspacing="10"    align="center" border="0" >	
        <tr>
		<td align="right">Старий пароль:
		</td>
		<td>
			<asp:textbox id="tbOldPassword" runat="server" CssClass="textBoxStyle" MaxLength="50" TextMode="Password"></asp:textbox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="обов'язкове поле." ControlToValidate="tbOldPassword" Display="Dynamic"></asp:RequiredFieldValidator>
		</td>
		</tr>
	    <tr>
		<td align="right">Новий пароль:
		</td>
		<td>
			<asp:textbox id="text_Password" runat="server" CssClass="textBoxStyle" MaxLength="50" TextMode="Password"></asp:textbox>
			<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="обов'язкове поле." ControlToValidate="text_Password" Display="Dynamic"></asp:RequiredFieldValidator>
		</td>
		</tr>
		<tr>
		<td align="right">Підтвердження:
		</td>
		<td>
			<asp:textbox id="tbPassword1" runat="server" CssClass="textBoxStyle" MaxLength="50" TextMode="Password"></asp:textbox>
			<asp:CompareValidator ID="cvComparePassword" runat="server" 
                ErrorMessage="<br>Паролі не рівні. Спробуйте ввести паролі ще раз." ControlToCompare="text_Password" 
                ControlToValidate="tbPassword1" Display="Dynamic" 
                Operator="Equal"></asp:CompareValidator>

		</td>
		</tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Label id="lbError" runat="server" ForeColor="Red" Visible="False" Text="Старий пароль введений невірно."></asp:Label>
        </td>
    </tr>
  	<tr>
	    <td colspan='2' align="right">
             <asp:Button ID="btnCancel" runat="server" CommandArgument="False" ForeColor="Black" BorderStyle="None" Font-Size="10pt"  Height="30px" BackColor="#ffc33f"
                            commandname="Cancel" CssClass="formbutton1" Text="Відмінити" CausesValidation="false" OnClick="btnCancel_Click"/>     
                   <asp:Button ID="btnUpdate" runat="server" CommandArgument="Update" ForeColor="Black" BorderStyle="None" Font-Size="10pt"  Height="30px" BackColor="#ffc33f"
                            commandname="Update" CssClass="formbutton1" Text="Зберегти" OnClick="btnUpdate_Click"/>
			 </td>
	</tr>
</table>