using BL.BO;

namespace BL.BlApi;

public interface ISalies : IBl<Salies>
{
    IEnumerable<object> AddProductToOrder(Order newOrder, int pId, int amount);
    void DoOrder(Salies o);
    bool IsSaliesExist(int id);
}