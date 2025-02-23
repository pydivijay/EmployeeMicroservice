using EmployeeMicroservice.Tests.Mocks;
using EmployeeMicroserviceAPI.Data;
using EmployeeMicroserviceAPI.Features.Employees.Queries;
using FluentAssertions;

namespace EmployeeMicroservice.Tests.Handlers
{
    public class GetAllEmployeesHandlerTests
    {
        private readonly EmployeeDbContext _context;

        public GetAllEmployeesHandlerTests()
        {
            _context = MockDbContext.GetDbContext();
        }

        // Test: Get All Employees
        [Fact]
        public async Task GetAllEmployeesHandler_ShouldReturnAllEmployees()
        {
            // Arrange
            var handler = new GetAllEmployeesHandler(_context);
            var query = new GetAllEmployeesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
