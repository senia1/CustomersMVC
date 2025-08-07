using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces

{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transactions>> GetTransactionsAsync()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transactions> GetTransactionsByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var result = await _dbContext.Transactions.FindAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Транзакция с идентификатором {id} не найдена");
            }

            return result;
        }

        public async Task<Transactions> AddCTransactionsAsync(Transactions transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var result = await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task UpdateTransactionsAsync(Transactions transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var transactionFromDb = await _dbContext.Transactions.FindAsync(transaction.Id);
            if (transactionFromDb == null)
            {
                throw new KeyNotFoundException($"Транзакция с идентификатором {transaction.Id} не найдена");
            }

            // Копирование свойств из transaction в transactionFromDb
            _dbContext.Entry(transactionFromDb).CurrentValues.SetValues(transaction);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTransactionsAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var transactionFromDb = await _dbContext.Transactions.FindAsync(id);
            if (transactionFromDb == null)
            {
                throw new KeyNotFoundException($"Транзакция с идентификатором {id} не найдена");
            }

            _dbContext.Transactions.Remove(transactionFromDb);
            await _dbContext.SaveChangesAsync();
        }
    }
}