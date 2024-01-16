using FinalProjectCMS.Models;

namespace FinalProjectCMS.Repository.Admin
{
    public interface IUserLoginRepository
    {
        TblLoginUsers validateUser(string un, string pwd);
    }
}
