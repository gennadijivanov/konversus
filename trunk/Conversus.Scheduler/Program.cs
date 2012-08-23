using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Conversus.Core.DomainModel;
using Conversus.Core.Infrastructure;
using Newtonsoft.Json;
using Conversus.Service.Helpers;

namespace Conversus.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IEnumerable<QueueType> queueTypes = Enum.GetValues(typeof (QueueType)).Cast<QueueType>();

                foreach (QueueType queueType in queueTypes)
                {
                    CreateClients(queueType, GetPureData(queueType));  
                }
                Logger.Log("Data from Lotus is imported");

                ServiceHelper.Instance.ClientService.SetAllRegisteredAsAbsent();
                Logger.Log("All absent are marked as absent");
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                Logger.Log(exc);
            }
        }

        private static void CreateClients(QueueType qType, PureResponse pureData)
        {
            foreach (var pureDayField in pureData.TakeRecep)
            {
                if (pureDayField.times == null)
                    continue;

                int year = int.Parse(pureDayField.date.Substring(0, 4));
                int month = int.Parse(pureDayField.date.Substring(4, 2));
                int day = int.Parse(pureDayField.date.Substring(6));
                DateTime dayDate = new DateTime(year, month, day);

                foreach (var client in pureDayField.times)
                {
                    var timeFields = client.time.Split(":".ToCharArray()).Select(int.Parse).ToList();
                    DateTime bookingTime = dayDate.Add(new TimeSpan(0, timeFields[0], timeFields[1]));
                    ServiceHelper.Instance.ClientService.CreateFromLotus(
                        client.FIO, client.TurnPIN, qType, bookingTime);
                }
            }
        }

        private static PureResponse GetPureData(QueueType qType)
        {
            JsonReader jr = new JsonTextReader(new StreamReader(ConfigurationManager.AppSettings[qType.ToString()]));
            return (new JsonSerializer()).Deserialize<PureResponse>(jr);
        }
    }
}
