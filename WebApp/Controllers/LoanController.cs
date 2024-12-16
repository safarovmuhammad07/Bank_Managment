using System.Net;
using Domain.Entities;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly IGenericService<Loan> _loanService;

    public LoanController(IGenericService<Loan> loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    public async Task<ApiResponse<List<Loan>>> GetAll()
    {
        return await _loanService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Loan>> GetById(int id)
    {
        return await _loanService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Loan loan)
    {
        return await _loanService.Add(loan);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Loan loan)
    {
        return await _loanService.Update(loan);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await _loanService.Delete(id);
    }
}