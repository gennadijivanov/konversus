using System.Collections;
using System.IO;

namespace Conversus.Core.Infrastructure
{
    public class InstallerBase : System.Configuration.Install.Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            string configFilePath = Path.Combine(StripDir(Context.Parameters["AssemblyPath"]), "hostSettings.cfg");
            
            string serviceHost = Context.Parameters["serviceHost"];
            string terminalServiceHost = Context.Parameters["terminalHost"];
            string configBody = PropertyManager.Instance.GetConfigString(serviceHost, terminalServiceHost);

            File.WriteAllText(configFilePath, configBody);

            base.Install(stateSaver);
        }

        private static string StripDir(string fullPath)
        {
            return !string.IsNullOrEmpty(fullPath) 
                ? fullPath.Substring(0, fullPath.LastIndexOf(@"\"))
                : string.Empty;
        }
    }
}
