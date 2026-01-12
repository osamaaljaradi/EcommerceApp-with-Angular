using ECOM.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOM.CORE.Services
{
    public interface IGenerateToken
    {
       string GetAndCreateToken(AppUser user);
    }
}
