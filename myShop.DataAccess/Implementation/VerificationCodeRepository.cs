using Microsoft.EntityFrameworkCore;
using myShop.DataAccess.Data;
using myShop.Entities.Models;
using myShop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.DataAccess.Implementation
{
    public class VerificationCodeRepository : GenericRepository<UserVerificationCode>  ,IVerificationCodeRepository
    {
        private readonly ApplicationDbContext _context;
          
        public VerificationCodeRepository(ApplicationDbContext context) :base (context)
        {
            _context = context;

        }

        public UserVerificationCode GetValidCode(string userId, string code)
        {
            return _context.UserVerificationCodes
                .FirstOrDefault(x => x.UserId == userId
                                     && x.Code == code
                                     && !x.IsUsed
                                     && x.ExpiresAt > DateTime.UtcNow);
        }

        public void CleanupExpiredCodes()
        {
            var expired = _context.UserVerificationCodes
                .Where(x => x.ExpiresAt < DateTime.UtcNow || x.IsUsed)
                .ToList();

            if (expired.Any())
            {
                _context.UserVerificationCodes.RemoveRange(expired);
            }
        }
    }
}
