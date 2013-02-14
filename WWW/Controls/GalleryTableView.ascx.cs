using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VikkiSoft_BLL;

public partial class GalleryTableView : ControlBase
{

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        LoadTitle();
    }

    private void LoadTitle()
    {
        Category c = new Category();
        if (c.LoadByPrimaryKey(CategoryID))
        {
            string title = "";
            if (!c.IsColumnNull("Title" + Utils.LangPrefix))
            {
                title = c.GetColumn("Title" + Utils.LangPrefix).ToString();
            }
            if (title.Trim() == "")
            {
                title = c.GetColumn("Name" + Utils.LangPrefix).ToString();
            }
            this.m_Name = title;
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
            if (Page.RouteData.Values["CategoryID"] != null)
            {
                return int.Parse(Page.RouteData.Values["CategoryID"].ToString());
            }
            return 0;
        }
    }

    private static HtmlTableCell AddTableCell(string innerHtml)
    {
        HtmlTableCell cell = new HtmlTableCell();
        cell.InnerHtml = innerHtml;
        cell.Align = "center";
        return cell;
    }
}
