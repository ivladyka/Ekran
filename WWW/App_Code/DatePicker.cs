using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for DatePicker
/// </summary>
public class DatePicker : ControlBase
{
    protected RadDatePicker m_dateInput;
    public RadDatePicker dateInput
    {
        get
        {
            if (m_dateInput != null) return m_dateInput;
            Control c = this.FindControl("dateInput");
            if (c != null)
            {
                if (c is RadDatePicker)
                {
                    m_dateInput = (RadDatePicker)c;
                    return m_dateInput;
                }
            }
            return null;
        }
    }

    protected RequiredFieldValidator m_rfvDateInput;
    protected RequiredFieldValidator rfvDateInput
    {
        get
        {
            if (m_rfvDateInput != null) return m_rfvDateInput;
            Control c = this.FindControl("rfvDateInput");
            if (c != null)
            {
                if (c is RequiredFieldValidator)
                {
                    m_rfvDateInput = (RequiredFieldValidator)c;
                    return m_rfvDateInput;
                }
            }
            return null;
        }
    }

    public DatePicker()
    {
        
    }

    public DateTime SelectedDate
    {
        get
        {
            if (dateInput.SelectedDate != null)
            {
                return Convert.ToDateTime(dateInput.SelectedDate);
            }
            return dateInput.MinDate;
        }
        set
        {
            dateInput.SelectedDate = value;
        }
    }

    public bool Enabled
    {
        get
        {
            return dateInput.Enabled;
        }

        set
        {
            dateInput.Enabled = value;
        }
    }

    public string DatePickerClientID
    {
        get
        {
            return dateInput.ClientID;
        }
    }

    public int Width
    {
        set
        {
            dateInput.Width = new Unit(value);
        }
    }

    public string OnClientInputKeyDown
    {
        set
        {
            dateInput.Attributes["onkeydown"] = value;
        }
    }

    public short TabIndex
    {
        set
        {
            dateInput.TabIndex = value;
        }
    }

    public string OnClientInputBlur
    {
        set
        {
            dateInput.Attributes["onblur"] = value;
        }
    }

    public string OnClientInputFocus
    {
        set
        {
            dateInput.Attributes["onfocus"] = value;
        }
    }

    public string OnClientDateSelected
    {
        set
        {
            dateInput.ClientEvents.OnDateSelected = value;
        }
    }

    public DateTime MinDate
    {
        get
        {
            return dateInput.MinDate;
        }
        set
        {
            dateInput.MinDate = value;
        }
    }

    public bool IsRequire
    {
        set
        {
            rfvDateInput.Enabled = value;
        }
    }

    public string ErrorMessage
    {
        set
        {
            rfvDateInput.ErrorMessage = value;
        }
    }

    public string ValidationGroup
    {
        set
        {
            rfvDateInput.ValidationGroup = value;
        }
    }
}
