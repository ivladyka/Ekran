using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Mail;
using MyGeneration.dOOdads;
using System.Web.UI.WebControls;
using VikkiSoft_BLL;
using System.Collections;
using System.Threading;
using System.Globalization;
using System.Web;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{
    public static void ShowMessage(System.Web.UI.Control control, string message)
    {
        if (!control.Page.IsClientScriptBlockRegistered(control.ID + "_ERROR_MESSAGE"))
        {
            message = message.Replace("\r", "\\r");
            message = message.Replace("\n", "\\n");
            message = message.Replace("'", "\\'");
            message = message.Replace("\"", "\\");
            control.Page.RegisterClientScriptBlock(control.ID + "_ERROR_MESSAGE", "<script>alert(\"" + message + "\")</script>");
        }
    }

    public static bool IsPagePostBack(System.Web.UI.Page page)
    {
        if (page.IsPostBack)
            return true;

        return IsPageCallBack(page);
    }

    public static bool IsPageCallBack(System.Web.UI.Page page)
    {
        if (page.Request.Params["rcbID"] != null)
            return true;
        return false;
    }

    public static bool IsPagePostBack(System.Web.UI.UserControl control)
    {
        return IsPagePostBack(control.Page);
    }

    public static bool IsEmptyId(string id)
    {
        if (id == null || id == "")
            return true;
        return false;
    }

    public static bool IsValueNull(object value)
    {
        if (value == null || value == DBNull.Value)
            return true;
        return false;
    }

    public static bool GetEntityValueBool(SqlClientEntity entity, string columnName)
    {
        bool entityValueBoll = false;
        if (!entity.IsColumnNull(columnName))
        {
            try
            {
                entityValueBoll = Convert.ToBoolean(entity.GetColumn(columnName).ToString());
            }
            catch { }
        }
        return entityValueBoll;
    }

    public static int GetEntityValueInt(SqlClientEntity entity, string columnName)
    {
        int entityValueInt = 0;
        if (!entity.IsColumnNull(columnName))
        {
            try
            {
                entityValueInt = int.Parse(entity.GetColumn(columnName).ToString());
            }
            catch { }
        }
        return entityValueInt;
    }

    public static decimal GetEntityValueDecimal(SqlClientEntity entity, string columnName)
    {
        decimal entityValueDecimal = 0;
        if (!entity.IsColumnNull(columnName))
        {
            try
            {
                entityValueDecimal = Convert.ToDecimal(entity.GetColumn(columnName).ToString());
            }
            catch { }
        }
        return entityValueDecimal;
    }

    public static void ResizeAndSaveJpgImage(byte[] Content, int MaxWidth, int MaxHeigh, string pathSave,
        bool checkLandscape)
    {
        MemoryStream m = new MemoryStream();
        m.Write(Content, 0, Content.Length);
        Bitmap bmp = new Bitmap(m);
        if (checkLandscape && bmp.Width > bmp.Height)
        {
            int height = MaxWidth;
            MaxWidth = MaxHeigh;
            MaxHeigh = height;
        }
        float K = System.Math.Max((float)bmp.Width / MaxWidth, (float)bmp.Height / MaxHeigh);
        Rectangle oRectangle = new Rectangle(0, 0, (int)(bmp.Width / K), (int)(bmp.Height / K));

        System.Drawing.Image oThumbNail = new Bitmap(oRectangle.Width, oRectangle.Height, bmp.PixelFormat);
        Graphics oGraphic = Graphics.FromImage(oThumbNail);
        oGraphic.CompositingQuality = CompositingQuality.HighQuality;
        oGraphic.SmoothingMode = SmoothingMode.HighQuality;
        oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
        oGraphic.DrawImage(bmp, oRectangle);

        oThumbNail.Save(pathSave, System.Drawing.Imaging.ImageFormat.Jpeg);
    }

    public static string GaleryImagePath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["GaleryImagePath"];
        }
    }

    public static void DeleteFile(string targetFolder, string fileName)
    {
        string filePath = Path.Combine(targetFolder, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public static string OrderImagePath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["OrderImagePath"];
        }
    }

    public static string OrderFTPPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["OrderFTPPath"];
        }
    }

    public static string FontPath
    {
        get
        {
            return System.Configuration.ConfigurationManager.AppSettings["FontPath"];
        }
    }

    public static void SendEmail(string subject, string body)
    {
        try
        {
            int portNumber;
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["SMTPServerPort"],
                         out portNumber);
            SmtpClient client;
            if (portNumber > 0)
            {
                client = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"], portNumber);
            }
            else
            {
                client = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
            }
            MailAddress from =
                new MailAddress(System.Configuration.ConfigurationManager.AppSettings["FromEmail"]);
            MailAddress to = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["ToEmail"]);
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.Subject = subject;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            if (System.Configuration.ConfigurationManager.AppSettings["EmailAccountPassword"].Trim() != "")
            {
                client.Credentials =
                    new System.Net.NetworkCredential(
                        System.Configuration.ConfigurationManager.AppSettings["FromEmail"],
                        System.Configuration.ConfigurationManager.AppSettings["EmailAccountPassword"]);
            }
            client.Send(message);
        }
        catch
        { }
    }

    public static void InitMenu(Menu menu, bool onlyTopLevel, bool ukr, bool adminMenu)
    {
        Hashtable catTitles = new Hashtable();
        Hashtable catTitlesEng = new Hashtable();
        Category cat = new Category();
        if (cat.LoadAll())
        {
            do
            {
                catTitles.Add(cat.CategoryID, cat.GetColumn("Name" + (ukr ? "" : LangPrefix)).ToString().ToUpper());
                catTitlesEng.Add(cat.CategoryID, cat.GetColumn("Name_en").ToString().ToUpper());
            } while (cat.MoveNext());
        }

        menu.Items.Clear();
        if (adminMenu)
        {
            menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 1, catTitles, catTitlesEng, adminMenu));
        }
        menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 2, catTitles, catTitlesEng, adminMenu));
        if (!onlyTopLevel)
        {
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 28, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 3, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 5, catTitles, catTitlesEng, adminMenu));
        }
        menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 7, catTitles, catTitlesEng, adminMenu));
        if (!onlyTopLevel)
        {
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 8, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 9, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 10, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 11, catTitles, catTitlesEng, adminMenu));
        }
        menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 12, catTitles, catTitlesEng, adminMenu));
        if (!onlyTopLevel)
        {
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 13, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 14, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 15, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 16, catTitles, catTitlesEng, adminMenu));
        }
        menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 18, catTitles, catTitlesEng, adminMenu));
        if (!onlyTopLevel)
        {
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 19, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 20, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 21, catTitles, catTitlesEng, adminMenu));
        }
        menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 22, catTitles, catTitlesEng, adminMenu));
        if (!onlyTopLevel)
        {

            menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 23, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 24, catTitles, catTitlesEng, adminMenu));
            menu.Items[menu.Items.Count - 1].ChildItems.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 25, catTitles, catTitlesEng, adminMenu));
        }
        else
        {
            menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 24, catTitles, catTitlesEng, adminMenu));
            menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 25, catTitles, catTitlesEng, adminMenu));
        }
        menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=CategoryEdit", 27, catTitles, catTitlesEng, adminMenu));
        if (adminMenu)
        {
            menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=RoomList", "НОМЕРИ"));
            menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=RoomCategoryList", "КАТЕГОРІЇ НОМЕРІВ"));
            menu.Items.Add(AddMenuItem("~/Office/Office.aspx?content=SettingsEdit&SettingID=1", "НАЛАШТУВАННЯ"));
        }
    }

    private static void AddMenuItem(MenuItemCollection items, string url, int categotyID, Hashtable catTitles, Hashtable catTitlesEng, bool adminMenu)
    {
        string title = "";
        string titleEn = "ekran";
        if (catTitles[categotyID] != null)
        {
            title = catTitles[categotyID].ToString();
            if (adminMenu)
            {
                items.Add(AddMenuItem(url + "&CategoryID=" + categotyID.ToString(), "", title, categotyID));
            }
            else
            {
                if (catTitlesEng[categotyID] != null)
                {
                    titleEn = catTitlesEng[categotyID].ToString();
                }
                items.Add(AddMenuItem(GenerateFriendlyURL(titleEn, categotyID.ToString()), "", title, categotyID));
            }
        }
    }

    private static MenuItem AddMenuItem(string url, int categotyID, Hashtable catTitles, Hashtable catTitlesEng, bool adminMenu)
    {
        string title = "";
        string titleEn = "ekran";
        if (catTitles[(categotyID == 0 ? 1 : categotyID)] != null)
        {
            title = catTitles[(categotyID == 0 ? 1 : categotyID)].ToString();
        }
        if (adminMenu)
        {
            return AddMenuItem(url + (categotyID > 0 && url.Length > 0 ? "&CategoryID=" + categotyID.ToString() : ""), "", title, categotyID);
        }
        if (catTitlesEng[categotyID] != null)
        {
            titleEn = catTitlesEng[categotyID].ToString();
        }
        return AddMenuItem(GenerateFriendlyURL(titleEn, categotyID.ToString()), "", title, categotyID);
    }

    private static MenuItem AddMenuItem(string url, string title)
    {
        return AddMenuItem(url, "", title, 0);
    }

    private static MenuItem AddMenuItem(string url, string title, string description, int categoryID)
    {
        MenuItem item = new MenuItem();
        item.Text = description;
        item.ToolTip = title;
        item.NavigateUrl = url;
        item.Value = categoryID.ToString();
        if (url.Length == 0)
        {
            item.Selectable = false;
        }   
        return item;
    }

    public static string LangPrefix
    {
        get
        {
            string culture = "uk-UA";
            if (Thread.CurrentThread.CurrentCulture != null)
            {
                culture = Thread.CurrentThread.CurrentCulture.Name;
            }
            string prefCulture = "";
            switch (culture)
            {
                case "en-US":
                    prefCulture = "_en";
                    break;
                case "pl-PL":
                    prefCulture = "_pl";
                    break;
            }
            return prefCulture;
        }
    }

    public static void InitCulture()
    {
        string culture = "uk-UA";
        if (HttpContext.Current.Request.Cookies["EKRAN_LV_UKR_LNG"] != null)
        {
            culture = HttpContext.Current.Request.Cookies["EKRAN_LV_UKR_LNG"].Value.ToString();
        }
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
    }

    public static string GenerateFriendlyURL(string title, string id)
    {
        string friendlyURL = "/hotel-" + id + "/";
        title = title.Trim();
        title = title.Trim('-');

        title = title.ToLower();
        char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
        title = title.Replace("c#", "C-Sharp");
        title = title.Replace("vb.net", "VB-Net");
        title = title.Replace("asp.net", "Asp-Net");

        title = title.Replace(".", "-");

        for (int i = 0; i < chars.Length; i++)
        {
            string strChar = chars.GetValue(i).ToString();
            if (title.Contains(strChar))
            {
                title = title.Replace(strChar, string.Empty);
            }
        }

        title = title.Replace(" ", "-");

        title = title.Replace("--", "-");
        title = title.Replace("---", "-");
        title = title.Replace("----", "-");
        title = title.Replace("-----", "-");
        title = title.Replace("----", "-");
        title = title.Replace("---", "-");
        title = title.Replace("--", "-");

        title = title.Trim();
        title = title.Trim('-');


        return friendlyURL + title;
    }

    public static string GenerateFriendlyURL(int id)
    {
        Category cat = new Category();
        if(cat.LoadByPrimaryKey(id))
        {
            return GenerateFriendlyURL(cat.s_Name_en, id.ToString());
        }
        return "Default.aspx";
    }
}
