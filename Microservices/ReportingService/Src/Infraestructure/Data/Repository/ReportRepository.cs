using System.Text;
using Application.Common;
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
    public async Task<PagedResult<OrderReport>> GetOrdersByDate(
        int page = 1,
        int pageSize = 10,
        DateOnly? from = null,
        DateOnly? to = null)
    {
        var offset = (page - 1) * pageSize;

        using var connection = await _dbConnectionFactory.CreateConnection();

        var sql = """
                        SELECT COUNT(*)
                        FROM orders
                        WHERE (@From IS NULL OR creationdate >= @From)
                            AND (@To IS NULL OR creationdate < @To);

                        SELECT *
                        FROM orders
                        WHERE (@From IS NULL OR creationdate >= @From)
                            AND (@To IS NULL OR creationdate < @To)
                        ORDER BY creationdate DESC
                        LIMIT @PageSize
                        OFFSET @OffSet;
            """;

        using var result = await connection.QueryMultipleAsync(sql, new
        {
            PageSize = pageSize,
            OffSet = offset,
            From = from,
            To = to
        });

        var count = await result.ReadSingleAsync<int>();

        var orders = (await result.ReadAsync<OrderReport>());

        return new PagedResult<OrderReport>
        {
            Items = orders.ToList(),
            TotalRecords = count,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<PagedResult<ProductReport>> GetProductsMoreSalesByDate(
        int page = 1,
        int pageSize = 10,
        DateOnly? from = null,
        DateOnly? to = null)
    {
        var offset = (page - 1) * pageSize;

        using var connection = await _dbConnectionFactory.CreateConnection();

        var sql =
            """
            SELECT COUNT(*)
            FROM 
            (orders o
            INNER JOIN order_item oi
                ON o.id = oi.orderId
            INNER JOIN products p
                ON p.id = oi.productId
            WHERE (@From IS NULL OR creationdate >= @From)
                AND (@To IS NULL OR creationdate < @To)
            GROUP BY p.id) x;

            SELECT p.name, p.description, SUM(oi.quantity) as CountSaled 
            FROM products p
            INNER JOIN order_item oi
                ON p.id = oi.productid
            INNER JOIN orders o
                ON o.id = oi.orderid
            WHERE (@From IS NULL OR creationdate >= @From)
                AND (@To IS NULL OR creationdate < @To)
            GROUP BY p.id
            ORDER BY CountSaled DESC
            LIMIT @PageSize
            OFFSET @OffSet;
            """;

        using var result = await connection.QueryMultipleAsync(sql, new
        {
            PageSize = pageSize,
            OffSet = offset,
            From = from,
            To = to
        });

        var count = await result.ReadSingleAsync<int>();

        var products = (await result.ReadAsync<ProductReport>()).ToList();
        
        return new PagedResult<ProductReport>
        {
            Items = products,
            TotalRecords = count,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task RegisterOrderCreated(
        Guid orderId,
        decimal total,
        DateTime creationDate,
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
                CreationDate = creationDate
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

    public async Task RegisterProductCreated(Guid idProduct,
                                            string name,
                                            string description,
                                            DateTime creationDate)
    {

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
            CreationDate = creationDate
        });
    }
}
