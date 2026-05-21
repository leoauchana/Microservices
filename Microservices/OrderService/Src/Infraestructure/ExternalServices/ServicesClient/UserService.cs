using Application.DTOs;
using Application.Interfaces.ExternalServices;
using Domain.Contracts;
using Infraestructure.ExternalServices.DTOs;
using Infraestructure.ExternalServices.Mapper;
using System.Net;
using System.Net.Http.Json;
namespace Infraestructure.ExternalServices.ServicesClient;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/User";
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<UserSnapshot?> GetById(Guid userId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/{userId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        var wrapper =  await response.Content.ReadFromJsonAsync<UserServiceDto.Response>();

        if(wrapper == null) return null;
        
        return wrapper.userFound.ToSnapshot();
    }
    public async Task<bool> Validate(Guid userId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/validate/{userId}");

        if (response.StatusCode == HttpStatusCode.NotFound) return false;

        response.EnsureSuccessStatusCode();

        return true;
    }

}
