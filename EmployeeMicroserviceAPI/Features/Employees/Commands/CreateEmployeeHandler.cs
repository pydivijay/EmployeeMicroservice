using EmployeeMicroserviceAPI.Data;
using EmployeeMicroserviceAPI.Models;
using MediatR;

namespace EmployeeMicroserviceAPI.Features.Employees.Commands
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly EmployeeDbContext _context;

        public CreateEmployeeHandler(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = request.Name,
                Position = request.Position,
                Salary = request.Salary
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return employee;
        }
    }
}
