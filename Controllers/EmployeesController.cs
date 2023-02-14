
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee=await mvcDemoDbContext.Employees.ToListAsync();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeView addempREquest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addempREquest.Name,
                Email = addempREquest.Email,
                Salary = addempREquest.Salary,
                DOB = addempREquest.DOB,
                Department = addempREquest.Department,
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return Redirect("Index");

           
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeView()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DOB = employee.DOB,
                    Department = employee.Department,

                };
                //return View(viewModel);
                return await Task.Run(()=> View("View",viewModel)) ;
            }
           // return RedirectToAction("Index");
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeView updateviewEmp)
        {

            var employee = await mvcDemoDbContext.Employees.FindAsync(updateviewEmp.Id);

            if (employee != null)
            {
                employee.Name = updateviewEmp.Name;
                employee.Email = updateviewEmp.Email;
                employee.Salary = updateviewEmp.Salary;
                employee.DOB = updateviewEmp.DOB;
                employee.Department = updateviewEmp.Department;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return Redirect("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeView updateviewEmp)
        {

            var employee = await mvcDemoDbContext.Employees.FindAsync(updateviewEmp.Id);

            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return Redirect("Index");
        }
    }
}
