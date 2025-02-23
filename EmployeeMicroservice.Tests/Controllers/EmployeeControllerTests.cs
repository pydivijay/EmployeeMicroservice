using EmployeeMicroserviceAPI.Controllers;
using EmployeeMicroserviceAPI.Features.Employees.Commands;
using EmployeeMicroserviceAPI.Features.Employees.Queries;
using EmployeeMicroserviceAPI.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

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

        // Test: Create Employee
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

        //  Test: Get All Employees
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

        //  Test: Update Employee
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

        [Fact]
        public async Task UpdateEmployee_ShouldReturnBadResponse()
        {
            // Arrange
            var updateCommand = new UpdateEmployeeCommand { Id = 1, Name = "Updated Name", Position = "Lead Dev", Salary = 85000 };

            // Act
            var result = await _controller.UpdateEmployee(2, updateCommand);

            // Assert
            var okResult = result as BadRequestObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var updateCommand = new UpdateEmployeeCommand { Id = 1, Name = "Updated Name", Position = "Lead Dev", Salary = 85000 };
            var updatedEmployee = new Employee { Id = 100, Name = "Updated Name", Position = "Lead Dev", Salary = 85000 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEmployeeCommand>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync((Employee?)null);

            // Act
            var result = await _controller.UpdateEmployee(updateCommand.Id, updateCommand);

            // Assert
            var okResult = result as NotFoundResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(404);
        }


        //  Test: Delete Employee
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

        [Fact]
        public async Task DeleteEmployee_ShouldReturnNotFound()
        {
            // Arrange
            var employeeId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEmployeeCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteEmployee(employeeId);

            // Assert
            var noContentResult = result as NotFoundResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(404);
        }
    }
}
