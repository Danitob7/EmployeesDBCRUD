using EmployeesDBProject.Data;
using EmployeesDBProject.Models;
using EmployeesDBProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EmployeesDBProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext dbContext;

        public EmployeesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel) {

            var employee = new Employee
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
            };

            await dbContext.Employees.AddAsync(employee);

            if (ModelState.IsValid)
            {
                await dbContext.SaveChangesAsync();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {

            var employees = await dbContext.Employees.ToListAsync();

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            var employee = await dbContext.Employees.FindAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee viewModel)
        {

            var employee = await dbContext.Employees.FindAsync(viewModel.Id);

           if(employee is not null)
            {
                employee.Name = viewModel.Name;
                employee.Email = viewModel.Email;
                employee.Phone = viewModel.Phone;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Employee viewModel)
        {

            var employee = await dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (employee is not null)
            {
                dbContext.Employees.Remove(employee);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
