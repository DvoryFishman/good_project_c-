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
        public Category Category { get; set; }

        public double Price { get; set; }
        public int QuantityInStock { get; set; }

        public Product() : this(-1,default(Category),0.0, 0) { }

        public Product(int id, Category category,double price , int quantityInStock)
        {
            ProductId = id;
            Price = price;
            Category = category;
            QuantityInStock = quantityInStock;
        }

        public override string ToString() => this.ToStringProperty();
    }
}