using System.Collections;
using System.IO;

namespace Conversus.Core.Infrastructure
{
    public class InstallerBase : System.Configuration.Install.Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            
            string serviceHost = Context.Parameters["serviceHost"];
            string terminalServiceHost = Context.Parameters["terminalHost"];

            File.WriteAllText(Path.Combine(StripDir(Context.Parameters["AssemblyPath"]), "hostSettings.cfg"),
                PropertyManager.Instance.GetConfigString(serviceHost, terminalServiceHost));
        }

        private static string StripDir(string fullPath)
        {
            return !string.IsNullOrEmpty(fullPath) 
                ? fullPath.Substring(0, fullPath.LastIndexOf(@"\"))
                : string.Empty;
        }
    }
}
