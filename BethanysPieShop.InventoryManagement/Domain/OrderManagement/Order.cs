using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.OrderManagement
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderFulfillmentDate { get; private set; }
        public List<OrderItem> OrderItems { get; }
        public bool Fulfilled { get; set; } = false;

        public Order() 
        {
            Id = new Random().Next(9999999);

            int numberOfSeconds = new Random().Next(100);
            OrderFulfillmentDate = DateTime.Now.AddSeconds(numberOfSeconds);

            OrderItems = new();
        }

        public string ShowOrderDetails()
        {
            StringBuilder orderDetails = new();

            orderDetails.AppendLine($"Order ID: {Id}");
            orderDetails.AppendLine($"Order fulfillment date: {OrderFulfillmentDate.ToShortTimeString()}");

            if (OrderItems != null)
            {
                foreach (OrderItem item in OrderItems)
                {
                    orderDetails.AppendLine($"{item.ProductId}. {item.ProductName}: {item.AmountOrdered}");
                }
            }

            return orderDetails.ToString();
        }
    }
}
