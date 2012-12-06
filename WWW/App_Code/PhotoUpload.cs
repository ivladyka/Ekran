using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for PhotoUpload
/// </summary>
public class PhotoUpload : ControlBase
{
    private RadAsyncUpload m_auFile;
    private Image m_imgPhoto;
    private ImageButton m_imgDelete;
    private HtmlInputHidden m_hdPhotoNameDeleted;
    private RadWindow m_radWindow;
    private bool m_CreateThumbnail = true;
    private Label m_lblAllowedExtensions;

    protected RadAsyncUpload auFile
    {
        get
        {
            if (m_auFile != null) return m_auFile;
            Control c = this.FindControl("auFile");
            if (c != null)
            {
                if (c is RadAsyncUpload)
                {
                    m_auFile = (RadAsyncUpload)c;
                    return m_auFile;
                }
            }
            return null;
        }
    }

    protected Image imgPhoto
    {
        get
        {
            if (m_imgPhoto != null) return m_imgPhoto;
            Control c = this.FindControl("imgPhoto");
            if (c != null)
            {
                if (c is Image)
                {
                    m_imgPhoto = (Image)c;
                    return m_imgPhoto;
                }
            }
            return null;
        }
    }

    protected ImageButton imgDelete
    {
        get
        {
            if (m_imgDelete != null) return m_imgDelete;
            Control c = this.FindControl("imgDelete");
            if (c != null)
            {
                if (c is ImageButton)
                {
                    m_imgDelete = (ImageButton)c;
                    return m_imgDelete;
                }
            }
            return null;
        }
    }

    protected HtmlInputHidden hdPhotoNameDeleted
    {
        get
        {
            if (m_hdPhotoNameDeleted != null) return m_hdPhotoNameDeleted;
            Control c = this.FindControl("hdPhotoNameDeleted");
            if (c != null)
            {
                if (c is HtmlInputHidden)
                {
                    m_hdPhotoNameDeleted = (HtmlInputHidden)c;
                    return m_hdPhotoNameDeleted;
                }
            }
            return null;
        }
    }

    protected RadWindow radWindow
    {
        get
        {
            if (m_radWindow != null) return m_radWindow;
            Control c = this.FindControl("radWindow");
            if (c != null)
            {
                if (c is RadWindow)
                {
                    m_radWindow = (RadWindow)c;
                    return m_radWindow;
                }
            }
            return null;
        }
    }

    protected Label lblAllowedExtensions
    {
        get
        {
            if (m_lblAllowedExtensions != null) return m_lblAllowedExtensions;
            Control c = this.FindControl("lblAllowedExtensions");
            if (c != null)
            {
                if (c is Label)
                {
                    m_lblAllowedExtensions = (Label)c;
                    return m_lblAllowedExtensions;
                }
            }
            return null;
        }
    }

    public string PhotoName
    {
        get
        {
            if (IsPhotoDeleted)
            {
                return "";
            }
            if (this.ViewState["VIKKI_UPLOAD_PHOTO_NAME" + this.ClientID] != null)
            {
                return this.ViewState["VIKKI_UPLOAD_PHOTO_NAME" + this.ClientID].ToString();
            }
            return "";
        }

        set
        {
            if(value != null)
            {
                if(value != "")
                {
                    this.ViewState["VIKKI_UPLOAD_PHOTO_NAME" + this.ClientID] = value;
                    string imageName = value;
                    if(this.CreateThumbnail)
                    {
                        string extension = value.Substring(value.Length - 4, 4);
                        imageName = value.Substring(0, value.Length - 4) + "_s" + extension;
                    }
                    imgPhoto.ImageUrl = Path.Combine(Utils.GaleryImagePath, imageName);
                    imgDelete.Visible = true;
                    imgPhoto.Attributes["onclick"] = "return VIKKI_ShowImageViewWindowPhotoUpload('0', '" 
                        + value + "', '" + radWindow.ClientID + "');";
                    return;
                }
            }
            imgDelete.Visible = false;
            imgPhoto.ImageUrl = Path.Combine(Utils.GaleryImagePath, "nophoto.jpg");
        }
    }

    public bool IsPhotoDeleted
    {
        get
        {
            if (hdPhotoNameDeleted.Value != "")
            {
                return true;
            }
            return false;
        }
    }

    public void DeletePhoto()
    {
        if (this.ViewState["VIKKI_UPLOAD_PHOTO_NAME" + this.ClientID] != null)
        {
            string photoName = this.ViewState["VIKKI_UPLOAD_PHOTO_NAME" + this.ClientID].ToString();
            string targetFolder = Server.MapPath(Utils.GaleryImagePath);
            Utils.DeleteFile(targetFolder, photoName);
            string extension = photoName.Substring(photoName.Length - 4, 4);
            string thumbnailName = photoName.Substring(0, photoName.Length - 4) + "_s" + extension;
            Utils.DeleteFile(targetFolder, thumbnailName);
        }
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        auFile.TemporaryFolder = Utils.GaleryImagePath + "/RadUploadTemp";
        imgDelete.Attributes["onclick"] = "return VIKKI_DeletePhoto('" + imgPhoto.ClientID
            + "', '" + hdPhotoNameDeleted.ClientID + "', '" + imgDelete.ClientID + "');";
        imgPhoto.ImageUrl = Path.Combine(Utils.GaleryImagePath, "nophoto.jpg");
    }

    public string AllowedFileExtensions
    {
        set
        {
            auFile.AllowedFileExtensions = value.ToString().Split(',');
            lblAllowedExtensions.Text = "<div style='color:#a1a1a1;'>Виберіть зображення для завантаження (" + value + ")</div>";
        }
    }

    public PhotoUpload()
    {
        
    }

    public bool CreateThumbnail
    {
        get
        {
            return m_CreateThumbnail;
        }
        set
        {
            m_CreateThumbnail = value;
        }
    }
}
