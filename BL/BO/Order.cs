using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class Order
    {
        bool favorite;
        List<ProductInOrder> ProductInOrders = new List<ProductInOrder>();
        double finalPriceInOrder;
        public override string ToString() => this.ToStringProperty();
    
    }
    
}
