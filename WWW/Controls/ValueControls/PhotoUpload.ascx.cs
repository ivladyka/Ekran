
using System;
using System.IO;
using Telerik.Web.UI;

public partial class Controls_ValueControls_PhotoUpload : PhotoUpload
{
    protected void auFile_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        string targetFolder = Server.MapPath(Utils.GaleryImagePath);
        foreach (UploadedFile af in auFile.UploadedFiles)
        {
            DeletePhoto();
            hdPhotoNameDeleted.Value = "";
            string newGUID = Guid.NewGuid().ToString();
            string extension = af.GetExtension();
            string newFileName = newGUID + extension;
            ViewState["VIKKI_UPLOAD_PHOTO_NAME" + this.ClientID] = newFileName;
            string path = Path.Combine(targetFolder, newFileName);
            af.SaveAs(path, true);
            if (CreateThumbnail)
            {
                try
                {
                    System.IO.FileStream fs = System.IO.File.OpenRead(Path.Combine(targetFolder, newFileName));
                    byte[] b = new byte[fs.Length];
                    fs.Read(b, 0, b.Length);
                    newFileName = newGUID + "_s" + extension;
                    Utils.ResizeAndSaveJpgImage(b, 181, 244, Path.Combine(targetFolder, newFileName), true);
                    fs.Dispose();
                }
                catch
                {
                }
            }
        }
    }
}
