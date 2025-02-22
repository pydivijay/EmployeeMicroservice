using MediatR;

namespace EmployeeMicroserviceAPI.Features.Employees.Commands
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
