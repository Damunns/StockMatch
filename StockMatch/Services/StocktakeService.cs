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
    // Insert an inventory count and return the new identity id
    public async Task<int> InsertInventoryCount(InventoryCount count)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = @"
INSERT INTO [dbo].[InventoryCount]
    ([Material], [Batch], [SerialNumber], [CountQty], [Plant], [StorageLocation], [Reference], [PhysInvDoc], [PhysInvDocItem], [CountUser], [CountDateTime])
VALUES
    (@Material, @Batch, @SerialNumber, @CountQty, @Plant, @StorageLocation, @Reference, @PhysInvDoc, @PhysInvDocItem, @CountUser, @CountDateTime);
SELECT CAST(SCOPE_IDENTITY() AS INT);
";
            var id = await db.QuerySingleAsync<int>(sql, new
            {
                Material = count.Material,
                Batch = count.Batch,
                SerialNumber = count.SerialNumber,
                CountQty = count.CountQty,
                Plant = count.Plant,
                StorageLocation = count.StorageLocation,
                Reference = count.Reference,
                PhysInvDoc = count.PhysInvDoc,
                PhysInvDocItem = count.PhysInvDocItem,
                CountUser = count.CountUser,
                CountDateTime = count.CountDateTime
            });

            return id;
        }
    }
    // Try to find a matching PhysInvDoc record for the provided material/batch/storage location
    public async Task<PhysInvDocs?> FindMatchingPhysInvDoc(string material, string batch, string storageLocation)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = @"
SELECT TOP 1 *
FROM   [dbo].[PhysInvDocs]
WHERE  [StockLocation] = @StorageLocation
       AND [Material] = @Material
       AND [Batch] = @Batch
";
            var result = await db.QueryFirstOrDefaultAsync<PhysInvDocs>(sql, new { StorageLocation = storageLocation, Material = material, Batch = batch});
            return result;
        }
    }

    // Update the InventoryCounts row with PhysInvDoc and PhysInvDocItem
    public async Task<int> UpdateInventoryCountDoc(int countId, string physInvDoc, string physInvDocItem)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = @"
UPDATE [dbo].[InventoryCount]
SET [PhysInvDoc] = @PhysInvDoc,
    [PhysInvDocItem] = @PhysInvDocItem
WHERE [CountID] = @CountID;
";
            var rows = await db.ExecuteAsync(sql, new { PhysInvDoc = physInvDoc, PhysInvDocItem = physInvDocItem, CountID = countId });
            return rows;
        }
    }

    // Update the PhysInvDoc PhysInvDocItem row with Status as "Counted" and the datetime
    public async Task<int> UpdatePhysInvDocWithCount(DateTime countTime, string physInvDoc, string physInvDocItem)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = @"
UPDATE [dbo].[PhysInvDocs]
SET [CountStatus] = 'Counted',
    [CountDateTime] = @CountTime
WHERE [PhysInvDoc] = @PhysInvDoc
  AND [PhysInvDocItem] = @PhysInvDocItem
";
            var rows = await db.ExecuteAsync(sql, new { PhysInvDoc = physInvDoc, PhysInvDocItem = physInvDocItem, CountTime = countTime });
            return rows;
        }
    }
}