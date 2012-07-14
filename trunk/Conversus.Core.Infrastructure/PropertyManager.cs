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

        private string GetProperty(ref string field, string regKey, string defaultValue)
        {
            if (string.IsNullOrEmpty(field))
            {
                if (!RegistryManager.Instance.TryGetValue(regKey, out field))
                {
                    field = defaultValue;
                    RegistryManager.Instance.SetValue(regKey, field);
                }
            }
            return field;
        }

        private void SetProperty(ref string field, string regKey, string value)
        {
            field = value;
            RegistryManager.Instance.SetValue(regKey, field);
        }
    }
}
