using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;


namespace CustomersMVC.Controllers;

public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;  
    }
    public async Task<IActionResult> Index()
    {
        var customers = await _customerService.GetCustomersAsync();
        return View(customers);
    }
}
