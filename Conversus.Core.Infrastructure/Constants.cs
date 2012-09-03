using System.Collections.Generic;
using Conversus.Core.DomainModel;

namespace Conversus.Core.Infrastructure
{
    public static class Constants
    {
        public const string AdminLogin = "admin";

        public const string DefaultAdminPassword = "admin";

        internal const string DefaultServiceHost = "http://localhost:8080/";

        internal const string DefaultTerminalServiceHost = "http://localhost:8180/";

        public static class Endpoints
        {
            public const string TerminalService = "TerminalService";

            public const string ClientService = "ClientService";

            public const string QueueService = "QueueService";

            public const string OperatorService = "OperatorService";

            public const string PropertyService = "PropertyService";

            public const string ReportService = "ReportService";
        }

        public static class Properties
        {
            public const string Company = "Company";

            public const string LicenseKey = "LicenseKey";
        }

        public static readonly Dictionary<QueueType, string> QueueTypeTitles =
            new Dictionary<QueueType, string>
                {
                    {QueueType.Approvement, "Сдача документов на согласование проектной документации"},
                    {QueueType.Taking, "Сдача документов на получение технических условий"}
                };

        public static readonly Dictionary<QueueType, string> QueueTypeLetters =
            new Dictionary<QueueType, string>
                {
                    {QueueType.Approvement, "A"},
                    {QueueType.Taking, "B"}
                };

        public const string VipQueueLetter = "C";

        public static readonly Dictionary<ClientStatus, string> ClientStatusTitles =
            new Dictionary<ClientStatus, string>
                {
                    {ClientStatus.Absent, "Неявка"},
                    {ClientStatus.Done, "Обработан"},
                    {ClientStatus.Performing, "Обслуживается"},
                    {ClientStatus.Postponed, "Отложен"},
                    {ClientStatus.Registered, "Зарегистрирован"},
                    {ClientStatus.Waiting, "Ожидает в очереди"},
                };
    }
}
