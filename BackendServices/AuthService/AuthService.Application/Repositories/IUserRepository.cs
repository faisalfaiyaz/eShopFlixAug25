using AuthService.Domain.Entities;

namespace AuthService.Application.Repositories;

public interface IUserRepository
{
    bool RegisterUser(User user, string role);
    User GetUserByEmail(string email);
    IEnumerable<User> GetAll();
}