using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlApi
{
    internal interface IOrder
    {
        SaleInProduct ProductInOrder(Order o, int ProductId, int ProductAmount);
        void CalcTotalPriceForProduct(ProductInOrder p);
        void CalcTotalPrice(Order o);
        void DoOrder(Order o);
        void SearchSaleForProduct(ProductInOrder p, bool IsFevorite);
    }
}
