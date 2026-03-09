using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public record Customer
    (int CustomerId,
     string Name,
     string Adress,
     string Phone)
    {
        public Customer() : this(0, "", "", "")
        {

        }
        public override string ToString() => this.ToStringProperty();
    }

}
