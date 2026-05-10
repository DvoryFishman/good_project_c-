using BL.BlImplementation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlApi
{
    public static class Factory
    {
        public static IBlManager Get => new BlManager();
    }
}