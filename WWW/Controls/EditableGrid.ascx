<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditableGrid.ascx.cs" Inherits="Controls_EditableGrid" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script language="javascript">
	function OnKeyPressGridFilterToEnterModeAJAX(controlUnigueID, eventMessage)
	{
		if(window.event.keyCode == 13) 
		{ 
			window.event.returnValue = false;
			window.event.cancelBubble = true; 
			window["<%= radList.ClientID %>"].AjaxRequest(controlUnigueID, eventMessage)
		}
	}
</script>
</telerik:RadCodeBlock>
<telerik:RadAjaxPanel id="rapEditableGrid" runat="server" EnableAJAX="True" LoadingPanelID="ralpEditableGrid">
<telerik:radgrid id="_radList" runat="server" Gridlines="Both" 
	AllowCustomPaging="True" Width="100%" AllowPaging="True"
	CssClass="RadGrid" AllowMultiRowSelection="True" AllowMultiRowEdit="False"
	AllowSorting="True" PageSize="20"  
    Skin="Sitefinity" BorderColor="#a1a1a1" BorderWidth="0px" >
    <GroupPanel>
		<PanelStyle cssclass="GroupPanel"></PanelStyle>
		<PanelItemsStyle cssclass="GroupPanelItem"></PanelItemsStyle>
	</GroupPanel>
	<GroupHeaderItemStyle BackColor="#C0C0C0" BorderColor="white"></GroupHeaderItemStyle>
	<MasterTableView GroupLoadMode="Server" AllowCustomPaging="False" CssClass="MasterTable"
		Width="100%" NoMasterRecordsText="Записи відсутні." CommandItemDisplay="Top">
		<RowIndicatorColumn UniqueName="RowIndicator">
			<HeaderStyle Width="20px" CssClass="GridHeader"></HeaderStyle>
		</RowIndicatorColumn>
		<EditFormSettings EditFormType="WebUserControl">
			<FormTableItemStyle Width="100%"></FormTableItemStyle>
			<FormTableStyle CellSpacing="0" CellPadding="2"></FormTableStyle>
			<FormStyle Width="100%" CssClass="EditForm"></FormStyle>
			<EditColumn UniqueName="EditCommandColumn1"></EditColumn>
		</EditFormSettings>
		<Columns>
			<telerik:GridTemplateColumn AllowFiltering="false">
				<HeaderStyle Width="25px"></HeaderStyle>
				<ItemTemplate>
				<asp:HyperLink Runat="server" ImageUrl ="../Images/Edit.gif" ID="lnkEdit" Text="Редагувати" ToolTip="Редагувати"></asp:HyperLink>
				<asp:CheckBox Runat="server" ID="chkApply" Visible="False"></asp:CheckBox>
				</ItemTemplate>
			</telerik:GridTemplateColumn>
		</Columns>
		<CommandItemTemplate>
			<asp:Panel ID="pnlCommandItem" Runat="server">
				<table width="100%" border="0">
					<tr height="19">
						<% if ((this.GridMode | GridModes.Add) == this.GridMode)
         {%>
						<td width="100">
							<a href='<%=this.UrlToAdd%>' runat="server" id="hlAddRecord" title="Додати Новий Запис"><img border=0 alt="Додати Новий Запис" src="../Images/AddRecord.gif" />Додати Запис</a>
						</td>
						<%} if ((this.GridMode | GridModes.Delete) == this.GridMode)
        {%>
						<td width="250" onclick="return confirm('Ви дійсно бажаєте видалити виділені записи ?');">
							<asp:LinkButton ID="btnDeleteSelected" runat="server" CommandName="Delete" ToolTip="Видалити виділені записи">
							<img border="0" alt="Видалити виділені записи" src="../Images/Delete.gif" />Видалити виділені записи</asp:LinkButton>
						</td>
						<%}%>
						<td align="right" width="*">&nbsp;
							<%if ((this.GridMode | GridModes.Refresh) == this.GridMode)
         {%>
								<asp:LinkButton ID="btnRedindGrid" runat="server" CommandName="RebindGrid" ToolTip="Поновити"><img border=0 alt="Поновити" src="../Images/Refresh.gif" />Поновити</asp:LinkButton>
							<%}%>
						</td>
					</tr>
				</table>
			</asp:Panel>
		</CommandItemTemplate>
		<ExpandCollapseColumn ButtonType="ImageButton" UniqueName="ExpandColumn" Visible="False">
			<HeaderStyle Width="19px"></HeaderStyle>
		</ExpandCollapseColumn>
	</MasterTableView>
	<SortingSettings SortedAscToolTip="Відсортовано по зростанню" 
        SortedDescToolTip="Відсортовано по спаданню" SortToolTip="Сортувати" />
	<SelectedItemStyle CssClass="SelectedRow"></SelectedItemStyle>
	<ClientSettings AllowGroupExpandCollapse="True" AllowDragToGroup="False" ReorderColumnsOnClient="True"
		AllowColumnsReorder="False">
		<Selecting AllowRowSelect="True"></Selecting>
		<Resizing AllowRowResize="False" ClipCellContentOnResize="False" AllowColumnResize="False"></Resizing>
	</ClientSettings>
	<HeaderStyle CssClass="GridHeader"></HeaderStyle>
	<EditItemStyle Height="25px" CssClass="EditedGridRow"></EditItemStyle>
	<PagerStyle Height="18px" CssClass="GridPager" Mode="NumericPages" PagerTextFormat="Change page: {4} &nbsp;Сторінка <strong>{0}</strong> з <strong>{1}</strong>. Всього записів: <strong>{5}</strong>."></PagerStyle>
	<ItemStyle HorizontalAlign="Center" CssClass="GridRow"></ItemStyle>
	<AlternatingItemStyle HorizontalAlign="Center" CssClass="GridAltRow"></AlternatingItemStyle>
	<FooterStyle CssClass="GridPager" HorizontalAlign="Left"></FooterStyle>
</telerik:radgrid>
<asp:Button ID="btnRefreshEditableGrid" runat="server" 
        onclick="btnRefreshEditableGrid_Click" CssClass="VIKKI_HiddenButton"  ></asp:Button>
</telerik:RadAjaxPanel>
<telerik:RadAjaxLoadingPanel ID="ralpEditableGrid" runat="server" Skin="Sitefinity">
 </telerik:RadAjaxLoadingPanel>