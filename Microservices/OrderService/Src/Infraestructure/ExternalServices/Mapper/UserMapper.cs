using Domain.Contracts;
using Infraestructure.ExternalServices.DTOs;

namespace Infraestructure.ExternalServices.Mapper;
internal static class UserMapper
{
    internal static UserSnapshot ToSnapshot(this UserServiceDto.Get dto) =>
    new(dto.id, dto.fullName, dto.email);
}