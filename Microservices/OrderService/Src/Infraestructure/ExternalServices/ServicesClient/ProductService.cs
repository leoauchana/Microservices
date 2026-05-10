using Application.DTOs;
using Application.Interfaces.ExternalServices;
using System.Net;
using System.Net.Http.Json;
namespace Infraestructure.ExternalServices.ServicesClient;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/Product";
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductDto.Get>?> GetAllById(List<Guid> productIds)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/getByIds", productIds);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<ProductDto.Get>>();
    }

    public async Task<ProductDto.Get?> GetById(Guid productId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/getById/{productId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProductDto.Get>();
    }
    public async Task ReduceStock(Dictionary<   > productStock)
    {

    }
}