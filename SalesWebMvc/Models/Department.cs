using System.Collections.Generic;
using System;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller sr)
        {
            Sellers.Add(sr);
        }

        public double TotalSales(DateTime init,DateTime final)
        {
            return Sellers.Sum(salles => salles.TotalSales(init,final));
        }
    }
}
