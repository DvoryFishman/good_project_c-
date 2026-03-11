using DO;
using DalApi;
using static Dal.DataSource;
//using Exception;
namespace Dal;

public class SaliesImplementation : ISalies
{

    public int Create(Salies item)
    {
        //var q = from c in salies
        //        where c.Id == item.Id
        //        select c;
        //Salies? sale = q.FirstOrDefault();
        //if (sale != null)
        //    throw new IdAlreadyExistsException();
        //int id = Config.StaticValue;
        //sale = item with { Id = id };
        //return item.Id;
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        Salies sale;
        int id = Config.StaticValue;
        sale = item with { SaleId = id };
        salies.Add(sale);
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
        return id;

    }

    public Salies? Read(Func<Salies, bool>? filter)
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from c in salies
                where filter != null && filter(c)
                select c;
        Salies? c1 = q.FirstOrDefault();
        if (c1 == null)
            throw new IdNotFoundException();
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
        return c1;

    }
    public List<Salies> ReadAll(Func<Salies, bool>? filter = null)
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from c in salies
                where filter != null && filter(c)
                select c;   
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
        return q.ToList();
    }
    public Salies? Read()
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from c in salies
                where c != null
                select c;
        Salies? c1 = q.FirstOrDefault();
        if (c1 == null)
            throw new IdNotFoundException();
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
        return c1;

    }
    public void Update(Salies item)
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from c in salies
                where item.Id == c.Id
                select c;
        //select new { c = item };
        Salies? s = q.FirstOrDefault();
        if (s == null)
            throw new IdNotFoundException();
        Delete(s.Id);
        Create(item);
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
    }
    public void Delete(int id)
    {
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "start");
        var q = from c in salies
                where c.Id == id
                select c;
        Salies? s = q.FirstOrDefault();
        if (s == null)
            throw new IdNotFoundException();
        salies.Remove(s);
        Tools.LogManager.writeToLog(Tools.LogManager.getPathCurrentFile(), "Create", "end");
    }

}
