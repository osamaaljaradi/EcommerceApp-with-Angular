using ECOM.CORE.DTO;
using ECOM.CORE.Entites.Product;
using ECOM.CORE.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOM.CORE.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<ReturnProductDTO> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsync(AddProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO);
        Task DeleteAsync(Product product);
    }
}
