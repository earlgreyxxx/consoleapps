using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace sqlsrv
{
  internal class SQLDatabase
  {
    private SqlConnection Conn;
    public SQLDatabase(string hostname,string database,string loginname,string password)
    {
      string connectionString = $"Data Source={hostname};Initial Catalog={database};User ID={loginname};Password={password}";
      Conn = new SqlConnection(connectionString);
      Conn.Open();
    }

    ~SQLDatabase()
    {
      Conn?.Dispose();
    }
  }
}