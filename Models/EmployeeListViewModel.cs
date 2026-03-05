using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmployeePortalApp.Models
{
    public class EmployeeListViewModel
    {
        public List<Employee> Employees { get; set; } = new();
        public int TotalPages { get; set; }
        public string SearchItem { get; set; } = string.Empty;
        public string SelectedDepartment { get; set; } = string.Empty;
        public string SelectedType { get; set; } = string.Empty;

        public int PageSize { get; set; } 
        public int PageNumber { get; set; }


        public IEnumerable<SelectListItem> DepartmentOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> EmployeeTypeOptions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> PageSizeOptions { get; set; } = new List<SelectListItem>();
        public int TotalCount { get; set; }
        //public int TotalPages { get; set; }
        //=> (int)Math.Ceiling((double)TotalCount / PageSize);

        //Additional properties for filtering, sorting, etc. can be added as needed
    }
}
