using System;
using VikkiSoft_BLL;
using Telerik.Web.UI;

public partial class SettingsEdit : EditControlBase
{
    public SettingsEdit()
    {
        this.m_Name = "налаштування";
        this.AllowUserTypes = "LoggedUser";
        BackURL = "";
    }

    protected override Type GetEditableEntityType()
    {
        return typeof(Settings);
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        if(IsNew)
        {
            text_Keywords.Focus();
        }
    }

    protected override void RedirectBackToList()
    {
        Response.Redirect("Office.aspx");
    }
}