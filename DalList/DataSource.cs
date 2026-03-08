

using DO;

namespace Dal;

internal static class DataSource
{
    static internal List<Product?> products = new List<Product>();
    static internal List<Customer?> customers = new List<Customer>();
    static internal List<Salies?> salies = new List<Salies>();
    internal static class Config
    {
        internal const int minimum = 1000;
        private static int statValue = minimum;
        public static int StaticValue { get { return statValue++; } }

       
    }
}