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
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
