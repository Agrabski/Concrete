using Concrete.Users;

namespace Concrete.Server.Requests;

public record CreateUserRequest(string Username, string Name, string Surname, string Password, UserRole Role, string Email);
