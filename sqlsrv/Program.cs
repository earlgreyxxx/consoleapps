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
      var xdocument = XDocument.Load(xmlfile) ?? throw new Exception("XMLファイルをロード出来ませんでした。");
      var elRoot = xdocument.Root ?? throw new Exception("XMLファイルの解析に失敗しました。");
      var elDatabases = elRoot.Elements("Database") ?? throw new Exception("Database要素が見つかりません。");
      var elDatabase = elDatabases.First(el => el.Attribute("Target")?.Value == "tmweb");
      var elLogin = elDatabase.Element("Login") ?? throw new Exception("認証要素が見つかりません");

      SqlConnectionStringBuilder builder = new()
      {
        DataSource = elDatabase.Attribute("DataSource")?.Value ?? "",
        InitialCatalog = elDatabase.Attribute("InitialCatalog")?.Value ?? "",
        UserID = elLogin.Element("UserID")?.Value ?? string.Empty,
        Password = elLogin.Element("Password")?.Value ?? string.Empty,
        TrustServerCertificate = true
      };

      using var conn = new SqlConnection(builder.ConnectionString);

      var row = TableRow<Book>.Query(conn, "WHERE [collection_id] = @collection_id", new { collection_id = 121 })?.FirstOrDefault();
      Console.WriteLine($"{row?.collection_book_code}:{row?.bib_book_title}");
    }
  }
}
