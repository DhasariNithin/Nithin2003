using Nithin2003.Models;

namespace Nithin2003.Services
{
    public class UserService : IUserService
    {
        // Temporary in-memory user list (Replace with database access later)
        private static List<MyUser> users = new List<MyUser>
        {
            new MyUser { Username = "Nitin", Password = "password123", AccountBalance = 5000 }
        };

        public MyUser GetUser(string username)
        {
            return users.FirstOrDefault(u => u.Username == username);
        }

        public void UpdateUserBalance(string username, int newBalance)
        {
            var user = GetUser(username);
            if (user != null)
            {
                user.AccountBalance = newBalance;
            }
        }
    }
}
