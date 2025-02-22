using EmployeeMicroserviceAPI.Models;
using MediatR;

namespace EmployeeMicroserviceAPI.Features.Employees.Queries
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<Employee>> { }
}
