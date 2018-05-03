using System.Data.SqlClient;

namespace ImportExportSql
{
    public interface IDataType
    {
        string DataTypeName { get; }

        IDataType CreateInstance();
        SqlParameter CreateParemeter(string parameterName);
        object ReadValue(SqlDataReader dr, int ix);
        object ReadValue(string value, bool allowNull);
        string ConvertToString(object value);
    }
}
