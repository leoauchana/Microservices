using System.Data;

namespace Application.Interfaces.Repositories;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnection();
}
