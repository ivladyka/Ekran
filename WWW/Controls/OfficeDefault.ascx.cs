using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OfficeDefault : ControlBase
{
    public OfficeDefault()
    {
        this.m_Name = "Адміністрування Екран";
        this.AllowUserTypes = "LoggedUser";
        BackURL = "";
    }
}