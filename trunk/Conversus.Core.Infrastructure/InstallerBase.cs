using System.Collections;

namespace Conversus.Core.Infrastructure
{
    public class InstallerBase : System.Configuration.Install.Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            var serviceHost = Context.Parameters["serviceHost"];
            PropertyManager.Instance.ServiceHost = serviceHost;
        }
    }

    public class InstallerWithTerminalBase : InstallerBase
    {
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            var terminalServiceHost = Context.Parameters["terminalHost"];
            PropertyManager.Instance.TerminalServiceHost = terminalServiceHost;
        }
    }
}
