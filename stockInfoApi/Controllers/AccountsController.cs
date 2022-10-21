using Microsoft.AspNetCore.Mvc;
using stockInfoApi.DAL.ControllerFeatures;
using stockInfoApi.DAL.Interfaces;
using stockInfoApi.Models.AccountDtos;
using stockInfoApi.Models.DboModels;

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
            return Ok(accounts);
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDbo>> GetAccountDbo(Guid id)
        {
            var account = await _features.GetAccountById(id);
            return Ok(account);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, PutAccountDto putAccountDto)
        {
            var response = await _features.UpdateAccount(id, putAccountDto);
            return Ok(response);
        }

        // POST: api/AccountDboes
        [HttpPost]
        public async Task<ActionResult<AccountDbo>> PostAccountDbo(PostAccountDto postAccountDto)
        {
            var response = await _features.CreateAccount(postAccountDto);
            return Ok(response);
        }

        // DELETE: api/AccountDboes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountDbo(Guid id)
        {
            var response = _features.DeleteAccount(id);
            return Ok(response);
        }
    }
}
