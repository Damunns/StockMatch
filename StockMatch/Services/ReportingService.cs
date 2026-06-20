using Dapper;
using Microsoft.Data.SqlClient;
using StockMatch.Models;
using System.Data;

namespace StockMatch.Services;

public class ReportingService
{
    private readonly string _connectionString;

    public ReportingService(IConfiguration config)
    {
        _connectionString = config["ConnectionStrings:DEVConnection"]
                            ?? throw new Exception("Connection string not found");
    }

    // Return recent/all inventory counts (limited to 1000 to avoid huge results)
    public async Task<List<InventoryCount>> GetAllInventoryCounts(int limit = 1000)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = $"SELECT TOP (@Limit) * FROM dbo.InventoryCount ORDER BY CountDateTime DESC";
            var raw = await db.QueryAsync<InventoryCount>(sql, new { Limit = limit });
            return raw.ToList();
        }
    }

    // Return all PhysInvDocs (useful for full listing)
    public async Task<List<PhysInvDocs>> GetAllPhysInvDocs()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var raw = await db.QueryAsync<PhysInvDocs>("SELECT * FROM dbo.PhysInvDocs");
            return raw.ToList();
        }
    }

    // Summary counts for PhysInvDocs: total / counted / not counted
    public async Task<(int total, int counted, int notCounted)> GetPhysInvDocsSummary()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var total = await db.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM dbo.PhysInvDocs");
            var counted = await db.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM dbo.PhysInvDocs WHERE CountStatus = 'Counted'");
            var notCounted = Math.Max(0, total - counted);
            return (total, counted, notCounted);
        }
    }
}