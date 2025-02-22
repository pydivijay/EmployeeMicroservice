using EmployeeMicroserviceAPI.Controllers;
using EmployeeMicroserviceAPI.Features.Employees.Commands;
using EmployeeMicroserviceAPI.Features.Employees.Queries;
using EmployeeMicroserviceAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;

namespace EmployeeMicroservice.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EmployeeController(_mediatorMock.Object);
        }

        // ✅ Test: Create Employee
        [Fact]
        public async Task CreateEmployee_ShouldReturnCreatedResponse()
        {
            // Arrange
            var command = new CreateEmployeeCommand { Name = "Test User", Position = "Dev", Salary = 60000 };
            var createdEmployee = new Employee { Id = 1, Name = "Test User", Position = "Dev", Salary = 60000 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEmployeeCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(createdEmployee);

            // Act
            var result = await _controller.CreateEmployee(command);

            // Assert
            var createdResult = result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(201);
            createdResult.Value.Should().BeEquivalentTo(createdEmployee);
        }

        // ✅ Test: Get All Employees
        [Fact]
        public async Task GetAllEmployees_ShouldReturnOkResponseWithEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Alice", Position = "Manager", Salary = 90000 },
                new Employee { Id = 2, Name = "Bob", Position = "Developer", Salary = 70000 }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllEmployeesQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(employees);

            // Act
            var result = await _controller.GetAllEmployees();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(employees);
        }

        // ✅ Test: Update Employee
        [Fact]
        public async Task UpdateEmployee_ShouldReturnOkResponse()
        {
            // Arrange
            var updateCommand = new UpdateEmployeeCommand { Id = 1, Name = "Updated Name", Position = "Lead Dev", Salary = 85000 };
            var updatedEmployee = new Employee { Id = 1, Name = "Updated Name", Position = "Lead Dev", Salary = 85000 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEmployeeCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(updatedEmployee);

            // Act
            var result = await _controller.UpdateEmployee(updateCommand.Id, updateCommand);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(updatedEmployee);
        }

        // ✅ Test: Delete Employee
        [Fact]
        public async Task DeleteEmployee_ShouldReturnNoContent()
        {
            // Arrange
            var employeeId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEmployeeCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteEmployee(employeeId);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
        }
    }
}
