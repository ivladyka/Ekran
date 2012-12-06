using System;
using System.IO;
using Telerik.Web.UI;
using VikkiSoft_BLL;
using System.Collections;
using System.Web.UI;

public partial class GalleryEdit : EditControlBase
{
    public GalleryEdit()
    {
        this.m_Name = "Фотографію";
        this.AllowUserTypes = "LoggedUser";
        BackURL = "";
    }

    protected override string[] GetPrimaryKeys()
    {
        return new string[] { "GalleryID" };
    }

    protected override Type GetEditableEntityType()
    {
        return typeof(Gallery);
    }

    protected override void OnSave()
    {
        if (IsNew)
        {
            string targetFolder = Server.MapPath(Utils.GaleryImagePath);
            foreach (UploadedFile af in auFile.UploadedFiles)
            {
                string newGUID = Guid.NewGuid().ToString();
                string newFileName = newGUID + ".jpg";
                string path = Path.Combine(targetFolder, newFileName);
                af.SaveAs(path, true);
                Gallery g = new Gallery();
                g.AddNew();
                g.CategoryID = CategoryID;
                g.PhotoName = newFileName;
                g.IsCover = false;
                g.ShowCommon = false;
                g.Save();
                try
                {
                    System.IO.FileStream fs = System.IO.File.OpenRead(Path.Combine(targetFolder, newFileName));
                    byte[] b = new byte[fs.Length];
                    fs.Read(b, 0, b.Length);
                    newFileName = newGUID + "_s.jpg";
                    Utils.ResizeAndSaveJpgImage(b, 2000, 150, Path.Combine(targetFolder, newFileName), false);
                }
                catch { }
            }
            Response.Redirect("Office.aspx?content=CategoryEdit&CategoryID=" + CategoryID);
        }
        else
        {
            base.OnSave();
        }
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        if (IsNew)
        {
            btnUpdate.Attributes["onclick"] = "return VIKKI_CheckUploadedFiles();";
            auFile.TemporaryFolder = Utils.GaleryImagePath + "/RadUploadTemp";
            trShowCommon.Visible = false;
            trCover.Visible = false;
        }
        else
        {
            trUploadImages.Visible = false;
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

    public override EditableEntity EditableEntity
    {
        get
        {
            return UserSession.GetObjectKey(UserSession.EDIT_CONTROL_EDITABLE_ENTITY
                + "GalleryEdit") as EditableEntity;
        }
        set
        {
            UserSession.SetObjectKey(UserSession.EDIT_CONTROL_EDITABLE_ENTITY
                + "GalleryEdit", value);
        }
    }

    protected override void ListEditableControls(Control parent, ArrayList list)
    {
        base.ListEditableControls(parent, list);
        list.Add((Control)chk_IsCover);
        list.Add((Control)chk_ShowCommon);
    }

    protected override void WriteDataToEntity()
    {
        base.WriteDataToEntity();
        if (chk_IsCover.Checked)
        {
            Gallery g = new Gallery();
            g.UpdateCoverByCategoryID(CategoryID);
        }
    }
}
