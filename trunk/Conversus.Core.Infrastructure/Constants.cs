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

        public static readonly Dictionary<QueueType, string> QueueTypeLetters = new Dictionary<QueueType, string>
                                                                                   {
                                                                                       { QueueType.Approvement, "A" },
                                                                                       { QueueType.Taking, "B" }
                                                                                   };

        public const string VipQueueLetter = "C";
    }
}
