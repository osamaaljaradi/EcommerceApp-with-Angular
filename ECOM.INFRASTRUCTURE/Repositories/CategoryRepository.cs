using ECOM.CORE.Entites.Product;
using ECOM.CORE.Interfaces;
using ECOM.INFRASTRUCTURE.Data;
using ECOM.INFRASTRUCTURE.Repositries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOM.INFRASTRUCTURE.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
