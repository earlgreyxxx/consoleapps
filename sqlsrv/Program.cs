using Microsoft.Data.SqlClient;
using Dapper;

namespace sqlsrv
{
  internal class Program
  {
    static void Main(string[] args)
    {
      string schema = "libpro001";
      SqlConnectionStringBuilder builder = new();
      builder.DataSource = "localhost,14330";
      builder.InitialCatalog = "tmweb@002";
      builder.UserID = $"{builder.InitialCatalog}${schema}";
      builder.Password = $"Ddk${schema}-PW";
      builder.TrustServerCertificate = true;

      using var conn = new SqlConnection(builder.ConnectionString);
      //ShowSimple(conn);
      //ShowMulti(conn);

      foreach(var row in stg_bibliography.Query(conn))
        Console.WriteLine($"{row.bib_book_title}／{row.bib_book_title_yomi}");
    }

    private static void ShowSimple(SqlConnection conn)
    {
      string queryString = stg_bibliography.SQL;

      var rows = conn.Query<stg_bibliography>(queryString);

      foreach (var row in rows)
      {
        Console.WriteLine($"{row.bib_book_title}／{row.bib_book_title_yomi}");
      }
    }

    private static void ShowMulti(SqlConnection conn)
    {
      string queryString = $@"{Book.SQL} WHERE [bib_id] = @bibid";

      var rows = conn.Query<Book>(queryString, new { bibid = 1 }, buffered: false);

      foreach (var row in rows)
      {
        Console.WriteLine($"{row.bib_book_title}／{row.collection_book_code}／{row.bib_id}");
      }

    }
  }
}
