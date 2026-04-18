using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using StockMatch.Models;

namespace StockMatch.Services;

public class MaterialService
{
    private readonly string _connectionString;

    public MaterialService(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DEVConnection")
                            ?? throw new Exception("Connection string not found");
    }

    // Get all users
    public async Task<IEnumerable<Materials>> GetAllMaterials()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var rawData = await db.QueryAsync<Materials>("SELECT * FROM dbo.Materials");
            var firstRow = rawData.FirstOrDefault();
            return rawData;
        }
    }

    // Gets User Details
    public async Task<Materials?> GetMaterialDetails(string MaterialReq)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = "SELECT * FROM dbo.Materials WHERE Material = @Material";
            return await db.QueryFirstOrDefaultAsync<Materials>(sql, new { Material = MaterialReq });
        }
    }
}