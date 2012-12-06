using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VikkiSoft_BLL;

public partial class RoomCategoryList : ListControlBase, Interfaces.IColouredGrid
{
    public RoomCategoryList()
	{
        this.m_Name = "Категорія номера";
        this.AllowUserTypes = "LoggedUser";
        BackURL = "~/Office/Office.aspx";
	}

	protected override string GetEditableControlName()
	{
        return "RoomCategoryEdit";
	}

	protected override Type GetEditableEntityType()
	{
        return typeof(RoomCategory);
	}

	protected override string[] GetPrimaryKeys()
	{
        return new string[] { "RoomCategoryID" };
    }

	public override void InitGrid()
	{
		base.InitGrid ();
		this.editableGrid.GridMode = GridModes.Add | GridModes.Delete
			| GridModes.Edit | GridModes.Refresh;				
		editableGrid.Width = 500;
        SetColumnSettings(RoomCategory.ColumnNames.RoomCategoryID, false, RoomCategory.ColumnNames.RoomCategoryID,
				0, HorizontalAlign.Center, "");
        SetColumnSettings(RoomCategory.ColumnNames.Name, true, "Назва", 0, HorizontalAlign.Center, "");
        SetColumnSettings(RoomCategory.ColumnNames.Name_pl, false, "", 0, HorizontalAlign.Center, "");
        SetColumnSettings(RoomCategory.ColumnNames.Name_en, false, "", 0, HorizontalAlign.Center, "");
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