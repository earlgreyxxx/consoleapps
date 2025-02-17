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
      builder.DataSource = "10.1.1.103,14330";
      builder.InitialCatalog = "tmweb@002";
      builder.UserID = $"{builder.InitialCatalog}${schema}";
      builder.Password = $"Ddk${schema}-PW";
      builder.TrustServerCertificate = true;

      using var conn = new SqlConnection(builder.ConnectionString);
      string queryString = "SELECT * FROM stg_bibliography";

      var rows = conn.Query<stg_bibliography>(queryString);

      foreach (var row in rows)
      {
        Console.WriteLine($"{row.bib_book_title}／{row.bib_book_title_yomi}");
      }
    }
  }
}
