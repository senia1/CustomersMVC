using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetCustomersAsync();
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var result = await _customerRepository.AddCustomerAsync(customer);

            return result;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var result = await _customerRepository.GetCustomerByIdAsync(id);

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

            var customerFromDb = await _customerRepository.GetCustomerByIdAsync(customer.Id);
            if (customerFromDb == null)
            {
                throw new KeyNotFoundException($"Покупатель с идентификатором {customer.Id} не найден");
            }

            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var customerFromDb = await _customerRepository.GetCustomerByIdAsync(id);
            if (customerFromDb == null)
            {
                throw new KeyNotFoundException($"Покупатель с идентификатором {id} не найден");
            }

            await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}