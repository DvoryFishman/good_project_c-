
namespace DO
{
    public record Product
    (
        int ProductId,
        string Category,
        double Price,
        int QuantityInStock)
    {

        public Product() : this( 0, "shoose", 10, 10)
        {


        }
    }
}
