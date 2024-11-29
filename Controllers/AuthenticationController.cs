using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindRestApi.Services.Interfaces;
using NorthwindRestApi.Models;
using Microsoft.Extensions.Logging;

namespace NorthwindRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticateService authenticateService, ILogger<AuthenticationController> logger)
        {
            _authenticateService = authenticateService;
            _logger = logger;
        }

        // This method handles login attempts from the front end
        [HttpPost]
        public ActionResult Post([FromBody] Credentials tunnukset)
        {
            // Log the received request data
            _logger.LogInformation("Received authentication request for username: {Username}", tunnukset.Username);
            _logger.LogInformation("Request payload: {@Payload}", tunnukset);

            var loggedUser = _authenticateService.Authenticate(tunnukset.Username, tunnukset.Password);

            if (loggedUser == null)
            {
                // Log the failed authentication attempt
                _logger.LogWarning("Authentication failed for username: {Username}", tunnukset.Username);
                return BadRequest(new { message = "Käyttäjätunus tai salasana on virheellinen" });
            }

            // Log the successful authentication
            _logger.LogInformation("User authenticated: {Username}", loggedUser.Username);
            _logger.LogInformation("Response payload: {@Response}", loggedUser);

            return Ok(loggedUser); // Return the loggedUser object to the front end
        }
    }
}
