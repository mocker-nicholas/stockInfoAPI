using MediatR;
using Microsoft.AspNetCore.Mvc;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Models.StockAppDtos.TransactionDtos;
using stockInfoApi.DAL.Queries.Transactions;

namespace stockInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions(GetTransactionsDto getTransactionsDto)
        {
            IEnumerable<StockTransactionDbo> transactions = await _mediator.Send(new GetAllTransactionsQuery(getTransactionsDto));
            if (!transactions.Any())
            {
                return NotFound(new ResponseMessageDto<StockTransactionDbo>("error", "No transactions found"));
            }
            return Ok(new ResponseMessageDto<IEnumerable<StockTransactionDbo>>("success", "success", transactions));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id, GetTransactionDto getTransactionDto)
        {
            StockTransactionDbo transaction = await _mediator.Send(new GetTransactionByIdQuery(id, getTransactionDto));
            if (transaction == null)
                return NotFound(new ResponseMessageDto<StockTransactionDbo>("error", "No transaction found"));
            return Ok(new ResponseMessageDto<StockTransactionDbo>("success", "success", transaction));
        }
    }
}
