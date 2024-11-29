﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
    

        // Alustetaan tietokantayhteys

        // Perinteinen tapa
        // NorthwindContext db = new NorthwindContext();

        // Dependency injektion tapa
        private NorthwindContext db;

        public CustomersController(NorthwindContext dbparametri)
        {
            db = dbparametri;
        }

        // Hakee kaikki asiakkaat
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var asiakkaat = db.Customers.ToList();
                return Ok(asiakkaat);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e.InnerException);
            }
        }

        // Hakee asiakkaan id:n perusteella
        [HttpGet("{id}")]
        public ActionResult GetOneCustomerById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null)
                {
                    return Ok(asiakas);
                }
                else
                {
                    //return BadRequest("Asiakasta id:llä " + id + " ei löydy."); // perinteinen tapa liittää muuttuja
                    return NotFound($"Asiakasta id:llä {id} ei löydy."); // string interpolation -tapa
                }

            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e);
            }

        }



        // Uuden lisääminen
        [HttpPost]
        public ActionResult AddNew([FromBody] Customer cust)
        {
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return Ok($"Lisättiin uusi asiakas {cust.CompanyName} from {cust.City}");
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e.InnerException);
            }
        }

        // Asiakkaan poistaminen
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {

                var asiakas = db.Customers.Find(id);

                if (asiakas != null)
                {  // Jos id:llä löytyy asiakas
                    db.Customers.Remove(asiakas);
                    db.SaveChanges();
                    return Ok("Asiakas " + asiakas.CompanyName + " poistettiin.");
                }

                return NotFound("Asiakasta id:llä " + id + " ei löytynyt.");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        // Asiakkaan muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditCustomer(string id, [FromBody] Customer customer)
        {
            var asiakas = db.Customers.Find(id);
            if (asiakas != null)
            {

                asiakas = customer;
               // Ei ole pakko luetella kaikkia kenttiä sittenkään

                db.SaveChanges();
                return Ok("Muokattu asiakasta " + asiakas.CompanyName);
            }

            return NotFound("Asikasta ei löytynyt id:llä " + id);
        }

        // Hakee nimen osalla: /api/companyname/hakusana
        [HttpGet("companyname/{cname}")]
        public ActionResult GetByName(string cname)
        {
            try
            {
                var cust = db.Customers.Where(c => c.CompanyName.Contains(cname));

                //var cust = from c in db.Customers where c.CompanyName.Contains(cname) select c; <-- sama mutta traditional


                // var cust = db.Customers.Where(c => c.CompanyName == cname); <--- perfect match

                return Ok(cust);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
