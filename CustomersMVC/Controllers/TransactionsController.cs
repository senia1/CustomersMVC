using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CustomersMVC.Controllers
{
    public class TransactionsController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionsService.GetTransactionsAsync();
            return View(transactions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transactions transaction)
        {
            if (ModelState.IsValid)
            {
                await _transactionsService.AddTransactionAsync(transaction);
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
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
                var transaction = await _transactionsService.GetTransactionsByIdAsync(id.Value);
                return View(transaction);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transactions transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _transactionsService.UpdateTransactionAsync(transaction);
                    TempData["success"] = "Transaction updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                return View(transaction);
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
                var transaction = await _transactionsService.GetTransactionsByIdAsync(id.Value);
                return View(transaction);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Transactions transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            try
            {
                await _transactionsService.DeleteTransactionAsync(id);
                TempData["success"] = "Transaction deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }
    }
}
