using DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq; // נוסף עבור בדיקת Any()
using Dal;

namespace DalTest1;

public static class Initialization
{
    private static IDal s_dal;

    // רשימה לאחסון ה-IDs שנוצרו
    public static List<int> CreatedProductIds { get; } = new List<int>();

    private static void createProduct(IProduct p)
    {
        CreatedProductIds.Clear();

        // יצירת מוצרים עם שמות מתוקנים
        // שים לב: ה-ID נשלח כ-0 כי ה-DAL אמור להקצות אותו אוטומטית
        CreatedProductIds.Add(p.Create(new Product(0, "SHIRT", 100, 2000)));
        CreatedProductIds.Add(p.Create(new Product(0, "SHOES", 150, 500)));
        CreatedProductIds.Add(p.Create(new Product(0, "SLIPPERS", 80, 300)));
        CreatedProductIds.Add(p.Create(new Product(0, "RUNNING SHORTS", 120, 1000)));
        CreatedProductIds.Add(p.Create(new Product(0, "SPORT SHIRT", 110, 1500)));
        CreatedProductIds.Add(p.Create(new Product(0, "RUNNING SHOES", 350, 400)));
        CreatedProductIds.Add(p.Create(new Product(0, "T-SHIRT", 60, 2500)));
        CreatedProductIds.Add(p.Create(new Product(0, "SWEATER", 200, 100)));
        CreatedProductIds.Add(p.Create(new Product(0, "FOOTBALL", 90, 800)));
        CreatedProductIds.Add(p.Create(new Product(0, "SOCKS", 25, 5000)));
    }

    private static void createCustomer(ICustomer c)
    {
        c.Create(new Customer(2000, "Sara Cohen", "Ben Gurion 152", "0531264589"));
        c.Create(new Customer(2001, "Tamar Ben Ari", "Savionim 12", "0551459965"));
        c.Create(new Customer(2002, "Yosi Arel", "Zait 23", "0531246955"));
        c.Create(new Customer(2003, "Esti Rubin", "Agefen 45", "0459875562"));
        c.Create(new Customer(2004, "Sara Lev", "Ben Gurion 72", "0456225412"));
        c.Create(new Customer(2005, "Shani Gold", "Savionim 22", "0558794657"));
        c.Create(new Customer(2006, "Yoni", "Sanedria 23", "0255486289"));
        c.Create(new Customer(2007, "Mali Ben David", "Ben Yeuda 45", "0232540121"));
        c.Create(new Customer(2008, "Yoeli Frid", "Lopian 23", "0548484841"));
        c.Create(new Customer(2009, "Michal Laloom", "Rashba 12", "04521665452"));
    }

    private static void createSails(ISalies s)
    {
        if (CreatedProductIds.Count == 0) return;
        int gid(int idx) => CreatedProductIds[idx % CreatedProductIds.Count];

        // יצירת מכירות לדוגמה
        s.Create(new Salies(0, gid(1), 1, 2000, true, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-7)));
        s.Create(new Salies(0, gid(0), 1, 2001, true, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-2)));
        s.Create(new Salies(0, gid(5), 1, 2002, false, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(-7)));
        s.Create(new Salies(0, gid(4), 1, 2003, true, DateTime.Now.AddDays(-8), DateTime.Now.AddDays(-5)));
        s.Create(new Salies(0, gid(3), 1, 2004, true, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-7)));
    }

    public static void initilize()
    {
        s_dal = DalApi.Factory.Get;

        // בדיקה קריטית: אם כבר קיימים מוצרים ב-XML, אל תבצע אתחול מחדש!
        // זה מונע את הכפילויות ואת הבעיה שראית
        if (s_dal.Product.ReadAll().Any())
        {
            return;
        }

        createCustomer(s_dal.Customer);
        createProduct(s_dal.Product);
        createSails(s_dal.Salies);
    }
}