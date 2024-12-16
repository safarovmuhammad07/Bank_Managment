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
    public ApiResponse<List<Account>> GetAll()
    {
        return _accountService.GetAll();
    }

    [HttpGet("{id:int}")]
    public ApiResponse<Account> GetById(int id)
    {
        return _accountService.GetById(id);
    }

    [HttpGet("[action]/{type}")]
    public ApiResponse<List<Account>> GetByType(string type)
    {
        return _extraAccountService.GetByType(type);
            
    }

    [HttpGet("[action]/{status}")]
    public ApiResponse<List<Account>> GetByStatus(string status)
    {
        return _extraAccountService.GetByStatus(status);
    }
    
    [HttpPost]
    public ApiResponse<bool> Add(Account account)
    {
        return _accountService.Add(account);
    }

    [HttpPut]
    public ApiResponse<bool> Update(Account account)
    {
        return _accountService.Update(account);
    }

    [HttpDelete("{id:int}")]
    public ApiResponse<bool> Delete(int id)
    {
        return _accountService.Delete(id);
    }
}