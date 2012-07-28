﻿using System.Collections.Generic;
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
    }
}
