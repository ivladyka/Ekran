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
                    node.NavigateUrl = item.NavigateUrl;
                    SelectNode(node);
                    foreach (MenuItem childItem in item.ChildItems)
                    {
                        TreeNode childNode = new TreeNode(childItem.Text);
                        childNode.NavigateUrl = childItem.NavigateUrl;
                        SelectNode(childNode);
                        node.ChildNodes.Add(childNode);
                    }
                    TreeView1.Nodes.Add(node);
                }
            }
        }
    }

    private void SelectNode(TreeNode node)
    {
        if (Request.Url.ToString().ToLower().IndexOf(node.NavigateUrl.ToLower().Replace("~", "")) >= 0)
        {
            node.Selected = true;
        }
    }
}
