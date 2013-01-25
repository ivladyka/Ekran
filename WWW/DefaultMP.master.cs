using System;
using System.Web.UI.HtmlControls;
using System.Threading;
using Telerik.Web.UI;
using VikkiSoft_BLL;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class DefaultMP : MasterPageBase
{
    string m_Keywords = "";
    string m_Description = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string culture = "uk-UA";
        if (Thread.CurrentThread.CurrentCulture != null)
        {
            culture = Thread.CurrentThread.CurrentCulture.Name;
        }
        switch (culture)
        {
            case "en-US":
                ddLanguage.SelectedValue = "ENG";
                break;
            case "pl-PL":
                ddLanguage.SelectedValue = "PL";
                break;
            case "uk-UA":
                ddLanguage.SelectedValue = "UA";
                break;
        }
        if (!Page.IsPostBack)
        {
            LoadCommonPageData();
        }
    }

    protected void ddLanguage_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    public string Copyright
    {
        get
        {
            return Resources.Vikkisoft.Copyright;
        }
    }

    public string ImageList
    {
        get
        {
            string imageList = "";
            Gallery g = new Gallery();
            bool isDataLoaded = false;
            if (CategoryID == 3)
            {
                isDataLoaded = g.LoadGallery();
            }
            if (!isDataLoaded)
            {
                isDataLoaded = g.LoadByCategoryID(CategoryID);
            }
            if (!isDataLoaded)
            {
                isDataLoaded = g.LoadByCategoryID(1);
            }
            if(isDataLoaded)
            {
                do
                {
                    imageList += "{image: '" + Utils.GaleryImagePath.Replace("~/", "") + "/" + g.s_PhotoName + "' },";
                } while (g.MoveNext());
            }
            imageList = imageList.TrimEnd(',');
            if (imageList.TrimEnd() == "")
            {
                imageList = "{image: 'images/1.jpg' },{ image: 'images/2.jpg' },{ image: 'images/3.jpg' }";
            }
            return imageList;
        }
    }

    private int CategoryID
    {
        get
        {
            if (Request.Params["CategoryID"] != null)
            {
                return int.Parse(Request.Params["CategoryID"]);
            }
            return 1;
        }
    }

    public string MetaDescription
    {
        get
        {
            return "<meta name='description' content=\"" + m_Description + "\" />";
        }
    }

    public string MetaKeywords
    {
        get
        {
            return "<meta name='keywords' content=\"" + m_Keywords + "\" />";
        }
    }

    private void LoadCommonPageData()
    {
        Settings s = new Settings();
        if (s.LoadByPrimaryKey(1))
        {
            Category c = new Category();
            if (c.LoadByPrimaryKey(CategoryID))
            {
                if (!c.IsColumnNull("Keywords" + Utils.LangPrefix))
                {
                    m_Keywords = c.GetColumn("Keywords" + Utils.LangPrefix).ToString();
                }
                if (!c.IsColumnNull("Description" + Utils.LangPrefix))
                {
                    m_Description = c.GetColumn("Description" + Utils.LangPrefix).ToString();
                }
            }
            if (m_Keywords.TrimEnd().Length == 0 && !s.IsColumnNull("Keywords" + Utils.LangPrefix))
            {
                m_Keywords = s.GetColumn("Keywords" + Utils.LangPrefix).ToString();
            }
            if (m_Description.TrimEnd().Length == 0 && !s.IsColumnNull("Description" + Utils.LangPrefix))
            {
                m_Description = s.GetColumn("Description" + Utils.LangPrefix).ToString();
            }
            if (m_Keywords.TrimEnd().Length == 0)
            {
                m_Keywords = Resources.Vikkisoft.MetaKeywords;
            }
            if (m_Description.TrimEnd().Length == 0)
            {
                m_Description = Resources.Vikkisoft.MetaDescription;
            }
        }
    }
}
