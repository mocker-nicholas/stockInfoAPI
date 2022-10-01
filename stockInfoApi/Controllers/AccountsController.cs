using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stockInfoApi.Data;
using stockInfoApi.Models.AccountDtos;
using stockInfoApi.Models.DboModels;

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
                return NotFound();
            }

            return accountDbo;
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, PutAccountDto putAccountDto)
        {
            var account = await _context.Accounts.FindAsync(id);
            if(account == null)
            {
                return NotFound(new { Error = "Account was not found" });
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
            var newAccount = new AccountDbo(
                postAccountDto.AccountType,
                postAccountDto.FirstName,
                postAccountDto.LastName,
                postAccountDto.EmailAddress,
                postAccountDto.Nickname
                );
            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountDbo", new { id = newAccount.AccountId }, newAccount);
        }

        // DELETE: api/AccountDboes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountDbo(Guid id)
        {
            var accountDbo = await _context.Accounts.FindAsync(id);
            if (accountDbo == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(accountDbo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountDboExists(Guid id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
