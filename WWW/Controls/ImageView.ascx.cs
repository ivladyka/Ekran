
using System.IO;
using VikkiSoft_BLL;

public partial class ImageView : ControlBase
{
    public ImageView()
    {
        this.m_Name = "";
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        LoadPhoto();
        btnCancel.Attributes["onclick"] = "return VIKKI_CloseRADWindow();";
    }

    private void LoadPhoto()
    {
        if (CategoryID > 0)
        {
            LoadCategoryPhoto();
        }
        else
        {
            if (OrderID > 0)
            {
                imageHolder.ImageUrl = Path.Combine(Utils.OrderImagePath + "//" + OrderID, PhotoName);
            }
            else
            {
                imageHolder.ImageUrl = Path.Combine(Utils.GaleryImagePath, PhotoName);
            }
            ImageCount.Text = "";
            hlPrevLink.Visible = false;
            hlNextLink.Visible = false;
        }
    }

    private void LoadCategoryPhoto()
    {
        string photoName = "";
        int index = 1;
        int count = 0;
        bool photoFound = false;
        hlPrevLink.CommandArgument = "";
        hlNextLink.CommandArgument = "";
        Gallery g = new Gallery();
        g.Where.CategoryID.Value = CategoryID;
        if (g.Query.Load())
        {
            photoName = g.s_PhotoName;
            count = g.RowCount;
            do
            {
                if (this.PhotoName == g.s_PhotoName)
                {
                    photoName = g.s_PhotoName;
                    photoFound = true;
                    continue;
                }
                if (photoFound)
                {
                    hlNextLink.CommandArgument = g.s_PhotoName;
                    break;
                }
                else
                {
                    hlPrevLink.CommandArgument = g.s_PhotoName;
                    index++;
                }
            } while (g.MoveNext());
        }
        imageHolder.ImageUrl = Path.Combine(Utils.GaleryImagePath, photoName);
        ImageCount.Text = "Фотографія " + index + " з " + count;
        hdPhotoName.Value = photoName;
        if (index == 1)
        {
            hlPrevLink.Visible = false;
        }
        else
        {
            hlPrevLink.Visible = true;
        }
        if (index == g.RowCount)
        {
            hlNextLink.Visible = false;
        }
        else
        {
            hlNextLink.Visible = true;
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

    private string PhotoName
    {
        get
        {
            if (hdPhotoName.Value.Trim() != "")
            {
                return hdPhotoName.Value.Trim();
            }
            if (Request.Params["PhotoName"] != null)
            {
                return Request.Params["PhotoName"].ToString();
            }
            return "";
        }
    }

    protected void hlNextLink_Click(object sender, System.EventArgs e)
    {
        hdPhotoName.Value = hlNextLink.CommandArgument;
        LoadPhoto();
    }

    protected void hlPrevLink_Click(object sender, System.EventArgs e)
    {
        hdPhotoName.Value = hlPrevLink.CommandArgument;
        LoadPhoto();
    }

    private int OrderID
    {
        get
        {
            if (Request.Params["OrderID"] != null)
            {
                return int.Parse(Request.Params["OrderID"]);
            }
            return 0;
        }
    }
}
