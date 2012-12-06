<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GalleryEdit.ascx.cs" Inherits="GalleryEdit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript">
    function VIKKI_CheckUploadedFiles() {
        var ul = $find("<%= auFile.ClientID %>");
        var inputs = ul.getInvalidFiles();
        for (i = inputs.length - 1; i >= 0; i--) {
            if (!ul.isExtensionValid(inputs[i])) {
                alert('Тільки файли з розширенням jpg можуть бути завантажені на сервер. Будь ласка видаліть файли з іншими розширеннями.');
                return false;
            }
        }
        if (ul.get_element().innerHTML.indexOf('ruUploadFailure') != -1) {
            alert('Максимальний розмір файла 100Мб. Допустимий формат зображень jpg. Будь ласка видаліть файли помічені знаком оклику.');
            return false;
        }
        if (ul._uploadsInProgress > 0) {
            alert('Будь ласка дочекайтесь завантаження всіх файлів.');
            return false;
        }
        return true;
    }
</script>
<TABLE id="Table3" class="EditControl3" cellspacing="5" cellpadding="5"  align="center" border="0" width="600px">	
    <tr id="trUploadImages" runat="server">			
		<td align="right">Завантажити&nbsp;фотографії:
		</td>
		<td >
                <telerik:RadAsyncUpload runat="server" ID="auFile" 
                    AllowedFileExtensions="jpg" MultipleFileSelection="Automatic">
                    <Localization Remove="Видалити" Select="Вибрати" />
                </telerik:RadAsyncUpload>
        </td>
    </tr>
    <tr id="trCover" runat="server">
        <td align="right">
            Обкладинка:
        </td>
	    <td>
            <asp:CheckBox id="chk_IsCover" runat="server" Text=""></asp:CheckBox>
        </td>
    </tr>
    <tr id="trShowCommon" runat="server">
	    <td align="right">
            Відображати в розділі "Галерея":
        </td>
        <td>
            <asp:CheckBox id="chk_ShowCommon" runat="server" Text=""></asp:CheckBox>
        </td>
    </tr>
	<tr>
	    <td colspan='2' align="right">
	    <asp:Button ID="btnCancel" runat="server" CommandArgument="False" ForeColor="Black" BorderStyle="None" Font-Size="10pt"  Height="30px" BackColor="#ffc33f"
                            commandname="Cancel" CssClass="formbutton1" Text="Відмінити" />     
                   <asp:Button ID="btnUpdate" runat="server" CommandArgument="Update" ForeColor="Black" BorderStyle="None" Font-Size="10pt"  Height="30px" BackColor="#ffc33f"
                            commandname="Update" CssClass="formbutton1" Text="Зберегти"/>
        </td>
	</tr>
</table>