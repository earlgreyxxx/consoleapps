using System.Text.RegularExpressions;

namespace sqlsrv
{
  using OptionMap = Dictionary<string, string>;
  using OptionList = List<string>;

  public static class Utils
  {
    /// <summary>
    /// 起動オプションをパースして返します。
    /// <code>hoge -p 10 --input-file=c:\hoge.txt --output-path .\ process1 process2 -x </code>を処理すると以下を返します
    /// <code>tuple(
    ///     Item1: { "p": "10","input-file": "c:\\hoge.txt","output-path": ".\\","x": "" }
    ///     Item2: [ "process1","process2" ]
    ///   )</code>
    /// </summary>
    /// <returns>Tuple&lt;map,list&gt;: map:Dictionary&lt;string,string&gt;、list:List&lt;string&gt;</returns>
    public static Tuple<OptionMap, OptionList> GetCommandlineArguments(List<string>? parameters = null)
    {
      OptionMap map = [];
      OptionList list = [];

      // 起動時のコマンド(いわゆるargv[0]は考えない)
      if (parameters == null || parameters.Count <= 0)
        parameters = Environment.GetCommandLineArgs().Skip(1).ToList();

      int pos = 0;
      while (pos < parameters.Count)
      {
        string param1 = parameters[pos++];

        Match m = Regex.Match(param1, @"^-{1,2}(?<key>[\w\-]+)(?:=(?<value>.+))?");
        if (!m.Success)
        {
          list.Add(param1);
          continue;
        }

        string key = m.Groups["key"].Value;
        string param2 = m.Groups["value"].Value;
        if (param2 != string.Empty)
        {
          map[key] = param2;
          continue;
        }

        if (pos < parameters.Count && !parameters[pos].StartsWith('-'))
          param2 = parameters[pos++];

        map[key] = param2;
      }

      return new Tuple<OptionMap, OptionList>(map, list);
    }
  }
}
