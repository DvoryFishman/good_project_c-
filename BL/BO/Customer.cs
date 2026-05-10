using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class Customer
    {
        public int CustomerId { get; init; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public Customer() : this(-1, "", "", "") { }

        public Customer(int id, string name, string address, string phone)
        {
            CustomerId = id;
            Name = name;
            Address = address;
            Phone = phone;
        }

        public override string ToString() => $"id: {CustomerId} , Name: {Name} , Address: {Address} , Phone: {Phone} .";
    }
}