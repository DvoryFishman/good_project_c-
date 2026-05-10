using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class Order
    {
        public int OrderId { get; init; }
        public int ClientId { get; set; }
        public bool IsFavorite { get; set; }
        public List<ProductInOrder> ProductsList { get; set; }
        public double TotalPrice { get; set; }

        public Order() : this(-1, -1, false, new List<ProductInOrder>(), 0.0) { }

        public Order(int orderId, int clientId, bool isFavorite, List<ProductInOrder> productsList, double totalPrice)
        {
            OrderId = orderId;
            ClientId = clientId;
            IsFavorite = isFavorite;
            ProductsList = productsList ?? new List<ProductInOrder>();
            TotalPrice = totalPrice;
        }

        public override string ToString() => this.ToStringProperty();
    }
}