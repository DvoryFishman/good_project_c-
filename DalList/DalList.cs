
using DalApi;

namespace Dal;

internal sealed  class DalList : IDal
{
    public  ICustomer   Customer => new CustomerImplementation();
    public IProduct Product => new ProductImplementation();
    public ISalies Salies => new SaliesImplementation();

    private static readonly DalList instance=new DalList();
    private DalList() { }
    public static DalList Instance
    {
        get { return instance; }
    }
}
