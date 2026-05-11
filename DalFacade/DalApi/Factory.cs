using System.Reflection;
using System.IO;

namespace DalApi;
using static DalApi.DalConfig;

public static class Factory
{
    public static IDal Get
    {
        get
        {
            string dalType = s_dalName ?? throw new DalConfigException($"DAL name is not extracted from the configuration");

            // safer retrieval to avoid KeyNotFoundException
            if (!s_dalPackages.TryGetValue(dalType, out string? dal) || string.IsNullOrWhiteSpace(dal))
            {
                throw new DalConfigException($"Package for {dalType} is not found in packages list in dal-config.xml");
            }

            Exception? loadException = null;
            bool loaded = false;
            try
            {
                // try normal load first (original behavior)
                Assembly.Load(dal);
                loaded = true;
            }
            catch (Exception ex)
            {
                loadException = ex;
            }

            if (!loaded)
            {
                // fallback: try to load the assembly file from the app base directory
                var dllPath = Path.Combine(AppContext.BaseDirectory, dal + ".dll");
                if (File.Exists(dllPath))
                {
                    try
                    {
                        Assembly.LoadFrom(dllPath);
                        loaded = true;
                    }
                    catch (Exception ex2)
                    {
                        // ensure we never pass a null to AggregateException
                        throw new DalConfigException(
                            $"Failed to load {dal}.dll from {dllPath}",
                            new AggregateException(loadException ?? new Exception("No prior load exception"), ex2));
                    }
                }
                else
                {
                    // ensure we never pass a null inner exception to DalConfigException
                    throw new DalConfigException(
                        $"Failed to load {dal}.dll package",
                        loadException ?? new Exception("Assembly package not found and no prior exception"));
                }
            }

            Type type = Type.GetType($"Dal.{dal}, {dal}") ??
                        throw new DalConfigException($"Class Dal.{dal} was not found in {dal}.dll");

            return type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?.GetValue(null) as IDal ??
                   throw new DalConfigException($"Class {dal} is not a singleton or wrong property name for Instance");
        }
    }
}