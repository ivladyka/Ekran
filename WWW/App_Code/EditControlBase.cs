using System;
using System.Collections;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MyGeneration.dOOdads;

/// <summary>
/// Summary description for EditControlBase
/// </summary>
public abstract class EditControlBase : SqlClientEntityControlBase
{
    protected System.Web.UI.WebControls.Button m_btnUpdate;
    protected System.Web.UI.WebControls.Button m_btnCancel;
    protected string[] prefixies = new string[] { "text_", "num_", "choice_", "date_", "msk_", "msknum_", "chk_", "time_", "int_", "alter_", "editor_", "upload_", "label_" };
    private string m_ReturnValue = "";

    public virtual EditableEntity EditableEntity
    {
        set
        {
            UserSession.SetObjectKey(UserSession.EDIT_CONTROL_EDITABLE_ENTITY, value);
        }
        get
        {
            return UserSession.GetObjectKey(UserSession.EDIT_CONTROL_EDITABLE_ENTITY) as EditableEntity;
        }
    }

    protected string UriReferrer
    {
        set
        {
            UserSession.SetKey(UserSession.EDIT_CONTROL_BASE_URI_REFERRER, value);
        }
        get
        {
            return UserSession.GetKeyAndRedirectIfNull(UserSession.EDIT_CONTROL_BASE_URI_REFERRER);
        }
    }

    public EditControlBase()
    {
        this.m_Name = "EditControlBase";
    }

    protected override void InitializeComponent()
    {
        base.InitializeComponent();
        if (this.m_Name == null || this.m_Name == "")
            return;
        if (this.IsNew)
        {
            this.m_Name = "Додати " + this.m_Name;
        }
        else
        {
            this.m_Name = "Редагувати " + this.m_Name;
        }
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();

        if (this.Request.UrlReferrer != null && this.Request.UrlReferrer.PathAndQuery != Request.Url.PathAndQuery)
        {
            this.UriReferrer = this.Request.UrlReferrer.PathAndQuery;
        }

        this.EditableEntity = this.GetEditableEntity(new ValueHolder(this.Request.QueryString));
        if (this.IsNew)
        {
            this.InitNewEntity();
        }
        else
        {
            this.ReadDataFromEntity();
        }
        if (BtnUpdate != null)
        {
            this.BtnUpdate.ToolTip = "Зберегти";
        }
        if (BtnCancel != null)
        {
            this.BtnCancel.ToolTip = "Відміна";
        }
    }

    protected virtual void ReadDataFromEntity()
    {
        ArrayList list = new ArrayList();
        this.ListEditableControls(this, list);
        foreach (Control control in list)
        {
            string name = this.GetValueNameFromControlId(control.ID);
            object value = this.EditableEntity[name];
            if (!Utils.IsValueNull(value))
            {
                this.SetControlValue(control, value);
            }
        }
    }

    virtual protected void WriteDataToEntity()
    {
        ArrayList list = new ArrayList();
        this.ListEditableControls(this, list);
        foreach (Control control in list)
        {
            if (control.Visible)
            {
                object value = this.GetControlValue(control);
                try
                {
                    if (control is ChoiceControlBase)
                    {
                        ChoiceControlBase choice = control as ChoiceControlBase;
                        if (choice.SelectedText == "")
                        {
                            ((SqlClientEntity)EditableEntity).SetColumnNull(this.GetValueNameFromControlId(control.ID));
                            continue;
                        }
                    }
                }
                catch{}
                if (value == null)
                {
                    ((SqlClientEntity) EditableEntity).SetColumnNull(this.GetValueNameFromControlId(control.ID));
                }
                else
                {
                    this.EditableEntity[this.GetValueNameFromControlId(control.ID)] = value;
                }
            }
        }
    }

    protected virtual object GetControlValue(Control control)
    {
        if (control is TextBox)
        {
            return (control as TextBox).Text.Trim();
        }

        if (control is NumericInput)
        {
            return (control as NumericInput).Value;
        }

        if (control is IntInput)
        {
            return (control as IntInput).Value;
        }

        if (control is DatePicker)
        {
            if ((control as DatePicker).SelectedDate == (control as DatePicker).MinDate)
            {
                return null;
            }
            return (control as DatePicker).SelectedDate;
        }

        if (control is ChoiceControlBase)
        {
            ChoiceControlBase choice = control as ChoiceControlBase;
            if (choice.UseValueInsteadText)
            {
                return choice.SelectedValue;
            }
            return choice.SelectedText;
        }

        if (control is CheckBox)
        {
            return (control as CheckBox).Checked ? 1 : 0;
        }

        if (control is EditorHTML)
        {
            return (control as EditorHTML).Html.Trim();
        }

        if (control is MaskedTextBox)
        {
            return (control as MaskedTextBox).Value;
        }

        if (control is PhotoUpload)
        {
            return (control as PhotoUpload).PhotoName;
        }

        return null;
    }


    protected virtual string GetValueNameFromControlId(string controlId)
    {
        foreach (string prefix in this.prefixies)
        {
            if (controlId.StartsWith(prefix))
            {
                return controlId.Substring(prefix.Length);
            }
        }

        throw new Exception(string.Format("Unsupported control ID [{0}]", controlId));
    }

    protected virtual void SetControlValue(Control control, object value)
    {
        if (control is TextBox)
        {
            (control as TextBox).Text = (string)value;
        }

        if (control is Label)
        {
            (control as Label).Text = (string)value;
        }

        if (control is NumericInput)
        {
            (control as NumericInput).Value = Convert.ToDouble(value);
        }

        if (control is IntInput)
        {
            (control as IntInput).Value = int.Parse(value.ToString());
        }

        if (control is DatePicker)
        {
            ((DatePicker)control).SelectedDate = (DateTime)value;
        }

        if (control is ChoiceControlBase)
        {
            ChoiceControlBase choice = control as ChoiceControlBase;
            if (choice.CountItems == 0)
            {
                choice.LoadByPrimaryKey(value.ToString());
            }
            if (choice.UseValueInsteadText)
            {
                choice.SelectedValue = value.ToString();
            }
            else
            {
                choice.SelectedText = value.ToString();
            }
        }

        if (control is CheckBox)
        {
            ((CheckBox)control).Checked = (Convert.ToInt32(value) == 1);
        }

        if (control is EditorHTML)
        {
            (control as EditorHTML).Html = (string)value;
        }

        if (control is MaskedTextBox)
        {
            (control as MaskedTextBox).Value = (string)value;
        }

        if (control is PhotoUpload)
        {
            (control as PhotoUpload).PhotoName = (string)value;
        }
    }

    protected virtual void ListEditableControls(Control parent, ArrayList list)
    {
        foreach (Control control in parent.Controls)
        {
            string id = control.ID;
            if (Utils.IsEmptyId(id))
                continue;

            if (this.CheckEditableControl(control))
                list.Add(control);
            else
                this.ListEditableControls(control, list);
        }
    }

    protected virtual bool IsSuitablePrefix(string controlID)
    {
        foreach (string prefix in this.prefixies)
        {
            if (controlID.StartsWith(prefix))
                return true;
        }
        return false;
    }

    virtual protected bool CheckEditableControl(Control control)
    {
        if (
            control is TextBox ||
            control is CheckBox ||
            control is ChoiceControlBase ||
            control is DatePicker ||
            control is NumericInput ||
            control is IntInput ||
            control is EditorHTML ||
            control is MaskedTextBox ||
            control is PhotoUpload ||
            control is Label
            )
        {
            return this.IsSuitablePrefix(control.ID);
        }
        return false;
    }

    protected virtual void InitNewEntity()
    {
    }

    override protected void SetEventHandlers()
    {
        base.SetEventHandlers();
        if (BtnUpdate != null)
        {
            this.BtnUpdate.Command += new CommandEventHandler(OnSave);
        }
        if (BtnCancel != null)
        {
            this.BtnCancel.Command += new CommandEventHandler(OnCancel);
        }
    }

    protected virtual void RedirectBackToList()
    {
        if (Page.Master == null)
        {
            CloseWindow();
        }
        else
        {
            if (this.BackURL != "~/Default.aspx" && this.BackURL != "")
            {
                this.Response.Redirect(BackURL);
            }
            else
            {
                this.Response.Redirect(this.UriReferrer);
            }
        }
    }

    protected void CloseWindow()
    {
        string jscript = "<script language='javascript'>\n "
            + "VIKKI_CloseWindow('" + m_ReturnValue + "')\n"
            + "</script>";
        if (!Page.IsStartupScriptRegistered("CloseWindowScript"))
        {
            Page.RegisterStartupScript("CloseWindowScript", jscript);
        }
    }

    protected virtual void OnCancel(object sender, CommandEventArgs e)
    {
        m_ReturnValue = "Close";
        this.RedirectBackToList();
    }

    protected virtual void OnSave(object sender, CommandEventArgs e)
    {
        m_ReturnValue = "Save";
        ReinitEditableEntity();

        OnSave();
    }

    protected virtual void OnSave()
    {
        if (this.EditableEntity != null)
        {
            try
            {
                this.WriteDataToEntity();
                this.EditableEntity.Save();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
                SqlException sqlEx = ex as SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                {
                    mes = "Can't insert duplicate record";
                }
                Utils.ShowMessage(this, mes);
                this.AfterSaveException();
                return;
            }
        }
        this.RedirectBackToList();
    }

    protected virtual void AfterSaveException()
    {

    }

    public void ReinitEditableEntity()
    {
        this.EditableEntity = null;
        this.EditableEntity = this.GetEditableEntity(new ValueHolder(this.Request.QueryString));
    }

    public string ReturnValue
    {
        set
        {
            m_ReturnValue = value;
        }
        get
        {
            return m_ReturnValue;
        }
    }

    public string ReturnURL
    {
        get
        {
            return this.UriReferrer;
        }
    }

    public Button BtnUpdate
    {
        get
        {
            if (m_btnUpdate == null)
            {
                UserControl uc = ContentUserControl;
                if (uc != null)
                {
                    m_btnUpdate = (Button)uc.FindControl("btnUpdate");
                }
            }
            return m_btnUpdate;
        }
    }

    public Button BtnCancel
    {
        get
        {
            if (m_btnCancel == null)
            {
                UserControl uc = ContentUserControl;
                if (uc != null)
                {
                    m_btnCancel = (Button)uc.FindControl("btnCancel");
                }
            }
            return m_btnCancel;
        }
    }

    private UserControl ContentUserControl
    {
        get
        {
            if (Page.Master == null)
            {
                HtmlForm form = (HtmlForm) Page.FindControl("form1");
                if (form != null)
                {
                    PlaceHolder placeHolder = (PlaceHolder)form.FindControl("PlaceHolder1");
                    if (placeHolder != null)
                    {
                        return (UserControl)placeHolder.FindControl(ContentControlName + "ID");
                    }
                }
            }
            else
            {
                HtmlForm form = (HtmlForm) Page.Master.FindControl("form1");
                if (form != null)
                {
                    ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)form.FindControl("cphWithoutScroll");
                    if (contentPlaceHolder != null)
                    {
                        return (UserControl) contentPlaceHolder.FindControl(((ProjectPageBase) Page).ContentControlName
                                                                            + "ID");
                    }

                }
            }
            return null;
        }
    }

    private string ContentControlName
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
}
