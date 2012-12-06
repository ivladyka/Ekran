using System;
using VikkiSoft_BLL;
using System.Net.Mail;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;

public partial class CategoryView : ControlBase
{
    public CategoryView()
    {
        
    }
    protected override void InitOnFirstLoading()
    {
        base.InitOnFirstLoading();
        LoadCategory();
        LoadDDLs();
        switch (CategoryID)
        {
            case 1:
                pnlBookingHome.Visible = true;
                 dpCheckInDate.MinDate = DateTime.Now;
                dpCheckOutDate.MinDate = DateTime.Now;
                break;
            case 23:
                pnlContactpage.Visible = true;
                dpCheckInDate2.MinDate = DateTime.Now;
                dpCheckOutDate2.MinDate = DateTime.Now;
                break;
            case 3:
                pnlGallery.Visible = true;
                break;
            case 27:
                pnlBooking.Visible = true;
                LoadBookingData();
                break;
            case 24:
                pnlContactUS.Visible = true;
                break;

        }
    }
    private void LoadCategory()
    {
        Category c = new Category();
        c.Where.CategoryID.Value = CategoryID;
        if (c.Query.Load())
        {
            if (CategoryID > 1)
            {
                lblName.Text = c.GetColumn("Name" + Utils.LangPrefix).ToString();
            }
            this.m_Name = c.GetColumn("Name" + Utils.LangPrefix).ToString();
            if (!c.IsColumnNull("CategoryContent" + Utils.LangPrefix))
            {
                lblCategoryContent.Text = c.GetColumn("CategoryContent" + Utils.LangPrefix).ToString();
            }
            if (!c.IsColumnNull(Category.ColumnNames.RoomCategoryID))
            {
                LoadRoomPrice(c.RoomCategoryID);
            }
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
    protected void btnSendMessage_Click(object sender, EventArgs e)
    {
        try
        {
            if (tbCFH.Text != "")
            {
                Response.Redirect("Default.aspx");
                return;
            }
            lbError.Text = "";
            lbError.Visible = false;
            SendEmail(Resources.Vikkisoft.MessageFrom + " " + tbName.Text + " (" + tbEmailAddress.Text + ")",
                tdComments.Text, tbEmailAddress.Text, false);
            tdComments.Text = "";
            ShowMessage(Resources.Vikkisoft.MessageSent, Color.Yellow);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, Color.Red);
        }
    }
    private void SendEmail(string subject, string body, string replyEmail, bool isHTML)
    {
        int portNumber;
        int.TryParse(System.Configuration.ConfigurationManager.AppSettings["SMTPServerPort"],
                     out portNumber);
        SmtpClient client;
        if (portNumber > 0)
        {
            client = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"], portNumber);
        }
        else
        {
            client = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SMTPServer"]);
        }
        MailAddress from =
            new MailAddress(System.Configuration.ConfigurationManager.AppSettings["FromEmail"]);
        MailAddress to = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["ToEmail"].Trim());
        MailMessage message = new MailMessage(from, to);
        message.ReplyToList.Add(new MailAddress(replyEmail));
        message.Body = body;
        message.IsBodyHtml = isHTML;
        message.Subject = subject;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        if (System.Configuration.ConfigurationManager.AppSettings["EmailAccountPassword"].Trim() != "")
        {
            client.Credentials =
                new System.Net.NetworkCredential(
                    System.Configuration.ConfigurationManager.AppSettings["FromEmail"],
                    System.Configuration.ConfigurationManager.AppSettings["EmailAccountPassword"]);
        }
        client.Send(message);
    }
    private void ShowMessage(string message, Color color)
    {
        lbError.Text = message;
        lbError.ForeColor = color;
        lbError.Visible = true;
    }
    private void LoadRoomPrice(int roomCategoryID)
    {
        Room r = new Room();
        if (r.LoadPriceByRoomCategoryID(roomCategoryID))
        {
            pnlRoomPrice.Visible = true;
            do
            {
                tblRoomPrices.Rows.Add(AddNewRow(r.GetColumn("RoomList").ToString() + " - " 
                    + r.Price.ToString("N") + " " + Resources.Vikkisoft.Grn.ToString()));
            }
            while (r.MoveNext());

        }
    }
    private HtmlTableRow AddNewRow(string innerHTML)
    {
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        Label lbl = new Label();
        lbl.Text = innerHTML;
        cell.Controls.Add(lbl);
        row.Cells.Add(cell);
        return row;
    }
    protected void Page_Prerender(object sender, System.EventArgs e)
    {
        ShowChildCategories();
    }


    private void ShowChildCategories()
    {
        if (CategoryID > 1)
        {
            Menu menu = ((MasterPageBase)Page.Master).TopMenuControl;
            foreach (MenuItem item in menu.Items)
            {
                if (item.NavigateUrl.IndexOf("&CategoryID=" + CategoryID) >= 0)
                {
                    if (item.ChildItems.Count > 0)
                    {
                        pnlChildCategories.Visible = true;
                        foreach (MenuItem childItem in item.ChildItems)
                        {
                            int childCategoryID = int.Parse(childItem.Value);
                            Gallery g = new Gallery();
                            if (g.LoadCoverByCategoryID(childCategoryID))
                            {
                                if (g.s_PhotoName.Length > 0)
                                {
                                    HtmlGenericControl li = new HtmlGenericControl("li");
                                    HtmlGenericControl divTitle = new HtmlGenericControl("div");
                                    divTitle.Attributes["class"] = "subpagetitle";
                                    HtmlGenericControl h4 = new HtmlGenericControl("h4");
                                    Category c = new Category();
                                    if (c.LoadByPrimaryKey(childCategoryID))
                                    {
                                        h4.InnerHtml = c.GetColumn("Name" + Utils.LangPrefix).ToString(); ;
                                    }
                                    divTitle.Controls.Add(h4);
                                    HyperLink hl = new HyperLink();
                                    hl.NavigateUrl = childItem.NavigateUrl;
                                    System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                                    if (item.ChildItems.Count == 4)
                                    {
                                        image.Width = Unit.Pixel(190);
                                    }
                                    else
                                    {
                                        image.Width = Unit.Pixel(262);
                                    }
                                    image.ImageUrl = Path.Combine(Utils.GaleryImagePath, g.s_PhotoName.Substring(0, g.s_PhotoName.ToString().Length - 4) + "_s.jpg");
                                    hl.Controls.Add(image);
                                    hl.Controls.Add(divTitle);
                                    li.Controls.Add(hl);
                                    /*if (g.GetColumn("PriceRange").ToString().Length > 0)
                                    {
                                        HtmlGenericControl div = new HtmlGenericControl("div");
                                        div.InnerHtml = g.GetColumn("PriceRange").ToString() + " " + Resources.Vikkisoft.Grn;
                                        if (item.ChildItems.Count == 4)
                                        {
                                            div.Style["width"] = "150px";
                                            div.Style["padding"] = "0";
                                            div.Style["margin"] = "0";
                                        }
                                        li.Controls.Add(div);
                                    }*/
                                    ulChildCategories.Controls.Add(li);
                                }
                            }
                        }
                    }
                    return;
                }
            }
        }
    }

    private void LoadDDLs()
    {
        LoadNumberDDL(ddlRooms, 1, 6);
        LoadNumberDDL(ddlAdults, 1, 4);
        LoadNumberDDL(ddlChildren, 0, 3);
        LoadNumberDDL(ddlRooms1, 1, 6);
        LoadNumberDDL(ddlAdults1, 1, 4);
        LoadNumberDDL(ddlChildren1, 0, 3);
        LoadNumberDDL(ddlRooms2, 1, 6);
        LoadNumberDDL(ddlAdults2, 1, 4);
        LoadNumberDDL(ddlChildren2, 0, 3);
        ddlRoomPrices.Items.Clear();
        ddlRoomPrices.Items.Add(new RadComboBoxItem("", "0"));
        Room r = new Room();
        if(r.LoadWithRoomCategoty())
        {
            do
            {
                ddlRoomPrices.Items.Add(new RadComboBoxItem("№ " + r.s_Number + " - " 
                    + r.GetColumn("RoomCategoryName" + Utils.LangPrefix).ToString()
                    + (r.IsColumnNull("Price") ? "" : " - " + r.Price.ToString("N") + " " 
                    + Resources.Vikkisoft.Grn), r.RoomID.ToString()));
            }
            while (r.MoveNext());
        }
    }

    private void LoadNumberDDL(RadComboBox ddl, int startIndex, int endIndex)
    {
        ddl.Items.Clear();
        for (int i = startIndex; i <= endIndex; i++)
        {
            ddl.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
        }
    }

    protected void btnBookNow_Click(object sender, EventArgs e)
    {
        SaveBookingDataAndRedirect(dpCheckInDate, dpCheckOutDate, ddlRooms, ddlAdults, ddlChildren);
    }

    protected void btnBookNow2_Click(object sender, EventArgs e)
    {
        SaveBookingDataAndRedirect(dpCheckInDate2, dpCheckOutDate2, ddlRooms2, ddlAdults2, ddlChildren2);
    }

    private void SaveBookingDataAndRedirect(DatePicker dpCheckIn, DatePicker dpCheckOut, RadComboBox ddlRoom,
        RadComboBox ddlAdult, RadComboBox ddlChild)
    {
        if (dpCheckOut.SelectedDate <= dpCheckIn.SelectedDate)
        {
            dateCompare.Visible = true;
            return;
        }
        CheckIn = dpCheckIn.SelectedDate;
        CheckOut = dpCheckOut.SelectedDate;
        Rooms = ddlRoom.SelectedValue;
        Adults = ddlAdult.SelectedValue;
        Children = ddlChild.SelectedValue;
        Response.Redirect("Default.aspx?content=CategoryView&CategoryID=27");
    }

    private void LoadBookingData()
    {
        dpCheckInDate1.MinDate = DateTime.Now;
        dpCheckOutDate1.MinDate = DateTime.Now;
        dpCheckInDate1.SelectedDate = CheckIn;
        dpCheckOutDate1.SelectedDate = CheckOut;
        dpCheckInDate1.ErrorMessage = "<br>" + Resources.Vikkisoft.RequiredField;
        dpCheckOutDate1.ErrorMessage = "<br>" + Resources.Vikkisoft.RequiredField;
        ddlRooms1.SelectedValue = Rooms;
        ddlAdults1.SelectedValue = Adults;
        ddlChildren1.SelectedValue = Children;
    }

    protected void btnBookNow1_Click(object sender, EventArgs e)
    {
        try
        {
            dateCompare1.Visible = false;
            if (tbCFH1.Text != "")
            {
                Response.Redirect("Default.aspx");
                return;
            }
            if (dpCheckOutDate1.SelectedDate <= dpCheckInDate1.SelectedDate)
            {
                dateCompare1.Visible = true;
                return;
            }
            lbError1.Text = "";
            lbError1.Visible = false;
            string urlLogo = "http";
            if (System.Web.HttpContext.Current.Request.IsSecureConnection)
            {
                urlLogo += "s";
            }
            urlLogo += "://" + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            if (System.Web.HttpContext.Current.Request.Url.Port != 80)
            {
                urlLogo += ":" + System.Web.HttpContext.Current.Request.Url.Port;
            }
            urlLogo += "/Images/logo1.jpg";

            string body = "<style type='text/css'> .auto-style1 {text-align: right;}</style>";

            body += "<table style=\"width:600px; background-color: white; border: none; margin:30px; padding:30px; border: #ffc33f 2px solid\">";
            body += "<tr><td><img src='" + urlLogo + "'  /></td><td><table><tr>";
            body += "<td style='font-size:18pt;'><b>" + Resources.Vikkisoft.Client + ":</b></td>";
            body += "<tr><td style='font-size:18pt;'>" + tbName1.Text + "</td></tr>";
            body += "<tr><td>Email: " + tbEmail1.Text + "</td></tr>";
            if (tbPhone.Text.Trim().Length > 0)
            {
                body += "<tr><td>" + Resources.Vikkisoft.Phone + ": " + tbPhone.Text + "</td></tr>";
            }
            body += "</table></td></tr>";
            body += "<tr><td style='width:200px; font-weight:bold'>" + Resources.Vikkisoft.CheckIn + ":</td>";
            body += "<td>" + dpCheckInDate1.SelectedDate.ToShortDateString() + "</td></tr>";
            body += "<tr><td style='font-weight:bold'>" + Resources.Vikkisoft.CheckOut + ":</td>";
            body += "<td>" + dpCheckOutDate1.SelectedDate.ToShortDateString() + "</td></tr>";
            body += "<tr><td style='font-weight:bold'>" + Resources.Vikkisoft.Rooms + ":</td>";
            body += "<td>" + ddlRooms1.SelectedValue + "</td></tr>";
            body += "<tr><td style='font-weight:bold'>" + Resources.Vikkisoft.Adults + ":</td>";
            body += "<td>" + ddlAdults1.SelectedValue + "</td></tr>";
            body += "<tr><td style='font-weight:bold'>" + Resources.Vikkisoft.Children + ":</td>";
            body += "<td>" + ddlChildren1.SelectedValue + "</td></tr>";
            if (ddlRoomPrices.SelectedValue != "0")
            {
                body += "<tr><td style='font-weight:bold'>" + Resources.Vikkisoft.Room + ":</td>";
                body += "<td>" + ddlRoomPrices.SelectedItem.Text + "</td></tr>";
            }
            body += "<tr><td style='font-weight:bold'>" + Resources.Vikkisoft.Message + ":</td>";
            body += "<td>" + tbMessage.Text + "</td></tr>";
            body += "</table>";

            SendEmail(Resources.Vikkisoft.BookingEmailSubject, body, tbEmail1.Text, true);

            ShowBookMessage(Resources.Vikkisoft.MessageSent, Color.Green);
            tbMessage.Text = "";
            ddlRooms1.SelectedValue = "1";
            ddlAdults1.SelectedValue = "1";
            ddlChildren1.SelectedValue = "0";
            ddlRoomPrices.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            ShowBookMessage(ex.Message, Color.Red);
        }
    }

    private void ShowBookMessage(string message, Color color)
    {
        lbError1.Text = message;
        lbError1.ForeColor = color;
        lbError1.Visible = true;
    }

    private DateTime CheckIn
    {
        set
        {
            UserSession.SetObjectKey("EKRAN_CheckIn", value);
        }
        get
        {
            if (UserSession.GetObjectKey("EKRAN_CheckIn") != null)
            {
                return (DateTime)UserSession.GetObjectKey("EKRAN_CheckIn");
            }
            return DateTime.Now;
        }
    }

    private DateTime CheckOut
    {
        set
        {
            UserSession.SetObjectKey("EKRAN_CheckOut", value);
        }
        get
        {
            if (UserSession.GetObjectKey("EKRAN_CheckOut") != null)
            {
                return (DateTime)UserSession.GetObjectKey("EKRAN_CheckOut");
            }
            return DateTime.Now.AddDays(1);
        }
    }

    private string Rooms
    {
        set
        {
            UserSession.SetKey("EKRAN_Rooms", value);
        }
        get
        {
            if (UserSession.GetKey("EKRAN_Rooms") != null)
            {
                return UserSession.GetKey("EKRAN_Rooms");
            }
            return "1";
        }
    }

    private string Adults
    {
        set
        {
            UserSession.SetKey("EKRAN_Adults", value);
        }
        get
        {
            if (UserSession.GetKey("EKRAN_Adults") != null)
            {
                return UserSession.GetKey("EKRAN_Adults");
            }
            return "1";
        }
    }

    private string Children
    {
        set
        {
            UserSession.SetKey("EKRAN_Children", value);
        }
        get
        {
            if (UserSession.GetKey("EKRAN_Children") != null)
            {
                return UserSession.GetKey("EKRAN_Children");
            }
            return "0";
        }
    }

    public string CheckInRes
    {
        get
        {
            return Resources.Vikkisoft.CheckIn;
        }
    }

    public string CheckOutRes
    {
        get
        {
            return Resources.Vikkisoft.CheckOut;
        }
    }

    public string Name
    {
        get
        {
            return Resources.Vikkisoft.Name;
        }
    }

    public string Message
    {
        get
        {
            return Resources.Vikkisoft.Message;
        }
    }

    public string RoomsRes
    {
        get
        {
            return Resources.Vikkisoft.Rooms;
        }
    }

    public string AdultsRes
    {
        get
        {
            return Resources.Vikkisoft.Adults;
        }
    }

    public string ChildrenRes
    {
        get
        {
            return Resources.Vikkisoft.Children;
        }
    }

    public string Phone
    {
        get
        {
            return Resources.Vikkisoft.Phone;
        }
    }

    public string OnlineBooking
    {
        get
        {
            return Resources.Vikkisoft.OnlineBooking;
        }
    }

    public string Room
    {
        get
        {
            return Resources.Vikkisoft.Room;
        }
    }
}
