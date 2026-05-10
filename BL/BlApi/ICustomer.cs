using BL.BO;

namespace BL.BlApi;

public interface ICustomer : IBl<Customer>
{
    bool IsCustomerExist();
}