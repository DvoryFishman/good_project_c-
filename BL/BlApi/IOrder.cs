using BL.BO;
using System;
using System.Collections.Generic;

namespace BL.BlApi
{
    // שינינו ל-public כדי שה-UI יראה את זה
    public interface IOrder
    {
        // השם שונה ל-AddProductToOrder כדי להתאים למה שכתבת במימוש (Implementation)
        List<SaleInProduct> AddProductToOrder(Order o, int ProductId, int ProductAmount);

        void CalcTotalPriceForProduct(ProductInOrder p);
        void CalcTotalPrice(Order o);
        void DoOrder(Order o);
        void SearchSaleForProduct(ProductInOrder p, bool IsFavorite);
    }
}