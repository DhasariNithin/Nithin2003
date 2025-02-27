using Nithin2003.Models;

namespace Nithin2003.Services
{
    public interface IUserService
    {
        MyUser GetUser(string username);
        void UpdateUserBalance(string username, int newBalance);
    }
}
