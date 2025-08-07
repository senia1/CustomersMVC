using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface ITransactionsService
    {
        Task<IEnumerable<Transactions>> GetTransactionsAsync();
        Task<Transactions> GetTransactionsByIdAsync(int id);
        Task<Transactions> AddTransactionAsync(Transactions transaction);
        Task UpdateTransactionAsync(Transactions transaction);
        Task DeleteTransactionAsync(int id);
    }
}
