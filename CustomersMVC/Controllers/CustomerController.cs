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

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost] // атрибут, указывающий, что этот метод обрабатывает POST-запросы
    public async Task<IActionResult> Create(Customer customer) // метод, принимающий объект customer в качестве параметра
    {
        if (ModelState.IsValid) // проверка, что модель customer прошла валидацию
        {
            await _customerService.AddCustomerAsync(customer); // вызов сервиса для создания нового покупателя в базе данных
            return RedirectToAction(nameof(Index)); // перенаправление на метод Index для отображения списка покупателей
        }
        return View(customer); // если модель не прошла валидацию, возвращаем представление с ошибками
    }

    ////POST
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Create(Product obj)
    //{
    //    if (obj.Name == obj.DisplayOrder.ToString())
    //    {
    //        ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
    //    }
    //    if (ModelState.IsValid)
    //    {
    //        _db.Products.Add(obj);
    //        _db.SaveChanges();
    //        TempData["success"] = "Product created successfully";
    //        return RedirectToAction("Index");
    //    }
    //    return View(obj);
    //}
}
