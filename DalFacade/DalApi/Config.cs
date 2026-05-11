using System.Xml.Linq;
using DalApi;

namespace DalApi;


static class DalConfig
{
    internal static string s_dalName;
    internal static Dictionary<string, string> s_dalPackages;

    static DalConfig()
    {
        // try to find dal-config.xml by walking up from base directory
        var baseDir = AppContext.BaseDirectory;
        string? configPath = null;
        for (int i = 0; i < 8; i++)
        {
            var candidate = Path.GetFullPath(Path.Combine(baseDir, "..", "xml", "dal-config.xml"));
            if (File.Exists(candidate)) { configPath = candidate; break; }
            baseDir = Path.GetFullPath(Path.Combine(baseDir, ".."));
        }
        if (configPath == null) configPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "xml", "dal-config.xml"));

        XElement dalConfig;
        try
        {
            dalConfig = XElement.Load(configPath);
        }
        catch (Exception ex)
        {
            throw new DalConfigException("dal-config.xml file is not found", ex);
        }

        s_dalName = dalConfig.Element("dal")?.Value ?? throw new DalConfigException("<dal> element is missing");

        var packages = dalConfig.Element("dal-packages")?.Elements() ?? throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);
    }
}
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }



