using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ProductivityTools.Learn.AzureFunctions
{
    public static class GenerateLicenseFile
    {
        [FunctionName("GenerateLicenseFile")]
        public static async Task Run(
            [QueueTrigger("orders", Connection = "AzureWebJobsStorage")]Order order,
            //            [Blob("licences/{rand-guid}.lic")]TextWriter outputBlob,
            IBinder binder,
            ILogger log)
        {

            var outputBlob = await binder.BindAsync<TextWriter>(
                new BlobAttribute($"licences/{order.Id}.lic")
                {
                    Connection="AzureWebJobsStorage"
                });

            outputBlob.WriteLine($"OrderName: {order.Name}");
            outputBlob.WriteLine($"Value: {order.Value}");
            log.LogInformation($"C# Queue trigger function processed: {order}");
        }
    }
}
