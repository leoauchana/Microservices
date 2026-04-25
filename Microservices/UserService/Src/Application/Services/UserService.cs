using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;
    public UserService(IRepository repository)
    {
        _repository = repository;

    }
}