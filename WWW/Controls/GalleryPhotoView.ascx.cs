using System.Web;
using VikkiSoft_BLL;

public partial class GalleryPhotoView : ControlBase
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
            this.m_Name = c.GetColumn("Name" + Utils.LangPrefix).ToString(); ;
            lblTitle.Text = this.m_Name;
        }
    }

    private int CategoryID
    {
        get
        {
            if (HttpContext.Current.Request.QueryString["CategoryID"] != null)
            {
                return int.Parse(HttpContext.Current.Request.QueryString["CategoryID"]);
            }
            if (Page.RouteData.Values["CategoryID"] != null)
            {
                return int.Parse(Page.RouteData.Values["CategoryID"].ToString());
            }
            return 0;
        }
    } 
}
