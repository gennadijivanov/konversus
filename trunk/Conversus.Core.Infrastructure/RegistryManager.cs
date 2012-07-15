using Microsoft.Win32;

namespace Conversus.Core.Infrastructure
{
    internal class RegistryManager
    {
        const string ConversusKeyName = @"Software\IMSMedia\SUO";

        private static RegistryManager _instance;
        public static RegistryManager Instance { get { return _instance ?? (_instance = new RegistryManager()); } }

        private RegistryManager()
        {
        }

        public bool TryGetValue(string key, out string value)
        {
            value = null;
            object val = ProgramKey.GetValue(key);
            if (val == null)
                return false;

            value = (string)val;
            return true;
        }

        public void SetValue(string key, string value)
        {
            ProgramKey.SetValue(key, value, RegistryValueKind.String);
        }

        private RegistryKey _programKey;
        private RegistryKey ProgramKey
        {
            get { return _programKey ?? (_programKey = GetKey()); }
        }

        private RegistryKey GetKey()
        {
            var rootKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            return rootKey.CreateSubKey(ConversusKeyName,
                RegistryKeyPermissionCheck.ReadWriteSubTree);
        }
    }
}
