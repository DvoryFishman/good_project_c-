using BL.BlApi;
using BL.BlImplementation;

namespace BL.BlImplementation
{
    public class BlManager : IBlManager
    {
        private static ICustomer _customer;
        private static IProduct _product;
        private static ISalies _salies;

        public ICustomer Customer
        {
            get
            {
                _customer ??= new CustomerImplementationBL(DalApi.Factory.Get);
                return _customer;
            }
        }

        public IProduct Product
        {
            get
            {
                _product ??= new ProductImplementation(DalApi.Factory.Get);
                return _product;
            }
        }

        public ISalies Salies
        {
            get
            {
                _salies ??= new SaliesImplementation(DalApi.Factory.Get);
                return _salies;
            }
        }
    }
}
