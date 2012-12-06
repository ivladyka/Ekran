using System;
using VikkiSoft_BLL;
using MyGeneration.dOOdads;

/// <summary>
/// Summary description for ControlBase
/// </summary>
public class ControlBase : System.Web.UI.UserControl, Interfaces.INamedControl
{
    protected string m_Name;
    protected bool m_IsSecureControl;
    private string m_AllowUserTypes = "";
    protected string m_Keywords = "";
    private string m_BackURL = "~/Default.aspx";
    private bool m_AddOnlyBrowserTitle = false;

    public ControlBase()
    {
        this.m_Name = "ControlBase";
        m_IsSecureControl = false;
    }

    protected virtual void SetEventHandlers()
    {
        this.Load += new System.EventHandler(this.Page_Load);
    }

    protected virtual void Page_Load(object sender, System.EventArgs e)
    {
        if(!IsAllowedUser())
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected virtual void InitOnFirstLoading()
    {

    }

    #region INamedControl Members

    public string Name
    {
        get { return this.m_Name; }
    }

    #endregion

    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
        if (!Utils.IsPagePostBack(this))
        {
            this.InitOnFirstLoading();
            if (Page is ProjectPageBase && Keywords != "")
            {
                ((ProjectPageBase)Page).AddKeywords(Keywords);
            }
        }
    }

    protected virtual void InitializeComponent()
    {
        this.SetEventHandlers();
    }

    private bool IsAllowedUser()
    {
        if (AllowUserTypes.Trim() != "")
        {
            if (Request.IsAuthenticated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public string AllowUserTypes
    {
        get
        {
            return m_AllowUserTypes;
        }
        set
        {
            m_AllowUserTypes = value;
        }
    }

    public string Keywords
    {
        set
        {
            m_Keywords = value;
        }
        get
        {
            return m_Keywords;
        }
    }

    public string BackURL
    {
        get
        {
            return m_BackURL;
        }
        set
        {
            m_BackURL = value;
        }
    }

    public bool AddOnlyBrowserTitle
    {
        get
        {
            return m_AddOnlyBrowserTitle;
        }
        set
        {
            m_AddOnlyBrowserTitle = value;
        }
    }
}
