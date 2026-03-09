
namespace BL.BlApi;

public interface IBl<T>
{
    public interface IBl
    {
        IProduct Product { get; }
        ICustomer Customer { get; }
        ISalies Salies { get; }
    }
}
