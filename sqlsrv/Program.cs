using Microsoft.Data.SqlClient;

namespace sqlsrv
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var parameters = Utils.GetCommandlineArguments([..args]);
      var dict = parameters.Item1;

      string? xmlfile;
      if (dict.ContainsKey("f") && dict["f"] != null && File.Exists(dict["f"]))
        xmlfile = dict["f"];
      else
        xmlfile = ".database.xml";

      if (!File.Exists(xmlfile))
      {
        Console.WriteLine("設定ファイル(.database.xml)が見つかりません。");
        return;
      }

      var dbinfo = new SqlDatabaseInfo("akashi/kodomo",xmlfile);

      using var conn = new SqlConnection(dbinfo.ToString());
      conn.Open();

      var rows = TableRow<TmnCollection>.Query(conn);
      if (rows == null)
        return;

      foreach (var row in rows)
      {
        var props = TableRow<TmnCollection>.GetEnumerator(row);
        foreach (var prop in props)
        {
          Console.WriteLine($"{prop.Key?.ToString()} = {prop.Value?.ToString()}");
        }
        Console.WriteLine("\n-------------------------------------------------------");
      }
    }
  }
}
