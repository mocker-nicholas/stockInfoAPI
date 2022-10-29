using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.StockAppDtos.TransactionDtos;

namespace stockInfoApi.DAL.ControllerFeatures
{
    public class TransactionsFeatures : ITransactionsFeatures
    {
        private readonly DevDbContext _context;
        public TransactionsFeatures(DevDbContext context)
        {
            _context = context;
        }
        public async Task<List<StockTransactionDbo>> GetAllTransactionsForAccount(Guid accountId)
        {
            List<StockTransactionDbo> transactions = await _context.Transactions.Where(x => x.AccountId == accountId).ToListAsync();
            return transactions;
        }

        public async Task<StockTransactionDbo?> GetTransactionById(Guid id, GetTransactionDto getTransactionDto)
        {
            StockTransactionDbo? transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.AccountId == getTransactionDto.AccountId && x.TransactionId == id);
            return transaction;
        }
    }
}
