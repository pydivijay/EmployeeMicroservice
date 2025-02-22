using EmployeeMicroserviceAPI.Models;
using MediatR;

namespace EmployeeMicroserviceAPI.Features.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
    }
}
