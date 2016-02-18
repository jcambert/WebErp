using System.Collections.Generic;
using WebErp.Models;

namespace WebErp.Services
{
    public interface IUserService
    {
        UserContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, string[] roles);
        User GetUser(string userId);
        List<Role> GetUserRoles(string username);
    }
}