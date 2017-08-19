using System;
using System.Threading.Tasks;

namespace NetCoreLoggingInterface
{
    public interface IAppLogger
    {
        void Debug(string message);
        void Debug(string message, Exception exception);
        //Task DebugAsync(string message);
        //Task DebugAsync(string message, Exception exception);

        void Info(string message);
        void Info(string message, Exception exception);
        //Task InfoAsync(string message);
        //Task InfoAsync(string message, Exception exception);

        void Warn(string message);
        void Warn(string message, Exception exception);
        //Task WarnAsync(string message);
        //Task WarnAsync(string message, Exception exception);

        void Error(string message);
        void Error(string message, Exception exception);
        //Task ErrorAsync(string message);
        //Task ErrorAsync(string message, Exception exception);

        void Fatal(string message);
        void Fatal(string message, Exception exception);
        //Task FatalAsync(string message);
        //Task FatalAsync(string message, Exception exception);
    }
}
