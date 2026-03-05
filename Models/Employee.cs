using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public required string FullName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [StringLength(50)]
        public required string Position { get; set; }
        [Required]
        public Department? Department { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateofBirth { get; set; }
        [Required]
        [StringLength(10)]
        public required string Gender { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal? Salary { get; set; }
        [Required]
        [Display(Name = "Employee Type")]
        public employeeType? Type { get; set; }


        // Additional properties can be added as needed
    }
}
