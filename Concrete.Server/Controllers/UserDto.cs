using Concrete.Users;

namespace Concrete.Server.Controllers;

public record UserDto(Guid Id, string Username, string Email, UserRole Role);
