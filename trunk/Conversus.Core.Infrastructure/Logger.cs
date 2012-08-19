using System;
using System.IO;
using System.Text;

namespace Conversus.Core.Infrastructure
{
    public static class Logger
    {
        private const string FileName = "log.log";

        public static void Log(string message)
        {
            Write(message);
        }

        public static void Log(Exception exception)
        {
            Write(string.Format("Exception. Message: {0}\r\nSource: {1}\r\nStack: {2}",
                exception.Message, exception.Source, exception.StackTrace));
        }

        private static void Write(string message)
        {
            message = string.Format("\r\n{0}:\t{1}", DateTime.Now, message);
            File.AppendAllText(FileName, message, Encoding.UTF8);
        }
    }
}
