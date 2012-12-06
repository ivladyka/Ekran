using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.Web.UI;


[Flags]
public enum GridModes
{
    None = 0x0,
    Refresh = 0x01,
    Add = 0x02,
    Edit = 0x04,
    Delete = 0x08,
    Print = 0x10,
    Send = 0x20,
    MoveUp = 0x40,
    MoveDown = 0x80,
    MoveUpDown = 0xC0,
    ClearFilter = 0x100,
    View = 0x200,
    PredeterminedPrint = 0x1000,
    CustomButton = 0x8000,
    CustomLink = 0x10000,
    PredeterminedPrintNew = 0x20000,
    FullControl = 0x1F
}


public delegate void PageIndexChangedEventHandler();
public delegate void ItemDataBoundEventHandler(object sender, Telerik.Web.UI.GridItemEventArgs e);
public delegate DataTable NeedDataSourceEventHandler();
public delegate void SortEventHandler(object sender, Telerik.Web.UI.GridSortCommandEventArgs e);
public delegate void DeleteEventHandler(object sender, EditableGridDeleteEventArgs e);
public delegate void PrintEventHandler(object sender, EditableGridPrintEventArgs e);
public delegate void MoveEventHandler(object sender, EditableGridMoveEventArgs e);
public delegate void ConfirmEventHandler(object sender, EditableGridMoveEventArgs e);
public delegate void RowDragDropEventHandler(object sender, GridDragDropEventArgs e);

public partial class EditableGrid : ControlBase, IPostBackEventHandler
{
    private RadGrid m_radList;
    private Button m_btnRefreshEditableGrid;

    protected RadGrid radList
    {
        get
        {
            if (m_radList != null) return m_radList;
            Control c = this.FindControl("_radList");
            if(c != null)
            {
                if (c is RadGrid)
                {
                    m_radList = (RadGrid)c;
                    return m_radList;
                }
            }
            return null;
        }
    }

    protected Button btnRefreshEditableGrid
    {
        get
        {
            if (m_btnRefreshEditableGrid != null) return m_btnRefreshEditableGrid;
            Control c = this.FindControl("btnRefreshEditableGrid");
            if (c != null)
            {
                if (c is Button)
                {
                    m_btnRefreshEditableGrid = (Button)c;
                    return m_btnRefreshEditableGrid;
                }
            }
            return null;
        }
    }

    public event PageIndexChangedEventHandler PageIndexChanged;
    public event ItemDataBoundEventHandler ItemDataBound;
    public event NeedDataSourceEventHandler NeedDataSource;
    public event SortEventHandler Sort;
    public event DeleteEventHandler Delete;
    public event PrintEventHandler Print;
    public event MoveEventHandler MoveUp;
    public event ConfirmEventHandler Confirm;
    public event MoveEventHandler MoveDown;
    public event PrintEventHandler PredeterminedPrint;
    public event PrintEventHandler PredeterminedPrintNew;
    public event PrintEventHandler ViewAssociatedClaims;
    public event PrintEventHandler CustomButton;
    public event RowDragDropEventHandler RowDragDrop;

    protected Hashtable m_columnSettings = new Hashtable();
    protected string m_title;
    protected GridModes m_gridModes = GridModes.Refresh;
    protected string m_urlToAdd = "";
    protected string m_printUrl = "";
    protected string m_viewUrl = "";
    protected string m_LookupDxScript = "";
    protected string[] m_selectedIndexies;
    private bool m_FilterOnEnterMode = false;
    private bool m_FilterOnEnterModeAjax = false;
    private bool m_SelectFilterTextBox = false;
    private bool m_PrintWithoutSelecting = false;
    private string m_FilterOnEnterModeFocisUniqueName = "";
    private bool m_RebindBeforeCustomButtonEvent = true;
    private bool m_RebindAfterCustomButtonEvent = false;
    private bool m_ShowCountRecord = false;
    private string m_CustomToolTip = "";
    private bool m_SelectedIndexiesStorageInSession = false;

    public EditableGrid()
    {
    }

    public string UrlToAdd
    {
        get { return this.m_urlToAdd; }
        set { this.m_urlToAdd = value; }
    }

    public object[] SelectedRecords
    {
        get
        {
            object[] ret = new object[this.radList.SelectedItems.Count];
            for (int i = 0; i < this.radList.SelectedItems.Count; i++)
            {
                ret[i] = this.radList.SelectedItems[i];
            }
            return ret;
        }
    }


    protected GridDataItem[] SelectedItems
    {
        get
        {
            GridDataItem[] ret;
            /*if (this.radList.SelectedItems != null)
            {
                ret = new GridDataItem[this.radList.SelectedItems.Count];
                this.radList.SelectedItems.CopyTo(ret, 0);
                return ret;
            }*/

            ret = new GridDataItem[this.m_selectedIndexies.Length];
            for (int i = 0; i < this.m_selectedIndexies.Length; i++)
            {
                try
                {
                    ret[i] = this.radList.Items[this.m_selectedIndexies[i]];
                }
                catch
                {
                    GridDataItem[] ret1 = new GridDataItem[i];
                    for (int j = 0; j < i; j++)
                    {
                        ret1[j] = ret[j];
                    }
                    return ret1;
                }
            }
            return ret;
        }
    }

    public string[] SelectedIndexies
    {
        get { return m_selectedIndexies; }
    }
    public GridDataItemCollection Items
    {
        get { return this.radList.Items; }
    }


    public bool AllowDragToGroup
    {
        set { radList.ClientSettings.AllowDragToGroup = value; }
    }
    public bool AllowMultiRowSelection
    {
        set { radList.AllowMultiRowSelection = value; }
    }

    public bool GroupingEnabled
    {
        get { return this.radList.GroupingEnabled; }
        set { this.radList.GroupingEnabled = value; }
    }

    public bool ShowGroupPanel
    {
        get { return this.radList.ShowGroupPanel; }
        set { this.radList.ShowGroupPanel = value; }
    }

    public bool AllowColumnsReorder
    {
        set { radList.ClientSettings.AllowColumnsReorder = value; }
    }

    public string Title
    {
        get { return "<strong>" + this.m_title + "</strong>"; }
        set { this.m_title = value; }
    }

    public bool AllowSorting
    {
        get { return this.radList.MasterTableView.AllowSorting; }
        set { this.radList.MasterTableView.AllowSorting = value; }
    }

    public string Filter
    {
        get { return this.radList.MasterTableView.FilterExpression; }
        set { this.radList.MasterTableView.FilterExpression = value; }
    }

    public bool EnableColumnFilter
    {
        get
        {
            return this.radList.AllowFilteringByColumn;
        }
        set
        {
            this.radList.AllowFilteringByColumn = value;
            this.radList.MasterTableView.AllowFilteringByColumn = value;
        }
    }

    public GridModes GridMode
    {
        get
        {
            return this.m_gridModes;
        }
        set
        {
            this.m_gridModes = value;
            this.SetMode(value);
        }
    }

    public Hashtable ColumnsSettings
    {
        get
        {
            return this.m_columnSettings;

        }
        set
        {
            this.m_columnSettings = value;
        }
    }

    public void ResetSortExpression()
    {
        this.radList.MasterTableView.SortExpressions.Clear();
    }

    public void AddGroupExpression(string groupExpression)
    {
        this.radList.MasterTableView.GroupByExpressions.Add(new GridGroupByExpression(groupExpression));
    }

    public void CollapseAll()
    {
        foreach (GridItem item in this.radList.MasterTableView.Controls[0].Controls)
        {
            if (item is GridGroupHeaderItem)
            {
                item.Expanded = false;
            }
        }
    }

    public void ResetGroupExpression()
    {
        this.radList.MasterTableView.GroupByExpressions.Clear();
    }

    void SetMode(GridModes mode)
    {
        if ((mode | GridModes.Edit) != mode)
        {
            radList.Columns[0].Visible = false;
        }
        else
        {
            radList.Columns[0].Visible = true;
        }
    }

    public int CurrentPageIndex
    {
        get
        {
            return radList.CurrentPageIndex;
        }
        set
        {
            radList.CurrentPageIndex = value;
        }
    }

    public GridColumn this[int index]
    {
        get
        {
            return radList.Columns[index];
        }
    }

    public string EditableControlName
    {
        get { return this.radList.MasterTableView.EditFormSettings.UserControlName; }
        set { this.radList.MasterTableView.EditFormSettings.UserControlName = value; }
    }

    public object DataSource
    {
        get { return this.radList.DataSource; }
        set { this.radList.DataSource = value; }
    }

    public void AddSortExpression(string sortExpression)
    {
        this.radList.MasterTableView.SortExpressions.AddSortExpression(sortExpression);
    }

    public void ClearSortExpression()
    {
        this.radList.MasterTableView.SortExpressions.Clear();
    }

    protected override void SetEventHandlers()
    {
        base.SetEventHandlers();
        this.radList.NeedDataSource += new Telerik.Web.UI.GridNeedDataSourceEventHandler(this.radList_NeedDataSource);
        this.radList.ItemCreated += new Telerik.Web.UI.GridItemEventHandler(this.radList_ItemCreated);
        this.radList.PageIndexChanged += new Telerik.Web.UI.GridPageChangedEventHandler(this.radList_PageIndexChanged);
        this.radList.SortCommand += new Telerik.Web.UI.GridSortCommandEventHandler(this.radList_SortCommand);
        this.radList.ItemDataBound += new Telerik.Web.UI.GridItemEventHandler(this.radList_ItemDataBound);
        this.radList.ColumnCreated += new Telerik.Web.UI.GridColumnCreatedEventHandler(this.radList_ColumnCreated);
        this.radList.ItemCommand += new Telerik.Web.UI.GridCommandEventHandler(this.radList_ItemCommand);
        this.radList.RowDrop += new Telerik.Web.UI.GridDragDropEventHandler(this.radList_RowDrop);
    }

    protected void ToggleRowSelection(object sender, EventArgs e)
    {
        ((sender as CheckBox).Parent.Parent as GridItem).Selected = (sender as CheckBox).Checked;
    }

    public virtual void BindDataGrid(bool refresh)
    {
        if (refresh)
        {
            this.OnNeedDataSource();
        }

        //backup selected items before data binding
        if (SelectedIndexiesStorageInSession && radList.SelectedIndexes.Count == 0
            && UserSession.GetObjectKey("EDITABLE_GRID_SELECTED_INDEXIES") != null)
        {
            m_selectedIndexies = (string[])UserSession.GetObjectKey("EDITABLE_GRID_SELECTED_INDEXIES");
        }
        else
        {
            this.m_selectedIndexies = new string[this.radList.SelectedIndexes.Count];
        }
        this.radList.SelectedIndexes.CopyTo(this.m_selectedIndexies, 0);

        this.radList.DataBind();

    }

    protected virtual void OnNeedDataSource()
    {
        if (this.NeedDataSource != null)
        {
            this.radList.DataSource = this.NeedDataSource();
        }
    }


    public virtual void BindDataGrid()
    {
        this.BindDataGrid(false);
    }

    private void radList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is Telerik.Web.UI.GridCommandItem)
        {
            LinkButton li = (LinkButton)e.Item.FindControl("btnCustomButton");
            if (li != null)
            {
                li.Text = "<img border='0' src='" + CustomImg + "' alt='" + CustomButtonText + "'><nobr>&nbsp;" + CustomButtonText + "</nobr>";
                li.ToolTip = CustomToolTip;
            }
        }
        if (ItemDataBound != null)
        {
            ItemDataBound(sender, e);
        }
        if (e.Item is GridFooterItem)
        {
            if (ShowCountRecord)
            {
                GridFooterItem gridFooterItem = (GridFooterItem)e.Item;
                if (gridFooterItem.Cells.Count > 1)
                {
                    gridFooterItem.Cells[1].ColumnSpan = gridFooterItem.Cells.Count;
                    gridFooterItem.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Count:</b>&nbsp;&nbsp;" + e.Item.OwnerTableView.Items.Count;
                    for (int i = 2; i < gridFooterItem.Cells.Count; i++)
                    {
                        gridFooterItem.Cells[i].Visible = false;
                    }
                }
            }
        }
    }

    private void radList_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        if (PageIndexChanged != null)
        {
            this.radList.CurrentPageIndex = e.NewPageIndex;
            PageIndexChanged();
        }
    }

    private void radList_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.UpdateCommandName)
        {
            if (!Page.IsValid)
            {
                e.Canceled = true;
                return;
            }

            this.BindDataGrid(true);
            return;
        }

        if (e.CommandName == RadGrid.DeleteCommandName)
        {
            if (radList.SelectedIndexes.Count == 0)
            {
                return;
            }

            this.BindDataGrid(true);

            try
            {
                foreach (GridEditableItem item in this.SelectedItems)
                {
                    this.OnDeleteItem(item);
                }
                this.BindDataGrid(true);
                if (m_NotDeletedErrorMessage != null) RegisterScript("alert('" + m_NotDeletedErrorMessage + "')");
                m_NotDeletedErrorMessage = null;
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
                SqlException sqlEx = ex as SqlException;
                if (sqlEx != null && sqlEx.Number == 547)
                {
                    mes = "Cannot delete record, because it is used.";
                }
                RegisterScript("alert('" + mes.Replace("'", "\\'") + "');");
            }
            this.BindDataGrid(true);
            return;
        }

        if (e.CommandName == "Print")
        {
            this.BindDataGrid(true);

            if (this.SelectedItems.Length == 0 && !m_PrintWithoutSelecting)
            {
                return;
            }

            this.OnPrint(this.SelectedItems);
            return;
        }

        if (e.CommandName == "CustomButton")
        {
            if (RebindBeforeCustomButtonEvent)
            {
                this.BindDataGrid(true);
            }
            if (this.CustomButton != null)
            {
                if (RebindBeforeCustomButtonEvent)
                {
                    this.CustomButton(this, new EditableGridPrintEventArgs(this.SelectedItems));
                }
                else
                {
                    this.CustomButton(this, new EditableGridPrintEventArgs(null));
                }
            }
            if (RebindAfterCustomButtonEvent)
            {
                this.BindDataGrid(true);
            }
            return;
        }

        if (e.CommandName == "MoveUp")
        {
            this.BindDataGrid(true);
            if (this.SelectedItems.Length == 0) return;
            this.OnMoveUp(this.SelectedItems);
            this.BindDataGrid(true);
            return;
        }

        if (e.CommandName == "Confirm")
        {
            this.BindDataGrid(true);
            if (this.SelectedItems.Length == 0) return;
            this.OnConfirm(this.SelectedItems);
            this.BindDataGrid(true);
            return;
        }

        if (e.CommandName == "MoveDown")
        {
            this.BindDataGrid(true);
            if (this.SelectedItems.Length == 0) return;
            this.OnMoveDown(this.SelectedItems);
            this.BindDataGrid(true);
            return;
        }

        if (e.CommandName == "ShowAll")
        {
            ClearFilter();
            this.BindDataGrid(true);
            return;
        }

        if (e.CommandName == "View")
        {
            this.BindDataGrid(true);
            RegisterScript(m_viewUrl);
            return;
        }

        if (e.CommandName == "LookupDiagnosis")
        {
            this.BindDataGrid(true);
            RegisterScript(this.m_LookupDxScript);
            return;
        }

        if (e.CommandName == "PredeterminedPrint")
        {
            this.BindDataGrid(true);

            if (this.SelectedItems.Length == 0)
            {
                return;
            }

            this.OnPredeterminedPrint(this.SelectedItems);
            this.BindDataGrid(true);
            return;
        }

        if (e.CommandName == "ViewAssociatedClaims")
        {
            this.BindDataGrid(true);

            if (this.SelectedItems.Length == 0)
            {
                return;
            }

            this.OnViewAssociatedClaims(this.SelectedItems);
            return;
        }

        if (e.CommandName == "PredeterminedPrintNew")
        {
            this.BindDataGrid(true);

            if (this.SelectedItems.Length == 0)
            {
                return;
            }

            this.OnPredeterminedPrintNew(this.SelectedItems);
            this.BindDataGrid(true);
            return;
        }
    }

    public void RegisterScript(string scriptBody)
    {
        this.radList.Controls.Add(new LiteralControl("<script type=\"text/javascript\">" + scriptBody + "</script>"));
    }

    private string m_NotDeletedErrorMessage = null;
    public string NotDeletedErrorMessage
    {
        set { m_NotDeletedErrorMessage = value; }
    }
    private void OnDeleteItem(GridEditableItem editedItem)
    {
        if (this.Delete != null)
        {
            this.Delete(this, new EditableGridDeleteEventArgs(editedItem));
        }
    }

    private void OnPrint(GridEditableItem[] printedItems)
    {
        if (this.Print != null)
        {
            this.Print(this, new EditableGridPrintEventArgs(printedItems));
        }
    }

    private void OnPredeterminedPrint(GridEditableItem[] printedItems)
    {
        if (this.PredeterminedPrint != null)
        {
            this.PredeterminedPrint(this, new EditableGridPrintEventArgs(printedItems));
        }
    }

    private void OnPredeterminedPrintNew(GridEditableItem[] printedItems)
    {
        if (this.PredeterminedPrintNew != null)
        {
            this.PredeterminedPrintNew(this, new EditableGridPrintEventArgs(printedItems));
        }
    }

    private void OnViewAssociatedClaims(GridEditableItem[] printedItems)
    {
        if (this.ViewAssociatedClaims != null)
        {
            this.ViewAssociatedClaims(this, new EditableGridPrintEventArgs(printedItems));
        }
    }

    private void OnMoveUp(GridEditableItem[] MovedUpItems)
    {
        if (this.MoveUp != null)
        {
            this.MoveUp(this, new EditableGridMoveEventArgs(MovedUpItems));
        }
    }

    private void OnConfirm(GridEditableItem[] ConfirmItems)
    {
        if (this.Confirm != null)
        {
            this.Confirm(this, new EditableGridMoveEventArgs(ConfirmItems));
        }
    }

    private void OnMoveDown(GridEditableItem[] MovedDownItems)
    {
        if (this.MoveDown != null)
        {
            this.MoveDown(this, new EditableGridMoveEventArgs(MovedDownItems));
        }
    }

    private void radList_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        if (this.Sort != null)
            this.Sort(source, e);
    }

    private void radList_ColumnCreated(object sender, Telerik.Web.UI.GridColumnCreatedEventArgs e)
    {
        if (e.Column is GridBoundColumn)
        {
            GridBoundColumn col = e.Column as GridBoundColumn;
            col.AllowFiltering = false;

            if (col.DataType == typeof(DateTime))
            {
                //col.DataFormatString = "{0:d}";
                col.DataFormatString = "{0:MM/dd/yyyy}";
            }

            if (this.ColumnsSettings[col.DataField] != null)
            {
                ColumnSettings colSetting = this.ColumnsSettings[col.DataField] as ColumnSettings;
                //col.DataFormatString = "${0:f2}";
                col.DataFormatString = colSetting.FormatString;
                col.Visible = colSetting.Visible;
                col.AllowFiltering = colSetting.AllowFiltering;
                col.ItemStyle.HorizontalAlign = colSetting.HorizontalAlign;
                col.ItemStyle.Font.Bold = colSetting.BoldFont;
                if (colSetting.HeaderText != string.Empty)
                    col.HeaderText = colSetting.HeaderText;
                if (colSetting.Width > 0)
                {
                    col.HeaderStyle.Width = Unit.Pixel(colSetting.Width);
                }
                col.CurrentFilterFunction = colSetting.CurrentFilterFunction;
                col.CurrentFilterValue = colSetting.CurrentFilterValue;
            }
        }
        if (e.Column is Telerik.Web.UI.GridCheckBoxColumn)
        {
            GridCheckBoxColumn col = e.Column as GridCheckBoxColumn;
            col.AllowFiltering = false;
            if (this.ColumnsSettings[col.DataField] != null)
            {
                ColumnSettings colSetting = this.ColumnsSettings[col.DataField] as ColumnSettings;
                col.Visible = colSetting.Visible;
                col.AllowFiltering = colSetting.AllowFiltering;
                col.ItemStyle.HorizontalAlign = colSetting.HorizontalAlign;
                col.ItemStyle.Font.Bold = colSetting.BoldFont;
                if (colSetting.HeaderText != string.Empty)
                    col.HeaderText = colSetting.HeaderText;
                if (colSetting.Width > 0)
                {
                    col.HeaderStyle.Width = Unit.Pixel(colSetting.Width);
                    col.ItemStyle.Width = Unit.Pixel(colSetting.Width);
                    col.FooterStyle.Width = Unit.Pixel(colSetting.Width);
                }
                col.CurrentFilterFunction = colSetting.CurrentFilterFunction;
                col.CurrentFilterValue = colSetting.CurrentFilterValue;
            }
        }
    }

    private void radList_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridHeaderItem)
        {
            foreach (string dataField in this.ColumnsSettings.Keys)
            {
                ColumnSettings colSetting = this.ColumnsSettings[dataField] as ColumnSettings;
                if (!colSetting.AllowSort)
                {
                    TableCell cell = (e.Item as GridHeaderItem)[dataField];
                    LinkButton sortButton = (LinkButton)(cell.Controls[0]);
                    LiteralControl ctrl = new LiteralControl(sortButton.Text);
                    cell.Controls.Remove(sortButton);
                    cell.Controls.Add(ctrl);
                }
            }
        }


        if (e.Item is GridFilteringItem && m_FilterOnEnterMode)
        {
            foreach (GridBoundColumn column in e.Item.OwnerTableView.AutoGeneratedColumns)
            {
                if (column.AllowFiltering)
                {
                    string filterType = "Contains";
                    if (column.DataTypeName == "System.DateTime")
                    {
                        filterType = "EqualTo";
                    }
                    ((TextBox)((GridFilteringItem)e.Item)[column.UniqueName].Controls[0]).Attributes["onkeypress"] = String.Format("OnKeyPressGridFilterToEnterMode('{0}','FilterOnEnter:{1}:{2}'); ", this.UniqueID, filterType, column.UniqueName);
                    if (m_FilterOnEnterModeFocisUniqueName == column.UniqueName)
                    {
                        string jscript = "<script language='javascript'>\n ";
                        jscript += "SetControlFocusAndSelectByName('" + ((TextBox)((GridFilteringItem)e.Item)[column.UniqueName].Controls[0]).UniqueID + "');\n";
                        jscript += "</script>";
                        if (!Page.IsStartupScriptRegistered("SetFilterTextBoxFocusScript"))
                        {
                            Page.RegisterStartupScript("SetFilterTextBoxFocusScript", jscript);
                        }
                    }
                }
            }
        }

        if (e.Item is GridFilteringItem && m_FilterOnEnterModeAjax)
        {
            foreach (GridBoundColumn column in e.Item.OwnerTableView.AutoGeneratedColumns)
            {
                if (column.AllowFiltering)
                {
                    string filterType = "Contains";
                    if (column.DataTypeName == "System.DateTime")
                    {
                        filterType = "EqualTo";
                    }
                    ((TextBox)((GridFilteringItem)e.Item)[column.UniqueName].Controls[0]).Attributes["onkeypress"] = String.Format("OnKeyPressGridFilterToEnterModeAJAX('{0}','FilterOnEnter:{1}:{2}'); ", this.UniqueID, filterType, column.UniqueName);
                }
            }
        }
    }

    private void ClearFilter()
    {
        foreach (GridBoundColumn column in radList.MasterTableView.AutoGeneratedColumns)
        {
            if (column.AllowFiltering)
            {
                column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                column.CurrentFilterValue = "";
                radList.MasterTableView.GetItems(GridItemType.FilteringItem)[0].FireCommandEvent("Filter", new Pair("NoFilter", column.UniqueName));
            }
        }
    }

    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument.Split(':')[0] == "FilterOnEnter")
        {
            if (this.m_FilterOnEnterMode && !m_SelectFilterTextBox)
            {
                foreach (GridBoundColumn column in radList.MasterTableView.AutoGeneratedColumns)
                {
                    if (column.AllowFiltering)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                    }
                }
            }
            radList.MasterTableView.GetItems(GridItemType.FilteringItem)[0].FireCommandEvent("Filter", new Pair(eventArgument.Split(':')[1], eventArgument.Split(':')[2]));
            GridItem di = radList.MasterTableView.GetItems(GridItemType.FilteringItem)[0];
            if (this.m_FilterOnEnterMode && !m_SelectFilterTextBox)
            {
                ((TextBox)((GridFilteringItem)di)[eventArgument.Split(':')[2]].Controls[0]).Text = "";
            }
            if (m_SelectFilterTextBox)
            {
                string jscript = "<script language='javascript'>\n ";
                jscript += "SetControlFocusAndSelectByName('" + ((TextBox)((GridFilteringItem)di)[eventArgument.Split(':')[2]].Controls[0]).UniqueID + "');\n";
                jscript += "</script>";
                if (!Page.IsStartupScriptRegistered("SetFilterTextBoxFocusScript"))
                {
                    Page.RegisterStartupScript("SetFilterTextBoxFocusScript", jscript);
                }
            }
        }
        if (eventArgument == "RefreshEditableGrid")
        {
            this.BindDataGrid(true);
        }
    }

    private void radList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        this.OnNeedDataSource();
    }

    public string onRowDblClickClientEvent
    {
        set { this.radList.ClientSettings.ClientEvents.OnRowDblClick = value; }
    }

    public string onRowClickClientEvent
    {
        set { this.radList.ClientSettings.ClientEvents.OnRowClick = value; }
    }

    public string OnRowSelected
    {
        set { this.radList.ClientSettings.ClientEvents.OnRowSelecting = value; }
    }

    public string GridClientID
    {
        get { return this.radList.ClientID; }
    }

    public string OnGridCreatedClientEvent
    {
        set { this.radList.ClientSettings.ClientEvents.OnGridCreated = value; }
        get { return this.radList.ClientSettings.ClientEvents.OnGridCreated; }
    }

    public string GridRowSelectClientAction = "alert('select');";

    public bool ShowGridHeader
    {
        set { this.radList.ShowHeader = value; }
    }

    public FontUnit ItemsFontSize
    {
        set
        {
            this.radList.ItemStyle.Font.Size = value;
            this.radList.AlternatingItemStyle.Font.Size = value;
        }
    }

    public string MasterTableViewItemCssClass
    {
        set
        {
            this.radList.MasterTableView.ItemStyle.CssClass = value;
        }
    }

    public string MasterTableViewAltItemCssClass
    {
        set
        {
            this.radList.MasterTableView.AlternatingItemStyle.CssClass = value;
        }
    }

    public void HideGridLines()
    {
        this.radList.GridLines = GridLines.None;
    }

    public GridItem FirstSelectedItem
    {
        get
        {
            if (radList.SelectedItems != null && radList.SelectedItems.Count > 0)
            {
                return this.radList.SelectedItems[0];
            }
            else
            {
                return null;
            }

        }
    }

    public int PageSize
    {
        get { return radList.PageSize; }
        set { radList.PageSize = value; }
    }

    public int Width
    {
        set
        {
            this.radList.Width = new Unit(value);
        }
    }

    public GridDataItemCollection MasterTableViewItems
    {
        get { return this.radList.MasterTableView.Items; }
    }

    public string OnMasterTableViewCreated
    {
        set { this.radList.ClientSettings.ClientEvents.OnMasterTableViewCreated = value; }
    }

    public string OnGridCreated
    {
        set { this.radList.ClientSettings.ClientEvents.OnGridCreated = value; }
    }

    public string SortString
    {
        get
        {
            return radList.MasterTableView.SortExpressions.GetSortString();
        }
    }

    public bool AllowPaging
    {
        set
        {
            radList.AllowPaging = value;
        }
    }

    public bool FilterOnEnterMode
    {
        set
        {
            m_FilterOnEnterMode = value;
        }
    }

    public bool FilterOnEnterModeAjax
    {
        set
        {
            m_FilterOnEnterModeAjax = value;
        }
    }

    public bool SelectFilterTextBox
    {
        set
        {
            m_SelectFilterTextBox = value;
        }
    }

    public string ViewURL
    {
        set
        {
            m_viewUrl = value;
        }
    }

    public string LookupDiagnosisScript
    {
        set
        {
            this.m_LookupDxScript = value;
        }
    }

    public bool PrintWithoutSelecting
    {
        set
        {
            m_PrintWithoutSelecting = value;
        }
    }

    public bool Enabled
    {
        set
        {
            //radList.Enabled = value;
            if (value)
            {
                radList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
            }
            else
            {
                radList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
        }
        get
        {
            return radList.Enabled;
        }
    }

    public bool AllowScroll
    {
        set
        {
            radList.ClientSettings.Scrolling.AllowScroll = value;
        }
    }

    public int ScrollHeight
    {
        set
        {
            radList.ClientSettings.Scrolling.ScrollHeight = value;
        }
    }

    public bool ScrollingUseStaticHeaders
    {
        set
        {
            radList.ClientSettings.Scrolling.UseStaticHeaders = value;
        }
    }

    public string FilterOnEnterModeFocisUniqueName
    {
        set
        {
            m_FilterOnEnterModeFocisUniqueName = value;
        }
    }

    private string m_CustomText;
    public string CustomText
    {
        get { return m_CustomText; }
    }

    private string m_CustomButtonText;
    public string CustomButtonText
    {
        get { return m_CustomButtonText; }
    }

    private string m_CustomClick;
    public string CustomClick
    {
        get { return m_CustomClick; }
    }
    private string m_CustomImg;
    public string CustomImg
    {
        get { return m_CustomImg; }
    }
    private string m_CustomUrl;
    public string CustomUrl
    {
        get { return m_CustomUrl; }
    }
    private string m_CustomTarget;
    public string CustomTarget
    {
        get { return m_CustomTarget; }
    }

    public string CustomToolTip
    {
        get { return m_CustomToolTip; }
    }

    public void SetCustomButton(string Text, string ImgUrl, PrintEventHandler Handler)
    {
        m_gridModes |= GridModes.CustomButton;
        m_CustomButtonText = Text;
        //m_CustomText=Text;
        m_CustomImg = ImgUrl;
        this.CustomButton = Handler;
    }

    public void SetCustomButton(string Text, string ImgUrl, PrintEventHandler Handler,
        string toolTip)
    {
        SetCustomButton(Text, ImgUrl, Handler);
        m_CustomToolTip = toolTip;

    }

    public void SetCustomLink(string NavigateUrl, string Text, string Target, string ImgUrl)
    {
        SetCustomLink(NavigateUrl, Text, Target, ImgUrl, "");
    }

    public void SetCustomLink(string NavigateUrl, string Text, string Target, string ImgUrl, string onClick)
    {
        m_gridModes |= GridModes.CustomLink;
        m_CustomText = Text;
        m_CustomImg = ImgUrl;
        m_CustomUrl = NavigateUrl;
        m_CustomTarget = Target;
        m_CustomClick = onClick;
    }

    public bool RebindBeforeCustomButtonEvent
    {
        get
        {
            return m_RebindBeforeCustomButtonEvent;
        }
        set
        {
            m_RebindBeforeCustomButtonEvent = value;
        }
    }

    public bool RebindAfterCustomButtonEvent
    {
        get
        {
            return m_RebindAfterCustomButtonEvent;
        }
        set
        {
            m_RebindAfterCustomButtonEvent = value;
        }
    }

    public bool ShowFooter
    {
        set
        {
            this.radList.ShowFooter = value;
        }
    }

    public bool ShowCountRecord
    {
        set
        {
            m_ShowCountRecord = value;
        }
        get
        {
            return m_ShowCountRecord;
        }
    }

    public bool SelectedIndexiesStorageInSession
    {
        get
        {
            return m_SelectedIndexiesStorageInSession;
        }
        set
        {
            m_SelectedIndexiesStorageInSession = value;
        }
    }

    public string OnRowCreatedClientEvent
    {
        set { this.radList.ClientSettings.ClientEvents.OnRowCreated = value; }
    }

    public string OnRowSelectedClientEvent
    {
        set { this.radList.ClientSettings.ClientEvents.OnRowSelected = value; }
    }

    public void SetFilterValue(string index, string filterValue)
    {
        GridItem[] gi = radList.MasterTableView.GetItems(GridItemType.FilteringItem);
        GridItem di = gi[0];
        if (this.m_FilterOnEnterMode && !m_SelectFilterTextBox)
        {
            ((TextBox)((GridFilteringItem)di)[index].Controls[0]).Text = filterValue;
        }
    }

    public string Skin
    {
        set
        {
            radList.Skin = value;
        }
    }

    public bool AllowRowSelect
    {
        set
        {
            radList.ClientSettings.Selecting.AllowRowSelect = value;
        }
    }

    public bool AllowRowResize
    {
        set
        {
            radList.ClientSettings.Resizing.AllowRowResize = value;
        }
    }

    public bool AllowColumnResize
    {
        set
        {
            radList.ClientSettings.Resizing.AllowColumnResize = value;
        }
    }

    public bool HideCommandPanel
    {
        set
        {
            if (value)
            {
                this.radList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
        }
    }

    public string EditableGridUniqueID
    {
        get
        {
            return this.UniqueID;
        }
    }

    protected void btnRefreshEditableGrid_Click(object sender, EventArgs e)
    {
        this.BindDataGrid(true);
    }

    public string RefreshEditableGridClientID
    {
        get { return btnRefreshEditableGrid.ClientID; }
    }

    public bool AllowRowsDragDrop
    {
        set { radList.ClientSettings.AllowRowsDragDrop = value; }
    }

    private void radList_RowDrop(object sender, GridDragDropEventArgs e)
    {
        if (this.RowDragDrop != null)
            this.RowDragDrop(sender, e);
    }
}

public class ColumnSettings
{
    bool allowSort = true;
    bool visible = true;
    bool allowFiltering = false;
    string headerText = string.Empty;
    string formatString = string.Empty;
    HorizontalAlign horizontalAlign = HorizontalAlign.Center;
    int width = 0;
    bool boldFont = false;
    Telerik.Web.UI.GridKnownFunction currentFilterFunction = Telerik.Web.UI.GridKnownFunction.NoFilter;
    string currentFilterValue = string.Empty;

    public Telerik.Web.UI.GridKnownFunction CurrentFilterFunction
    {
        get { return currentFilterFunction; }
        set { currentFilterFunction = value; }
    }

    public string CurrentFilterValue
    {
        get { return currentFilterValue; }
        set { currentFilterValue = value; }
    }

    public HorizontalAlign HorizontalAlign
    {
        get { return this.horizontalAlign; }
        set { this.horizontalAlign = value; }
    }

    public bool AllowSort
    {
        get { return this.allowSort; }
        set { this.allowSort = value; }
    }

    public bool Visible
    {
        get { return this.visible; }
        set { this.visible = value; }
    }

    public bool AllowFiltering
    {
        get { return this.allowFiltering; }
        set { this.allowFiltering = value; }
    }

    public string HeaderText
    {
        get { return this.headerText; }
        set { this.headerText = value; }
    }

    public string FormatString
    {
        get { return this.formatString; }
        set { this.formatString = value; }
    }

    public int Width
    {
        get { return this.width; }
        set { this.width = value; }
    }

    public bool BoldFont
    {
        get { return this.boldFont; }
        set { this.boldFont = value; }
    }

}
