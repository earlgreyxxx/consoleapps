using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace DBFactory
{
  internal class Program
  {
    static void Main(string[] args)
    {
      DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
      //var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
      var fc = SqlClientFactory.Instance;
      //var rows = providers.Rows.Cast<DataRow>().Select(row => 


      Console.WriteLine(fc);
    }
  }
}
