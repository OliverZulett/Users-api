using Microsoft.AspNetCore.Mvc;
using Users.Models.Data;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly UsersContext _context;
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(UsersContext context, ILogger<HealthCheckController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ActionName(nameof(CheckHealth))]
        public IActionResult CheckHealth() {
            bool isDatabaseHealthy = CheckDatabaseHealth();
            bool areExternalServicesHealthy = CheckExternalServicesHealth();
            bool isApiHealthy = isDatabaseHealthy && areExternalServicesHealthy;
            if (isApiHealthy)
            {
                _logger.LogInformation("API health check successful.");
                return Ok("API is healthy");
            }
            else
            {
                return StatusCode(503, "API is not healthy");
            }
        }

        private bool CheckDatabaseHealth()
        {
            bool isDatabaseHealthy = false;

            try
            {
                using (_context)
                {
                    if (_context.Database.CanConnect())
                    {
                        // The database connection is successful
                        _logger.LogInformation("The database connection is successful.");
                        isDatabaseHealthy = true;
                    }
                    else
                    {
                        // The database connection is not successful
                        _logger.LogInformation("Failed to establish a connection to the database.");
                        isDatabaseHealthy = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking the database health.");
                isDatabaseHealthy = false;
            }

            return isDatabaseHealthy;
        }

        private bool CheckExternalServicesHealth()
        {
            return true;
        }
    }
}
