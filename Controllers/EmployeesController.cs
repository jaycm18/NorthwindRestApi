using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Models;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext db;

        public EmployeesController(NorthwindContext dbparametri)
        {
            db = dbparametri;
        }

        // Hakee kaikki työntekijät
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var tyontekijat = db.Employees.ToList();
                return Ok(tyontekijat);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e.InnerException);
            }
        }

        // Hakee työntekijän id:n perusteella
        [HttpGet("{id}")]
        public ActionResult GetOneEmployeeById(int id)
        {
            try
            {
                var tyontekija = db.Employees.Find(id);
                if (tyontekija != null)
                {
                    return Ok(tyontekija);
                }
                else
                {
                    return NotFound($"Työntekijää id:llä {id} ei löydy.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e);
            }
        }

        // Uuden työntekijän lisääminen
        [HttpPost]
        public ActionResult AddNew([FromBody] Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return Ok($"Lisättiin uusi työntekijä {employee.FirstName} {employee.LastName}");
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e.InnerException);
            }
        }

        // Työntekijän poistaminen
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var tyontekija = db.Employees.Find(id);
                if (tyontekija != null)
                {
                    db.Employees.Remove(tyontekija);
                    db.SaveChanges();
                    return Ok($"Työntekijä {tyontekija.FirstName} {tyontekija.LastName} poistettiin.");
                }
                return NotFound($"Työntekijää id:llä {id} ei löytynyt.");
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        // Työntekijän muokkaaminen
        [HttpPut("{id}")]
        public ActionResult EditEmployee(int id, [FromBody] Employee employee)
        {
            var tyontekija = db.Employees.Find(id);
            if (tyontekija != null)
            {
                tyontekija = employee;
                db.SaveChanges();
                return Ok($"Muokattu työntekijää {tyontekija.FirstName} {tyontekija.LastName}");
            }
            return NotFound($"Työntekijää ei löytynyt id:llä {id}");
        }

        // Hakee työntekijän nimen osalla: /api/employees/name/hakusana
        [HttpGet("name/{ename}")]
        public ActionResult GetByName(string ename)
        {
            try
            {
                var tyontekijat = db.Employees.Where(e => e.FirstName.Contains(ename) || e.LastName.Contains(ename));
                return Ok(tyontekijat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
