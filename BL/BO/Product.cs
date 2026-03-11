using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{


    public record Product
    (   int ProductId,
        string Category,
        double Price,
        int QuantityInStock,
        List<SaleInProduct> SaleInProducts = new List<SaleInProduct>()
    )
    {

        public Product() : this(0, "shoose", 10, 10, null)
        {


        }
        public override string ToString() => this.ToStringProperty();
    }



}
