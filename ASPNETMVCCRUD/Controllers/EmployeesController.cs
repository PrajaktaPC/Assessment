using Microsoft.AspNetCore.Mvc;
using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace ASPNETMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDBContext mvcDemoDbContext;
        public EmployeesController(MVCDemoDBContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel adddEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = adddEmployeeRequest.Name,
                Email = adddEmployeeRequest.Email,
                Salary = adddEmployeeRequest.Salary,
                Department = adddEmployeeRequest.Department,
                DateOfBirth = adddEmployeeRequest.DateOfBirth
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            
            if (employee != null)
            {
             var viewModel = new UpdateEmployeeViewModel()
            {
              Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary,
                Department = employee.Department,
                DateOfBirth = employee.DateOfBirth  
            };
            return await Task.Run(()=> View("View", viewModel));

            }

            return RedirectToAction("Index");
        }
    [HttpPost]
    public async Task<IActionResult> View(UpdateEmployeeViewModel model)
    {
        var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

        if(employee != null)
        {
            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Salary = model.Salary;
            employee.DateOfBirth = model.DateOfBirth;
            employee.Department = model.Department;

            await mvcDemoDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
    {
        var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

        if(employee != null)
        {
            mvcDemoDbContext.Employees.Remove(employee);
            await mvcDemoDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }


    }
}
