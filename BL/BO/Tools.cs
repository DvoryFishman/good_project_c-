using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    internal class Tools
    {
        public static string ToStringProperty<T>(this T obj)
        {
            if (obj == null) return "null";

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var result = properties.Select(p =>
            {
                var value = p.GetValue(obj);
                if (value is IEnumerable enumerable && !(value is string))
                {
                    return $"{p.Name}: [{string.Join(", ", enumerable.Cast<object>().Select(e => e?.ToString() ?? "null"))}]";
                }
                return $"{p.Name}: {value}";
            });

            return string.Join(", ", result);
        }
        public static Customer ToDataObject(Customer bo)
        {
            if (bo == null) return null;
            return new Customer{
                CustomerId = bo.CustomerId,
                Name = bo.Name,
                Adress = bo.Adress,
                Phone = bo.Phone
            };
        }
        }
}
