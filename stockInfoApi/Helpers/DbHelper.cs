using stockInfoApi.Data;

namespace stockInfoApi.Helpers
{
    public class DbHelper
    {
        private readonly DevDbContext _context;


        public DbHelper (DevDbContext context)
        {
            _context = context;
        }

        public void SimpleQuery()
        {
            var thing = _context.Stocks.FirstOrDefault();
        }
    }
}
