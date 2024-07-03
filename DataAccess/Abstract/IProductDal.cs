using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.DTO;


namespace DataAccess.Abstact
{
    public interface IProductDal:IEntityRepository<Product>
    {

    }
}
