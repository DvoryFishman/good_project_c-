

namespace DalApi
{
    public interface IDal
    {
        IProduct Product { get; }
        ICustomer Customer { get; }
        ISalies Salies { get; }
    }
}
