namespace Conversus.Core.Infrastructure
{
    public class PropertyManager
    {
        private static PropertyManager _instance;
        public static PropertyManager Instance { get { return _instance ?? (_instance = new PropertyManager()); } }

        private PropertyManager()
        {
        }

        private const string _serviceHostRegistryKey = "serviceHost";
        private string _serviceHost;
        public string ServiceHost
        {
            get { return GetProperty(ref _serviceHost, _serviceHostRegistryKey, Constants.DefaultServiceHost); }
            set { SetProperty(ref _serviceHost, _serviceHostRegistryKey, value); }
        }

        private const string _terminalServiceHostRegistryKey = "terminalServiceHost";
        private string _terminalServiceHost;
        public string TerminalServiceHost
        {
            get { return GetProperty(ref _terminalServiceHost, _terminalServiceHostRegistryKey, Constants.DefaultTerminalServiceHost); }
            set { SetProperty(ref _terminalServiceHost, _terminalServiceHostRegistryKey, value); }
        }

        private const string _companyNameKey = "companyName";
        private string _companyName;
        public string CompanyName
        {
            get { return GetProperty(ref _companyName, _companyNameKey, null); }
            set { SetProperty(ref _companyName, _companyNameKey, value); }
        }

        private const string _licenseRegistryKey = "licenseKey";
        private string _licenseKey;
        public string LicenseKey
        {
            get { return GetProperty(ref _licenseKey, _licenseRegistryKey, null); }
            set { SetProperty(ref _licenseKey, _licenseRegistryKey, value); }
        }

        private T GetProperty<T>(ref T field, string regKey, T defaultValue) where T: class
        {
            if (field == default(T))
            {
                if (!RegistryManager.Instance.TryGetValue(regKey, out field))
                {
                    field = defaultValue;
                    RegistryManager.Instance.SetValue(regKey, field);
                }
            }
            return field;
        }

        private void SetProperty<T>(ref T field, string regKey, T value) where T: class
        {
            field = value;
            RegistryManager.Instance.SetValue(regKey, field);
        }
    }
}
