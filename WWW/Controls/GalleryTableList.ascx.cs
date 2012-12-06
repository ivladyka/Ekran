using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VikkiSoft_BLL;

public partial class GalleryTableList : ControlBase
{
    public GalleryTableList()
    {
        this.m_Name = "Галерея";
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        LoadCategories();
    }

    private void LoadCategories()
    {
        const int countCells = 2;
        HtmlTableRow row = new HtmlTableRow();
        Category c = new Category();
        //c.Where.IsGallery.Value = true;
        if(c.Query.Load())
        {
            do
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
                if (row.Cells.Count < countCells)
                {
                    row.Cells.Add(AddCategoryCellInnerHTML(c.CategoryID, title));
                }
                else
                {
                    tblCategory.Rows.Add(row);
                    row = new HtmlTableRow();
                    row.Cells.Add(AddCategoryCellInnerHTML(c.CategoryID, title));
                }
            } while (c.MoveNext());
        }
        for (int i = row.Cells.Count; i < countCells; i++)
        {
            row.Cells.Add(AddTableCell("&nbsp;"));
        }
        tblCategory.Rows.Add(row);
    }

    private static HtmlTableCell AddCategoryCellInnerHTML(int categoryID, string title)
    {
        HtmlTableCell cell = new HtmlTableCell();
        cell.Align = "center";
        Gallery g = new Gallery();
        g.Where.CategoryID.Value = categoryID;
        g.Query.Top = 1;
        if(g.Query.Load())
        {
            Image i = new Image();
            i.CssClass = "GaleryThImageStyle";
            i.ImageUrl = Path.Combine(Utils.GaleryImagePath, g.s_PhotoName.Substring(0, g.s_PhotoName.Length - 4) + ".jpg" );
            HyperLink hl = new HyperLink();
            hl.NavigateUrl = "../Default.aspx?content=GalleryTableView&CategoryID=" + categoryID;
            hl.CssClass = "GaleryImageStyle";
            hl.Controls.Add(i);
            Literal l = new Literal();
            l.Text = "<br><span class='galerytext'>" + title + "</span>";
            hl.Controls.Add(l);
            cell.Controls.Add(hl);

        }
        return cell;
    }

    private static HtmlTableCell AddTableCell(string innerHtml)
    {
        HtmlTableCell cell = new HtmlTableCell();
        cell.InnerHtml = innerHtml;
        cell.Align = "center";
        return cell;
    }
}
