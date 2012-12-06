using System;
using System.Web.UI;
using Telerik.Web.UI;

/// <summary>
/// Summary description for NumericInput
/// </summary>
public class NumericInput : ControlBase
{
    private RadNumericTextBox m_numInput;
    protected RadNumericTextBox numInput
    {
        get
        {
            if (m_numInput != null) return m_numInput;
            Control c = this.FindControl("numInput");
            if (c != null)
            {
                if (c is RadNumericTextBox)
                {
                    m_numInput = (RadNumericTextBox)c;
                    return m_numInput;
                }
            }
            return null;
        }
    }

    public NumericInput()
    {
        
    }

    public double Value
    {
        get
        {
            try
            {
                double numValue = 0;
                if (numInput.Text != "")
                {   
                    numValue = Convert.ToDouble(this.numInput.Value);
                }
                return numValue;
            }
            catch (FormatException)
            {
                throw new Exception(string.Format("Can't convert [{0}] to double", this.numInput.Text));
            }
        }
        set
        {
            this.numInput.Value = value;
        }
    }

    public int Width
    {
        set { numInput.Width = value; }
    }

    public string NumInputClientID
    {
        get
        {
            return numInput.ClientID;
        }
    }
}
