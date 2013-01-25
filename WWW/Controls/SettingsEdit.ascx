<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SettingsEdit.ascx.cs" Inherits="SettingsEdit" %>
<table id="Table3" align="center" border="0" cellpadding="2" cellspacing="5" >
            <tr>
                <td align="right">
                    Ключові слова:
                </td>
                <td>
                    <asp:TextBox ID="text_Keywords" runat="server" CssClass="textBoxStyle" MaxLength="250" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Ключові&nbsp;слова&nbsp;(пол.):
                </td>
                <td>
                    <asp:TextBox ID="text_Keywords_pl" runat="server" CssClass="textBoxStyle" MaxLength="250" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Ключові слова (анг.):
                </td>
                <td>
                    <asp:TextBox ID="text_Keywords_en" runat="server" CssClass="textBoxStyle" MaxLength="250" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Опис:
                </td>
                <td>
                    <asp:TextBox ID="text_Description" runat="server" CssClass="textBoxStyle" MaxLength="200" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Опис (пол.):
                </td>
                <td>
                    <asp:TextBox ID="text_Description_pl" runat="server" CssClass="textBoxStyle" MaxLength="200" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Опис (анг.):
                </td>
                <td>
                    <asp:TextBox ID="text_Description_en" runat="server" CssClass="textBoxStyle" MaxLength="200" Width="600px"></asp:TextBox>
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