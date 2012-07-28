using System;

namespace Conversus.Service.Contract
{
    [Serializable]
    public class OperatorInfo
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public QueueInfo Queue { get; set; }

        public string CurrentWindow { get; set; }
    }
}