using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindContext db;

        public ProductsController(NorthwindContext dbparametri)
        {
            db = dbparametri;
        }

        // Hakee kaikki tuotteet
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var tuotteet = db.Products.ToList();
                return Ok(tuotteet);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e.InnerException);
            }
        }

        // Hakee tuotteen id:n perusteella
        [HttpGet("{id}")]
        public ActionResult GetOneProductById(int id)
        {
            try
            {
                var tuote = db.Products.Find(id);
                if (tuote != null)
                {
                    return Ok(tuote);
                }
                else
                {
                    return NotFound($"Tuotetta id:llä {id} ei löydy.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e);
            }
        }

        // Uuden tuotteen lisääminen
        [HttpPost]
        public ActionResult AddNew([FromBody] Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return Ok($"Lisättiin uusi tuote {product.ProductName}");
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e.InnerException);
            }
        }

        // Tuotteen poistaminen
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var tuote = db.Products.Find(id);
                if (tuote != null)
                {
                    db.Products.Remove(tuote);
                    db.SaveChanges();
                    return Ok($"Tuote {tuote.ProductName} poistettiin.");
                }
                return NotFound($"Tuotetta id:llä {id} ei löytynyt.");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        // Tuotteen muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditProduct(int id, [FromBody] Product product)
        {
            var tuote = db.Products.Find(id);
            if (tuote != null)
            {
                tuote = product;
                db.SaveChanges();
                return Ok($"Muokattu tuotetta {tuote.ProductName}");
            }
            return NotFound($"Tuotetta ei löytynyt id:llä {id}");
        }

        // Hakee tuotteen nimen osalla: /api/products/name/hakusana
        [HttpGet("name/{pname}")]
        public ActionResult GetByName(string pname)
        {
            try
            {
                var tuotteet = db.Products.Where(p => p.ProductName.Contains(pname));
                return Ok(tuotteet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
