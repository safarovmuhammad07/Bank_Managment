using System.Net;
using Domain.Entities;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchController : ControllerBase
{
    private readonly IGenericService<Branch> _branchService;

    public BranchController(IGenericService<Branch> branchService)
    {
        _branchService = branchService;
    }

    [HttpGet]
    public async Task<ApiResponse<List<Branch>>> GetAll()
    {
        return await _branchService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Branch>> GetById(int id)
    {
        return await _branchService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Branch branch)
    {
        return await _branchService.Add(branch);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Branch branch)
    {
        return await _branchService.Update(branch);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await _branchService.Delete(id);
    }
}