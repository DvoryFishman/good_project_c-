using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class SaleInProduct
    {
        public int SaleId { get; set; }
        public double SalePrice { get; set; }
        public int AmountNeeded { get; set; }

        public SaleInProduct()
        {
            SaleId = -1;
            SalePrice = 0.0;
            AmountNeeded = 0;
        }

        public SaleInProduct(int saleId, double salePrice, int amountNeeded)
        {
            SaleId = saleId;
            SalePrice = salePrice;
            AmountNeeded = amountNeeded;
        }

        public override string ToString() => this.ToStringProperty();
    }
}