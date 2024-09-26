using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Luodaan tietokantayhteys
        NorthwindContext db = new NorthwindContext();

        // Hakee kaikki asiakkaat
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var asiakkaat = db.Customers.ToList();
            return Ok(asiakkaat);
        }
    }
}
