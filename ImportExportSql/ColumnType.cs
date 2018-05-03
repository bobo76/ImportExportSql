using System;
using System.Data.SqlClient;

namespace ImportExportSql
{
    public class ColumnTypeBase
    {
        public object ReadValue(SqlDataReader dr, int ix)
        {
            if (dr.IsDBNull(ix))
                return DBNull.Value;
            return ReadValueObject(dr, ix);
        }

        public virtual string ConvertToString(object value)
        {
            if (value == DBNull.Value || value == null)
                return "";
            return value.ToString();
        }
        internal virtual object ReadValueObject(SqlDataReader dr, int ix)
        {
            throw new Exception("not implemented");
        }
        public object ReadValue(string value, bool isNullable)
        {
            if (string.IsNullOrEmpty(value) && isNullable)
                return DBNull.Value;
            return ReadValueObject(value);
        }
        internal virtual object ReadValueObject(string value)
        {
            throw new Exception("not implemented");
        }
    }
    
    public class ColumnTypeNVarchar : ColumnTypeBase, IDataType
    {
        public virtual string DataTypeName { get; } = "nvarchar";
        public ColumnTypeNVarchar()
        {
        }
        public virtual IDataType CreateInstance()
        {
            return new ColumnTypeNVarchar();
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.NVarChar);
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlString(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return value;
        }
    }

    public class ColumnTypeVarchar : ColumnTypeNVarchar
    {
        public override string DataTypeName { get; } = "varchar";
        public override IDataType CreateInstance()
        {
            return new ColumnTypeVarchar();
        }
    }

    public class ColumnTypeNChar : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "nchar";
        public IDataType CreateInstance()
        {
            return new ColumnTypeNChar();
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.NChar);
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlString(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return value;
        }
    }

    public class ColumnTypeBit : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "bit";
        public IDataType CreateInstance()
        {
            return new ColumnTypeBit();
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Bit);
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlBoolean(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return bool.Parse(value);
        }
    }

    public class ColumnTypeInt : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "int";
        public IDataType CreateInstance()
        {
            return new ColumnTypeInt();
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Int);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlInt32(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return int.Parse(value);
        }
    }

    public class ColumnTypeSmallint : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "smallint";
        public IDataType CreateInstance()
        {
            return new ColumnTypeSmallint();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.SmallInt);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetInt16(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return short.Parse(value);
        }
    }

    public class ColumnTypeTinyint : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "tinyint";
        public IDataType CreateInstance()
        {
            return new ColumnTypeTinyint();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.TinyInt);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetByte(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return byte.Parse(value);
        }
    }

    public class ColumnTypeReal : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "real";
        public IDataType CreateInstance()
        {
            return new ColumnTypeReal();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Real);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetFloat(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return float.Parse(value, System.Globalization.NumberStyles.AllowDecimalPoint);
        }
    }

    public class ColumnTypeMoney : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "money";
        public IDataType CreateInstance()
        {
            return new ColumnTypeMoney();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Money);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlMoney(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return decimal.Parse(value);
        }
    }

    public class ColumnTypeDateTime : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "datetime";
        public IDataType CreateInstance()
        {
            return new ColumnTypeDateTime();
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            var dateTmp = dr.GetSqlDateTime(ix);
            return (DateTime)dateTmp;
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.DateTime);
        }
        public override string ConvertToString(object value)
        {
            if (value == DBNull.Value || value == null)
                return "";
            return ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss.FFFtt");
        }
        internal override object ReadValueObject(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DBNull.Value;

            return DateTime.Parse(value);
        }
    }
    public class ColumnTypeSmallDateTime : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "smalldatetime";
        public IDataType CreateInstance()
        {
            return new ColumnTypeSmallDateTime();
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            var dateTmp = dr.GetSqlDateTime(ix);
            return (DateTime)dateTmp;
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.DateTime);
        }
        public override string ConvertToString(object value)
        {
            if (value == DBNull.Value || value == null)
                return "";
            return ((DateTime)value).ToString("yyyy-MM-dd hh:mm:ss");
        }
        internal override object ReadValueObject(string value)
        {
            return DateTime.Parse(value);
        }
    }

    public class ColumnTypeDate : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "date";
        public IDataType CreateInstance()
        {
            return new ColumnTypeDate();
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            var dateTmp = dr.GetSqlDateTime(ix);
            return (DateTime)dateTmp;
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.DateTime);
        }
        public override string ConvertToString(object value)
        {
            if (value == DBNull.Value || value == null)
                return "";
            var sqlDateTmp = (System.Data.SqlTypes.SqlDateTime)value;
            return (sqlDateTmp).Value.ToString("yyyy-MM-dd");
        }
        internal override object ReadValueObject(string value)
        {
            return DateTime.Parse(value);
        }
    }

    public class ColumnTypeTime : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "time";
        public IDataType CreateInstance()
        {
            return new ColumnTypeTime();
        }

        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            var dateTmp = dr.GetSqlDateTime(ix);
            return (DateTime)dateTmp;
        }
        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Time);
        }
        public override string ConvertToString(object value)
        {
            if (value == DBNull.Value || value == null)
                return "";
            var sqlDateTmp = (System.Data.SqlTypes.SqlDateTime)value;
            return (sqlDateTmp).Value.ToString("yyyy-MM-dd hh:mm:ss.FFFtt");
        }
        internal override object ReadValueObject(string value)
        {
            return DateTime.Parse(value);
        }
    }

    public class ColumnTypeUniqueidentifier : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "uniqueidentifier";
        public IDataType CreateInstance()
        {
            return new ColumnTypeUniqueidentifier();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.UniqueIdentifier);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetGuid(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return Guid.Parse(value);
        }
    }

    public class ColumnTypeDecimal : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "decimal";
        public IDataType CreateInstance()
        {
            return new ColumnTypeDecimal();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Decimal);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetDecimal(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return decimal.Parse(value);
        }
    }

    public class ColumnTypeVarbinary : ColumnTypeBase, IDataType
    {
        public virtual string DataTypeName { get; } = "varbinary";
        public virtual IDataType CreateInstance()
        {
            return new ColumnTypeVarbinary();
        }

        public virtual SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.VarBinary);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            var len = dr.GetBytes(ix, 0, null, 0, 0);
            byte[] buffer = new byte[len];
            dr.GetBytes(ix, 0, buffer, 0, (int)len);
            return buffer;
        }
        public override string ConvertToString(object value)
        {
            if (value == DBNull.Value || value == null)
                return "";
            return Convert.ToBase64String((byte[])value);
        }
        internal override object ReadValueObject(string value)
        {
            return Convert.FromBase64String(value);
        }
    }

    public class ColumnTypeImage : ColumnTypeVarbinary, IDataType
    {
        public override string DataTypeName { get; } = "image";
        public override IDataType CreateInstance()
        {
            return new ColumnTypeImage();
        }

        public override SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.VarBinary);
        }
    }
    public class ColumnTypeHierarchyId : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "hierarchyid";
        public IDataType CreateInstance()
        {
            return new ColumnTypeHierarchyId();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Variant);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetValue(ix);
        }
        internal override object ReadValueObject(string value)
        {
            //todo
            return value;
        }
    }

    public class ColumnTypeXml : ColumnTypeBase, IDataType
    {
        public string DataTypeName { get; } = "xml";
        public IDataType CreateInstance()
        {
            return new ColumnTypeXml();
        }

        public SqlParameter CreateParemeter(string parameterName)
        {
            return new SqlParameter(parameterName, System.Data.SqlDbType.Xml);
        }
        internal override object ReadValueObject(SqlDataReader dr, int ix)
        {
            return dr.GetSqlXml(ix);
        }
        internal override object ReadValueObject(string value)
        {
            return value;
        }
    }
}
