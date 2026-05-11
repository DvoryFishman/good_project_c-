
using DO;
using DalApi;
using static Dal.DataSource;
namespace Dal;

public class ProductImplementation : IProduct
{

    public int Create(Product item)
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        Product product;
        int id = Config.StaticValue;
        product = item with { ProductId = id };
        products.Add(product);
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
        return id;
        
    }

    public Product? Read(Func<Product, bool>? filter)
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from p in products
                where filter != null && filter(p)
                select p;
        Product? p1 = q.FirstOrDefault();
        if (p1 == null)
            throw new IdNotFoundException();
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
        return p1;

    }
    public Product? Read()
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from p in products
                where p != null
                select p;
        Product? p1 = q.FirstOrDefault();
        if (p1 == null)
            throw new IdNotFoundException();
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
        return p1;

    }
    public List<Product> ReadAll(Func<Product, bool>? filter = null)
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from p in products
                where filter != null && filter(p)
                select p;
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
        return q.ToList();
    }
    public void Update(Product item)
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from p in products
                where item.ProductId == p.ProductId
                select p;
        Product? p1 = q.FirstOrDefault();
        Delete(p1.ProductId);
        Create(item);
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
    }
    public void Delete(int id)
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from p in products
                where p.ProductId == id
                select p;
        Product? product = q.FirstOrDefault();
        if (product == null)
            throw new IdNotFoundException();
        products.Remove(product);
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
    }

}
