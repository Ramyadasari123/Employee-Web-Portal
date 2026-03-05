using EmployeePortalApp.Data;
using EmployeePortalApp.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmployeePortalApp.Services
{
    public class EmployeeServices
    {
        private readonly AppDBContext _context;
        public EmployeeServices(AppDBContext context)
        {
            _context = context;
        }
        public async Task<(List<Employee> emp, int totalCount)> getEmployees(string SearchItem, string SelectedDepartments, string selectedType, int pagenumber, int PageSize)
        {
            var employees = _context.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(SearchItem))
            {
                    var pattern = $"%{SearchItem.Trim()}%";
                employees = employees.Where(x => EF.Functions.Like(x.FullName, pattern));
            }
            if (!string.IsNullOrEmpty(SelectedDepartments))
            {
                if (Enum.TryParse(SelectedDepartments, out Department department))
                {
                    employees = employees.Where(x => x.Department == department);
                }
            }
            if (!string.IsNullOrEmpty(selectedType))
            {
                if (Enum.TryParse(selectedType, out employeeType type))
                {
                    employees = employees.Where(x => x.Type == type);
                }
            }
            int count = employees.Count();

            // Ensure deterministic paging by ordering before Skip/Take
            employees = employees.OrderBy(e => e.Id)
                                 .Skip((pagenumber - 1) * PageSize)
                                 .Take(PageSize);

            return (await Task.FromResult(employees.ToList()), count); 
        }

        public Employee? getEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == id);
        }

        public void CreateEmployee(Employee employee)
        {
            if (employee != null)
            {
                // Let the database generate the identity value. Do not set employee.Id explicitly.
                _context.Employees.Add(employee);
                _context.SaveChanges();
                // After SaveChanges, EF will populate employee.Id with the generated value.
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            var emp = getEmployeeById(employee.Id);
            if (emp != null)
            {
                emp.FullName = employee.FullName;
                emp.Email = employee.Email;
                emp.Position = employee.Position;
                emp.Department = employee.Department;
                emp.HireDate = employee.HireDate;
                emp.DateofBirth = employee.DateofBirth;
                emp.Salary = employee.Salary;
                emp.Type = employee.Type;
            }
            _context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var emp = getEmployeeById(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
            }
            _context.SaveChanges();
        }
    }
}
