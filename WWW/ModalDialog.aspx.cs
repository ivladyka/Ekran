using System;
using System.Web.UI;

public partial class ModalDialog : System.Web.UI.Page
{
    protected UserControl userControl;

    protected void Page_Load(object sender, EventArgs e)
    {
        userControl = (System.Web.UI.UserControl)Page.LoadControl("Controls/" + ContentControlName + ".ascx");
        userControl.ID = ContentControlName + "ID";
        if (PlaceHolder1 != null)
        {
            this.PlaceHolder1.Controls.Add(userControl);
        }
        if (userControl is Interfaces.INamedControl)
        {
            PageTitle = (userControl as Interfaces.INamedControl).Name;
        }
    }

    protected string ContentControlName
    {
        get
        {
            string contentControlName = "";
            if (Request.Params["content"] != null)
            {
                contentControlName = Request.Params["content"].ToString();
            }
            return contentControlName;
        }
    }

    public string HeadElement
    {
        get
        {
            object o = ViewState["HeadElement"];
            if (o is string) return (string)o;
            return "";
        }
        set
        {
            ViewState["HeadElement"] = value;
        }
    }

    public string GetGridColorScheme()
    {
        if (userControl is Interfaces.IColouredGrid)
        {
            return (userControl as Interfaces.IColouredGrid).GridColorScheme.ToString();
        }
        return "Yellow";
    }

    public string PageTitle
    {
        get
        {
            return lbTitle.Text;
        }
        set
        {
            lbTitle.Text = value;
            Page.Title = value;
        }
    }

}
