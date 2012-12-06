using System;
using System.Collections;

/// <summary>
/// Summary description for SqlClientEntityControlBase
/// </summary>
public abstract class SqlClientEntityControlBase : ControlBase
{
    public SqlClientEntityControlBase()
    {
        this.m_Name = "SqlClientEntityControlBase";
    }

    protected abstract Type GetEditableEntityType();

    public virtual bool IsNew
    {
        get
        {
            return this.GetQueryStringParams().Contains("IsNew");
        }
    }

    protected virtual string[] GetPrimaryKeys()
    {
        ArrayList ret = this.GetQueryStringParams();
        return (string[])ret.ToArray(typeof(string));
    }

    protected virtual string[] GetPrimaryKeysForNewEntity()
    {
        ArrayList ret = this.GetQueryStringParams();
        if (ret.Contains("IsNew"))
            ret.Remove("IsNew");
        return (string[])ret.ToArray(typeof(string));
    }

    protected ArrayList GetQueryStringParams()
    {
        ArrayList queryStringParams = new ArrayList();
        foreach (string key in this.Request.QueryString.Keys)
        {
            if (key == "content")
                continue;
            queryStringParams.Add(key);
        }
        return queryStringParams;
    }


    protected virtual string GetKeySynonym(string keyName)
    {
        return keyName;
    }

    protected virtual EditableEntity CreateEditableEntityInstance()
    {
        return new EditableEntity(this.GetEditableEntityType());
    }

    protected virtual EditableEntity GetEditableEntity(Interfaces.IValueHolder valueHolder)
    {
        if (valueHolder == null)
            throw new NullReferenceException("valueHolder can't be null");

        valueHolder.GetKeySynonym = new Interfaces.GetKeySynonymDelegate(this.GetKeySynonym);
        EditableEntity entity = this.CreateEditableEntityInstance();
        string[] keys;

        if (this.IsNew)
        {
            keys = this.GetPrimaryKeysForNewEntity();
            entity.AddNew();
            foreach (string key in keys)
            {
                entity[key] = valueHolder[key];
            }
            return entity;
        }

        keys = this.GetPrimaryKeys();
        foreach (string key in keys)
        {
            if (!entity.SetWhereParamValue(key, valueHolder[key]))
                throw new Exception(string.Format("Can't set SqlClientEntity where parameter [{0}] to value [{1}]", key, valueHolder[key]));
        }

        if (!entity.Load())
            throw new Exception("Can't load SqlClientEntity of type " + entity.GetType());

        return entity;
    }	
}
