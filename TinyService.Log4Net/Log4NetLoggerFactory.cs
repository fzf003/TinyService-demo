using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure.CommonComposition;
using TinyService.Infrastructure.Log;

namespace TinyService.Log4Net
{
    [Component(IsSingleton = true)]
    public class Log4NetLoggerFactory : ILoggerFactory
    {

        public Log4NetLoggerFactory(string configFile = "log4net.config")
        {
            var file = new FileInfo(configFile);
            if (!file.Exists)
            {
                file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile));
            }

            if (file.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(file);
            }
            else
            {
                BasicConfigurator.Configure(new TraceAppender { Layout = new PatternLayout() });
            }
        }

        public ILogger Create(string name)
        {
            return new Log4NetLogger(LogManager.GetLogger(name));
        }

        public ILogger Create(Type type)
        {
            return new Log4NetLogger(LogManager.GetLogger(type));
        }
    }
}
