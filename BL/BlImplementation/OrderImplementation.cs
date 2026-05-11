using System;
using System.Collections.Generic;
using System.Linq;
using BL.BO;
using DO;

namespace BL.BlImplementation
{
    internal class OrderImplementation : BlApi.IOrder
    {
        // גישה לשכבת הנתונים
        private DalApi.IDal _dal = DalApi.Factory.Get;

        public OrderImplementation()
        {
        }

        // 1. עדכון המבצעים הרלוונטיים למוצר ספציפי בהזמנה
        public void SearchSaleForProduct(ProductInOrder p, bool IsExistingClient)
        {
            try
            {
                DateTime now = DateTime.Now;

                // שליפת כל המבצעים מ-DO.Salies וסינון לפי התנאים
                var salesQuery = _dal.Salies.ReadAll()
                    .Where(s => s.ProductId == p.ProductId) // מבצע ששייך למוצר
                    .Where(s => s.CampaingStartDate <= now && s.CampaingEndDate >= now) // תוקף המבצע
                    .Where(s => p.Amount >= s.QuentityForSale); // האם הכמות בהזמנה מספיקה למבצע

                // אם הלקוח אינו קיים (לא חבר מועדון), נסנן מבצעים שמוגדרים רק למועדון
                if (!IsExistingClient)
                {
                    salesQuery = salesQuery.Where(s => s.OnlyForTheClub == false);
                }

                // מיון לפי המחיר הזול ביותר (כדאיות) והמרה לטיפוס BO
                p.Sales = salesQuery
                    .OrderBy(s => s.TotalPriceOnSale)
                    .Select(s => new SaleInProduct
                    {
                        SaleId = s.Id,
                        SalePrice = s.TotalPriceOnSale ,
                        AmountNeeded = s.QuentityForSale 
                    })
                    .ToList();
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BlNotFoundException("error searching sales for product", e);
            }
        }

        // 2. חישוב מחיר למוצר בודד בהזמנה תוך ניצול מקסימלי של מבצעים
        public void CalcTotalPriceForProduct(ProductInOrder p)
        {
            int count = p.Amount;
            double finalSum = 0;
            List<SaleInProduct> usedSales = new List<SaleInProduct>();

            foreach (var sale in p.Sales)
            {
                if (count < sale.AmountNeeded)
                    continue;

                // בדיקה כמה פעמים ניתן לממש את המבצע הספציפי
                int timesToApply = count / sale.AmountNeeded;
                finalSum += timesToApply * sale.SalePrice;

                // עדכון השארית
                count %= sale.AmountNeeded;

                // תיעוד המבצע שמומש בפועל
                usedSales.Add(sale);

                if (count == 0) break;
            }

            // חישוב הכמות שנותרה לפי מחיר הבסיס של המוצר
            finalSum += count * p.BasePrice;

            // עדכון האובייקט בתוצאות
            p.Sales = usedSales;
            p.TotalPrice = finalSum;
        }

        // 3. חישוב סך כל עלות ההזמנה (סכום כל המוצרים)
        public void CalcTotalPrice(Order o)
        {
            o.TotalPrice = o.ProductsList.Sum(p => p.TotalPrice);
        }

        // 4. הוספת מוצר לסל הקניות/הזמנה
        public List<SaleInProduct> AddProductToOrder(Order o, int ProductId, int ProductAmount)
        {
            try
            {
                // שליפת נתוני המוצר מה-DAL
                DO.Product dalProduct = _dal.Product.Read(x => x.ProductId == ProductId);
                if (dalProduct == null)
                    throw new BlNotFoundException("מוצר לא נמצא");

                var existingItem = o.ProductsList.FirstOrDefault(p => p.ProductId == ProductId);

                if (existingItem != null)
                {
                    // עדכון כמות למוצר קיים ובדיקת מלאי
                    if (existingItem.Amount + ProductAmount > dalProduct.QuantityInStock)
                        throw new BlNotFoundException("אין מספיק מלאי");

                    existingItem.Amount += ProductAmount;
                }
                else
                {
                    // הוספת מוצר חדש ובדיקת מלאי
                    if (ProductAmount > dalProduct.QuantityInStock)
                        throw new BlNotFoundException("אין מספיק מלאי");

                    existingItem = new ProductInOrder
                    {
                        ProductId = ProductId,
                        BasePrice = dalProduct.Price,
                        Amount = ProductAmount
                    };
                    o.ProductsList.Add(existingItem);
                }

                // הרצת הלוגיקה: חיפוש מבצעים -> חישוב מחיר שורה -> חישוב סך הזמנה
                bool isExisting = o.ClientId > 0; // הנחה: אם יש מזהה לקוח, הוא קיים במערכת
                SearchSaleForProduct(existingItem, isExisting);
                CalcTotalPriceForProduct(existingItem);
                CalcTotalPrice(o);

                return existingItem.Sales;
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BlNotFoundException("error adding product to order", e);
            }
        }

        // 5. סגירת הזמנה - עדכון מלאי סופי ב-DB
        public void DoOrder(Order o)
        {
            try
            {
                foreach (var item in o.ProductsList)
                {
                    DO.Product p = _dal.Product.Read(x => x.ProductId == item.ProductId);
                    // שימוש ב-with ליצירת Record מעודכן (Immutability)
                    _dal.Product.Update(p with { QuantityInStock = p.QuantityInStock - item.Amount });
                }
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BlNotFoundException("error completing order", e);
            }
        }
    }
}