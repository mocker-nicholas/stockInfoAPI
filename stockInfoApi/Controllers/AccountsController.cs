using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.Data;
using stockInfoApi.Helpers;
using stockInfoApi.Models.AccountDtos;
using stockInfoApi.Models.DboModels;
using stockInfoApi.Models.ErrorDtos;

namespace stockInfoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DevDbContext _context;

        public AccountsController(DevDbContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDbo>>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDbo>> GetAccountDbo(Guid id)
        {
            var accountDbo = await _context.Accounts.FindAsync(id);

            if (accountDbo == null)
            {
                return NotFound(
                    new ErrorMessageDto($"No account was found for accountId: {id}")
                    );
            }

            return accountDbo;
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, PutAccountDto putAccountDto)
        {
            var validDto = DtoValidations.ValidPutAccountDto(putAccountDto);
            if(validDto.Error)
            {
                return BadRequest(new ErrorMessageDto($"{validDto.Message}"));
            }

            var account = await _context.Accounts.FindAsync(id);
            if(account == null)
            {
                return NotFound(new ErrorMessageDto("Account was not found"));
            }

            account.AccountType = putAccountDto.AccountType;
            account.FirstName = putAccountDto.FirstName;
            account.LastName = putAccountDto.LastName;
            account.EmailAddress = putAccountDto.EmailAddress;
            account.Nickname = putAccountDto.Nickname;

            // How the Heck does this thing know which account I am talking about?
            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountDboExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(account);
        }

        // POST: api/AccountDboes
        [HttpPost]
        public async Task<ActionResult<AccountDbo>> PostAccountDbo(PostAccountDto postAccountDto)
        {
            var validDto = DtoValidations.ValidPostAccountDto(postAccountDto);
            if (validDto.Error)
            {
                return BadRequest(new ErrorMessageDto($"{validDto.Message}"));
            }

            var existingAccount = AccountAlreadyExists(postAccountDto.EmailAddress);
            if (existingAccount)
            {
                return BadRequest(new ErrorMessageDto("Account already exists"));
            }
            var newAccount = new AccountDbo(
                postAccountDto.AccountType,
                postAccountDto.FirstName,
                postAccountDto.LastName,
                postAccountDto.EmailAddress,
                postAccountDto.Nickname
                );
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetAccountDbo", 
                new { id = newAccount.AccountId }, 
                newAccount
                );
        }

        // DELETE: api/AccountDboes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountDbo(Guid id)
        {
            var accountDbo = await _context.Accounts.FindAsync(id);
            if (accountDbo == null)
            {
                return NotFound(new ErrorMessageDto($"No account was found for id: {id}");
            }

            _context.Accounts.Remove(accountDbo);
            await _context.SaveChangesAsync();

            return Ok(new SuccessMessageDto($"Account: {id} was successfully deleted"));
        }

        private bool AccountDboExists(Guid id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

        private bool AccountAlreadyExists(string email)
        {
            var existingAccount = _context.Accounts
                .Where(a => a.EmailAddress.Equals(email));
            if (existingAccount.Any())
                return true;
            return false;
        }
    }
}
