using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
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
