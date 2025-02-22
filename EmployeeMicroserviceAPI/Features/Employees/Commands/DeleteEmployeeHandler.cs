using EmployeeMicroserviceAPI.Data;
using MediatR;

namespace EmployeeMicroserviceAPI.Features.Employees.Commands
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly EmployeeDbContext _context;

        public DeleteEmployeeHandler(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.Id);

            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
