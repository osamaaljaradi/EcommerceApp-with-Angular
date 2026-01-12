using ECOM.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOM.CORE.Interfaces
{
    public interface IAuth
    {
        Task<string> registerAcync(RegisterDTO registerDTO);
        Task sendEmail(string email, string code, string component, string subject, string message);
        Task<string> LoginAsync(LoginDTO login);
        Task<bool> sendEmailForForgetPassword(string email);
        Task<string> resetPassword(ResetPasswordDTO resetPassword);
        Task<bool> activeAccount(ActiveAccountDTO accountDTO);
    }
}
