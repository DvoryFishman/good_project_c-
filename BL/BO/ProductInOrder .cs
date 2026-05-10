using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class ProductInOrder
    {
        public int ProductId { get; set; }
        public double BasePrice { get; set; }
        public int Amount { get; set; }
        public List<SaleInProduct> Sales { get; set; }
        public double TotalPrice { get; set; }

        public ProductInOrder()
        {
            ProductId = -1;
            BasePrice = 0.0;
            Amount = 0;
            Sales = new List<SaleInProduct>();
            TotalPrice = 0.0;
        }

        public ProductInOrder(int productId, double basePrice, int amount, List<SaleInProduct> sales = null, double totalPrice = 0.0)
        {
            ProductId = productId;
            BasePrice = basePrice;
            Amount = amount;
            Sales = sales ?? new List<SaleInProduct>();
            TotalPrice = totalPrice;
        }

        public override string ToString() => this.ToStringProperty();
    }
}