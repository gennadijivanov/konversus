using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Conversus.Core.Infrastructure
{
    [Serializable]
    public class ConfigModel
    {
        public string ServiceHost { get; set; }

        public string TerminalServiceHost { get; set; }
    }

    public class PropertyManager
    {
        private static PropertyManager _instance;
        public static PropertyManager Instance { get { return _instance ?? (_instance = new PropertyManager()); } }

        private readonly ConfigModel _config;

        private PropertyManager()
        {
            _config = GetConfig();

            if (string.IsNullOrWhiteSpace(_config.ServiceHost))
                _config.ServiceHost = Constants.DefaultServiceHost;

            if (string.IsNullOrWhiteSpace(_config.TerminalServiceHost))
                _config.TerminalServiceHost = Constants.DefaultTerminalServiceHost;
        }

        public string ServiceHost
        {
            get { return _config.ServiceHost; }
        }

        public string TerminalServiceHost
        {
            get { return _config.TerminalServiceHost; }
        }

        public ConfigModel GetConfig()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigModel));

            XmlReaderSettings settings = new XmlReaderSettings();

            try
            {
                using (var textReader = File.OpenText("hostSetting.cfg"))
                {
                    using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                    {
                        return (ConfigModel)serializer.Deserialize(xmlReader);
                    }
                }
            }
            catch
            {
                return new ConfigModel();
            }
        }

        public string GetConfigString(string serviceHost, string terminalServiceHost)
        {
            var config = new ConfigModel
                             {
                                 ServiceHost = serviceHost,
                                 TerminalServiceHost = terminalServiceHost
                             };

            XmlSerializer serializer = new XmlSerializer(typeof(ConfigModel));

            XmlWriterSettings settings = new XmlWriterSettings
                                             {
                                                 Encoding = new UnicodeEncoding(false, false), // no BOM in a .NET string
                                                 Indent = true,
                                                 OmitXmlDeclaration = false
                                             };

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, config);
                }
                return textWriter.ToString();
            }
        }
    }
}
