using System;
using System.Web;
using System.Web.Security;
using VikkiSoft_BLL;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.IsAuthenticated)
        {
            string s = Request.Params["ReturnUrl"];
            s = GetReturnUrl(s);
            Response.Redirect(s);
        }
        if (!Page.IsPostBack)
        {
           
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (string.Empty != tbUserName.Text && string.Empty != tbPassword.Text)
        {
            VikkiSoft_BLL.Settings s = new Settings();
            s.Where.UserName.Value = tbUserName.Text;
            s.Where.Password.Value = Encrypt.EncryptRijndael(tbPassword.Text);
            s.Where.SettingID.Value = 1;
            if (s.Query.Load())
            {
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(s.UserName.ToString(), chkKeepInSystem.Checked);
                Response.Cookies.Add(authCookie);

                string str = FormsAuthentication.GetRedirectUrl(s.UserName.ToString(), chkKeepInSystem.Checked);
                str = GetReturnUrl(str);

                string jscript = "<script language='javascript'>\n "
                    + "VIKKI_RedirectFromLoginPage('" + str
                    + "', '" + Encrypt.EncryptRijndael(s.UserName.ToString()) + "');\n"
                    + "</script>";
                if (!Page.IsStartupScriptRegistered("RedirectFromLoginPageScript"))
                {
                    Page.RegisterStartupScript("RedirectFromLoginPageScript", jscript);
                }
            }
            else
            {
                lbNotFound.Visible = true;
            }
        }
    }

    private string GetReturnUrl(string ReturnUrl)
    {
        if (ReturnUrl == null || ReturnUrl.ToLower().IndexOf("default.aspx") >= 0)
        {
            ReturnUrl = "Office/Office.aspx";
        }
        return ReturnUrl;
    }

    protected override void InitializeCulture()
    {
        Utils.InitCulture();
        base.InitializeCulture();
    }
}
