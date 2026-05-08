using Application.DTOs;
using Application.Interfaces.ExternalServices;
using System.Net;
using System.Net.Http.Json;
namespace Infraestructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/User";
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<UserDto.GetById?> GetById(Guid userId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/getById/{userId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserDto.GetById>();
    }
    public async Task<bool> Validate(Guid userId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/validate/{userId}");

        if (response.StatusCode == HttpStatusCode.NotFound) return false;

        response.EnsureSuccessStatusCode();

        return true;
    }

}
