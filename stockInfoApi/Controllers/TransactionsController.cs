using Microsoft.AspNetCore.Mvc;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Models.StockAppDtos.TransactionDtos;

namespace stockInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsFeatures _features;
        public TransactionsController(ITransactionsFeatures features)
        {
            _features = features;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions(GetTransactionsDto getTransactionsDto)
        {
            List<StockTransactionDbo> transactions = await _features.GetAllTransactionsForAccount(getTransactionsDto.AccountId);
            if (!transactions.Any())
            {
                return NotFound(new ResponseMessageDto<StockTransactionDbo>("error", "No transactions found"));
            }
            return Ok(new ResponseMessageDto<List<StockTransactionDbo>>("success", "success", transactions));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id, GetTransactionDto getTransactionDto)
        {
            StockTransactionDbo transaction = await _features.GetTransactionById(id, getTransactionDto);
            if (transaction == null)
                return NotFound(new ResponseMessageDto<StockTransactionDbo>("error", "No transaction found"));
            return Ok(new ResponseMessageDto<StockTransactionDbo>("success", "success", transaction));
        }
    }
}
