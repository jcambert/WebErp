using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Data.Repositories;
using WebErp.Models;

namespace WebErp.Data.Extensions
{
    public static class UserExtensions
    {
        public static User GetSingleByUsername(this IModelBaseRepository<User> userRepository, string username)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.Username == username);
        }
    }
}
