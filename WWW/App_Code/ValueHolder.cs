using System;
using System.Collections.Specialized;
using System.Data;

/// <summary>
/// Summary description for ValueHolder
/// </summary>
public class ValueHolder : Interfaces.IValueHolder
{
    protected object m_data;
    protected Interfaces.GetKeySynonymDelegate m_getKeySynonym;

    public ValueHolder(DataRowView row)
    {
        this.m_data = row;
    }

    public ValueHolder(NameValueCollection collection)
    {
        this.m_data = collection;
    }

    #region IValueHolder Members

    public Interfaces.GetKeySynonymDelegate GetKeySynonym
    {
        set
        {
            this.m_getKeySynonym = value;
        }
    }

    protected object GetValue(string valueName)
    {
        object ret = null;
        if (this.m_data is DataRowView)
        {
            DataRowView dataRowView = this.m_data as DataRowView;
            if (dataRowView.DataView.Table.Columns.Contains(valueName))
                ret = (this.m_data as DataRowView)[valueName];
        }

        if (this.m_data is NameValueCollection)
        {
            ret = (this.m_data as NameValueCollection)[valueName];
        }
        return ret;
    }

    public object this[string valueName]
    {
        get
        {
            object ret = this.GetValue(valueName);

            if (ret == null && this.m_getKeySynonym != null)
            {
                ret = this.GetValue(this.m_getKeySynonym(valueName));
            }

            if (ret == null)
            {
                throw new Exception(string.Format("Can't find value for property [{0}]", valueName));
            }
            return ret;
        }
    }

    #endregion
}
