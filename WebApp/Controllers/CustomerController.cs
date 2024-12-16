using Domain.Entities;
using Infratructure.Interfaces;
using Infratructure.Responses;
using Infratructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController: ControllerBase
{
    
    
    private readonly IExtraCustomer _customerService;
    private readonly IGenericService<Customer> customerService;

    public CustomerController(IGenericService<Customer> _customerService)
    {
        customerService = _customerService;
    }

    public CustomerController(IExtraCustomer customerService)
    {
        _customerService = customerService;
    }
    
    
    [HttpGet]
    public async Task<ApiResponse<List<Customer>>> GetAll()
    {
        return await customerService.GetAll();
    }
    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Customer>> GetById(int id)
    {
        return await customerService.GetById(id);
    }

    [HttpGet("{customerId}")]
    public async Task<ApiResponse<int>> GetCustomerCount()
    {
        return await _customerService.GetCustomerCount();
    }
    
    [HttpPost]
    public async Task<ApiResponse<bool>> Add(Customer customer)
    {
        return await customerService.Add(customer);
    }
    [HttpPut]
    public async Task<ApiResponse<bool>> Update(Customer customer)
    {
        return await customerService.Update(customer);
    }
    [HttpDelete]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await customerService.Delete(id);
    }
}