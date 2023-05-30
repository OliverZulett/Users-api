using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Users.Controllers;
using Users.Models.Data;
using Users.Models.Repository;
using Xunit;

namespace UsersTests.Controllers
{
    public class HealthCheckControllerTests
    {
        private readonly HealthCheckController _healthCheckController;

        public HealthCheckControllerTests()
        {
            // Configurar la base de datos en memoria
            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Crear una instancia del contexto utilizando la base de datos en memoria
            var context = new UsersContext(options);

            // Crear una instancia del logger utilizando un LoggerFactory
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<HealthCheckController>();

            // Crear una instancia del controlador con el contexto y el logger
            _healthCheckController = new HealthCheckController(context, logger);
        }

        [Fact]
        public void CheckHealth_ReturnsOkWhenApiIsHealthy()
        {
            // Act
            var result = _healthCheckController.CheckHealth();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("API is healthy", ((OkObjectResult)result).Value);
        }
    }
}
