using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    Task<bool> Create(UserDto.Request request);
    Task<List<UserDto.Response>> GetAll();
    Task<UserDto.Response> GetById(string id);
}
