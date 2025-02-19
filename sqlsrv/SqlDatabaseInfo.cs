using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace sqlsrv
{
  class SqlDatabaseInfo
  {
    private string _ConfPath = string.Empty;

    public string ConfPath
    {
      get
      {
        return _ConfPath;
      }
      private set
      {
        if (!File.Exists(value))
          throw new FileNotFoundException();

        _ConfPath = value;
      }
    }
    public string? DataSource { get; private set; }
    public string? InitialCatalog { get; private set; }
    public string? UserID { get; private set; }
    public string? Password { get; private set; }
    public bool TrustServerCertificate { get; private set; } = true;
    public string? Target { get; private set; }
    public Tuple<string?,IEnumerable<Table>?,IEnumerable<Query>?>? Model { get; private set; }

    public string ConnectionString
    {
      get
      {
        SqlConnectionStringBuilder builder = new()
        {
          DataSource = DataSource,
          InitialCatalog = InitialCatalog,
          UserID = UserID,
          Password = Password,
          TrustServerCertificate = TrustServerCertificate,
        };

        return builder.ConnectionString;
      }
    }

    public SqlDatabaseInfo(string target,string confPath = "")
    {
      if (string.IsNullOrEmpty(confPath))
      {
        string runPath = Assembly.GetExecutingAssembly().Location;
        string runDir = Path.GetDirectoryName(runPath) ?? string.Empty;

        confPath = Path.Combine(runDir, "conf","database.xml");
      }

      ConfPath = Path.GetFullPath(confPath);
      Load(target);
    }

    private void Load(string target)
    {
      target = target.Trim();
      if (string.IsNullOrEmpty(target))
        throw new Exception("targetの指定がありません");

      var xdoc = XDocument.Load(ConfPath) ?? throw new Exception("XMLファイルをロード出来ませんでした。");
      var elRoot = xdoc.Root ?? throw new Exception("ルート要素がありません");
      var elDatabases = elRoot.Elements("Database");
      var elDatabase = elDatabases.FirstOrDefault(el => el.Attribute("Target")?.Value == target) ?? throw new Exception("指定された対象が見つかりません");
      var elLogin = elDatabase?.Element("Login") ?? throw new Exception("認証要素が見つかりません");

      DataSource = elDatabase.Attribute("DataSource")?.Value ?? "";
      InitialCatalog = elDatabase.Attribute("InitialCatalog")?.Value ?? "";
      UserID = elLogin.Element("UserID")?.Value ?? string.Empty;
      Password = elLogin.Element("Password")?.Value ?? string.Empty;
      Target = elDatabase.Attribute("Target")?.Value;

      var models = elDatabase.Element("Models");
      var mname = models?.Attribute("Name")?.Value ?? string.Empty;
      var mpath = models?.Attribute("Path")?.Value ?? string.Empty;

      if (string.IsNullOrEmpty(mname) || string.IsNullOrEmpty(mpath))
        return;

      var confPathDir = Path.GetDirectoryName(ConfPath) ?? string.Empty;
      var modelpath = Path.Combine(confPathDir, mpath);

      if (File.Exists(modelpath))
        LoadModel(modelpath, mname);
    }

    private void LoadModel(string xmlpath, string name)
    {
      var xdoc = XDocument.Load(xmlpath);
      Model = xdoc.Descendants("Model").Select(el =>
      {
        var Tables = el.Element("Tables");
        var Queries = el.Element("Queries");
        return new Tuple<string?, IEnumerable<Table>?, IEnumerable<Query>?>(
          el.Attribute("Name")?.Value,
          Tables?.Elements("Table").Select(table => new Table() { Value = table.Value, Name = table.Attribute("ClassName")?.Value }),
          Queries?.Elements("Query").Select(query => new Query() { Value = query.Value, Name = query.Attribute("ClassName")?.Value })
        );
      }).FirstOrDefault(model => model.Item1 == name);
    }

    public override string ToString()
    {
      return ConnectionString;
    }
  }

  internal class Table
  {
    public required string Value { get; set; }
    public required string? Name { get; set; }
  }

  internal class Query
  {
    public required string Value { get; set; }
    public required string? Name { get; set; }
  }
}
