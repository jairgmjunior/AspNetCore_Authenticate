using Shop.Models;

namespace Shop.Repositories
{
    public class UserRepository
    {
        public static User Get (string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, UserName = "Primeiro", Password = "primeiro", Role = "employee"  });
            users.Add(new User { Id = 2, UserName = "Segundo", Password = "segundo", Role = "manager"  });

            return users.FirstOrDefault(p => p.UserName.ToLower() == username.ToLower() && 
                                        p.Password.ToLower() == password.ToLower());
        }
    }
}