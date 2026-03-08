using System;
using Dal;
using DalApi;
using DO;
using DalTest1;
using Tools;

//using System.Security.Cryptography.X509Certificates;
//using System.Linq.Expressions;
//using System.Diagnostics.CodeAnalysis;
//using System.Security.Principal;
namespace DalTest1;

public class Program
{
    private static IDal s_dal = DalApi.Factory.Get;
    public static bool Sum(Customer c)
    {
        Console.WriteLine($"c{c}");
        return true;
    }
    public static void Main(string[] args)
    {
        Func<Customer, bool> del1;
        del1 = Sum;
   
        try
        {
            Initialization.initilize();
        }
        catch (IdAlreadyExistsException e)
        {
            Tools.LogManager.writeToLog(getPathCurrentFile(), initilize, e.message);
            Console.WriteLine(e.Message);
        }
        catch (IdNotFoundException e)
        {
            Tools.LogManager.writeToLog(getPathCurrentFile(), initilize, e.message);
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Tools.LogManager.writeToLog(getPathCurrentFile(), initilize, e.message);
            Console.WriteLine(e.Message);
        }


        int num = 0;
        try
        {
            do
            {
                Console.WriteLine("insert 1 : customer \n 2 : salies\n 3 : product \n 4: clean loger \n 5: exit");
                num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1: userCustomer(s_dal.Customer); break;
                    case 2: userSalies(s_dal.Salies); break;
                    case 3: userProduct(s_dal.Product); break;
                    case 4: CleanOldLogs(); break;
                    case 5: break;
                }
            } while (num != 5);
        }
        catch (IdAlreadyExistsException e)
        {
            Tools.LogManager.writeToLog(getPathCurrentFile(), inTheKey, e.message);
            Console.WriteLine(e.Message);
        }
        catch (IdNotFoundException e)
        {
            Tools.LogManager.writeToLog(getPathCurrentFile(), inTheKey, e.message);

            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Tools.LogManager.writeToLog(getPathCurrentFile(), inTheKey, e.message);
            Console.WriteLine(e.Message);

        }

    }
    public static void userCustomer(ICrud<Customer> c)

    {
        int num = menu();



        switch (num)
        {
            case 1:
                c.Create(getCustomer(0));
                break;
            case 2:
                Console.WriteLine(c.Read(c => c.CustomerId > 50));
                break;
            case 3:
                List<Customer> list = c.ReadAll(c=>true);
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                break;
            case 4:
                int id = getId();
                c.Update(getCustomer(id));
                break;
            case 5:
                c.Delete(getId());
                break;
            default:
                Console.WriteLine("the number is not currect");
                break;



        }

    }
    public static void userProduct(ICrud<Product> p)

    {
        int num = menu();



        switch (num)
        {
            case 1:
                p.Create(getProduct());
                break;
            case 2:
                Console.WriteLine(p.Read(c => c.Price > 50));
                break;
            case 3:
                List<Product> list = p.ReadAll(c => true);
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                break;
            case 4:
                int id = getId();
                p.Update(getProduct(id));
                break;
            case 5:
                p.Delete(getId());
                break;
            default:
                Console.WriteLine("the number is not currect");
                break;



        }

    }
    public static void userSalies(ICrud<Salies> s)

    {
        int num = menu();



        switch (num)
        {
            case 1:
                s.Create(getSalies());
                break;
            case 2:
                Console.WriteLine(s.Read(c=> c.TotalPriceOnSale<5));
                break;
            case 3:
                List<Salies> list = s.ReadAll(c => true);
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
                break;
            case 4:
                int id = getId();
                s.Update(getSalies(id));
                break;
            case 5:
                s.Delete(getId());
                break;
            default:
                Console.WriteLine("the number is not currect");
                break;



        }

    }

    public static int menu()
    {
        Console.WriteLine("insert 1 : create customer \n 2 : read customer\n 3 : read all customers \n 4:updaet \n 5: delete");
        int num = int.Parse(Console.ReadLine());
        return num;

    }
    public static int getId()
    {
        Console.WriteLine("insert index");
        int index = int.Parse(Console.ReadLine());
        return index;
    }
    public static Customer getCustomer(int id )
    {
        Console.WriteLine("inser name");
        string name = Console.ReadLine();
        Console.WriteLine("inser address");
        string address = Console.ReadLine();
        Console.WriteLine("inser phone");
        string phone = Console.ReadLine();
        Customer newCustomer = new Customer()
        {
            CustomerId = id,
            Name = name,
            Adress = address,
            Phone = phone,

        };
        return newCustomer;

    }
    public static Product getProduct(int id = 0)
    {
        Console.WriteLine("inser categury");
        string categury = Console.ReadLine();
        Console.WriteLine("inser price");
        int price = int.Parse(Console.ReadLine());
        Console.WriteLine("inser quentity");
        int quentity = int.Parse(Console.ReadLine());
        Product newProduct = new Product()
        {

            ProductId = id,
            Category = categury,
            Price = price,
            QuantityInStock = quentity


        };
        return newProduct;

    }
    public static Salies getSalies(int id = 0)
    {
        Console.WriteLine("inser productId");
        int productId = int.Parse(Console.ReadLine());
        Console.WriteLine("inser quentityForSale");
        int quentityForSale = int.Parse(Console.ReadLine());
        Console.WriteLine("inser totalPriceOnSale");
        double totalPriceOnSale = double.Parse(Console.ReadLine());
        Console.WriteLine("inser campaingStartDate");
        DateTime campaingStartDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("inser campaingEndDate");
        DateTime campaingEndDate = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("inser onlyForTheClub");
        bool onlyForTheClub = bool.Parse(Console.ReadLine());
        Salies newSalies = new Salies()
        {
            Id = id,
            ProductId = productId,
            QuentityForSale = quentityForSale,
            TotalPriceOnSale = totalPriceOnSale,
            OnlyForTheClub = onlyForTheClub,
            CampaingStartDate = campaingStartDate,
            CampaingEndDate = campaingEndDate

        };
        return newSalies;

    }
 
}