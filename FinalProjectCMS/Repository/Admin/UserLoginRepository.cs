using FinalProjectCMS.Models;
using System.Linq;

namespace FinalProjectCMS.Repository.Admin
{
    public class UserLoginRepository:IUserLoginRepository
    {
        private readonly ASPCMSDBContext _context;
        private string pwd;
        private object un;

        public UserLoginRepository(ASPCMSDBContext context)
        {
            _context = context;
        }
        #region find user by redential
        public TblLoginUsers validateUser(string un, string pwd)
        {
            if (_context != null)
            {
                TblLoginUsers user = _context.TblLoginUsers.FirstOrDefault(us => us.UserName == un && us.Password == pwd);
                if (user != null)
                {
                    return user;
                }

            }
            return null;
        }
        #endregion

    }
}
