using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VikkiSoft_BLL;

public partial class RoomList : ListControlBase, Interfaces.IColouredGrid
{
    public RoomList()
	{
        this.m_Name = "Номери";
        this.AllowUserTypes = "LoggedUser";
        BackURL = "~/Office/Office.aspx";
	}

	protected override string GetEditableControlName()
	{
        return "RoomEdit";
	}

	protected override Type GetEditableEntityType()
	{
        return typeof(Room);
	}

	protected override string[] GetPrimaryKeys()
	{
        return new string[] { "RoomID" };
    }

	public override void InitGrid()
	{
		base.InitGrid ();
		this.editableGrid.GridMode = GridModes.Add | GridModes.Delete
			| GridModes.Edit | GridModes.Refresh;				
		editableGrid.Width = 400;
        SetColumnSettings(Room.ColumnNames.RoomID, false, Room.ColumnNames.RoomID,
				0, HorizontalAlign.Center, "");
        SetColumnSettings(Room.ColumnNames.Number, true, "Номер", 0, HorizontalAlign.Center, "");
        SetColumnSettings("RoomCategoryName", true, "Категорія номеру", 0, HorizontalAlign.Center, "");
        SetColumnSettings("RoomCategoryName_en", false, "", 0, HorizontalAlign.Center, "");
        SetColumnSettings("RoomCategoryName_pl", false, "", 0, HorizontalAlign.Center, "");
        SetColumnSettings(Room.ColumnNames.Price, true, "Ціна", 0, HorizontalAlign.Center, "");
	}

    protected override DataTable GetDataSource()
    {
        Room r = new Room();
        r.LoadWithRoomCategoty();
        return r.DefaultView.Table;
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
                    decimal price;
                    decimal.TryParse(dataRowView[Room.ColumnNames.Price].ToString(), out price);
                    e.Item.Cells[5].Text = "";
                    if (price > 0)
                    {
                        e.Item.Cells[5].Text = price.ToString("F");
                    }
                }
            }
        }
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