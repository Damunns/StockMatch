using Dapper;
using Microsoft.Data.SqlClient;
using StockMatch.Models;
using System;
using System.Data;

namespace StockMatch.Services;

public class MasterDataService
{
    private readonly string _connectionString;

    public MasterDataService(IConfiguration config)
    {
        _connectionString = config["ConnectionStrings:DEVConnection"]
                            ?? throw new Exception("Connection string not found");
    }

    // Get all Materials
    public async Task<List<Materials>> GetAllMaterials()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var rawData = await db.QueryAsync<Materials>("SELECT * FROM dbo.Materials");
            return rawData.ToList();
        }
    }

    // Get Material Details
    public async Task<Materials?> GetMaterialDetails(string MaterialReq)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = "SELECT * FROM dbo.Materials WHERE Material = @Material";
            return await db.QueryFirstOrDefaultAsync<Materials>(sql, new { Material = MaterialReq });
        }
    }

    // Get all Storage Locations
    public async Task<List<StorageLocations>> GetAllStorageLocations()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var rawData = await db.QueryAsync<StorageLocations>("SELECT * FROM dbo.StorageLocations");
            return rawData.ToList();
        }
    }
}