using DO;
using DalApi;
using System;
using Dal;

namespace DalTest1;

public static class Initialization
{
    private static IDal s_dal;
    private static void createProduct(IProduct p)
    {
        p.Create(new Product( 10000, "shirt", 100, 2000));
        p.Create(new Product( 10001, "shoose", 100, 2000));
        p.Create(new Product(10002, "slippers", 100, 2000));
        p.Create(new Product(10003, "runnig shorts", 100, 2000));
        p.Create(new Product(10004, "sport shirt", 100, 2000));
        p.Create(new Product(10005, "runnig shoose", 100, 2000));
        p.Create(new Product(10006, "tishert", 100, 2000));
        p.Create(new Product(10007, "sweater", 100, 2000));
        p.Create(new Product(10008, "football", 100, 2000));
        p.Create(new Product(10009, "socks", 100, 2000));
    }
    private static void createCustomer(ICustomer c)
    {
        c.Create(new Customer(2000, "sara coen", "ben gorion 152", "0531264589"));
        c.Create(new Customer(2001, "tamar ben ari", "savionim 12", "0551459965"));
        c.Create(new Customer(2002, "yosi arel", "zait 23", "0531246955"));
        c.Create(new Customer(2003, "esti rubin", "agefen 45", "0459875562"));
        c.Create(new Customer(2004, "sara lev", "ben gorion 72", "0456225412"));
        c.Create(new Customer(2005, "shani gold", "savionim 22", "0558794657"));
        c.Create(new Customer(2006, "yoni", "sanedria 23", "0255486289"));
        c.Create(new Customer(2007, "mali ben david", "ben yeuda 45", "0232540121"));
        c.Create(new Customer(2008, "yoeli frid", "lopian 23", "0548484841"));
        c.Create(new Customer(2009, "michhal laloom", "rashba 12", "04521665452"));
    }
    private static void createSails(ISalies s)
    {
        s.Create(new Salies(3000, 10001, 10000, 1, true, new DateTime( 2002, 12, 04), new DateTime(2002, 12, 07)));
        s.Create(new Salies(3001, 10000, 2000, 1, true, new DateTime(2002, 12, 04), new DateTime(2002, 12, 07)));
        s.Create(new Salies(3002, 10005, 30222, 1, false, new DateTime(2002, 12, 04), new DateTime(2002, 12, 07)));
        s.Create(new Salies(3003, 10004, 40000, 1, true, new DateTime(2002, 12, 04), new DateTime(2002, 12, 07)));
        s.Create(new Salies(3000, 10003, 20333, 1, true, new DateTime(2002, 12, 04), new DateTime(2002, 12, 07)));
        s.Create(new Salies(3001, 10004, 50000, 1, false, new DateTime(2002, 12, 04), new DateTime(2002, 12, 07)));
        s.Create(new Salies(3002, 10002, 25100, 1, true, new DateTime(2002, 12, 04), new DateTime(2002, 12, 07)));
        s.Create(new Salies(3003, 10002, 50600, 1, true, new DateTime(2002, 12, 04), new DateTime(2002, 12, 07)));
        s.Create(new Salies(3002, 10005, 50000, 1, false, new DateTime(2002, 12, 04), new DateTime(2002, 12, 07)));
        s.Create(new Salies(3003, 10003, 20000, 1, false, new DateTime(2002, 12, 04), new DateTime(2002, 12, 07)));
    }
    public static void initilize()
    {
        s_dal = DalApi.Factory.Get;
        createCustomer(s_dal.Customer);
        createProduct(s_dal.Product);
        createSails(s_dal.Salies);
    }
}

