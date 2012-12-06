using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VikkiSoft_BLL;

public partial class RoomEdit : EditControlBase
{
    public RoomEdit()
    {
        this.m_Name = "Номер";
        this.AllowUserTypes = "LoggedUser";
    }
    protected override Type GetEditableEntityType()
    {
        return typeof(Room);
    }

    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        text_Number.Focus();
    }
}