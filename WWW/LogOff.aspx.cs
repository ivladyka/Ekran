using System;
using System.Web.Security;

public partial class Office_LogOff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.IsAuthenticated == true)
        {
            FormsAuthentication.SignOut();
        }
        Session.Remove("VS_FX_LVIDDecrypted");
        string jscript = "<script language='javascript'>\n "
                    + "VIKKI_RedirectToLoginPage();\n"
                    + "</script>";
        if (!Page.IsStartupScriptRegistered("RedirectToLoginPageScript"))
        {
            Page.RegisterStartupScript("RedirectToLoginPageScript", jscript);
        }
    }
}
