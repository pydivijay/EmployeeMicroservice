using EmployeeMicroservice.Tests.Mocks;
using EmployeeMicroserviceAPI.Data;
using EmployeeMicroserviceAPI.Features.Employees.Commands;
using FluentAssertions;

namespace EmployeeMicroservice.Tests.Handlers
{
    public class DeleteEmployeeHandlerTests
    {
        private readonly EmployeeDbContext _context;

        public DeleteEmployeeHandlerTests()
        {
            _context = MockDbContext.GetDbContext();
        }

        // Test: Delete Employee - Success
        [Fact]
        public async Task DeleteEmployeeHandler_ShouldDeleteEmployee_WhenEmployeeExists()
        {
            // Arrange
            var handler = new DeleteEmployeeHandler(_context);
            var command = new DeleteEmployeeCommand { Id = 1 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            var employee = await _context.Employees.FindAsync(1);

            // Assert
            result.Should().BeTrue();
            employee.Should().BeNull(); // Employee should be removed
        }

        // Test: Delete Employee - Not Found
        [Fact]
        public async Task DeleteEmployeeHandler_ShouldReturnFalse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var handler = new DeleteEmployeeHandler(_context);
            var command = new DeleteEmployeeCommand { Id = 999 }; // Non-existent employee

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }
    }
}
