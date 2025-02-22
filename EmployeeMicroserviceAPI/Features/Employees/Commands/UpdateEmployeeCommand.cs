using EmployeeMicroserviceAPI.Models;
using MediatR;

namespace EmployeeMicroserviceAPI.Features.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest<Employee>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
    }
}
