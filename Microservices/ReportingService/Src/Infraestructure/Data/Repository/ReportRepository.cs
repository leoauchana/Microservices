using System.Text;
using Application.Interfaces.Repositories;
using Application.ReadModels;
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Infraestructure.Data.Repository;

public class ReportRepository : IReportRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly ILogger<ReportRepository> _logger;
    public ReportRepository(IDbConnectionFactory dbConnectionFactory,
    ILogger<ReportRepository> logger)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _logger = logger;
    }
    public async Task<List<OrderReport>> GetOrdersByDate(
        int limit = 10,
        DateOnly? date = null)
    {
        using var connection = await _dbConnectionFactory.CreateConnection();

        var sql = new StringBuilder(
            """
            SELECT * FROM orders
            WHERE 1 = 1
            """);

        if (date.HasValue)
        {
            sql.Append("""
        
                        AND creationdate = @Date
                        """);
        }
        sql.Append("""
        
                    LIMIT @Limit
                    """);

        var orders = await connection.QueryAsync<OrderReport>(sql.ToString(), new
        {
            Limit = limit,
            Date = date
        });
        return orders.ToList();
    }
    public async Task RegisterOrderCreated(
        Guid orderId,
        decimal total,
        DateOnly creationDate,
        Dictionary<Guid, int> productStock)
    {
        using var connection =
            (NpgsqlConnection)await _dbConnectionFactory.CreateConnection();

        using var transaction = await connection.BeginTransactionAsync();

        try
        {
            string orderSql = """
                    INSERT INTO orders (id, total, creationdate)
                    VALUES (@Id, @Total, @CreationDate)
                    """;

            await connection.ExecuteAsync(orderSql, new
            {
                Id = orderId,
                Total = total,
                CreationDate = creationDate.ToDateTime(TimeOnly.MinValue)
            }, transaction);

            string orderItemSql = """
                    INSERT INTO order_item (id, orderid, productid, quantity)
                    VALUES (@Id, @OrderId, @ProductId, @Quantity)
                    """;

            foreach (var product in productStock)
            {
                await connection.ExecuteAsync(
                    orderItemSql,
                    new
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        ProductId = product.Key,
                        Quantity = product.Value,
                    }, transaction);
            }
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Error to execution transaction: " + e.Message);

            await transaction.RollbackAsync();
        }

    }
    public async Task<List<ProductReport>> GetProducts(int limit = 10)
    {
        using var connection =
                    (NpgsqlConnection)await _dbConnectionFactory.CreateConnection();

        string sql = """
                        SELECT * FROM products
                        LIMIT @Limit
                     """;
        var products = await connection.QueryAsync<ProductReport>(sql,
        new
        {
            Limit = limit
        });
        return products.ToList();
    }
    public async Task RegisterProductCreated(Guid idProduct,
                                            string name,
                                            string description,
                                            DateOnly creationDate)
    {

        _logger.LogDebug($"Valueof creationDate in report repository is: {CreationDate}");

        // TODO: Implement the validation in method to register product created
        using var connection =
                    (NpgsqlConnection)await _dbConnectionFactory.CreateConnection();

        string sql = """
                        INSERT INTO products (id, name, description, creationdate)
                        VALUES (@Id, @Name, @Description, @CreationDate)
                     """;
        await connection.ExecuteAsync(sql, new
        {
            Id = idProduct,
            Name = name,
            Description = description,
            CreationDate = creationDate.ToDateTime(TimeOnly.MinValue)
        });
    }
}
