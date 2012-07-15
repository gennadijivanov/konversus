using System.Collections;

namespace Conversus.Core.Infrastructure
{
    public class InstallerBase : System.Configuration.Install.Installer
    {
        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            var serviceHost = Context.Parameters["serviceHost"];
            PropertyManager.Instance.ServiceHost = serviceHost;
        }
    }

    public class InstallerWithTerminalBase : InstallerBase
    {
        //[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            var terminalServiceHost = Context.Parameters["terminalHost"];
            PropertyManager.Instance.TerminalServiceHost = terminalServiceHost;
        }
    }
}
