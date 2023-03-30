﻿using BethanysPieShop.InventoryManagement.Domain.Contracts;
using BethanysPieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public class FreshProduct : Product, ISaveable
    {
        public DateTime ExpiryDateTime { get; set; }
        public string? StorageInstructions { get; set; }

        public FreshProduct(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock) : base(id, name, description, price, unitType, maxAmountInStock)
        {
        }

        public override void IncreaseStock()
        {
            AmountInStock++;
        }

        public override string DisplayDetailsFull()
        {
            StringBuilder sb = new();
            // TODO: add price
            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            sb.Append("Storage instructions: " + StorageInstructions);
            sb.Append("Expiry date: " + ExpiryDateTime.ToShortDateString());

            return sb.ToString();
        }

        public string ConvertToStringForSaving()
        {
            return $"{Id};{Name};{Description};{maxItemsInStock};{Price.ItemPrice};{(int)Price.Currency};{(int)UnitType};2;";
        }

        public override object Clone()
        {
            return new FreshProduct(0, this.Name, this.Description, new Price()
            {
                ItemPrice = this.Price.ItemPrice,
                Currency = this.Price.Currency
            }, this.UnitType, this.maxItemsInStock);
        }
    }
}
