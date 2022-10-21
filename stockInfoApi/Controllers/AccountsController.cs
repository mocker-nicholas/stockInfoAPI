using Microsoft.AspNetCore.Mvc;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.Models.AccountDtos;
using stockInfoApi.Models.DboModels;
using stockInfoApi.DAL.ResponseDtos;
using stockInfoApi.Helpers;
using stockInfoApi.Core.Models.ResponseDtos;

namespace stockInfoApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountFeatures _features;

        public AccountsController(IAccountFeatures features)
        {
            _features = features;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDbo>>> GetAccounts()
        {
            var accounts = await _features.GetAllAccounts();
            if(accounts == null)
            {
                return NotFound(new ResponseMessageDto<AccountDbo>("error", "No accounts found"));
            }
            return Ok(new ResponseMessageDto<IEnumerable<AccountDbo>>("success", "success", accounts));
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDbo>> GetAccountDbo(Guid id)
        {
            var account = await _features.GetAccountById(id);
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

            AccountDbo account = await _features.UpdateAccount(id, putAccountDto);
            if(account == null)
            {
                return NotFound(new ResponseMessageDto<AccountDbo>("error", "No Account Found"));
            }

            if(account.AccountId != id)
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

            AccountDbo account = await _features.CreateAccount(postAccountDto);
            if(account == null)
            {
                return BadRequest(new ResponseMessageDto<AccountDbo>("error", "email already in use"));
            }
            return Ok(new ResponseMessageDto<AccountDbo>("success", "succes", account));
        }

        // DELETE: api/AccountDboes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountDbo(Guid id)
        {
            AccountDbo account = await _features.DeleteAccount(id);
            if(account == null)
            {
                return Ok(new ResponseMessageDto<AccountDbo>("error", "No account found"));
            }
            return Ok(new ResponseMessageDto<AccountDbo>("success", "succes", account));
        }
    }
}
