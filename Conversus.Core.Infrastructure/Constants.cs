using System.Collections.Generic;
using Conversus.Core.DomainModel;

namespace Conversus.Core.Infrastructure
{
    public static class Constants
    {
        public const string AdminLogin = "admin";

        public const string AdminPassword = "admin";

        public static readonly Dictionary<QueueType, string> QueueTypeTitles = new Dictionary<QueueType, string>
                                                                                   {
                                                                                       { QueueType.Approvement, "Согласование" },
                                                                                       { QueueType.Taking, "Получение" }
                                                                                   };
    }
}
