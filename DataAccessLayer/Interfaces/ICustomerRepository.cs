using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetCustomersAsync();

    //Task<Customer> GetCustomerByIdAsync(int id);
    Task<Customer> AddCustomerAsync(Customer customer);
    //Task UpdateCustomerAsync(Customer customer);
    //Task DeleteCustomerAsync(int id);
}
