using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
//using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace RelJEnterpriseEventsApp
{
    public static class ProcessEvents
    {
        [FunctionName("ProcessEvents")]
        public static async Task Run([EventHubTrigger("reljev", Connection = @"EventHubConnectionString")] EventData[] events,  ILogger log)
        {
            var exceptions = new List<Exception>();            

            foreach (EventData eventData in events)
            {
                try
                {

                    string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                    log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");
                    await Task.Yield();
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }
            
        }
    }
}
