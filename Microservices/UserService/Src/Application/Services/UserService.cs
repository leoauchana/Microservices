using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Utilities;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;
    public UserService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Create(UserDto.Create newUser)
    {
        var userEmail = Email.Create(newUser.email);

        var userFound = await _repository.GetTheFirstOne<User>(u => u.Email == userEmail);

        if (userFound != null)
            throw new BusinessConflictException("User already exists with this email.");

        var user = new User(newUser.userName, newUser.password, newUser.fullName, userEmail);

        await _repository.Add(user);

        return true;
    }
    public async Task<List<UserDto.Get>> GetAll()
    {
        var users = await _repository.GetAll<User>();

        if (!users.Any())
            return new List<UserDto.Get>();

        return users.Select(u => new UserDto.Get(u.Id.ToString(), u.Username, u.Email.Value, u.FullName)).ToList();
    }
    public async Task<UserDto.Get> GetById(string id)
    {
        var idValidated = id.ValidateId();
        
        var user = await _repository.GetForId<User>(idValidated);
        
        if (user == null)
            throw new EntityNotFoundException("User not found.");
        
        return new UserDto.Get(user.Id.ToString(), user.Username, user.Email.Value, user.FullName);
    }

    public async Task<bool> Validate(string idUser)
    {
        var idValidated = idUser.ValidateId();
        
        var user = await _repository.GetForId<User>(idValidated);
        
        return user != null;
    }
}