using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace Dal;

public sealed class DalXml3 : IDal
{
    private static readonly DalXml3 instance = new DalXml3();
    public static DalXml3 Instance { get { return instance; } }

    private DalXml3() { }

    public IProduct Product { get; } = new ProductImplementation();
    public ICustomer Customer { get; } = new CustomerImplementation();
    public ISalies Salies { get; } = new SaliesImplementation();
}