using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalFacade;
using DO;
namespace BL.BlImplementation;

internal class CustomerImplementationBL : IBl<BO.Customer>
{

    private DalApi.IDal _dal = DalApi.Factory.Get; // הפניה לשכבת ה-DAL

    public CustomerImplementationBL(DalApi.ICustomer customerDAL)
    {
        _customerDAL = customerDAL;
    }

    public int Create(BO.Customer item)
    {
        DO.Customer customerDO = new DO.Customer
        {
            CustomerId = item.CustomerId,
            Name = item.Name,
            Adress = item.Adress,
            Phone = item.Phone
        };

        return _customerDAL.Create(customerDO);
    }

    public DO.Customer? Read(int id)
    {
        Customer? customerDO = _customerDAL.Read(id);
        return customerDO != null ? new Customer
        {
            CustomerId = customerDO.CustomerId,
            Name = customerDO.Name,
            Adress = customerDO.Adress,
            Phone = customerDO.Phone
        } : null;
    }

    public Customer? Read(Func<Customer, bool>? filter)
    {
        var allCustomers = ReadAll();
        return filter != null ? allCustomers.FirstOrDefault(filter) : null;
    }

    public List<Customer> ReadAll(Func<Customer, bool>? filter = null)
    {
        var customersDO = _customerDAL.ReadAll();
        var customersBO = customersDO.Select(customerDO => new Customer
        {
            CustomerId = customerDO.CustomerId,
            Name = customerDO.Name,
            Adress = customerDO.Adress,
            Phone = customerDO.Phone
        }).ToList();

        return filter != null ? customersBO.Where(filter).ToList() : customersBO;
    }

    public void Update(Customer item)
    {
        Customer customerDO = new Customer
        {
            CustomerId = customerDO.CustomerId,
            Name = customerDO.Name,
            Adress = customerDO.Adress,
            Phone = customerDO.Phone
        };

        _customerDAL.Update(customerDO);
    }

    public void Delete(int id)
    {
        _customerDAL.Delete(id);
    }
}
