using MediatR;
using Microsoft.AspNetCore.Mvc;
using stockInfoApi.DAL.Models.DboModels;
using stockInfoApi.DAL.Models.ResponseDtos;
using stockInfoApi.DAL.Queries.Accounts;
using stockInfoApi.DAL.Validations;
using stockInfoApi.Models.AccountDtos;

namespace stockInfoApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDbo>>> GetAccounts()
        {
            var accounts = await _mediator.Send(new GetAccountListQuery());
            if (accounts == null)
            {
                return NotFound(new ResponseMessageDto<AccountDbo>("error", "No accounts found"));
            }
            return Ok(new ResponseMessageDto<IEnumerable<AccountDbo>>("success", "success", accounts));
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDbo>> GetAccountDbo(Guid id)
        {
            var account = await _mediator.Send(new GetAccountByIdQuery(id));
            if (account == null)
            {
                return NotFound(new ResponseMessageDto<AccountDbo>("error", "No account found"));
            }
            return Ok(new ResponseMessageDto<AccountDbo>("success", "success", account));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, PutAccountDto putAccountDto)
        {
            ValidationCheck validDto = DtoValidations.ValidPutAccountDto(putAccountDto);
            if (validDto.Error)
            {
                return BadRequest(new ResponseMessageDto<AccountDbo>("error", $"{validDto.Error}"));
            }

            AccountDbo account = await _mediator.Send(new UpdateAccountByIdQuery(id, putAccountDto));
            if (account == null)
            {
                return NotFound(new ResponseMessageDto<AccountDbo>("error", "No Account Found"));
            }

            if (account.AccountId != id)
            {
                return BadRequest(new ResponseMessageDto<AccountDbo>("error", "email already in use"));
            }
            return Ok(new ResponseMessageDto<AccountDbo>("success", "success", account));
        }

        // POST: api/AccountDboes
        [HttpPost]
        public async Task<ActionResult<AccountDbo>> PostAccountDbo(PostAccountDto postAccountDto)
        {
            ValidationCheck validDto = DtoValidations.ValidPostAccountDto(postAccountDto);
            if (validDto.Error)
            {
                return BadRequest(new ResponseMessageDto<AccountDbo>("error", $"{validDto.Error}"));
            }

            AccountDbo account = await _mediator.Send(new CreateAccountQuery(postAccountDto));
            if (account == null)
            {
                return BadRequest(new ResponseMessageDto<AccountDbo>("error", "email already in use"));
            }
            return Ok(new ResponseMessageDto<AccountDbo>("success", "succes", account));
        }

        // DELETE: api/AccountDboes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountDbo(Guid id)
        {
            AccountDbo account = await _mediator.Send(new DeleteAccountByIdQuery(id));
            if (account == null)
            {
                return Ok(new ResponseMessageDto<AccountDbo>("error", "No account found"));
            }
            return Ok(new ResponseMessageDto<AccountDbo>("success", "succes", account));
        }
    }
}
