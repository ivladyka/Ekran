using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for MasterPageBase
/// </summary>
public class MasterPageBase : System.Web.UI.MasterPage
{
    public string PageName
    {
        set
        {
            Label lbPageName = (Label) Page.Master.FindControl("lbPageName");
            if (lbPageName != null)
            {
                lbPageName.Text = value;
            }
        }
    }

    public MenuItemCollection TopMenuItemsCollection
    {
        get
        {
            Control topMenu = (Control) Page.Master.FindControl("TopMenu");
            if (topMenu != null)
            {
                Menu mnTop = (Menu)topMenu.FindControl("mnTop");
                if (mnTop != null)
                {
                    return mnTop.Items;
                }
            }
            return null;
        }
    }

    public Menu TopMenuControl
    {
        get
        {
            Control topMenu = (Control)Page.Master.FindControl("TopMenu");
            if (topMenu != null)
            {
                Menu mnTop = (Menu)topMenu.FindControl("mnTop");
                if (mnTop != null)
                {
                    return mnTop;
                }
            }
            return null;
        }
    }
}
