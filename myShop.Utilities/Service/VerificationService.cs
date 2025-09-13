using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myShop.DataAccess.Implementation;
using myShop.Entities.Repositories;
using myShop.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
namespace myShop.Utilities.Service
{
    public class VerificationService
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;

        public VerificationService(IunitOfWork unitOfWork, IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public void GenerateAndSendCode(ApplicationUser user)
        {
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            var verification = new UserVerificationCode
            {
                UserId = user.Id,
                Code = code,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            _unitOfWork.verificationCode.Add(verification);
            _unitOfWork.complete();

            _emailSender.SendEmailAsync(user.Email, "Verification Code",
                $"Your verification code is: <b>{code}</b>").Wait();
        }

        public bool VerifyCode(string userId, string code)
        {
            var record = _unitOfWork.verificationCode.GetValidCode(userId, code);

            if (record == null) return false;

            record.IsUsed = true;
            _unitOfWork.complete();

            return true;
        }

        public void CleanupExpiredCodes()
        {
            _unitOfWork.verificationCode.CleanupExpiredCodes();
            _unitOfWork.complete();
        }
    }
}
