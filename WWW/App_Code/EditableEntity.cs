using System;
using System.Reflection;
using MyGeneration.dOOdads;
using WhereParameter=MyGeneration.dOOdads.WhereParameter;

/// <summary>
/// Summary description for EditableEntity
/// </summary>
public class EditableEntity
{
    protected MyGeneration.dOOdads.SqlClientEntity m_sqlClientEntity;

    private EditableEntity()
    { }

    public EditableEntity(MyGeneration.dOOdads.SqlClientEntity e)
    {
        m_sqlClientEntity = e;
    }

    public EditableEntity(Type sqlClientEntityType)
    {
        this.m_sqlClientEntity = Activator.CreateInstance(sqlClientEntityType) as MyGeneration.dOOdads.SqlClientEntity;
        if (this.m_sqlClientEntity == null)
        {
            throw new Exception("Can't create SqlClientEntity of type " + sqlClientEntityType.ToString());
        }
    }

    public static explicit operator MyGeneration.dOOdads.SqlClientEntity(EditableEntity entity)
    {
        return entity.m_sqlClientEntity;
    }

    public object this[string valueName]
    {
        get
        {
            return this.GetPropertyValue(valueName);
        }
        set
        {
            this.SetPropertyValue(valueName, value);
        }
    }

    public void Delete()
    {
        this.m_sqlClientEntity.DeleteAll();
        this.m_sqlClientEntity.Save();
    }

    public void AddNew()
    {
        this.m_sqlClientEntity.AddNew();
    }

    public void Save()
    {
        this.m_sqlClientEntity.Save();
    }

    public bool Load()
    {
        return this.m_sqlClientEntity.Query.Load();
    }

    public MyGeneration.dOOdads.SqlClientEntity SqlClientEntity
    {
        get { return this.m_sqlClientEntity; }
        set { this.m_sqlClientEntity = value; }
    }

    public static bool SetPropertyValue(ref object entity, string valueName, object value)
    {
        PropertyInfo pInfo = entity.GetType().GetProperty(valueName);
        if (pInfo == null)
            return false;


        if (pInfo.PropertyType != value.GetType())
        {
            try
            {
                value = Convert.ChangeType(value, pInfo.PropertyType);
            }
            catch
            {
                return false;
                //throw new Exception(string.Format("Can't convert [{0}] from [{1}] to [{2}]", value, value.GetType(), pInfo.PropertyType));
            }
        }

        pInfo.SetValue(entity, value, null);
        return true;
    }

    public bool SetPropertyValue(string valueName, object value)
    {
        object o = this.m_sqlClientEntity;
        return EditableEntity.SetPropertyValue(ref o, valueName, value);
    }

    public static bool SetWhereParamValue(ref SqlClientEntity entity, string valueName, object value)
    {
        PropertyInfo wherePropertyInfo = entity.GetType().GetProperty("Where");
        if (wherePropertyInfo == null)
            return false;

        object wherePropertyValue = wherePropertyInfo.GetValue(entity, null);
        if (wherePropertyValue == null)
            return false;

        PropertyInfo whereParamPropertyInfo = wherePropertyValue.GetType().GetProperty(valueName);
        if (whereParamPropertyInfo == null)
            return false;

        WhereParameter whereParamValue = whereParamPropertyInfo.GetValue(wherePropertyValue, null) as WhereParameter;
        if (whereParamValue == null)
            return false;

        whereParamValue.Value = value;
        return true;
    }

    public bool SetWhereParamValue(string valueName, object value)
    {
        return EditableEntity.SetWhereParamValue(ref this.m_sqlClientEntity, valueName, value);
    }

    public object GetPropertyValue(string valueName)
    {
        try
        {
            return EditableEntity.GetPropertyValue(valueName, this.m_sqlClientEntity);
        }
        catch
        {
            return null;
        }
    }

    public static object GetPropertyValue(string valueName, object entity)
    {
        PropertyInfo pInfo = entity.GetType().GetProperty(valueName);
        if (pInfo == null)
            return null;

        return pInfo.GetValue(entity, null);
    }
}
