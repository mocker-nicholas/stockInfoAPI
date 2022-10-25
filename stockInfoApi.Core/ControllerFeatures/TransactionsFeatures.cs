using Microsoft.EntityFrameworkCore;
using stockInfoApi.DAL.Data;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Models.DboModels;

namespace stockInfoApi.DAL.ControllerFeatures
{
    public class TransactionsFeatures : ITransactionsFeatures
    {
        private readonly DevDbContext _context;
        public TransactionsFeatures(DevDbContext context)
        {
            _context = context;
        }
        public async Task<List<StockTransactionDbo>> GetAllTransactions(Guid id)
        {
            List<StockTransactionDbo> transactions = await _context.Transactions.Where(x => x.AccountId == id).ToListAsync();
            return transactions;
        }
    }
}
