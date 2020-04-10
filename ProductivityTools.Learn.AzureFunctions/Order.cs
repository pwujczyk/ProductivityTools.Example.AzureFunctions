using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Learn.AzureFunctions
{
    public class Order
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }

        public int Id { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
