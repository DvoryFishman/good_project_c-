using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BlApi;
using BL.BO;
using DO;

namespace BL.BlImplementation
{
    internal class SaliesImplementation : ISalies
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        public SaliesImplementation(DalApi.IDal saliesDAL)
        {
            _dal = saliesDAL;
        }

        public BL.BO.Salies Create(BL.BO.Salies item)
        {
            try
            {
                var saliesDO = BL.BO.Tools.ToDataObject(item);
                var createdId = _dal.Salies.Create(saliesDO);
                var result = _dal.Salies.Read(x => x.Id == createdId);
                return BL.BO.Tools.ToBO(result);
            }
            catch (DO.IdAlreadyExistsException e)
            {
                throw new BL.BO.BlAlreadyExistsException("Salies already exists", e);
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error creating salies", e);
            }
        }

        public BL.BO.Salies? Read(Func<BL.BO.Salies, bool> filter)
        {
            try
            {
                var salies = _dal.Salies.ReadAll(x => true).Select(s => BL.BO.Tools.ToBO(s)).FirstOrDefault(filter);
                return salies;
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error reading salies", e);
            }
        }

        public List<BL.BO.Salies> ReadAll(Func<BL.BO.Salies, bool>? filter = null)
        {
            try
            {
                var saliesDO = _dal.Salies.ReadAll();
                var saliesBO = from salies in saliesDO
                               let cb = BL.BO.Tools.ToBO(salies)
                               where filter == null || filter(cb)
                               select cb;
                return saliesBO.ToList();
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error reading salies list", e);
            }
        }

        public void Update(BL.BO.Salies item)
        {
            try
            {
                var saliesDO = BL.BO.Tools.ToDataObject(item);
                _dal.Salies.Update(saliesDO);
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error updating salies", e);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _dal.Salies.Delete(id);
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error deleting salies", e);
            }
        }

        public bool IsSaliesExist(int id)
        {
            try
            {
                return _dal.Salies.Read(x => x.Id == id) != null;
            }
            catch (DO.IdNotFoundException)
            {
                return false;
            }
        }
    }
}