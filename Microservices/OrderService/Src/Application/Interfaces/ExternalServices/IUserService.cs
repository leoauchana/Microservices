using Application.DTOs;
using Domain.Contracts;

namespace Application.Interfaces.ExternalServices;

public interface IUserService
{
    Task<bool> Validate(Guid userId);
    Task<UserSnapshot?> GetById(Guid userId);
}
