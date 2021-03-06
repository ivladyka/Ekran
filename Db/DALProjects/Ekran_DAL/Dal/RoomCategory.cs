
/*
'===============================================================================
'  Generated From - CSharp_dOOdads_BusinessEntity.vbgen
' 
'  ** IMPORTANT  ** 
'  How to Generate your stored procedures:
' 
'  SQL        = SQL_StoredProcs.vbgen
'  ACCESS     = Access_StoredProcs.vbgen
'  ORACLE     = Oracle_StoredProcs.vbgen
'  FIREBIRD   = FirebirdStoredProcs.vbgen
'  POSTGRESQL = PostgreSQL_StoredProcs.vbgen
'
'  The supporting base class SqlClientEntity is in the Architecture directory in "dOOdads".
'  
'  This object is 'abstract' which means you need to inherit from it to be able
'  to instantiate it.  This is very easilly done. You can override properties and
'  methods in your derived class, this allows you to regenerate this class at any
'  time and not worry about overwriting custom code. 
'
'  NEVER EDIT THIS FILE.
'
'  public class YourObject :  _YourObject
'  {
'
'  }
'
'===============================================================================
*/

// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;

using MyGeneration.dOOdads;

namespace VikkiSoft_BLL.DAL
{
	public abstract class _RoomCategory : SqlClientEntity
	{
		public _RoomCategory()
		{
			this.QuerySource = "RoomCategory";
			this.MappingName = "RoomCategory";

		}	

		//=================================================================
		//  public Overrides void AddNew()
		//=================================================================
		//
		//=================================================================
		public override void AddNew()
		{
			base.AddNew();
			
		}
		
		
		public override void FlushData()
		{
			this._whereClause = null;
			this._aggregateClause = null;
			base.FlushData();
		}
		
		//=================================================================
		//  	public Function LoadAll() As Boolean
		//=================================================================
		//  Loads all of the records in the database, and sets the currentRow to the first row
		//=================================================================
		public bool LoadAll() 
		{
			ListDictionary parameters = null;
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "LoadAllRoomCategory]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int RoomCategoryID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.RoomCategoryID, RoomCategoryID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "LoadRoomCategoryByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter RoomCategoryID
			{
				get
				{
					return new SqlParameter("@RoomCategoryID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter Name
			{
				get
				{
					return new SqlParameter("@Name", SqlDbType.VarChar, 50);
				}
			}
			
			public static SqlParameter Name_en
			{
				get
				{
					return new SqlParameter("@Name_en", SqlDbType.VarChar, 50);
				}
			}
			
			public static SqlParameter Name_pl
			{
				get
				{
					return new SqlParameter("@Name_pl", SqlDbType.VarChar, 50);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string RoomCategoryID = "RoomCategoryID";
            public const string Name = "Name";
            public const string Name_en = "Name_en";
            public const string Name_pl = "Name_pl";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[RoomCategoryID] = _RoomCategory.PropertyNames.RoomCategoryID;
					ht[Name] = _RoomCategory.PropertyNames.Name;
					ht[Name_en] = _RoomCategory.PropertyNames.Name_en;
					ht[Name_pl] = _RoomCategory.PropertyNames.Name_pl;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string RoomCategoryID = "RoomCategoryID";
            public const string Name = "Name";
            public const string Name_en = "Name_en";
            public const string Name_pl = "Name_pl";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[RoomCategoryID] = _RoomCategory.ColumnNames.RoomCategoryID;
					ht[Name] = _RoomCategory.ColumnNames.Name;
					ht[Name_en] = _RoomCategory.ColumnNames.Name_en;
					ht[Name_pl] = _RoomCategory.ColumnNames.Name_pl;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string RoomCategoryID = "s_RoomCategoryID";
            public const string Name = "s_Name";
            public const string Name_en = "s_Name_en";
            public const string Name_pl = "s_Name_pl";

		}
		#endregion		
		
		#region Properties
	
		public virtual int RoomCategoryID
	    {
			get
	        {
				return base.Getint(ColumnNames.RoomCategoryID);
			}
			set
	        {
				base.Setint(ColumnNames.RoomCategoryID, value);
			}
		}

		public virtual string Name
	    {
			get
	        {
				return base.Getstring(ColumnNames.Name);
			}
			set
	        {
				base.Setstring(ColumnNames.Name, value);
			}
		}

		public virtual string Name_en
	    {
			get
	        {
				return base.Getstring(ColumnNames.Name_en);
			}
			set
	        {
				base.Setstring(ColumnNames.Name_en, value);
			}
		}

		public virtual string Name_pl
	    {
			get
	        {
				return base.Getstring(ColumnNames.Name_pl);
			}
			set
	        {
				base.Setstring(ColumnNames.Name_pl, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_RoomCategoryID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.RoomCategoryID) ? string.Empty : base.GetintAsString(ColumnNames.RoomCategoryID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.RoomCategoryID);
				else
					this.RoomCategoryID = base.SetintAsString(ColumnNames.RoomCategoryID, value);
			}
		}

		public virtual string s_Name
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Name) ? string.Empty : base.GetstringAsString(ColumnNames.Name);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Name);
				else
					this.Name = base.SetstringAsString(ColumnNames.Name, value);
			}
		}

		public virtual string s_Name_en
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Name_en) ? string.Empty : base.GetstringAsString(ColumnNames.Name_en);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Name_en);
				else
					this.Name_en = base.SetstringAsString(ColumnNames.Name_en, value);
			}
		}

		public virtual string s_Name_pl
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Name_pl) ? string.Empty : base.GetstringAsString(ColumnNames.Name_pl);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Name_pl);
				else
					this.Name_pl = base.SetstringAsString(ColumnNames.Name_pl, value);
			}
		}


		#endregion		
	
		#region Where Clause
		public class WhereClause
		{
			public WhereClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffWhereParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffWhereParameter(this);
					}

					return _tearOff;
				}
			}

			#region WhereParameter TearOff's
			public class TearOffWhereParameter
			{
				public TearOffWhereParameter(WhereClause clause)
				{
					this._clause = clause;
				}
				
				
				public WhereParameter RoomCategoryID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.RoomCategoryID, Parameters.RoomCategoryID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter Name
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Name, Parameters.Name);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter Name_en
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Name_en, Parameters.Name_en);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter Name_pl
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Name_pl, Parameters.Name_pl);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter RoomCategoryID
		    {
				get
		        {
					if(_RoomCategoryID_W == null)
	        	    {
						_RoomCategoryID_W = TearOff.RoomCategoryID;
					}
					return _RoomCategoryID_W;
				}
			}

			public WhereParameter Name
		    {
				get
		        {
					if(_Name_W == null)
	        	    {
						_Name_W = TearOff.Name;
					}
					return _Name_W;
				}
			}

			public WhereParameter Name_en
		    {
				get
		        {
					if(_Name_en_W == null)
	        	    {
						_Name_en_W = TearOff.Name_en;
					}
					return _Name_en_W;
				}
			}

			public WhereParameter Name_pl
		    {
				get
		        {
					if(_Name_pl_W == null)
	        	    {
						_Name_pl_W = TearOff.Name_pl;
					}
					return _Name_pl_W;
				}
			}

			private WhereParameter _RoomCategoryID_W = null;
			private WhereParameter _Name_W = null;
			private WhereParameter _Name_en_W = null;
			private WhereParameter _Name_pl_W = null;

			public void WhereClauseReset()
			{
				_RoomCategoryID_W = null;
				_Name_W = null;
				_Name_en_W = null;
				_Name_pl_W = null;

				this._entity.Query.FlushWhereParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffWhereParameter _tearOff;
			
		}
	
		public WhereClause Where
		{
			get
			{
				if(_whereClause == null)
				{
					_whereClause = new WhereClause(this);
				}
		
				return _whereClause;
			}
		}
		
		private WhereClause _whereClause = null;	
		#endregion
	
		#region Aggregate Clause
		public class AggregateClause
		{
			public AggregateClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffAggregateParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffAggregateParameter(this);
					}

					return _tearOff;
				}
			}

			#region AggregateParameter TearOff's
			public class TearOffAggregateParameter
			{
				public TearOffAggregateParameter(AggregateClause clause)
				{
					this._clause = clause;
				}
				
				
				public AggregateParameter RoomCategoryID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.RoomCategoryID, Parameters.RoomCategoryID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter Name
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Name, Parameters.Name);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter Name_en
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Name_en, Parameters.Name_en);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter Name_pl
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Name_pl, Parameters.Name_pl);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter RoomCategoryID
		    {
				get
		        {
					if(_RoomCategoryID_W == null)
	        	    {
						_RoomCategoryID_W = TearOff.RoomCategoryID;
					}
					return _RoomCategoryID_W;
				}
			}

			public AggregateParameter Name
		    {
				get
		        {
					if(_Name_W == null)
	        	    {
						_Name_W = TearOff.Name;
					}
					return _Name_W;
				}
			}

			public AggregateParameter Name_en
		    {
				get
		        {
					if(_Name_en_W == null)
	        	    {
						_Name_en_W = TearOff.Name_en;
					}
					return _Name_en_W;
				}
			}

			public AggregateParameter Name_pl
		    {
				get
		        {
					if(_Name_pl_W == null)
	        	    {
						_Name_pl_W = TearOff.Name_pl;
					}
					return _Name_pl_W;
				}
			}

			private AggregateParameter _RoomCategoryID_W = null;
			private AggregateParameter _Name_W = null;
			private AggregateParameter _Name_en_W = null;
			private AggregateParameter _Name_pl_W = null;

			public void AggregateClauseReset()
			{
				_RoomCategoryID_W = null;
				_Name_W = null;
				_Name_en_W = null;
				_Name_pl_W = null;

				this._entity.Query.FlushAggregateParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffAggregateParameter _tearOff;
			
		}
	
		public AggregateClause Aggregate
		{
			get
			{
				if(_aggregateClause == null)
				{
					_aggregateClause = new AggregateClause(this);
				}
		
				return _aggregateClause;
			}
		}
		
		private AggregateClause _aggregateClause = null;	
		#endregion
	
		protected override IDbCommand GetInsertCommand() 
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "InsertRoomCategory]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.RoomCategoryID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "UpdateRoomCategory]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "DeleteRoomCategory]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.RoomCategoryID);
			p.SourceColumn = ColumnNames.RoomCategoryID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.RoomCategoryID);
			p.SourceColumn = ColumnNames.RoomCategoryID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.Name);
			p.SourceColumn = ColumnNames.Name;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.Name_en);
			p.SourceColumn = ColumnNames.Name_en;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.Name_pl);
			p.SourceColumn = ColumnNames.Name_pl;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
