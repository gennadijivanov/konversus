using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Windows.Forms;
using Conversus.Core.Infrastructure;


namespace Conversus.ServiceHost.Installer
{
    [RunInstaller(true)]
    public partial class Installer : InstallerWithTerminalBase
    {
        public Installer()
        {
            InitializeComponent();

            //System.Diagnostics.Debugger.Launch();
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            var targetDir = Context.Parameters["targetDir"];

            MessageBox.Show(targetDir);

            //install host service
        }
    }
}
