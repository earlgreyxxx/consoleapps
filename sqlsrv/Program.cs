using Microsoft.Data.SqlClient;
using Dapper;
using System.Xml.Linq;

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

      var dbinfo = new SqlDatabaseInfo("tmweb",xmlfile);

      using var conn = new SqlConnection(dbinfo.ToString());

      var row = TableRow<LentalInfo>.Query(conn, "WHERE [collection_id] = @collection_id", new { collection_id = 121 })?.FirstOrDefault();
      Console.WriteLine($"{row?.collection_book_code}:{row?.bib_book_title}");
    }
  }
}
