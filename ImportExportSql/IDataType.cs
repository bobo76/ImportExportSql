using System.Data.SqlClient;

namespace ImportExportSql
{
    public interface IDataType
    {
        string DataTypeName { get; }
        bool AllowNull { get; set; }

        IDataType CreateInstance();
        SqlParameter CreateParemeter(string parameterName);
        object ReadValue(SqlDataReader dr, int ix);
        object ReadValue(string value);
        string ConvertToString(object value);
    }
}
