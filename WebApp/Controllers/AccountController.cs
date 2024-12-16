using System.Net;
using Domain.Entities;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IGenericService<Account> _accountService;
    private readonly IExtraAccountService _extraAccountService;
    

    public AccountController(IExtraAccountService accountService)
    {
        _extraAccountService = accountService;
    }

    public AccountController(IGenericService<Account> accountService)
    {
        _accountService = accountService;
    }
    

    [HttpGet]
    public async Task<ApiResponse<List<Account>>> GetAll()
    {
        return await _accountService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Account>> GetById(int id)
    {
        return await _accountService.GetById(id);
    }

    [HttpGet("[action]/{type}")]
    public Task<ApiResponse<List<Account>>> GetByType(string type)
    {
        return Task.FromResult(_extraAccountService.GetByType(type));
    }

    [HttpGet("[action]/{status}")]
    public  async Task<ApiResponse<List<Account>>> GetByStatus(string status)
    {
        return  await _extraAccountService.GetByStatus(status);
    }
    
    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Account account)
    {
        return await _accountService.Add(account);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Account account)
    {
        return await _accountService.Update(account);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await _accountService.Delete(id);
    }
}