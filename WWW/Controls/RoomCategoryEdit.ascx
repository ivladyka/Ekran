<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RoomCategoryEdit.ascx.cs" Inherits="RoomCategoryEdit" %>
<TABLE id="Table3" class="EditControl3" cellpadding="2" cellspacing="7"  align="center" border="0" width="550px">	
            <tr>
                <td align="right">
                    Назва:
                </td>
                <td>
                    <asp:TextBox ID="text_Name" runat="server" CssClass="textBoxStyle" 
                        MaxLength="50" Width="300px"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" 
                        ControlToValidate="text_Name" Display="Dynamic" 
                        ErrorMessage="Обов'язкове поле."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Назва (пол.): 
                </td>
                <td>
                    <asp:TextBox ID="text_Name_pl" runat="server" CssClass="textBoxStyle" 
                        MaxLength="50" Width="300px"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="rfvName_pl" runat="server" 
                        ControlToValidate="text_Name_pl" Display="Dynamic" 
                        ErrorMessage="Обов'язкове поле."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Назва (анг.):
                </td>
                <td>
                    <asp:TextBox ID="text_Name_en" runat="server" CssClass="textBoxStyle" 
                        MaxLength="50" Width="300px"></asp:TextBox>
                    &nbsp;
                    <asp:RequiredFieldValidator ID="rfvName_en" runat="server" 
                        ControlToValidate="text_Name_en" Display="Dynamic" 
                        ErrorMessage="Обов'язкове поле."></asp:RequiredFieldValidator>
                </td>
            </tr>
  	<tr>
	    <td colspan="2" align="right">
	     <asp:Button ID="btnCancel" runat="server" CommandArgument="False" ForeColor="Black" BorderStyle="None" Font-Size="10pt"  Height="30px" BackColor="#ffc33f"
                            commandname="Cancel" CssClass="formbutton1" Text="Відмінити" />     
                   <asp:Button ID="btnUpdate" runat="server" CommandArgument="Update" ForeColor="Black" BorderStyle="None" Font-Size="10pt"  Height="30px" BackColor="#ffc33f"
                            commandname="Update" CssClass="formbutton1" Text="Зберегти"/>
			 </td>
	</tr>
</TABLE>