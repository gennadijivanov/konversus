using System;

namespace Conversus.Core.DomainModel
{
    public enum ClientStatus
    {
        /// <summary>
        /// Отложенный
        /// </summary>
        Delayed,
        /// <summary>
        /// на приеме
        /// </summary>
        Performing,
        /// <summary>
        /// пришел и сидит в очереди с билетом
        /// </summary>
        Waiting,
        /// <summary>
        /// зарегистрирован (импортирован с лотуса)
        /// </summary>
        Registered,
        /// <summary>
        /// Обработан ОП
        /// </summary>
        Done,
        /// <summary>
        /// неявка
        /// </summary>
        Absent
    }

    public interface IClient : IEntity
    {
        string Name { get; set; }
        /// <summary>
        /// время записи
        /// </summary>
        DateTime BookingTime { get; set; }
        /// <summary>
        /// время получения талона
        /// </summary>
        DateTime? TakeTicket { get; set; }
        /// <summary>
        /// время начала обработки оператором
        /// </summary>
        DateTime? PerformStart { get; set; }
        /// <summary>
        /// время окончания обработки оператором
        /// </summary>
        DateTime? PerformEnd { get; set; }

        ClientStatus Status { get; set; }

        int? PIN { get; set; }

        string Ticket { get; set; }

        Guid QueueId { get; set; }
    }
}
