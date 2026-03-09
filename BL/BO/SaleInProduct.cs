using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    internal class SaleInProduct
    {
        int SaleId;
        int AmountInSale;
        double Price;
        bool IsToAllCustomer;

        public override string ToString() => this.ToStringProperty();
    }
}
