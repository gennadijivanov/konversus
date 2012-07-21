using System.Collections;
using System.ComponentModel;
using Conversus.Core.Infrastructure;


namespace Conversus.ServiceHost.Installer
{
    [RunInstaller(true)]
    public partial class Installer : InstallerWithTerminalBase
    {
        public Installer()
        {
            InitializeComponent();
        }

        //public override void Uninstall(IDictionary savedState)
        //{
        //    base.Uninstall(savedState);
        //    serviceProcessInstaller1.Uninstall(savedState);
        //    serviceInstaller1.Uninstall(savedState);
        //}
    }
}
