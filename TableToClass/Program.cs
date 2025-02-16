using TableToClass.src;

namespace TableToClass
{
  /// <summary>
  /// </summary>
  internal class Program
  {
    static void Main(string[] args)
    {
      var template = new TableClass();
      template.hostName = "localhost,14330";
      template.databaseName = "tmweb@002";
      template.dbuser = "libpro001";
      template.loginName = $"{template.databaseName}${template.dbuser}";
      template.passwd = $"Ddk${template.dbuser}-PW";
      template.tables = ["stg_bibliography", "stg_collection"];

      Console.WriteLine(template.TransformText());
    }
  }
}
