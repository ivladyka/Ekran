using Telerik.Web.UI;
using VikkiSoft_BLL;
using MyGeneration.dOOdads;

public partial class RoomCategoryChoice : ChoiceControlBase
{
    protected override RadComboBox ddlList
    {
        get { return ddlRoomCategory; }
    }

    protected override void InitDDL()
    {
        RoomCategory rc = new RoomCategory();
        rc.Query.AddOrderBy(RoomCategory.ColumnNames.Name, WhereParameter.Dir.ASC);
        if (rc.Query.Load())
        {
            do
            {
                RadComboBoxItem item = new RadComboBoxItem((!rc.IsColumnNull("Name" + Utils.LangPrefix) ? rc.GetColumn("Name" + Utils.LangPrefix).ToString() : ""),
                    rc.RoomCategoryID.ToString());
                this.ddlList.Items.Add(item);
            } while (rc.MoveNext());
        }
    }
}