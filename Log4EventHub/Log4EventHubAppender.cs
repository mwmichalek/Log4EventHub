using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender;
using log4net.Core;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Log4EventHub
{
    public class Log4EventHubAppender : AppenderSkeleton {

        private LoggingEventSerializer _serializer;

        private EventHubClient _eventHubClient;

        public string ApplicationName { get; set; }

        public string ConnectionString { get; set; }

        public string EventHubName { get; set; }

        static Log4EventHubAppender() 
        {
            
        }


        public override void ActivateOptions() 
        {
            _eventHubClient = EventHubClient.CreateFromConnectionString(ConnectionString, EventHubName);
            _serializer = new LoggingEventSerializer {
                ApplicationName = ApplicationName
            };
        }

        protected override void Append(LoggingEvent loggingEvent) 
        {
            var content = _serializer.SerializeLoggingEvents(new[] { loggingEvent });

            var eventData = new EventData(Encoding.UTF8.GetBytes(content)) {
                PartitionKey = "0"
            };

            _eventHubClient.SendAsync(eventData);
        }
    }
}
