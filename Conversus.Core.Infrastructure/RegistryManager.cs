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

        public bool TryGetValue<T>(string key, out T value)
        {
            value = default(T);
            object val = ProgramKey.GetValue(key);
            if (val == null)
                return false;

            value = (T)val;
            return true;
        }

        public void SetValue<T>(string key, T value) where T: class
        {
            if (value == default(T))
                return;
            ProgramKey.SetValue(key, value, RegistryValueKind.Unknown);
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
