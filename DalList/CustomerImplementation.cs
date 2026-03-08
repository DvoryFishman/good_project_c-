
using DO;
using DalApi;
using static Dal.DataSource;
using System;
using Tools;
using class1;
namespace Dal;

public class CustomerImplementation : ICustomer
{

    public int Create(Customer item)
    {
        Tools.LogManager.writeToLog(getPathCurrentFile(),Create,"start");
        var q = from c in customers
                where c.CustomerId == item.CustomerId
                select c;
        Customer? customer = q.FirstOrDefault();
        if (customer != null)
            throw new IdAlreadyExistsException();
        customers.Add(item);
        Tools.LogManager.writeToLog(getPathCurrentFile(), Create, "end");
        return item.CustomerId;
    }

    public Customer? Read(Func<Customer, bool>? filter)
    {
        Tools.LogManager.writeToLog(getPathCurrentFile(), Read, "start");
        var q = from c in customers
                where filter != null && filter(c)
                select c;
        Customer? c1 = q.FirstOrDefault();
        if (c1 == null)
            throw new IdNotFoundException();
        Tools.LogManager.writeToLog(getPathCurrentFile(), Read, "end");
        return c1;

    }

    public Customer? Read()
    {
        Tools.LogManager.writeToLog(getPathCurrentFile(), Read, "start");
        var q = from c in customers
                where c != null
                select c;
        Customer? c1 = q.FirstOrDefault();
        if (c1 == null)
            throw new IdNotFoundException();
        Tools.LogManager.writeToLog(getPathCurrentFile(), Read, "end");
        return c1;

    }

    public List<Customer> ReadAll(Func<Customer, bool>? filter = null)
    {
        Tools.LogManager.writeToLog(getPathCurrentFile(), ReadAll, "start");
        var q = from c in customers
                where filter != null && filter(c)
                select c;
        Tools.LogManager.writeToLog(getPathCurrentFile(), ReadAll, "end");
        return q.ToList();
    }


    public void Update(Customer item)
    {
        Tools.LogManager.writeToLog(getPathCurrentFile(), Update, "start");
        var q = from c in customers
                where item.CustomerId == c.CustomerId
                select c;
        Customer? c1 = q.FirstOrDefault();
        if (c1 == null)
            throw new IdNotFoundException();
        Delete(c1.CustomerId);
        Create(item);
        Tools.LogManager.writeToLog(getPathCurrentFile(), Update, "end");
    }
    public void Delete(int id)
    {
        Tools.LogManager.writeToLog(getPathCurrentFile(), Delete, "start");
        var q = from c in customers
                where c.CustomerId == id
                select c;
        Customer? customer = q.FirstOrDefault();
        if (customer == null)
            throw new IdNotFoundException();
        customers.Remove(customer);
        Tools.LogManager.writeToLog(getPathCurrentFile(), Delete, "end");
    }

}

