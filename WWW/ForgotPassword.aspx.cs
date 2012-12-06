using System;
using System.Drawing;
using System.Net.Mail;
using VikkiSoft_BLL;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSendPassword_Click(object sender, EventArgs e)
    {
        lbNotFound.Visible = true;
        Settings s = new Settings();
        s.Where.SettingID.Value = 1;
        if (s.Query.Load())
        {
            try
            {
                //Send E-mail
                Utils.SendEmail("Пароль Адміністратора готелю Екран", "Ім'я користувача: " + s.s_UserName
                    + "   Ваш пароль: " + Encrypt.DecryptRijndael(s.s_Password));
                trPasswordSend1.Visible = trPasswordSend2.Visible = trPasswordSend3.Visible = false;
                trPasswordSent.Visible = true;
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
            }
            return;
        }
    }

    private void ShowMessage(string message, Color color)
    {
        lbNotFound.Text = message;
        lbNotFound.ForeColor = color;
    }
}