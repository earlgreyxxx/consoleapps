using Microsoft.Data.SqlClient;
using Dapper;
using System.Reflection;

namespace sqlsrv
{
  internal class TableRow<T>
  {
    public static string SQL { get; } = string.Empty;
    public static IEnumerable<T>? Query(SqlConnection conn,string? additional = null)
    {
      Type type = typeof(T);
      PropertyInfo? propertyInfo = type.GetProperty("SQL");
      string? value = propertyInfo?.GetValue(null)?.ToString();
      if (value == null)
        throw new Exception("SQLプロパティがありません。");

      IEnumerable<T>? rv = null;
      if (!string.IsNullOrEmpty(additional))
      {

      }
      else
      {
        rv = conn.Query<T>(value, buffered: false);
      }
      return rv;
    }
  }
}
