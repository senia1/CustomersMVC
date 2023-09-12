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

    //GET
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var customerFromDb = await _customerService.GetCustomerByIdAsync(id.Value); // передаем параметр id

        if (customerFromDb == null)
        {
            return NotFound();
        }

        return View(customerFromDb);
    }

    //POST

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Customer obj)
    {
        if (id == null || obj == null)
        {
            return NotFound();
        }

        if (id != obj.Id)
        {
            return BadRequest();
        }

        var customerFromDb = await _customerService.GetCustomerByIdAsync(id);
        if (customerFromDb == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _customerService.UpdateCustomerAsync(obj);

            TempData["success"] = "Customer updated successfully";
            return RedirectToAction("Index");
        }

        return View(obj);
    }

}
