using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BL.BO;

namespace BL.BlImplementation;

internal class CustomerImplementationBL : BL.BlApi.ICustomer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public CustomerImplementationBL(DalApi.IDal customerDAL)
    {
        _dal = customerDAL;
    }

    public BL.BO.Customer Create(BL.BO.Customer item)
    {
        try
        {
            var customerDO = BL.BO.Tools.ToDataObject(item);
            var createdDO = _dal.Customer.Create(customerDO);
            var result = _dal.Customer.Read(x => x.CustomerId == createdDO);
            return BL.BO.Tools.ToBO(result);
        }
        catch (DO.IdAlreadyExistsException e)
        {
            throw new BL.BO.BlAlreadyExistsException("customer already exists", e);
        }
        catch (DO.IdNotFoundException e)
        {
            throw new BL.BO.BlNotFoundException("error creating customer", e);
        }
    }

    public BL.BO.Customer? Read(Func<BL.BO.Customer, bool> filter)
    {
        try
        {
            var customer = _dal.Customer.ReadAll(x => true).Select(s => BL.BO.Tools.ToBO(s)).FirstOrDefault(filter);
            return customer;
        }
        catch (DO.IdNotFoundException e)
        {
            throw new BL.BO.BlNotFoundException("error reading customer", e);
        }
    }

    public List<BL.BO.Customer> ReadAll(Func<BL.BO.Customer, bool>? filter = null)
    {
        try
        {
            var customersDO = _dal.Customer.ReadAll();
            var customersBO = from customer in customersDO
                              let cb = BL.BO.Tools.ToBO(customer)
                              where filter == null || filter(cb)
                              select cb;
            return customersBO.ToList();
        }
        catch (DO.IdNotFoundException e)
        {
            throw new BL.BO.BlNotFoundException("error reading customers", e);
        }
    }

    public void Update(BL.BO.Customer item)
    {
        try
        {
            var customer = BL.BO.Tools.ToDataObject(item);
            _dal.Customer.Update(customer);
        }
        catch (DO.IdNotFoundException e)
        {
            throw new BL.BO.BlNotFoundException("error updating customer", e);
        }
    }

    public void Delete(int id)
    {
        try
        {
            _dal.Customer.Delete(id);
        }
        catch (DO.IdNotFoundException e)
        {
            throw new BL.BO.BlNotFoundException("error deleting customer", e);
        }
    }

    public bool IsCustomerExist()
    {
        try
        {
            return _dal.Customer.ReadAll().Any();
        }
        catch (DO.IdNotFoundException e)
        {
            throw new BL.BO.BlNotFoundException("error checking if customer exists", e);
        }
    }
}