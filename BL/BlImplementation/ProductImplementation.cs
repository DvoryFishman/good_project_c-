using BL.BlApi;
using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
namespace BL.BlImplementation
{
    internal class ProductImplementation : IProduct
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        public ProductImplementation(DalApi.IDal productDAL)
        {
            _dal = productDAL;
        }

        public BL.BO.Product Create(BL.BO.Product item)
        {
            try
            {
                var productDO = BL.BO.Tools.ToDataObject(item);
                var createdId = _dal.Product.Create(productDO);
                var result = _dal.Product.Read(x => x.ProductId == createdId);
                return BL.BO.Tools.ToBO(result);
            }
            catch (DO.IdAlreadyExistsException e)
            {
                throw new BL.BO.BlAlreadyExistsException("Product already exists", e);
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error creating product", e);
            }
        }

        public BL.BO.Product? Read(Func<BL.BO.Product, bool> filter)
        {
            try
            {
                var product = _dal.Product.ReadAll(x => true).Select(s => BL.BO.Tools.ToBO(s)).FirstOrDefault(filter);
                return product;
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error reading product", e);
            }
        }

        public List<BL.BO.Product> ReadAll(Func<BL.BO.Product, bool>? filter = null)
        {
            try
            {
                var productsDO = _dal.Product.ReadAll();
                var productsBO = from product in productsDO
                                 let cb = BL.BO.Tools.ToBO(product)
                                 where filter == null || filter(cb)
                                 select cb;
                return productsBO.ToList();
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error reading products", e);
            }
        }

        public void Update(BL.BO.Product item)
        {
            try
            {
                var product = BL.BO.Tools.ToDataObject(item);
                _dal.Product.Update(product);
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error updating product", e);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _dal.Product.Delete(id);
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error deleting product", e);
            }
        }

        public bool IsExistProduct(BL.BO.Product item)
        {
            try
            {
                return _dal.Product.ReadAll().Any(c => c?.ProductId == item.ProductId);
            }
            catch (DO.IdNotFoundException e)
            {
                throw new BL.BO.BlNotFoundException("error checking if product exists", e);
            }
        }

        public void InForce(BL.BO.ProductInOrder p, bool IsFavorite)
        {
            // Implementation of InForce method from IProduct interface
        }
    }
}