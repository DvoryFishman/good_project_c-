using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace BL.BO
{
    public class ProductInOrder
    {
        int ProductId;
        string ProductName;
        double BasicPrice;
        int AmountInOrder;
        List<SaleInProduct> SaleInProducts = new List<SaleInProduct>();
        double FinalPrice;

        public override string ToString() => this.ToStringProperty();
    }
}
