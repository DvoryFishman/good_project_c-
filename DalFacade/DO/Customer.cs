
namespace DO;

public record Customer
(int CustomerId,
 string Name,
 string Adress,
 string Phone)
{
    public Customer() : this(0, "", "", "")
    {

    }

    //public int ProductId { get; set; }

    //public static int findIndex(Func<object, bool> value)
    //{
    //    throw new NotImplementedException();
    //}
}
