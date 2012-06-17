using System.ServiceModel;

namespace Conversus.Service.Helpers
{
    public class ClientServiceBase<T>
    {
        static T Channel;
        static bool Initialized = false;
        public bool UseWcf;

        public static T GetClient(string endPoint)
        {
            if (Initialized)
                return Channel;

            var factory = new ChannelFactory<T>(endPoint);
            Channel = factory.CreateChannel();

            Initialized = true;

            return Channel;
        }
    }
}