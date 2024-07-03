using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstact;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        //[CacheAspect(1)]
        //[SecuredOperation("product.add,admin")]
        //[ValidationAspect(typeof(ProductValidator))]
        //[TransactionScopeAspect]
        //[CacheRemoveAspect("IProductService.Get")

        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName));

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        private IResult CheckIfProductNameExists(string productName)
        {

            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        public IDataResult<List<Product>> GetAll()
        {

            //if (DateTime.Now.Hour < 18)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintanenceTime);
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new DataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), true, "Products are packeaged");
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), Messages.ProductAdded);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        //public IDataResult<List<ProductDetailsDto>> GetProductDetails()
        //{
        //    if (DateTime.Now.Hour < 22)
        //    {
        //        return new ErrorDataResult<List<ProductDetailsDto>>(Messages.MaintanenceTime);
        //    }
        //    return new SuccessDataResult<List<ProductDetailsDto>>(_productDal.GetProductDetails());
        //}
        //[CacheRemoveAspect("IProductService.Get")]
        //[TransactionScopeAspect()]

        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId);
            if (result.Count >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryInvalid);
            }
            throw new NotImplementedException();
        }

        //[TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        //public IDataResult<List<Product>> GetAllByCategoryId(object categoryId)
        //{
        //    return _productDal.GetAll(p => p.CategoryId == categoryId);
        //}
    }
}
