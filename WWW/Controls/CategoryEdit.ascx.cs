using System;
using VikkiSoft_BLL;
using Telerik.Web.UI;

public partial class CategoryEdit : EditControlBase
{
    public CategoryEdit()
    {
        this.m_Name = "сторінку";
        this.AllowUserTypes = "LoggedUser";
        BackURL = "";
    }

    protected override Type GetEditableEntityType()
    {
        return typeof(Category);
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        if(IsNew)
        {
            text_Name.Focus();
            rtsCategory.Visible = false;
        }
    }

    protected override void RedirectBackToList()
    {
        Response.Redirect("Office.aspx");
    }

    protected void rtsCategory1_TabClick(object sender, RadTabStripEventArgs e)
    {
        switch (e.Tab.Text)
        {
            case "Інформація":
                pnlCategoryEdit.Visible = true;
                pnlGalleryList.Visible = false;
                break;
            case "Фотографії":
                pnlCategoryEdit.Visible = false;
                pnlGalleryList.Visible = true;
                GalleryList.RebindGrid();
                break;
        }
    }

    protected override void InitializeComponent()
    {
        base.InitializeComponent();
        SetPageName();
    }

    protected virtual void ReadDataFromEntity()
    {
        base.ReadDataFromEntity();
        SetPageName();
    }

    private void SetPageName()
    {
        Category c = new Category();
        c.Where.CategoryID.Value = CategoryID;
        if (c.Query.Load())
        {
            this.m_Name = c.GetColumn("Name").ToString();
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
            return 1;
        }
    }

    protected override void WriteDataToEntity()
    {
        base.WriteDataToEntity();
        VikkiSoft_BLL.Category u = (VikkiSoft_BLL.Category)this.EditableEntity;
        if (choice_RoomCategoryID.SelectedText == "")
        {
            u.SetColumnNull(VikkiSoft_BLL.Category.ColumnNames.RoomCategoryID);
        }

    }

}
