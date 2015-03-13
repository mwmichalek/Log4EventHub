using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using Microsoft.ServiceBus;
using Xunit;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Log4EventHubTest
{
    public class LoggerTests {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (LoggerTests));

        public LoggerTests() {
            var config = new FileInfo("log4net.config");
            XmlConfigurator.Configure(config);
        }

        [Fact]
        public void LoadLogger()
        {
            foreach (var number in Enumerable.Range(0, 100)) {
                Logger.InfoFormat("Holy crap, this worked {0}!", number);
                Thread.Sleep(5000);
            }
            Assert.True(true);

        }

    }

}
