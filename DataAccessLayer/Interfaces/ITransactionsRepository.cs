using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface ITransactionsRepository
{
    Task<IEnumerable<Transactions>> GetTransactionsAsync();

    Task<Transactions> GetTransactionsByIdAsync(int id);
    Task<Transactions> AddCTransactionsAsync(Transactions transaction);
    Task UpdateTransactionsAsync(Transactions transaction);
    Task DeleteTransactionsAsync(int id);
}
