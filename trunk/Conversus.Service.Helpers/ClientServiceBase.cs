using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using Conversus.Service.Impl;

namespace Conversus.Service.Helpers
{
    public class ClientServiceBase<T>
    {
        static T Channel;
        static bool Initialized = false;

        public static T GetClient(string endPoint)
        {
            if (Initialized)
                return Channel;

            const bool isJoined = false;

            if (isJoined)
            {
                Assembly implementationAssembly = Assembly.GetAssembly(typeof(QueueService));
                var types = implementationAssembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(T)) && type.IsClass)
                    {
                        ConstructorInfo ci = type.GetConstructor(new Type[] { });
                        Channel = (T)ci.Invoke(new Object[] { });
                    }
                }
            }
            else
            {
                var factory = new ChannelFactory<T>(endPoint);
                Channel = factory.CreateChannel();
            }
            Initialized = true;

            return Channel;
        }
    }
}