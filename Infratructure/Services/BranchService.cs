using System.Net;
using Dapper;
using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Npgsql;

namespace Infratructure.Services;

public class BranchService(DapperContext context) : IGenericService<Branch>
{
    public async Task<ApiResponse<List<Branch>>> GetAll()
    {
        using var connection = context.Connection();
        const string sql = "select * from branches where deletedat is null";
        var res = await connection.QueryAsync<Branch>(sql);
        return new ApiResponse<List<Branch>>(res.ToList());
    }

    public async Task<ApiResponse<Branch>> GetById(int id)
    {
        using var connection = context.Connection();
        const string sql = "select * from branches where branchid = @Id and deletedat is null";
        var res = await connection.QuerySingleOrDefaultAsync<Branch>(sql, new { Id = id });
        return res == null ? new ApiResponse<Branch>(HttpStatusCode.NotFound, "Branch not found") : new ApiResponse<Branch>(res);
    }

    public async Task<ApiResponse<bool>> Add(Branch data)
    {
        using var connection = context.Connection();
        const string sql = "insert into branches(branchname, branchlocation, createdat)  values(@BranchName, @BranchLocation, current_date);";
        var res = await connection.ExecuteAsync(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Branch data)
    {
        using var connection = context.Connection();
        const string sql = " update branches set branchname = @BranchName, branchlocation = @BranchLocation where branchid = @BranchId and deletedat is null;";
        var res = await connection.ExecuteAsync(sql, data);
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error") : new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        const string sql = "update branches set deletedat = current_date where branchid = @Id and deletedat is null";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        return res == 0 ? new ApiResponse<bool>(HttpStatusCode.NotFound, "Branch not found or already deleted") : new ApiResponse<bool>(res > 0);
    }
}
