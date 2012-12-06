using System;
using System.Web;
using System.Web.Security;

/// <summary>
/// Summary description for UserSession
/// </summary>
public class UserSession
{
    #region Cookies Constants
    public const string EDIT_CONTROL_EDITABLE_ENTITY = "EDITABLE_ENTITY";
    public const string EDIT_CONTROL_BASE_URI_REFERRER = "EDIT_CONTROL_BASE_URI_REFERRER";

    #endregion Cookies Constants

    #region Constructor
    public UserSession()
    {
    }
    #endregion Constructor

    #region Base Cookies Methods

    public static void SetKey(string Key, string Value)
    {
        HttpContext.Current.Session[Key] = Value;
    }

    public static string GetKey(string Key)
    {
        return HttpContext.Current.Session[Key] as string;
    }

    public static void SetObjectKey(string Key, object Value)
    {
        HttpContext.Current.Session[Key] = Value;
    }

    public static object GetObjectKey(string Key)
    {
        return HttpContext.Current.Session[Key];
    }

    public static void ClearKey(string Key)
    {
        HttpContext.Current.Session.Remove(Key);
    }

    public static string GetKeyAndRedirectIfNull(string Key)
    {
        string valueKey = "";
        if (HttpContext.Current.Session[Key] == null)
        {
            HttpContext.Current.Response.Redirect("Default.aspx");
        }
        else
        {
            valueKey = HttpContext.Current.Session[Key] as string;
        }
        return valueKey;
    }
    #endregion Base Cookies Methods


    #region Login\LogOff
    public static bool IsLoggined(string userName)
    {
        try
        {
            string value = HttpContext.Current.User.Identity.Name; ;
            return (value == userName);
        }
        catch
        {
            return false;
        }
    }

    public static int UserID
    {
        get
        {
            if (HttpContext.Current.Session["VS_FX_LVIDDecrypted"] == null)
            {
                SetLoginSession();
            }
            else
            {
                if (HttpContext.Current.Request.Cookies["VS_FX_LVID"] == null)
                {
                    SetLoginSession();
                }
                else
                {
                    if (HttpContext.Current.Session["VS_FX_LVIDDecrypted"].ToString() !=
                        Microsoft.JScript.GlobalObject.unescape(HttpContext.Current.Request.Cookies["VS_FX_LVID"].Value))
                    {
                        SetVS_FX_LVIDDecrypted();
                    }
                }
            }
            int userID = 0;
            try
            {
                userID = int.Parse(HttpContext.Current.Session["VS_FX_LVIDDecrypted"].ToString());
            }
            catch
            {
                //FormsAuthentication.SignOut();
            }
            
            return userID; 
        }
    }

    private static void SetLoginSession()
    {
        if (HttpContext.Current.Request.Cookies["VS_FX_LVID"] == null)
        {
            HttpCookie VS_FX_LVIDCookie = new HttpCookie("VS_FX_LVID",
                Encrypt.EncryptRijndael(Microsoft.JScript.GlobalObject.escape(HttpContext.Current.User.Identity.Name)));
            HttpContext.Current.Response.Cookies.Add(VS_FX_LVIDCookie);
        }
        SetVS_FX_LVIDDecrypted();
    }

    private static void SetVS_FX_LVIDDecrypted()
    {
        try
        {
            HttpContext.Current.Session["VS_FX_LVIDDecrypted"] = Encrypt.DecryptRijndael(Microsoft.JScript.GlobalObject.unescape(HttpContext.Current.Request.Cookies["VS_FX_LVID"].Value));
        }
        catch
        {
            HttpContext.Current.Session["VS_FX_LVIDDecrypted"] = Encrypt.DecryptRijndael(HttpContext.Current.Request.Cookies["VS_FX_LVID"].Value);
        }
    }
    #endregion Login\LogOff
}
