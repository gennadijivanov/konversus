using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Conversus.Service.Impl;

namespace Conversus.Service.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Type serviceType = typeof(ClientService);
            
            using (ServiceHost host = new ServiceHost(serviceType))
            {
                host.Open();

                Console.WriteLine();
                Console.WriteLine("Press <ENTER> to terminate Host");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
