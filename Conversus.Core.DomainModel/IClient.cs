using System;

namespace Conversus.Core.DomainModel
{
    public enum ClientStatus
    {
        /// <summary>
        /// Отложенный
        /// </summary>
        Postponed = 1,

        /// <summary>
        /// на приеме
        /// </summary>
        Performing = 2,

        /// <summary>
        /// пришел и сидит в очереди с билетом
        /// </summary>
        Waiting = 3,

        /// <summary>
        /// зарегистрирован (импортирован с лотуса)
        /// </summary>
        Registered = 4,

        /// <summary>
        /// Обработан ОП
        /// </summary>
        Done = 5,

        /// <summary>
        /// неявка
        /// </summary>
        Absent = 6
    }

    public interface IClient : IEntity
    {
        string Name { get; set; }
        /// <summary>
        /// время записи
        /// </summary>
        DateTime BookingTime { get; set; }
        /// <summary>
        /// время изменения объекта
        /// </summary>
        DateTime ChangeTime { get; set; }

        ClientStatus Status { get; set; }

        int? PIN { get; set; }

        string Ticket { get; set; }

        Guid QueueId { get; set; }

        Guid? UserId { get; set; }
    }
}
