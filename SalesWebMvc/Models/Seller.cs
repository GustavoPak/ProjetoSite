using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(60, MinimumLength = 3,ErrorMessage = "{0} Name size should be between {2} and {1}")]
        public string Name { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Insert a valid Email")]
        [Required(ErrorMessage = "{0} Required")]
        public string Email { get; set; }
        
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} Required")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} Required")]
        [Range(100.0,5000.0, ErrorMessage = " {0} Must be in {1} to {2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SallesRecord> Sales { get; set; } = new List<SallesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSale(SallesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSale(SallesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime init,DateTime final)
        {
            return Sales.Where(sr => sr.Date >= init && sr.Date <= final).Sum(sr => sr.Amount); 
        }
    }
}
