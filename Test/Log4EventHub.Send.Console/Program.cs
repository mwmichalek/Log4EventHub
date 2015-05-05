using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Log4EventHub.Send.Console {
    class Program {
        static void Main(string[] args) {

            string eventHubConnectionString =
                "Endpoint=sb://tcfb-sb-events.servicebus.windows.net/;SharedAccessKeyName=eventlistener;SharedAccessKey=XpJIP0OZyvCkinuU+wwVUyhR0RFqrrCDWW6pPlulMrY=";
            //string eventHubConnectionString = "Endpoint=sb://tcfq-sb-events.servicebus.windows.net/;SharedAccessKeyName=eventlistener;SharedAccessKey=yfMvSpH8JIMLtvTX+Vy7F5lBaZbxlYLeWx2XgkkjQAE=";
            string eventHubName = "tcfb-eh-events";
            //string eventHubName = "tcfq-eh-events";

            var eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionString, eventHubName);
            for (int x = 0; x < 10; x++) {
                try {
                    var message = @"{""Key"": ""Value""}";

                    System.Console.WriteLine("{0} > Sending message: {1}", DateTime.Now.ToString("U"), message);
                    eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                } catch (Exception exception) {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("{0} > Exception: {1}", DateTime.Now.ToString("U"), exception.Message);
                    System.Console.ResetColor();
                }

                Thread.Sleep(1000);
            }


        }
    }
}
