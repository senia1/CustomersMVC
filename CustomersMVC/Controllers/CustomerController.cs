using BusinessLogicLayer;
using DataAccessLayer.Entities;
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (ModelState.IsValid) 
        {
            await _customerService.AddCustomerAsync(customer); 
            return RedirectToAction(nameof(Index)); 
        }
        return View(customer); 
    }
    // GET
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            return View(customer);
        }
        catch (Exception ex)
        {
            return View("Error", ex.Message);
        }
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Customer customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        try
        {
            if (ModelState.IsValid)
            {
                await _customerService.UpdateCustomerAsync(customer);
                TempData["success"] = "Customer updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        catch (Exception ex)
        {
            return View("Error", ex.Message);
        }
    }

    // GET
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id.Value);
            return View(customer);
        }
        catch (Exception ex)
        {
            return View("Error", ex.Message);
        }
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Customer customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        try
        {
            await _customerService.DeleteCustomerAsync(id);
            TempData["success"] = "Customer deleted successfully";
            return RedirectToAction(nameof(Index));

        }
        catch (Exception ex)
        {
            return View("Error", ex.Message);
        }
    }
}
