using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using StockMatch.Models;

public class UserService
{
    private readonly string _connectionString;

    public UserService(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DEVConnection")
                            ?? throw new Exception("Connection string not found");
    }

    // Get all users
    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var rawData = await db.QueryAsync<Users>("SELECT * FROM dbo.Users");
            var firstRow = rawData.FirstOrDefault();
            return rawData;
        }
    }

    // Gets User Details
    public async Task<Users?> GetCurrentUserDetails()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var sql = "SELECT * FROM dbo.Users WHERE LogonUsername = @LogonUsername";
            return await db.QueryFirstOrDefaultAsync<Users>(sql, new { LogonUsername = Environment.UserName });
        }
    }
}

