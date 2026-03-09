
using BL.BO;

namespace BL.BlApi;

public interface IProduct : IBl<Product>

{
    void InForce(ProductInOrder p, bool IsFevorite);
}
