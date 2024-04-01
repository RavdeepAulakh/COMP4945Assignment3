using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Assignment3.Controllers;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NUnit.Framework.Legacy;

namespace Assignment3.Tests
{
    public class EmployeeControllerTests
    {
        [Test]
        public async Task Index_ReturnsViewResult_WithListOfEmployees()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                context.Employees.Add(new Employee { EmployeeId = 1, Name = "John", Address = "123 Main St" });
                context.Employees.Add(new Employee { EmployeeId = 2, Name = "Jane", Address = "456 Elm St" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(dbContextOptions))
            {
                var controller = new EmployeeController(context);

                // Act
                var result = await controller.Index();

                // Assert
                ClassicAssert.NotNull(result);
                ClassicAssert.IsInstanceOf<ViewResult>(result);

                var viewResult = result as ViewResult;
                ClassicAssert.NotNull(viewResult);

                var model = viewResult.Model as IEnumerable<Employee>;
                ClassicAssert.NotNull(model);
                ClassicAssert.AreEqual(2, model.Count());
            }
        }

        // Add more unit tests for other actions similarly
    }
}
