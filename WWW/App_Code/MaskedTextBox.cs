using System.Web.UI;
using Telerik.Web.UI;

/// <summary>
/// Summary description for MaskedTextBox
/// </summary>
public class MaskedTextBox : ControlBase
{
    private RadMaskedTextBox m_maskedtb;
    private bool m_UseTextWithLiterals = false;

    public MaskedTextBox()
    {
        
    }

    protected RadMaskedTextBox maskedtb
    {
        get
        {
            if (m_maskedtb != null) return m_maskedtb;
            Control c = this.FindControl("maskedtb");
            if (c != null)
            {
                if (c is RadMaskedTextBox)
                {
                    m_maskedtb = (RadMaskedTextBox)c;
                    return m_maskedtb;
                }
            }
            return null;
        }
    }

    public string Mask
    {
        set { maskedtb.Mask = value; }
        get { return maskedtb.Mask; }
    }

    public string Value
    {
        set
        {
            if (UseTextWithLiterals)
            {
                maskedtb.TextWithLiterals = value;
            }
            else
            {
                maskedtb.Text = value;
            }
        }
        get
        {
            if (UseTextWithLiterals)
            {
                return maskedtb.TextWithLiterals;
            }
            return maskedtb.Text;
        }
    }

    public bool Enabled
    {
        get
        {
            return maskedtb.Enabled;
        }
        set
        {
            maskedtb.Enabled = value;
        }
    }

    public string MaskClientID
    {
        get
        {
            return maskedtb.ClientID;
        }
    }

    public System.Web.UI.WebControls.Unit Width
    {
        get { return maskedtb.Width; }
        set { maskedtb.Width = value; }
    }

    public bool UseTextWithLiterals
    {
        get
        {
            return m_UseTextWithLiterals;
        }
        set
        {
            m_UseTextWithLiterals = value;
        }
    }
}
