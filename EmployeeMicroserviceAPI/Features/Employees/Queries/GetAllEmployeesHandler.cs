using EmployeeMicroserviceAPI.Data;
using EmployeeMicroserviceAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMicroserviceAPI.Features.Employees.Queries
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly EmployeeDbContext _context;

        public GetAllEmployeesHandler(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.ToListAsync(cancellationToken);
        }
    }
}
