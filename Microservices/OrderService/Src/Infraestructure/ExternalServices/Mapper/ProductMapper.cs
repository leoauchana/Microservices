using Domain.Contracts;
using Infraestructure.ExternalServices.DTOs;

namespace Infraestructure.ExternalServices.Mapper;

internal static class ProductMapper
{
    internal static ProductSnapshot ToSnapshot(this ProductServiceDto.Get dto) =>
    new(dto.id, dto.name, dto.price, dto.stock);

    internal static List<ProductSnapshot> ToSnapshots(this List<ProductServiceDto.Get> dtos) =>
        dtos.Select(dto => dto.ToSnapshot()).ToList();
}
