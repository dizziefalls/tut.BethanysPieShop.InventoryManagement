using BethanysPieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;

        protected int maxItemsInStock = 0;


        public int Id { get { return id; } set { id = value; } }

        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                    description = string.Empty;
                else
                    description = value.Length > 250 ? value[..250] : value;
            }
        }

        public UnitType UnitType { get; set; }
        public int AmountInStock { get; protected set; }
        public bool IsBelowStockThreshold { get; protected set; }
        public Price Price { get; set; }

        public Product(int id) : this(id, string.Empty)
        {
        }

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            UnitType = unitType;

            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }


        public virtual void UseProduct(int items)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
                UpdateLowStock();
                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items in stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }

        public virtual void IncreaseStock()
        {
            AmountInStock++;
        }

        public virtual void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;

            if (newStock < maxItemsInStock)
            {
                AmountInStock = newStock;
            }
            else
            {
                AmountInStock = maxItemsInStock; // overstock catch
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordered that couldn't be stored.");
            }

            if (AmountInStock > StockThreshold)
            {
                IsBelowStockThreshold = false;
            }
        }

        protected virtual void DecreaseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock();

            Log(reason);
        }

        public virtual string DisplayDetailsShort()
        {
            return $"{Id}, {Name} \n{AmountInStock} items in stock.";
        }

        public virtual string DisplayDetailsFull()
        {
            return DisplayDetailsFull("");
        }

        public virtual string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new();
            // TODO: add price
            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

            sb.Append(extraDetails);

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();
        }
    }
}
