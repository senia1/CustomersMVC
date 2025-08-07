using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer
{
    public class TransactionsService: ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public async Task<IEnumerable<Transactions>> GetTransactionsAsync()
        {
            return await _transactionsRepository.GetTransactionsAsync();
        }

        public async Task<Transactions> GetTransactionsByIdAsync(int id)
        {
            return await _transactionsRepository.GetTransactionsByIdAsync(id);
        }

        public async Task<Transactions> AddTransactionAsync(Transactions transaction)
        {
            return await _transactionsRepository.AddCTransactionsAsync(transaction);
        }

        public async Task UpdateTransactionAsync(Transactions transaction)
        {
            await _transactionsRepository.UpdateTransactionsAsync(transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            await _transactionsRepository.DeleteTransactionsAsync(id);
        }
    }
}
