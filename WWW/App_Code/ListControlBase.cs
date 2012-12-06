using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for ListControlBase
/// </summary>
public abstract class ListControlBase : SqlClientEntityControlBase
{
    private EditableGrid m_editableGrid;
    protected string m_requestPageName;
    private bool m_EditInModalWindow = false;
    private int m_ModaldialogHeight = 290;
    private int m_ModaldialogWidth = 510;
    private string m_EditURLAdditionalParameters = "";
    private string m_ScriptAfterCloseModalDialog = "";
    private string m_ScriptConditionAfterCloseModalDialog = "retVal == 'Save'";
    private bool m_ReadOnly = false;

    //protected new abstract string[] GetPrimaryKeys();		
    protected virtual string GetEditableControlName()
    {
        return "";
    }

    public ListControlBase()
    {
        this.m_Name = "ListControlBase";
    }

    protected virtual string GetUrlForNewEntity()
    {
        if (EditInModalWindow)
        {
            return "../ModalDialog.aspx?content=" + this.GetEditableControlName() + "&IsNew=true";
        }
        else
        {
            return this.m_requestPageName + "?content=" + this.GetEditableControlName() + "&IsNew=true";
        }
    }

    protected override void InitializeComponent()
    {
        base.InitializeComponent();
        if (this.Request.Url.Query != "")
        {
            this.m_requestPageName = this.Request.Url.AbsoluteUri.Replace(this.Request.Url.Query, "");
        }
        else
        {
            this.m_requestPageName = this.Request.Url.AbsoluteUri;
        }

    }

    public virtual void InitGrid()
    {
        this.EditableGridControl.GridMode = GridModes.Refresh;
        this.EditableGridControl.EditableControlName = this.GetEditableControlName();
        this.EditableGridControl.UrlToAdd = this.GetUrlForNewEntity();
        if (!Utils.IsPagePostBack(this))
        {
            this.InitGridDefaults();
        }
    }

    public EditableGrid EditableGridControl
    {
        get
        {
            if(m_editableGrid == null)
            {
                HtmlForm form = null;
                if (Page.Master == null)
                {
                    form = (HtmlForm)Page.FindControl("form1");
                }
                else
                {
                    form = (HtmlForm)Page.Master.FindControl("form1");
                }
                if (form != null)
                {
                    ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)form.FindControl("cphWithoutScroll");
                    UserControl uc = null;
                    if (contentPlaceHolder != null)
                    {
                        uc = (UserControl)
                            contentPlaceHolder.FindControl(ContentControlName + "ID");
                    }
                    else
                    {
                        uc = (UserControl)
                            form.FindControl(ContentControlName + "ID");
                    }
                    if(uc != null)
                    {
                        m_editableGrid = (EditableGrid)uc.FindControl("editableGrid");
                    }
                    if(m_editableGrid == null)
                    {
                        m_editableGrid = (EditableGrid)this.FindControl("editableGrid");
                    }
                }
            }
            return m_editableGrid;
        }
    }

    public virtual void InitGridDefaults()
    {

    }

    override protected void SetEventHandlers()
    {
        base.SetEventHandlers();
        this.EditableGridControl.NeedDataSource += new NeedDataSourceEventHandler(this.OnEditableGridNeedDataSource);
        this.EditableGridControl.ItemDataBound += new ItemDataBoundEventHandler(this.OnEditableGridItemDataBound);
        this.EditableGridControl.Delete += new DeleteEventHandler(this.OnEditableGridDelete);
        this.EditableGridControl.Sort += new SortEventHandler(this.OnEditableGridSort);
        this.EditableGridControl.RowDragDrop += new RowDragDropEventHandler(this.OnEditableGridRowsDragDrop);
    }

    protected virtual DataTable GetDataSource()
    {
        EditableEntity entity = this.CreateEditableEntityInstance();
        this.SetFilters(entity);
        entity.Load();
        return entity.SqlClientEntity.DefaultView.Table;
    }

    protected virtual void SetFilters(EditableEntity entity)
    {
    }

    protected virtual DataTable OnEditableGridNeedDataSource()
    {
        this.InitGrid();
        if (ReadOnly)
        {
            EditableGridControl.Enabled = false;
            EditableGridControl.AllowSorting = false;
        }
        return this.GetDataSource();
    }

    public virtual void RebindGrid()
    {
        this.InitGridDefaults();
        this.EditableGridControl.BindDataGrid(true);
    }

    protected virtual void OnEditableGridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            DataRowView dataRowView = e.Item.DataItem as DataRowView;
            if (dataRowView != null)
            {
                HyperLink lnkEdit = e.Item.Cells[2].FindControl("lnkEdit") as HyperLink;
                if (lnkEdit != null)
                {
                    string primaryKeysString = "";
                    string[] keys = this.GetPrimaryKeys();
                    if (keys == null)
                    {
                        lnkEdit.Visible = false;
                        return;
                    }
                    foreach (string key in this.GetPrimaryKeys())
                    {
                        primaryKeysString += "&" + key + "=" + dataRowView[this.GetKeySynonym(key)].ToString();
                    }
                    if (primaryKeysString != "")
                    {
                        lnkEdit.NavigateUrl = this.m_requestPageName + "?content="
                            + this.GetEditableControlName() + primaryKeysString
                            + EditURLAdditionalParameters;
                        if (EditInModalWindow)
                        {
                            lnkEdit.Attributes["onclick"] = "return Edit" + this.ClientID
                                + "ModalWindow('../ModalDialog.aspx?content="
                                + this.GetEditableControlName() + primaryKeysString
                                + EditURLAdditionalParameters + "');";
                        }
                    }
                }
                if(ReadOnly)
                {
                    e.Item.Cells[2].Text = "";
                }
            }
        }
        if (e.Item is Telerik.Web.UI.GridCommandItem)
        {
            if (EditInModalWindow)
            {
                LinkButton btnRedindGrid = (LinkButton) e.Item.FindControl("btnRedindGrid");
                if (btnRedindGrid != null)
                {
                    string controlClientID = this.ClientID;
                    string jscript = "<script language='javascript'>\n "
                                     + "function Rebind" + controlClientID + "Grid()\n"
                                     + "{\n"
                                     + "VIKKI_ClickButtonByClientID('" + editableGrid.RefreshEditableGridClientID + "');\n"
                                     + "return false;\n"
                                     + "}\n"
                                     + "function Edit" + controlClientID + "ModalWindow(navigateurl)\n"
                                     + "{\n"
                                     + "var retVal = window.showModalDialog(navigateurl, '', 'dialogWidth:"
                                     + ModaldialogWidth + "px; dialogHeight:"
                                     + ModaldialogHeight +
                                     "px; resizable:yes; status:no; scroll:yes; help:no; edge: sunken;');\n";
                    jscript += "if(" + ScriptConditionAfterCloseModalDialog + " || retVal == null)\n"
                               + "{\n"
                               + "Rebind" + controlClientID + "Grid();\n";
                    if (ScriptAfterCloseModalDialog != "")
                    {
                        jscript += ScriptAfterCloseModalDialog;
                    }
                    jscript += "}\n"
                               + "return false;\n"
                               + "}\n"
                               + "</script>";
                    if (!Page.IsStartupScriptRegistered("Rebind" + controlClientID + "GridScript"))
                    {
                        Page.RegisterStartupScript("Rebind" + controlClientID + "GridScript", jscript);
                    }
                }
            }
            HtmlAnchor hlAddRecord = (HtmlAnchor)e.Item.FindControl("hlAddRecord");
            if(hlAddRecord != null)
            {
                hlAddRecord.HRef = EditableGridControl.UrlToAdd;
                if (EditInModalWindow)
                {
                    hlAddRecord.Attributes["onclick"] = "return Edit" + this.ClientID + "ModalWindow('"
                        + EditableGridControl.UrlToAdd + "');";
                }
            }
        }
    }

    protected virtual void OnEditableGridDelete(object sender, EditableGridDeleteEventArgs e)
    {
        EditableEntity entity = this.GetEditableEntity(new ValueHolder(e.DeletedItem.DataItem as DataRowView));
        entity.Delete();
    }

    public bool EditInModalWindow
    {
        get
        {
            return m_EditInModalWindow;
        }
        set
        {
            m_EditInModalWindow = value;
        }
    }

    public int ModaldialogHeight
    {
        get
        {
            return m_ModaldialogHeight;
        }
        set
        {
            m_ModaldialogHeight = value;
        }
    }

    public int ModaldialogWidth
    {
        get
        {
            return m_ModaldialogWidth;
        }
        set
        {
            m_ModaldialogWidth = value;
        }
    }

    public string EditURLAdditionalParameters
    {
        set
        {
            m_EditURLAdditionalParameters = value;
        }
        get
        {
            return m_EditURLAdditionalParameters;
        }
    }

    public string ScriptAfterCloseModalDialog
    {
        get
        {
            return m_ScriptAfterCloseModalDialog;
        }
        set
        {
            m_ScriptAfterCloseModalDialog = value;
        }
    }

    public string ScriptConditionAfterCloseModalDialog
    {
        get
        {
            return m_ScriptConditionAfterCloseModalDialog;
        }
        set
        {
            m_ScriptConditionAfterCloseModalDialog = value;
        }
    }

    public GridDataItemCollection EditableGridItems
    {
        get { return EditableGridControl.Items; }
    }

    protected virtual void OnEditableGridSort(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
    {

    }

    public bool ReadOnly
    {
        get
        {
            return m_ReadOnly;
        }
        set
        {
            m_ReadOnly = value;
        }
    }

    public EditableGrid editableGrid
    {
        get { return m_editableGrid; }
    }

    protected string ContentControlName
    {
        get
        {
            string contentControlName = "";
            if (Request.Params["content"] != null)
            {
                contentControlName = Request.Params["content"].ToString();
            }
            return contentControlName;
        }
    }

    public void SetColumnSettings(string columnName, bool visible,
            string headerText, int width, HorizontalAlign hAlign, string formatString)
    {
        ColumnSettings colSetting = new ColumnSettings();
        colSetting.HeaderText = headerText;
        colSetting.Visible = visible;
        colSetting.Width = width;
        colSetting.HorizontalAlign = hAlign;
        colSetting.FormatString = formatString;
        this.editableGrid.ColumnsSettings[columnName] = colSetting;
    }

    public string GridClientID
    {
        get
        {
            return editableGrid.GridClientID;
        }
    }

    protected virtual void OnEditableGridRowsDragDrop(object sender, Telerik.Web.UI.GridDragDropEventArgs e)
    {

    }
}
