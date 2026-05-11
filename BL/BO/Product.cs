using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class Product
    {
        public int ProductId { get; init; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int QuantityInStock { get; set; }

        public Product() : this(-1, 0.0, Category.SHIRT, 0) { }

        public Product(int id, double price, Category category, int quantityInStock)
        {
            ProductId = id;
            Price = price;
            Category = category;
            QuantityInStock = quantityInStock;
        }

        public override string ToString() => this.ToStringProperty();
    }
}