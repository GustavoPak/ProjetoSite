using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SallesRecordsController : Controller
    {
        public readonly SallesRecordService _salles;

        public SallesRecordsController(SallesRecordService salles)
        {
            _salles = salles;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime ? mindate, DateTime? maxdate)
        {
            if (!mindate.HasValue)
            {
                mindate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxdate.HasValue)
            {
                maxdate = DateTime.Now;
            }

            ViewData["minDate"] = mindate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxdate.Value.ToString("yyyy-MM-dd");

            var result = await _salles.FindByDateAsync(mindate, maxdate);

            return View(result);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
