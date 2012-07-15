using System.ComponentModel;
using Conversus.Core.Infrastructure;

namespace Conversus.AdminView.Installer
{
    [RunInstaller(true)]
    public partial class Installer : InstallerBase
    {
        public Installer()
        {
            InitializeComponent();
        }
    }
}
