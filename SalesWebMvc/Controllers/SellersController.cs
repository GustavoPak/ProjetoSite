using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;


namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _depService;

        public SellersController(SellerService seller,DepartmentService department)
        {
            _sellerService = seller;
            _depService = department;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _depService.FindAllAsync();
            var viewmodel = new SellerFormViewModel { Departments = departments };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
           if (!ModelState.IsValid)
            {
                var departments = await _depService.FindAllAsync();
                var viewmodel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewmodel);
            }

           await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),new {message = "Id not Provided!" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found!" });
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int ? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Provided!" });
            }

            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found!" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int ? Id)
        {
            if(Id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Provided!" });
            }

            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not Found!" });
            }

            List<Department> dep = await _depService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel {Seller = obj ,Departments = dep};

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _depService.FindAllAsync();
                var viewmodel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewmodel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch!" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
