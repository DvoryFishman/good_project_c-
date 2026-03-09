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
        list<ProductInOrder> ProductInOrders = new list<ProductInOrder>();
        double finalPriceInOrder;
        public override string ToString() => this.ToStringProperty();
    
    }
    
}
