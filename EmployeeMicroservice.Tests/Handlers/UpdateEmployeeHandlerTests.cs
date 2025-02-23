using EmployeeMicroservice.Tests.Mocks;
using EmployeeMicroserviceAPI.Data;
using EmployeeMicroserviceAPI.Features.Employees.Commands;

namespace EmployeeMicroservice.Tests.Handlers
{
    public class UpdateEmployeeHandlerTests
    {
        private readonly EmployeeDbContext _context;

        public UpdateEmployeeHandlerTests()
        {
            _context = MockDbContext.GetDbContext();
        }

        [Fact]
        public async Task UpdateEmployee_ShouldModifyEmployee()
        {
            // Arrange
            var handler = new UpdateEmployeeHandler(_context);
            var command = new UpdateEmployeeCommand
            {
                Id = 1,
                Name = "Updated Name",
                Position = "Lead Engineer",
                Salary = 90000
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
        }
    }
}
