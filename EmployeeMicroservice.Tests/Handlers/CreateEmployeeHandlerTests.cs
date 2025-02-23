using EmployeeMicroservice.Tests.Mocks;
using EmployeeMicroserviceAPI.Data;
using EmployeeMicroserviceAPI.Features.Employees.Commands;

namespace EmployeeMicroservice.Tests.Handlers
{
    public class CreateEmployeeHandlerTests
    {
        private readonly EmployeeDbContext _context;

        public CreateEmployeeHandlerTests()
        {
            _context = MockDbContext.GetDbContext();
        }

        [Fact]
        public async Task CreateEmployee_ShouldAddEmployee()
        {
            // Arrange
            var handler = new CreateEmployeeHandler(_context);
            var command = new CreateEmployeeCommand
            {
                Name = "New Employee",
                Position = "Tester",
                Salary = 50000
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Employee", result.Name);
        }
    }
}
