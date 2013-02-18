using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VikkiSoft_BLL;

public partial class GalleryList : ListControlBase, Interfaces.IColouredGrid
{
    public GalleryList()
    {
        this.m_Name = "Фотографії";
        this.AllowUserTypes = "LoggedUser";
    }

    protected override string GetEditableControlName()
    {
        return "GalleryEdit";
    }

    protected override Type GetEditableEntityType()
    {
        return typeof(Gallery);
    }

    protected override string[] GetPrimaryKeys()
    {
        return new string[] { "GalleryID" };
    }

    public override void InitGrid()
    {
        base.InitGrid();
        this.editableGrid.UrlToAdd += "&CategoryID=" + CategoryID;
        this.editableGrid.GridMode = GridModes.Add | GridModes.Delete | GridModes.Edit
            | GridModes.Refresh;
        editableGrid.Width = 730;
        SetColumnSettings(Gallery.ColumnNames.GalleryID, false, Gallery.ColumnNames.GalleryID,
            0, HorizontalAlign.Center, "");
        SetColumnSettings(Gallery.ColumnNames.CategoryID, false, Gallery.ColumnNames.CategoryID,
            0, HorizontalAlign.Center, "");
        SetColumnSettings(Gallery.ColumnNames.CategoryID, false, Gallery.ColumnNames.CategoryID,
            0, HorizontalAlign.Center, "");
        SetColumnSettings(Gallery.ColumnNames.PhotoName, true, "Фотографії",
           0, HorizontalAlign.Center, "");
        SetColumnSettings(Gallery.ColumnNames.IsCover, true, "Обкладинка", 0, HorizontalAlign.Center, "");
        SetColumnSettings(Gallery.ColumnNames.ShowCommon, true, "Відображати в розділі \"Галерея\"", 0, HorizontalAlign.Center, "");
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ModaldialogHeight = 220;
        ModaldialogWidth = 650;
        EditURLAdditionalParameters = "&CategoryID=" + CategoryID;
    }

    protected override void OnEditableGridItemDataBound(object sender, GridItemEventArgs e)
    {
        base.OnEditableGridItemDataBound(sender, e);
        if (e.Item is GridDataItem)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                DataRowView dataRowView = e.Item.DataItem as DataRowView;
                if (dataRowView != null)
                {
                    Image i = new Image();
                    i.ImageUrl = Path.Combine(Utils.GaleryImagePath, 
                        dataRowView["PhotoName"].ToString().Substring(0, dataRowView["PhotoName"].ToString().Length-4) + "_s.jpg");
                    e.Item.Cells[5].Controls.Add(i);

                    HyperLink lnkEdit = e.Item.Cells[2].FindControl("lnkEdit") as HyperLink;
                    if (lnkEdit != null)
                    {
                        lnkEdit.Attributes["onclick"] = "return Edit" + this.ClientID
                                + "ModalWindow('../ModalDialog.aspx?content="
                                + this.GetEditableControlName() + "&GalleryID=" + dataRowView["GalleryID"].ToString()
                                + EditURLAdditionalParameters + "');";
                    }
                }
            }
        }
        if (e.Item is Telerik.Web.UI.GridCommandItem)
        {
            LinkButton btnRedindGrid = (LinkButton)e.Item.FindControl("btnRedindGrid");
            if (btnRedindGrid != null)
            {
                string controlClientID = this.ClientID;
                string jscript = "<script language='javascript'>\n "
                                 + "function Rebind" + controlClientID + "Grid()\n"
                                 + "{\n"
                                 + "VIKKI_ClickButtonByClientID('" + editableGrid.RefreshEditableGridClientID + "');\n"
                                 + "return false;\n"
                                 + "}\n"
                                 + "function Edit" + controlClientID + "ModalWindow(navigateurl)\n"
                                 + "{\n"
                                 + "var retVal = window.showModalDialog(navigateurl, '', 'dialogWidth:"
                                 + ModaldialogWidth + "px; dialogHeight:"
                                 + ModaldialogHeight +
                                 "px; resizable:yes; status:no; scroll:yes; help:no; edge: sunken;');\n";
                jscript += "if(" + ScriptConditionAfterCloseModalDialog + " || retVal == null)\n"
                           + "{\n"
                           + "Rebind" + controlClientID + "Grid();\n";
                if (ScriptAfterCloseModalDialog != "")
                {
                    jscript += ScriptAfterCloseModalDialog;
                }
                jscript += "}\n"
                           + "return false;\n"
                           + "}\n"
                           + "</script>";
                if (!Page.IsStartupScriptRegistered("Rebind" + controlClientID + "GridScript"))
                {
                    Page.RegisterStartupScript("Rebind" + controlClientID + "GridScript", jscript);
                }
            }
        }
    }

    protected override void OnEditableGridDelete(object sender, EditableGridDeleteEventArgs e)
    {
        base.OnEditableGridDelete(sender, e);
        GridEditableItem gei = e.DeletedItem;
        string targetFolder = Server.MapPath(Utils.GaleryImagePath);
        Utils.DeleteFile(targetFolder, gei["PhotoName"].Text);
        Utils.DeleteFile(targetFolder, gei["PhotoName"].Text.Substring(0, gei["PhotoName"].Text.Length-4) + "_s.jpg");
    }

    protected override DataTable GetDataSource()
    {
        Gallery g = new Gallery();
        g.LoadByCategoryID(CategoryID);
        return g.DefaultView.Table;
    }

    private int CategoryID
    {
        get
        {
            if(Request.Params["CategoryID"] != null)
            {
                return int.Parse(Request.Params["CategoryID"]);
            }
            return 0;
        }
    }

    protected override void OnEditableGridRowsDragDrop(object sender, GridDragDropEventArgs e)
    {
        base.OnEditableGridRowsDragDrop(sender, e);
        if (e.DestDataItem != null)
        {
            int galleryIDDest = 0;
            int.TryParse(e.DestDataItem["GalleryID"].Text, out galleryIDDest);
            if (galleryIDDest > 0)
            {
                foreach (GridDataItem item in e.DraggedItems)
                {
                    try
                    {
                        int galleryIDDrag = int.Parse(item["GalleryID"].Text);
                        Gallery g = new Gallery();
                        g.Move(CategoryID, galleryIDDrag, galleryIDDest);
                        galleryIDDest = galleryIDDrag;
                    }
                    catch { }
                }
            }
        }
        this.RebindGrid();
    }

    #region IColouredGrid Members

    public Interfaces.GridColorSchemes GridColorScheme
    {
        get
        {
            return Interfaces.GridColorSchemes.Blue;
        }
    }

    #endregion
}
