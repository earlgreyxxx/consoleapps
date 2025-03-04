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

      string xmlfullpath = Path.GetFullPath(xmlfile);

      if (!File.Exists(xmlfullpath))
      {
        Console.WriteLine("設定ファイル(.database.xml)が見つかりません。");
        return;
      }

      var dbinfo = new SqlDatabaseInfo("akashi/kodomo",xmlfullpath);

      using var conn = new SqlConnection(dbinfo.ToString());
      conn.Open();

      var rows = TableRow<RoomStatus>.Query(conn,"WHERE [使用時間] IS NOT NULL AND [月日] BETWEEN @START AND @END ORDER BY j.[月日] ASC,[施設区分] ASC,[使用施設CD] ASC",new { START = "2024-12-01", END = "2025-02-28" });
      if (rows == null)
        return;

      foreach (var row in rows)
      {
        var props = TableRow<RoomStatus>.GetEnumerator(row);
        foreach (var prop in props)
        {
          Console.WriteLine($"{prop.Key?.ToString()} = {prop.Value?.ToString()}");
        }
        Console.WriteLine($"{new string('-', Console.WindowWidth)}"); 
      }
    }
  }
}
