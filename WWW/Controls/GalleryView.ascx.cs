using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VikkiSoft_BLL;

public partial class GalleryView : ControlBase
{
    public GalleryView()
    {
        BackURL = "~/Default.aspx?content=GalleryTableList";
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        LoadGallery();
        LoadTitle();
    }

    private void LoadGallery()
    {
        Gallery g = new Gallery();
        g.Where.CategoryID.Value = CategoryID;
        if (g.Query.Load())
        {
            HtmlTableRow row = new HtmlTableRow();
            do
            {
                Image i = new Image();
                i.ImageUrl = Path.Combine(Utils.GaleryImagePath, g.PhotoName);
                gallery.Controls.Add(i);
            } 
            while (g.MoveNext());
        }
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
            return 0;
        }
    }
}