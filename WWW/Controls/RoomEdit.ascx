<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RoomEdit.ascx.cs" Inherits="RoomEdit" %>
<%@ Register TagPrefix="uc1" TagName="RoomCategoryChoice" Src="ChoiceControls/RoomCategoryChoice.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NumericInput" Src="ValueControls/NumericInput.ascx" %>
<TABLE id="Table3" class="EditControl3" cellpadding="2" cellspacing="7"  align="center" border="0" width="550px">	
    <tr>
        <td align="right">
            Назва:
        </td>
        <td>
            <asp:TextBox ID="text_Number" runat="server" CssClass="textBoxStyle" MaxLength="50" Width="300px"></asp:TextBox>
            &nbsp;
            <asp:RequiredFieldValidator ID="rfvNumber" runat="server" ControlToValidate="text_Number" Display="Dynamic" 
                ErrorMessage="Обов'язкове поле."></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
	    <td align="right">Категорія номера:
		</td>
		<td>
            <uc1:RoomCategoryChoice id="choice_RoomCategoryID" runat="server" UseValueInsteadText="true" Width="300" AddEmptyItem="false"></uc1:RoomCategoryChoice>
		</td>
    </tr>
    <tr>
        <td align="right">
            Ціна, грн:
        </td>
        <td>
            <uc1:NumericInput ID="num_Price" runat="server" />
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