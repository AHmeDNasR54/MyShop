using myShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myShop.Entities.Repositories
{
    public interface IVerificationCodeRepository : IGenericRepository<UserVerificationCode>
    {
        UserVerificationCode GetValidCode(string userId, string code);
        void CleanupExpiredCodes();
    }
}
