using System;
using DO;
using BL;
using BL.BlImplementation;
using BL.BlApi;
using BL.BO;
using System.Collections.Generic;

namespace BlTest;

public class Program
{
    // הגדרה מדויקת לפי ה-Factory שלך
    static readonly IBlManager s_bl = Factory.Get;

    public static void Main(string[] args)
    {
        Console.WriteLine("Do you want to initialize data? (y/n)");
        string ans = Console.ReadLine();

        if (ans == "y")
        {
            try { DalTest1.Initialization.initilize(); }
            catch (Exception e) { Console.WriteLine($"Initialization failed: {e.Message}"); }
        }

        int num = 0;
        do
        {
            try
            {
                Console.WriteLine("Choose an entity to test:\n 1: Customer\n 2: Product\n 3: Order (Complex Logic)\n 4: Exit");
                if (!int.TryParse(Console.ReadLine(), out num)) continue;

                switch (num)
                {
                    case 1: userCustomer(); break;
                    case 2: userProduct(); break;
                    case 3: userOrder(); break;
                    case 4: break;
                }
            }
            catch (Exception e) { Console.WriteLine($"Error: {e.Message}"); }
        } while (num != 4);
    }

    #region Customer Testing
    public static void userCustomer()
    {
        Console.WriteLine("insert 1: Read Customer \n 2: Read All Customers");
        int num = int.Parse(Console.ReadLine());
        switch (num)
        {
            case 1:
                Console.WriteLine("Insert Customer ID:");
                int id = int.Parse(Console.ReadLine());
                // שינוי ל-Read
                Console.WriteLine(s_bl.Customer.Read(id));
                break;
            case 2:
                // שינוי ל-ReadAll
                var list = s_bl.Customer.ReadAll();
                foreach (var item in list) Console.WriteLine(item);
                break;
        }
    }
    #endregion

    #region Product Testing
    public static void userProduct()
    {
        Console.WriteLine("insert 1: Read Product \n 2: Read All Products");
        int num = int.Parse(Console.ReadLine());
        switch (num)
        {
            case 1:
                Console.WriteLine("Insert Product ID:");
                int id = int.Parse(Console.ReadLine());
                // שינוי ל-Read
                Console.WriteLine(s_bl.Product.Read(id));
                break;
            case 2:
                // שינוי ל-ReadAll
                var list = s_bl.Product.ReadAll();
                foreach (var item in list) Console.WriteLine(item);
                break;
        }
    }
    #endregion

    #region Order Testing
    public static void userOrder()
    {
        // שימוש ב-Salies במקום Order כפי שמוגדר ב-BlManager שלך
        Console.WriteLine("Choose action for Order:\n 1: Add Product to Order\n 2: Do Order\n 3: Get Order Details");
        int num = int.Parse(Console.ReadLine());

        switch (num)
        {
            case 1:
                TestAddProductToOrder();
                break;
            case 2:
                TestDoOrder();
                break;
            case 3:
                Console.WriteLine("Insert Order ID:");
                int id = int.Parse(Console.ReadLine());
                // שינוי ל-Salies.Read
                Console.WriteLine(s_bl.Salies.Read(id));
                break;
        }
    }

    private static void TestAddProductToOrder()
    {
        Order newOrder = new Order { ProductsList = new List<ProductInOrder>(), ClientId = 1 };
        Console.WriteLine("Insert Product ID to add:");
        int pId = int.Parse(Console.ReadLine());
        Console.WriteLine("Insert Quantity:");
        int amount = int.Parse(Console.ReadLine());

        // שינוי ל-Salies
        var salesUsed = s_bl.Salies.AddProductToOrder(newOrder, pId, amount);

        Console.WriteLine("\n--- Order Updated ---");
        foreach (var sale in salesUsed) Console.WriteLine($"- Sale ID: {sale.SaleId}");
    }

    private static void TestDoOrder()
    {
        Console.WriteLine("Insert Order ID to finalize:");
        int id = int.Parse(Console.ReadLine());
        Order o = s_bl.Salies.Read(id);
        s_bl.Salies.DoOrder(o);
        Console.WriteLine("Order finalized.");
    }
    #endregion
}