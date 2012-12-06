<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryEdit.ascx.cs" Inherits="CategoryEdit" %>
<%@ Register TagPrefix="uc1" TagName="NumericInput" Src="ValueControls/NumericInput.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EditorHTML" Src="ValueControls/EditorHTML.ascx" %>
<%@ Register TagPrefix="uc1" TagName="GalleryList" Src="GalleryList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RoomCategoryChoice" Src="ChoiceControls/RoomCategoryChoice.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="officecategoriedit"><br />
<telerik:RadTabStrip ID="rtsCategory" runat="server" Skin="Default" AutoPostBack="True" Align="Justify" Width="80%" OnTabClick="rtsCategory1_TabClick" Font-Size="16pt" SelectedIndex="1" CssClass="tabsstyle">
    <Tabs>
        <telerik:RadTab Text="Інформація" Selected="True">
        </telerik:RadTab>
        <telerik:RadTab Text="Фотографії">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<asp:Panel ID="pnlCategoryEdit" runat="server" >
  <table id="Table3" align="center" border="0" cellpadding="2" cellspacing="5" >
            <tr>
                <td align="right">
                    Назва:
                </td>
                <td>
                    <asp:TextBox ID="text_Name" runat="server" CssClass="textBoxStyle" 
                        MaxLength="255" Width="300px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" 
                        ControlToValidate="text_Name" Display="Dynamic" 
                        ErrorMessage="Обов'язкове поле."></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    Назва (пол.):
                </td>
                <td>
                    <asp:TextBox ID="text_Name_pl" runat="server" CssClass="textBoxStyle" 
                        MaxLength="255" Width="300px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="rfvName_pl" runat="server" 
                        ControlToValidate="text_Name_pl" Display="Dynamic" 
                        ErrorMessage="Обов'язкове поле." ></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    Назва (анг.):
                </td>
                <td>
                    <asp:TextBox ID="text_Name_en" runat="server" CssClass="textBoxStyle" 
                        MaxLength="255" Width="300px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="rfvName_en" runat="server" 
                        ControlToValidate="text_Name_en" Display="Dynamic" 
                        ErrorMessage="Обов'язкове поле."></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
		        <td align="right">Категорія номера:
		        </td>
		        <td>
                    <uc1:RoomCategoryChoice id="choice_RoomCategoryID" runat="server" UseValueInsteadText="true" Width="300"></uc1:RoomCategoryChoice>
		        </td>
		    </tr>
            <tr>
                <td align="right" valign="top">
                    Опис:
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <uc1:EditorHTML ID="editor_CategoryContent" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    Опис (пол.):
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <uc1:EditorHTML ID="editor_CategoryContent_pl" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    Опис (анг.):
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <uc1:EditorHTML ID="editor_CategoryContent_en" runat="server" />
                </td>
            </tr>
            <tr>
               <td align="right" colspan="2">
                        &nbsp;                    
                           <asp:Button ID="btnCancel" runat="server" CommandArgument="False" ForeColor="Black" BorderStyle="None" Font-Size="10pt"  Height="30px" BackColor="#ffc33f"
                            commandname="Cancel" CssClass="formbutton1" Text="Відмінити" />     
                   <asp:Button ID="btnUpdate" runat="server" CommandArgument="Update" ForeColor="Black" BorderStyle="None" Font-Size="10pt"  Height="30px" BackColor="#ffc33f"
                            commandname="Update" CssClass="formbutton1" Text="Зберегти"/>
                </td>
             </tr>
            
    </table>
</asp:Panel>
<asp:Panel ID="pnlGalleryList" runat="server" Visible="false">
    <uc1:GalleryList id="GalleryList" runat="server"></uc1:GalleryList>
</asp:Panel></div>