using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class ProductInOrder
    {
        int ProductId;
        string ProductName;
        double BasicPrice;
        int AmountInOrder;
        list<SaleInProduct> SaleInProducts = new list<SaleInProduct>();
        double FinalPrice;

        public override string ToString() => this.ToStringProperty();
    }
}
