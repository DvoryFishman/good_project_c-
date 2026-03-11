using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlApi
{
    public interface ICrud<T>
    {
        int Create(T item); //Creates new entity object in DAL
        T? Read(); //stage 2 
        T? Read(Func<T, bool>? filter); //stage 2 
        List<T> ReadAll(Func<T, bool>? filter = null); //stage 2 
        void Update(T item); //Updates entity object
        void Delete(int id); //Deletes an object by its Id
    }
}
