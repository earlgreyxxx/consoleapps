using Microsoft.Data.SqlClient;
using Dapper;
using System.Reflection;

namespace sqlsrv
{
  internal class TableRow<T>
  {
    static TableRow()
    {
      SqlMapper.SetTypeMap(
        typeof(T),
        new CustomPropertyTypeMap(typeof(T), (type, column) => type.GetProperty(column.Replace("-", string.Empty)) ?? throw new Exception("プロパティーがありません"))
      );
    }

    private static object? GetPropertyValue(string propName)
    {
      return typeof(T).GetProperty(propName)?.GetValue(null);
    }

    public static IEnumerable<T>? Query(SqlConnection conn,string? additional = null,object? param = null)
    {
      string value = (GetPropertyValue("SQL")?.ToString()) ?? throw new Exception("SQLプロパティがありません。");

      IEnumerable<T>? rv;
      if (string.IsNullOrEmpty(additional) && param != null)
      {
        rv = conn.Query<T>(value, buffered: false);
      }
      else
      {
        string sql = $"{value} {additional}";
        rv = conn.Query<T>(sql,param,buffered: false);
      }
      return rv;
    }

    public static T? QuerySingle(SqlConnection conn,string? additional = null,object? param = null)
    {
      string value = (GetPropertyValue("SQL")?.ToString()) ?? throw new Exception("SQLプロパティがありません。");

      T rv;
      if (string.IsNullOrEmpty(additional) && param != null)
      {
        rv = conn.QuerySingle<T>(value);
      }
      else
      {
        string sql = $"{value} {additional}";
        rv = conn.QuerySingle<T>(sql, param);
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

    public static int Update(SqlConnection conn, string condition, object? param = null)
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

    public static IEnumerable<KeyValuePair<string?, object?>> GetEnumerator(T? inst)
    {
      return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(prop => new KeyValuePair<string?,object?>(prop?.Name,prop?.GetValue(inst)));
    }
  }
}
