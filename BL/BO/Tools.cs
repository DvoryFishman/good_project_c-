using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DO;

namespace BL.BO
{
    internal static class Tools
    {
        public static string ToStringProperty<T>(this T obj)
        {
            if (obj == null) return "null";

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var result = properties.Select(p =>
            {
                var value = p.GetValue(obj);
                if (value is System.Collections.IEnumerable enumerable && !(value is string))
                {
                    return $"{p.Name}: [{string.Join(", ", enumerable.Cast<object>().Select(e => e?.ToString() ?? "null"))}]";
                }
                return $"{p.Name}: {value}";
            });

            return string.Join(", ", result);
        }

        public static DO.Customer ToDataObject(this BO.Customer bo)
        {
            if (bo == null) return null;
            return new DO.Customer(
                bo.CustomerId,
                bo.Name ?? "",
                bo.Address ?? "",
                bo.Phone ?? ""
            );
        }

        public static BO.Customer ToBO(this DO.Customer customer)
        {
            if (customer == null) return null;
            return new BO.Customer(
                customer.CustomerId,
                customer.Name ?? "",
                customer.Adress ?? "",
                customer.Phone ?? ""
            );
        }

        public static DO.Product ToDataObject(this BO.Product bo)
        {
            if (bo == null) return null;
            return new DO.Product(
                bo.ProductId,
                bo.Category.ToString(),
                bo.Price,
                bo.QuantityInStock
            );
        }

        public static BO.Product ToBO(this DO.Product product)
        {
            if (product == null) return null;

            Enum.TryParse<Category>(product.Category, out var category);

            return new BO.Product(
                product.ProductId,
                product.Price,
                category,
                product.QuantityInStock
            );
        }

        public static BO.Salies ToBO(this DO.Salies salies)
        {
            if (salies == null) return null;
            return new BO.Salies(
                salies.Id,
                salies.ProductId,
                salies.QuentityForSale ,
                salies.TotalPriceOnSale,
                salies.OnlyForTheClub,
                salies.CampaingStartDate,
                salies.CampaingEndDate
            );
        }

        public static DO.Salies ToDataObject(this BO.Salies salies)
        {
            if (salies == null) return null;
            return new DO.Salies(
                salies.Id,
                salies.ProductId,
                salies.QuentityForSale ?? 0,
                salies.TotalPriceOnSale ?? 0.0,
                salies.OnlyForTheClub ?? false,
                salies.CampaingStartDate ?? DateTime.Now,
                salies.CampaingEndDate ?? DateTime.Now
            );
        }
    }
}