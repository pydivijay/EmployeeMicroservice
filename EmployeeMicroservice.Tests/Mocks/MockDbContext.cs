using EmployeeMicroserviceAPI.Data;
using EmployeeMicroserviceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMicroservice.Tests.Mocks
{
    public static class MockDbContext
    {
        public static EmployeeDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB per test
                .Options;

            var dbContext = new EmployeeDbContext(options);

            // Ensure database is empty before seeding
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Employees.AddRange(new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Position = "Software Engineer", Salary = 60000 },
            new Employee { Id = 2, Name = "Jane Smith", Position = "Project Manager", Salary = 80000 }
        });

            dbContext.SaveChanges();
            return dbContext;
        }
    }
}
