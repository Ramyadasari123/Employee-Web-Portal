using EmployeePortalApp.Models;
using EmployeePortalApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace EmployeePortalApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeServices _employeeServices;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(EmployeeServices employeeServices, ILogger<EmployeeController> logger)
        {
            _employeeServices = employeeServices ;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string SearchItem, [FromQuery] string SelectedDepartment, [FromQuery] string SelectedType, [FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var (employees, totalCount) = await _employeeServices.getEmployees(SearchItem, SelectedDepartment, SelectedType, PageNumber, PageSize);

            var model = new EmployeeListViewModel
            {
                Employees = employees,
                TotalPages = (int)Math.Ceiling((double)totalCount / PageSize),
                SearchItem = SearchItem,
                SelectedDepartment = SelectedDepartment,
                SelectedType = SelectedType,
                PageSize = PageSize,
                PageNumber = PageNumber
            };
            GetSelectionLists();
            ViewBag.pageSize = new SelectList(new List<int> { 3, 5, 10, 15 });                   // Populate the selection lists for departments and employee types
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            GetSelectionLists(); // Populate the selection lists for departments and employee types
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromFormAttribute] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Insertion started");
                    _employeeServices.CreateEmployee(employee);
                    _logger.LogInformation($"New employee is created with name:{employee.FullName} ");
                    return RedirectToAction("Success", new { Id = employee.Id });
                }
                GetSelectionLists(); // Populate the selection lists for departments and employee types
                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating employee");
            }
            return View(employee);


        }
        public IActionResult Success(int id)
        {
            var employee = _employeeServices.getEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        public IActionResult Details([FromRoute]int id)
        {
            var employee = _employeeServices.getEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Update([FromRoute]int id)
        {
            var employee = _employeeServices.getEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            GetSelectionLists();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Update  started");
                _employeeServices.UpdateEmployee(employee);
                TempData["SuccessMessage"] = $"Employee:{ employee.Id} Employee updated successfully!";
                _logger.LogInformation($"New employee is created with name:{employee.Id} ");
                return RedirectToAction("List");
            }

            // Must repopulate dropdowns before returning the view so selects render correctly
            GetSelectionLists();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Delete([FromRoute]int id)
        {
            var employee = _employeeServices.getEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute]int id)
        {
            var employee = _employeeServices.getEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Delete  started");
            _employeeServices.DeleteEmployee(id);
             TempData["SuccessMessage"] = $"Employee:{ employee.Id} Employee deleted successfully!";
            _logger.LogInformation($"employee is deleted with name:{id} ");
             return RedirectToAction("List");
        }
        [HttpGet]
        public JsonResult getPosition(Department department)
        {
            var positions = new Dictionary<Department, List<string>>
            {

                { Department.IT,new List<string>{"Software Developer","Network Engineer","System Analyst","Database Administrator" } },
                { Department.HR,new List<string>{"HR Manager","Recruiter","Training Specialist","Compensation and Benefits Specialist" } },
                { Department.Finance,new List<string>{"Financial Analyst","Accountant","Auditor","Tax Specialist" } },
                { Department.Sales,new List<string>{"Sales Manager","Sales Executive","Business Development Manager","Account Manager" } }
            };
            var result = positions.ContainsKey(department) ? positions[department] : new List<string>();
                return Json(result);
        }
        private void GetSelectionLists()
        {
            ViewBag.Departments = new SelectList(Enum.GetValues(typeof(Department)).Cast<Department>());
            ViewBag.EmployeeTypes = new SelectList(Enum.GetValues(typeof(employeeType)).Cast<employeeType>());
            ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>());
            ViewBag.Position = new SelectList(Enum.GetValues(typeof(Position)).Cast<Position>());// Initialize with an empty list
        }

        [HttpGet("Employee/Edit/{idSegment}")]
        public IActionResult Edit([FromRoute] string idSegment)
        {
            if (!int.TryParse(idSegment, out var id))
            {
                // invalid id segment (e.g. "Employee") -> return 400 or redirect to list
                return BadRequest("Invalid employee id.");
            }

            var employee = _employeeServices.getEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            GetSelectionLists();
            // reuse the Update view
            return View("Update", employee);
        }

        [HttpPost("Employee/Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeServices.UpdateEmployee(employee);
                TempData["SuccessMessage"] = $"Employee:{ employee.Id} Employee updated successfully!";
                return RedirectToAction("List");
            }
            GetSelectionLists();
            return View("Update", employee);
        }

        [HttpGet("Employee/Edit")]
        public IActionResult Edit()
        {
            // No id provided — redirect to the list page (or show a friendly message)
            TempData["ErrorMessage"] = "No employee id specified. Select an employee to edit from the list.";
            return RedirectToAction("List");
        }
    }
}
