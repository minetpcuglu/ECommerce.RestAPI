using System;
using System.IO;
using System.Reflection;
using System.Xml;
using log4net;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace ECommerce.Core.CrossCuttingConcerns.Logging.Log4Net
{
   public class LoggerServiceBase
   {
       private ILog _log;

       public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(@File.OpenRead(@"log4net.config"));
            ILoggerRepository loggerRepository =
                LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);
            _log = LogManager.GetLogger(loggerRepository.Name, name);


        }

        public bool IsInfoEnabled => _log.IsInfoEnabled;
        public bool IsDebugEnabled => _log.IsDebugEnabled;
        public bool IsErrorEnabled => _log.IsErrorEnabled;
        public bool IsFatalEnabled => _log.IsFatalEnabled;
        public bool IsWarnEnabled => _log.IsWarnEnabled;

        public void Info(object logMessage)
        {
            if(IsInfoEnabled)
            {
                _log.Info(logMessage);
            }
        }
        public void Error(object logMessage)
        {
            if (IsErrorEnabled)
            {
                _log.Error(logMessage);
            }
        }
        public void Fatal(object logMessage)
        {
            if (IsFatalEnabled)
            {
                try
                {
                    _log.Fatal(logMessage);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        public void Debug(object logMessage)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(logMessage);
            }
        }
        public void Warn(object logMessage)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(logMessage);
            }
        }
    }
}
