using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace sqlsrv
{
  using Table = KeyValuePair<string?, string>;
  using Query = KeyValuePair<string?, string>;

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

      var xdoc = XDocument.Load(ConfPath) ?? throw new FileNotFoundException(ConfPath);
      var elRoot = xdoc.Root ?? throw new Exception("ルート要素がありません");

      var elDatabase = elRoot.XPathSelectElement($"./Database[@Target=\"{target}\"]") ?? throw new Exception("指定された対象が見つかりません");
      var elLogin = elDatabase.XPathSelectElement("./Login") ?? throw new Exception("認証要素が見つかりません");

      DataSource = elDatabase.Attribute("DataSource")?.Value ?? "";
      InitialCatalog = elDatabase.Attribute("InitialCatalog")?.Value ?? "";
      UserID = elLogin.XPathSelectElement("./UserID")?.Value ?? string.Empty;
      Password = elLogin.XPathSelectElement("./Password")?.Value ?? string.Empty;
      Target = elDatabase.Attribute("Target")?.Value;

      var models = elDatabase.Element("Models");
      if (models == null)
        return;

      string mname = models?.Attribute("Name")?.Value ?? target;
      string mpath = models?.Attribute("Path")?.Value ?? string.Empty;

      if (string.IsNullOrEmpty(mpath))
      {
        LoadModel(models, mname);
      }
      else
      {
        var confPathDir = Path.GetDirectoryName(ConfPath) ?? string.Empty;
        var modelpath = Path.Combine(confPathDir, mpath);

        if (!File.Exists(modelpath))
          throw new FileNotFoundException(modelpath);

        LoadModel(modelpath, mname);
      }
    }

    private void LoadModel(XElement? Models,string name)
    {
      if (Models == null)
        return;

      var model = Models?.XPathSelectElement($"Model[@Name=\"{name}\"]");
      var Tables = model?.Element("Tables");
      var Queries = model?.Element("Queries");
      Model = new Tuple<string?, IEnumerable<Table>?, IEnumerable<Query>?>(
        model?.Attribute("Name")?.Value,
        Tables?.Elements("Table").Select(table => new Table(table.Attribute("ClassName")?.Value,table.Value)),
        Queries?.Elements("Query").Select(query => new Query(query.Attribute("ClassName")?.Value, query.Value))
      );
    }

    private void LoadModel(string xmlpath, string name)
    {
      var xdoc = XDocument.Load(xmlpath);
      LoadModel(xdoc.Root ?? throw new Exception("Models要素がありません。"), name);
    }

    public override string ToString()
    {
      return ConnectionString;
    }
  }
}
