using Application.Interfaces.ExternalServices;
using Domain.Contracts;
using Infraestructure.ExternalServices.DTOs;
using Infraestructure.ExternalServices.Mapper;
using System.Collections.Immutable;
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

    public async Task<List<ProductSnapshot>?> GetAllById(List<Guid> productIds)
    {   
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/getByIds", productIds);

        if (response.StatusCode == HttpStatusCode.BadRequest)
            return null;

        response.EnsureSuccessStatusCode();

        var wrapper = await response.Content.ReadFromJsonAsync<ProductServiceDto.List>();

        if (wrapper == null || wrapper.count == 0) 
            return new List<ProductSnapshot>();

        return wrapper.productsFound.ToSnapshots();
    }

    public async Task<ProductSnapshot?> GetById(Guid productId)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/getById/{productId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProductSnapshot>();
    }
    public async Task ReduceStock(Dictionary<Guid, int> productStock)
    {
        var response = await _httpClient.PatchAsJsonAsync($"{BaseUrl}/reduceStock", productStock);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return;

        response.EnsureSuccessStatusCode();
    }
}