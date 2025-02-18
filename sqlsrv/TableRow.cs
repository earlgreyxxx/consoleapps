using Microsoft.Data.SqlClient;
using Dapper;
using System.Reflection;

namespace sqlsrv
{
  internal class TableRow<T>
  {
    private static Object? GetPropertyValue(string propName)
    {
      Type type = typeof(T);
      PropertyInfo? propInfo = type.GetProperty(propName);

      return propInfo?.GetValue(null);
    }

    public static IEnumerable<T>? Query(SqlConnection conn,string? additional = null,object? parameters = null)
    {
      string? value = (GetPropertyValue("SQL")?.ToString()) ?? throw new Exception("SQLプロパティがありません。");

      IEnumerable<T>? rv;
      if (string.IsNullOrEmpty(additional) && parameters != null)
      {
        rv = conn.Query<T>(value, buffered: false);
      }
      else
      {
        string sql = $"{value} {additional}";
        rv = conn.Query<T>(sql,parameters,buffered: false);
      }
      return rv;
    }

    public static int Insert(SqlConnection conn, T value)
    {
      bool? readOnly = (bool)(GetPropertyValue("ReadOnly") ?? throw new Exception("ReadOnlyプロパティがありません"));
      if (readOnly == true)
        throw new Exception("このオブジェクトはReadOnly属性です。");

      int rv = 0;
      return rv;
    }

    public static int Update(SqlConnection conn, object cv, string condition)
    {
      bool? readOnly = (bool)(GetPropertyValue("ReadOnly") ?? throw new Exception("ReadOnlyプロパティがありません"));
      if (readOnly == true)
        throw new Exception("このオブジェクトはReadOnly属性です。");

      int rv = 0;
      return rv;
    }

    public static int Delete(SqlConnection conn, string condition, object? param = null)
    {
      bool? readOnly = (bool)(GetPropertyValue("ReadOnly") ?? throw new Exception("ReadOnlyプロパティがありません"));
      if (readOnly == true)
        throw new Exception("このオブジェクトはReadOnly属性です。");

      int rv = 0;
      return rv;
    }
  }
}
