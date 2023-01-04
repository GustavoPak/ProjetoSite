using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SallesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SallesRecordService (SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SallesRecord>> FindByDateAsync (DateTime? min, DateTime? max)
        {
            var result =  from obj in _context.Sales select obj;

            if (min.HasValue)
            {
                result = result.Where(x => x.Date >= min.Value);
            }
            if (max.HasValue)
            {
                result = result.Where(x => x.Date <= max.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

    }
}
