using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Office_OfficeMP : MasterPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Url.ToString().ToLower().IndexOf("login.aspx") >= 0)
            {
                TreeView1.Visible = false;
                divHeader.Visible = false;
            }
            else
            {
                TreeView1.Nodes.Clear();
                Menu menu = new Menu();
                Utils.InitMenu(menu, false, true, true);
                foreach (MenuItem item in menu.Items)
                {
                    TreeNode node = new TreeNode(item.Text);
                    node.NavigateUrl = FormatURL(item.NavigateUrl);
                    SelectNode(node);
                    foreach (MenuItem childItem in item.ChildItems)
                    {
                        TreeNode childNode = new TreeNode(childItem.Text);
                        childNode.NavigateUrl = FormatURL(childItem.NavigateUrl);
                        SelectNode(childNode);
                        node.ChildNodes.Add(childNode);
                    }
                    TreeView1.Nodes.Add(node);
                }
            }
        }
    }

    private string FormatURL(string input)
    {
        if (input.IndexOf("Default.aspx") >= 0)
        {
            string output = input.Replace("CategoryView", "CategoryEdit").Replace("Default.aspx", "Office/Office.aspx");
            if (output.IndexOf("CategoryID") == -1)
            {
                output += "?content=CategoryEdit&CategoryID=1";
            }
            return output;
        }
        return input;
    }

    private void SelectNode(TreeNode node)
    {
        if (Request.Url.ToString().ToLower().IndexOf(node.NavigateUrl.ToLower().Replace("~", "")) >= 0)
        {
            node.Selected = true;
        }
    }
}
