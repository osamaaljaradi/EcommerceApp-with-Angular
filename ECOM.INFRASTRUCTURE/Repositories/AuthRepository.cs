using ECOM.CORE.DTO;
using ECOM.CORE.Entites;
using ECOM.CORE.Interfaces;
using ECOM.CORE.Services;
using ECOM.CORE.Sharing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOM.INFRASTRUCTURE.Repositories
{
    public class AuthRepository:IAuth
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IGenerateToken generateToken;

        public AuthRepository(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken generateToken)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.generateToken = generateToken;
        }

        public async Task<string> registerAcync(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return null;
            }
            if (await userManager.FindByNameAsync(registerDTO.UserName) is not null) 
            {
                return "This UserName is Already Registerd";
            }
            if (await userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return "This Email is Already Registerd";
            }
            AppUser user = new AppUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                DisplayName=registerDTO.DisplayName
            };
            var result = await userManager.CreateAsync(user,registerDTO.Password);
            if (result.Succeeded is not true)
            {
                return result.Errors.ToList()[0].Description;
            }
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            await sendEmail(user.Email, token, "Active", "Active Email", "please active your email, click on button to active");

            return "done";
        }

        public async Task sendEmail(string email,string code,string component,string subject,string message)
        {
            var result = new EmailDTO(email, "osamazydalialjaradi@gmail.com", subject,
                EmailStringBody.send(email, code, component, message));
            await emailService.sendEmail(result);
        }

        public async Task<string> LoginAsync(LoginDTO login)
        {
            if(login == null)
            {
                return null;
            }
            var finduser= await userManager.FindByEmailAsync(login.Email);
            if (!finduser.EmailConfirmed)
            {
                var token =await userManager.GenerateEmailConfirmationTokenAsync(finduser);
                await sendEmail(finduser.Email, token, "Active", "Active Email", "please active your email, click on button to active");
                return "Please Confirme your first, we have send activate to E-mail";
            }
            var result = await signInManager.CheckPasswordSignInAsync(finduser, login.Password, true);
            if (result.Succeeded)
            {
                return generateToken.GetAndCreateToken(finduser);
            }
            return "Please check your email and password, something went wrong";
        }

        public async Task<bool> sendEmailForForgetPassword(string email)
        {
            var findUser=await userManager.FindByEmailAsync(email);
            if(findUser is null)
            {
                return false;
            }
            var token =await userManager.GeneratePasswordResetTokenAsync(findUser);
            await sendEmail(findUser.Email, token, "Reset-Password", "Reset Password", "Click on button to Reset your Password");
            return true;
        }

        public async Task<string> resetPassword(ResetPasswordDTO resetPassword)
        {
            var findUser=await userManager.FindByEmailAsync(resetPassword.Email);
            if (findUser is null) 
            {
                return null;
            }

            var result = await userManager.ResetPasswordAsync(findUser, resetPassword.Token, resetPassword.Password);
            if (result.Succeeded)
            {
                return "Password Change Success";
            }
            return result.Errors.ToList()[0].Description;
        }

        public async Task<bool> activeAccount(ActiveAccountDTO accountDTO)
        {
            var findUser=await userManager.FindByEmailAsync(accountDTO.Email);
            if(findUser is null)
            {
                return false;
            }
            var result = await userManager.ConfirmEmailAsync(findUser, accountDTO.Token);
            if (result.Succeeded)
            {
                return true;
            }
            var token = await userManager.GenerateEmailConfirmationTokenAsync(findUser);
            await sendEmail(findUser.Email, token, "Active", "Active Email", "please active your email, click on button to active");
            return false;

        }

    }
}
