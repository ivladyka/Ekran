using System;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for IntInput
/// </summary>
public class IntInput : ControlBase
{
    private TextBox m_tbInt;

    protected TextBox tbInt
    {
        get
        {
            if (m_tbInt != null) return m_tbInt;
            Control c = this.FindControl("tbInt");
            if (c != null)
            {
                if (c is TextBox)
                {
                    m_tbInt = (TextBox)c;
                    return m_tbInt;
                }
            }
            return null;
        }
    }

    public IntInput()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Value
    {
        get
        {
            try
            {
                int numValue = 0;
                if (tbInt.Text != "")
                {
                    numValue = int.Parse(this.tbInt.Text);
                }
                return numValue;
            }
            catch (FormatException)
            {
                throw new Exception(string.Format("Не можливо перетворити [{0}] до цілого", this.tbInt.Text));
            }
        }
        set
        {
            this.tbInt.Text = value.ToString();
        }
    }
}
