using Dapper;
using Microsoft.Data.SqlClient;
using StockMatch.Models;
using System;
using System.Data;

namespace StockMatch.Services;

public class StocktakeService
{
    private readonly string _connectionString;

    public StocktakeService(IConfiguration config)
    {
        _connectionString = config["ConnectionStrings:DEVConnection"]
                            ?? throw new Exception("Connection string not found");
    }

    // Get all Materials
    public async Task<List<PhysInvDocs>> GetPhysInvDoc(string StorageLocationReq, string BatchReq)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = "SELECT [PhysInvDoc], [PhysInvDocItem]\r\nFROM [dbo].[PhysInvDocs]\r\nWHERE [StockLocation] = @StorageLocation\r\nAND [Batch] = @Batch";
            var rawData = await db.QueryAsync<PhysInvDocs>(sql, new { Batch = BatchReq, StorageLocation = StorageLocationReq });
            return rawData.ToList();
        }
    }

    // Get Material Details
    public async Task<PhysInvDocs?> GetBatchMaterial(string StorageLocationReq, string BatchReq)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = "SELECT [Material] FROM [dbo].[PhysInvDocs] WHERE [StockLocation] = @StorageLocation\r\nAND [Batch] = @Batch";
            var rawData = await db.QueryFirstOrDefaultAsync<PhysInvDocs>(sql, new { Batch = BatchReq, StorageLocation = StorageLocationReq });
            return rawData;
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