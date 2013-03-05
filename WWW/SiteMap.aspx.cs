using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Text;
using VikkiSoft_BLL;
using System.Web.UI.WebControls;

public partial class SiteMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "text/xml";
        XmlTextWriter writer = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
        writer.WriteStartDocument();
        writer.WriteStartElement("urlset");
        writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");

        string siteUrl = Request.Url.Scheme + Uri.SchemeDelimiter + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        if (!Request.Url.IsDefaultPort)
        {
            siteUrl += ":" + Request.Url.Port;
        }
        Menu menu = new Menu();
        Utils.InitMenu(menu, false, false, false);
        foreach (MenuItem item in menu.Items)
        {
            writer.WriteStartElement("url");
            writer.WriteElementString("loc", siteUrl + item.NavigateUrl);
            writer.WriteEndElement();
            foreach (MenuItem childItem in item.ChildItems)
            {
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", siteUrl + childItem.NavigateUrl);
                writer.WriteEndElement();
            }
        }
        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Flush();
        Response.End();
    }
}