using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ProductivityTools.Learn.AzureFunctions
{
    public static class OnPaymentReceived
    {
        [FunctionName("OnPaymentReceived")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Queue("orders")]IAsyncCollector<Order> orderQuene,
            [Table("orders")]IAsyncCollector<Order> orderTable,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var order= JsonConvert.DeserializeObject<Order>(requestBody);

            order.PartitionKey = "orderspk";
            order.RowKey = order.Id.ToString();
            await orderQuene.AddAsync(order);
            await orderTable.AddAsync(order);

            log.LogInformation($"Order {order.Name} received");

            return new OkObjectResult("Thank you for the order");
        }
    }
}
