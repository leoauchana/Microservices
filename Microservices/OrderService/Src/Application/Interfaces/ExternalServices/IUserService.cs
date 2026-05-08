using Application.DTOs;

namespace Application.Interfaces.ExternalServices;

public interface IUserService
{
    Task<bool> Validate(Guid userId);
    Task<UserDto.GetById?> GetById(Guid userId);
}
