using EmployeeMicroserviceAPI.Data;
using EmployeeMicroserviceAPI.Models;
using MediatR;

namespace EmployeeMicroserviceAPI.Features.Employees.Commands
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        private readonly EmployeeDbContext _context;

        public UpdateEmployeeHandler(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.Id);

            if (employee == null)
                return null;

            employee.Name = request.Name;
            employee.Position = request.Position;
            employee.Salary = request.Salary;

            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }
    }
}
