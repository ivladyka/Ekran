using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for ChoiceControlBase
/// </summary>
public delegate void SelectedItemChangedEventHandler(object sender, SelectedItemChangedEventArgs e);

public class ChoiceControlBase : ControlBase
{
    protected bool addEmptyItem = true;
    public event SelectedItemChangedEventHandler SelectedItemChanged;
    protected string[] m_textValues;

    virtual protected RadComboBox ddlList
    {
        get { return null; }
    }

    public ChoiceControlBase()
    {
        this.m_Name = "ChoiceControlBase";
    }

    public bool AllowCustomText
    {
        get { return this.ddlList.AllowCustomText; }
        set { this.ddlList.AllowCustomText = value; }
    }

    public bool OpenOnLoad
    {
        get { return this.ddlList.OpenDropDownOnLoad; }
        set { this.ddlList.OpenDropDownOnLoad = value; }
    }

    public bool UseValueInsteadText
    {
        get
        {
            if (this.ViewState["USE_VALUE_INSTEAD_TEXT"] != null)
            {
                return (bool)this.ViewState["USE_VALUE_INSTEAD_TEXT"];
            }
            return false;
        }
        set
        {
            this.ViewState["USE_VALUE_INSTEAD_TEXT"] = value;
        }
    }

    public bool AutoPostBack
    {
        get { return this.ddlList.AutoPostBack; }
        set { this.ddlList.AutoPostBack = value; }
    }

    public virtual string SelectedText
    {
        set { this.ddlList.Text = value; }
        get { return this.ddlList.Text; }
    }

    public virtual string SelectedValue
    {
        set
        {
            try
            {
                this.ddlList.SelectedValue = value;
            }
            catch { }
        }
        get { return this.ddlList.SelectedValue; }
    }

    public string Text
    {
        get
        {
            return this.Request.Params["text"].Trim();
        }
    }

    public bool AddEmptyItem
    {
        get { return this.addEmptyItem; }
        set { this.addEmptyItem = value; }
    }

    public void Reset()
    {
        this.ddlList.SelectedValue = null;
        this.ddlList.Text = "";
    }

    protected void InitFromArray()
    {
        foreach (string text in this.m_textValues)
        {
            RadComboBoxItem item = new RadComboBoxItem(text);
            this.ddlList.Items.Add(item);
        }
    }

    protected override void InitializeComponent()
    {
        base.InitializeComponent();
        this.ddlList.MarkFirstMatch = true;
    }


    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();

        if (!this.ddlList.EnableLoadOnDemand)
            this.InitDDLInternal();
    }

    private void InitDDLInternal()
    {
        this.ddlList.Items.Clear();
        if (this.AddEmptyItem)
        {
            this.ddlList.Items.Add(new RadComboBoxItem("", ""));
        }
        this.InitDDL();
    }

    protected virtual void ReInitDLL()
    {
        this.InitDDLInternal();
    }

    protected override void SetEventHandlers()
    {
        base.SetEventHandlers();
        this.ddlList.ItemsRequested += new RadComboBoxItemsRequestedEventHandler(OnItemsRequested);
        this.ddlList.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(OnSelectedIndexChanged);
    }

    protected virtual void OnItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
    {
        this.InitDDLInternal();
    }

    protected virtual void InitDDL()
    {
        if (m_textValues != null && this.m_textValues.Length != 0)
        {
            this.InitFromArray();
        }
    }

    protected virtual void OnSelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        this.OnSelectedIndexChangedInternal();
    }

    void OnSelectedIndexChangedInternal()
    {
        if (this.SelectedItemChanged != null && !Utils.IsPageCallBack(this.Page))
        {
            this.SelectedItemChanged(this, new SelectedItemChangedEventArgs(this.SelectedText, this.SelectedValue));
        }
    }
    public string DDLClientID
    {
        get
        {
            return this.ddlList.ClientID;
        }
    }

    public bool Enabled
    {
        get
        {
            return this.ddlList.Enabled;
        }
        set
        {
            this.ddlList.Enabled = value;
        }
    }

    public string OnClientKeyPressing
    {
        set
        {
            ddlList.OnClientKeyPressing = value;
        }
    }

    public string OnClientSelectedIndexChanged
    {
        set
        {
            ddlList.OnClientSelectedIndexChanged = value;
        }
    }

    public short TabIndex
    {
        set
        {
            ddlList.TabIndex = value;
        }
    }

    public Unit Width
    {
        get { return ddlList.Width; }
        set { ddlList.Width = value; }
    }

    public void RebindData()
    {
        InitDDLInternal();
    }

    public string OnClientItemsRequested
    {
        set
        {
            ddlList.OnClientItemsRequested = value;
        }
    }

    public void ClearAllItems()
    {
        this.ddlList.Items.Clear();
    }

    public void SelectFirstItem()
    {
        if (ddlList.Items.Count > 0)
        {
            this.SelectedValue = ddlList.Items[0].Value;
        }
    }

    public virtual void LoadByPrimaryKey(string primaryKeyValue)
    {

    }

    public int CountItems
    {
        get
        {
            return ddlList.Items.Count;
        }
    }
}
