using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VikkiSoft_BLL;


public partial class ChangePassword : ControlBase
{
    public ChangePassword()
    {
        this.m_Name = "Змінити пароль";
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        tbOldPassword.Focus();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lbError.Visible = false;
        Settings s = new Settings();
        s.Where.SettingID.Value = 1;
        if (s.Query.Load())
        {
            if (s.s_Password != Encrypt.EncryptRijndael(tbOldPassword.Text))
            {
                lbError.Visible = true;
                return;
            }
            s.Password = Encrypt.EncryptRijndael(text_Password.Text);
            s.Save();
        }
        btnCancel_Click(sender, e);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Office/Office.aspx");
    }
}