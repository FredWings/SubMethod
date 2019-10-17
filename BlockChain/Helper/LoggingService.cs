using System;
using System.Globalization;
using log4net;

namespace BlockChain.Helper
{
    sealed class Log
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Log));
        
        public static void Debug(object message)
        {
            log.Debug(message);
        }

        public static void DebugFormatted(string format, params object[] args)
        {
            log.DebugFormat(CultureInfo.InvariantCulture, format, args);
        }

        public static void Info(object message)
        {
            log.Info(message);
        }

        public static void InfoFormatted(string format, params object[] args)
        {
            log.InfoFormat(CultureInfo.InvariantCulture, format, args);
        }

        public static void Warn(object message)
        {
            log.Warn(message);
        }

        public static void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public static void WarnFormatted(string format, params object[] args)
        {
            log.WarnFormat(CultureInfo.InvariantCulture, format, args);
        }

        public static void Error(object message)
        {
            log.Error(message);
        }

        public static void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        public static void ErrorFormatted(string format, params object[] args)
        {
            log.ErrorFormat(CultureInfo.InvariantCulture, format, args);
        }

        public static void Fatal(object message)
        {
            log.Fatal(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }

        public static void FatalFormatted(string format, params object[] args)
        {
            log.FatalFormat(CultureInfo.InvariantCulture, format, args);
        }

        public static bool IsDebugEnabled
        {
            get
            {
                return log.IsDebugEnabled;
            }
        }

        public static bool IsInfoEnabled
        {
            get
            {
                return log.IsInfoEnabled;
            }
        }

        public static bool IsWarnEnabled
        {
            get
            {
                return log.IsWarnEnabled;
            }
        }

        public static bool IsErrorEnabled
        {
            get
            {
                return log.IsErrorEnabled;
            }
        }

        public static bool IsFatalEnabled
        {
            get
            {
                return log.IsFatalEnabled;
            }
        }
    }
}
