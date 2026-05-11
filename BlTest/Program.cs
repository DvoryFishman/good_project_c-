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
    static readonly IBlManager s_bl = Factory.Get;

    public static void Main(string[] args)
    {
        Console.WriteLine("Do you want to initialize data? (y/n)");
        string ans = Console.ReadLine() ?? "n";

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

    public static void userCustomer()
    {
        Console.WriteLine("insert 1: Read Customer \n 2: Read All Customers");
        if (!int.TryParse(Console.ReadLine(), out int num)) return;
        switch (num)
        {
            case 1:
                Console.WriteLine("Insert Customer ID:");
                if (!int.TryParse(Console.ReadLine(), out int id)) return;
                var cust = s_bl.Customer.Read(c => c.CustomerId == id);
                Console.WriteLine(cust != null ? cust.ToString() : "Customer not found");
                break;
            case 2:
                var list = s_bl.Customer.ReadAll();
                foreach (var item in list) Console.WriteLine(item);
                break;
        }
    }

    public static void userProduct()
    {
        Console.WriteLine("insert 1: Read Product \n 2: Read All Products");
        if (!int.TryParse(Console.ReadLine(), out int num)) return;
        switch (num)
        {
            case 1:
                Console.WriteLine("Insert Product ID:");
                if (!int.TryParse(Console.ReadLine(), out int id)) return;
                var prod = s_bl.Product.Read(p => p.ProductId == id);
                Console.WriteLine(prod != null ? prod.ToString() : "Product not found");
                break;
            case 2:
                var list = s_bl.Product.ReadAll();
                foreach (var item in list) Console.WriteLine(item);
                break;
        }
    }

    public static void userOrder()
    {
        Console.WriteLine("Choose action for Order:\n 1: Add Product to Order\n 2: Do Order\n 3: Get Order Details");
        if (!int.TryParse(Console.ReadLine(), out int num)) return;

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
                if (!int.TryParse(Console.ReadLine(), out int id)) return;
                var sale = s_bl.Salies.Read(s => s.Id == id);
                Console.WriteLine(sale != null ? sale.ToString() : "Order/Salies not found");
                break;
        }
    }

    private static void TestAddProductToOrder()
    {
        Order newOrder = new Order { ProductsList = new List<ProductInOrder>(), ClientId = 1 };
        Console.WriteLine("Insert Product ID to add:");
        if (!int.TryParse(Console.ReadLine(), out int pId)) return;
        Console.WriteLine("Insert Quantity:");
        if (!int.TryParse(Console.ReadLine(), out int amount)) return;

        var salesUsed = s_bl.Salies.AddProductToOrder(newOrder, pId, amount);

        Console.WriteLine("\n--- Order Updated ---");
        foreach (var sale in salesUsed)
        {
            // Replace 'Sale' with the correct type if it's not 'Sale'
            if (sale is Order typedOrder)
                Console.WriteLine($"- Order ID: {typedOrder.OrderId}");
            else
                Console.WriteLine("- Sale object does not have an OrderId property.");
        }
    }
    private static void TestDoOrder()
    {
        Console.WriteLine("Insert Order ID to finalize:");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;
        var o = s_bl.Salies.Read(s => s.Id == id);
        if (o == null) { Console.WriteLine("Order not found"); return; }
        s_bl.Salies.DoOrder(o);
        Console.WriteLine("Order finalized.");
    }
}