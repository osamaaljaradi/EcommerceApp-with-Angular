using ECOM.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOM.CORE.Services
{
    public interface IEmailService
    {
        Task sendEmail(EmailDTO emailDTO);
    }
}
