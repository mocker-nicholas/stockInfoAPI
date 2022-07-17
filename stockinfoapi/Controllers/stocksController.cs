using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stockinfoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class stocksController : ControllerBase
    {
        public async Task LoadStock(string queries)
        {

        }
    }
}
