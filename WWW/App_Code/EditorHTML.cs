using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for EditorHTML
/// </summary>
public class EditorHTML : ControlBase
{
    protected RadEditor m_editorHTML;
    protected RequiredFieldValidator m_rfvEditorHTML;
    protected RadEditor editorHTML
    {
        get
        {
            if (m_editorHTML != null) return m_editorHTML;
            Control c = this.FindControl("editorHTML");
            if (c != null)
            {
                if (c is RadEditor)
                {
                    m_editorHTML = (RadEditor)c;
                    return m_editorHTML;
                }
            }
            return null;
        }
    }

    
    protected RequiredFieldValidator rfvEditorHTML
    {
        get
        {
            if (m_rfvEditorHTML != null) return m_rfvEditorHTML;
            Control c = this.FindControl("rfvEditorHTML");
            if (c != null)
            {
                if (c is RequiredFieldValidator)
                {
                    m_rfvEditorHTML = (RequiredFieldValidator)c;
                    return m_rfvEditorHTML;
                }
            }
            return null;
        }
    }

    public string Html
    {
        get
        {
            return editorHTML.Content;
        }
        set
        {
            editorHTML.Content = value;
        }
    }

    /*public bool Editable
    {
        set
        {
            editorHTML.Editable = value;
        }
    }*/

    public bool HasPermission
    {
        set
        {
            editorHTML.Enabled = value;
        }
    }

    public EditorHTML()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool IsRequire
    {
        set
        {
            rfvEditorHTML.Enabled = value;
        }
    }
}
