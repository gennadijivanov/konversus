using System.ComponentModel;
using Conversus.Core.Infrastructure;

namespace Conversus.TerminalView.Installer
{
    [RunInstaller(true)]
    public partial class Installer : InstallerWithTerminalBase
    {
        public Installer()
        {
            InitializeComponent();
        }
    }
}
