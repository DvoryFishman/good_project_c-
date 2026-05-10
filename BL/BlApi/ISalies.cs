using BL.BO;

namespace BL.BlApi;

public interface ISalies : IBl<Salies>
{
    bool IsSaliesExist(int id);
}