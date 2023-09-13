using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext) 
    { 
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync() 
    {
        return await _dbContext.Customers.ToListAsync();
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        var result = await _dbContext.AddAsync(customer);
        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        var result = await _dbContext.Customers.FindAsync(id);

        if (result == null)
        {
            return null;
            // throw new KeyNotFoundException($"Покупатель с идентификатором {id} не найден");
        }

        return result;
    }
    public async Task UpdateCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer));
        }

        var customerFromDb = await _dbContext.Customers.FindAsync(customer.Id);
        if (customerFromDb == null)
        {
            throw new KeyNotFoundException($"Покупатель с идентификатором {customer.Id} не найден");
        }

        // Копирование свойств из customer в customerFromDb
        _dbContext.Entry(customerFromDb).CurrentValues.SetValues(customer);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        var customerFromDb = await _dbContext.Customers.FindAsync(id);
        if (customerFromDb == null)
        {
            throw new KeyNotFoundException($"Покупатель с идентификатором {id} не найден");
        }

        _dbContext.Customers.Remove(customerFromDb);
        await _dbContext.SaveChangesAsync();
    }
}
