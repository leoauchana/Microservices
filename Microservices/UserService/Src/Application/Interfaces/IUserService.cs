using Application.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    Task<bool> Create(UserDto.Create request);
    Task<List<UserDto.Get>> GetAll();
    Task<UserDto.Get> GetById(string id);
    Task<bool> Validate(string idUser);
}
