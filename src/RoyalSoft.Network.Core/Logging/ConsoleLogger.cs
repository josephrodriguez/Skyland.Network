using System;

namespace RoyalSoft.Network.Core.Logging
{
    class ConsoleLogger : ILog
    {
        public void Trace(string format, params object[] parameters)
        {
        }

        public void Debug(string format, params object[] parameters)
        {
        }

        public void Info(string format, params object[] parameters)
        {
        }

        public void Warn(string format, params object[] parameters)
        {
        }

        public void Error(string format, params object[] parameters)
        {
        }

        public void Error(Exception exception)
        {
        }

        private void Write(string message)
        {
            
        }
    }
}
