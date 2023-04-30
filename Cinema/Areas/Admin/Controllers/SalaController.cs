using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using Cinema.Models.ViewModel;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Cinema.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SalaController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;
		public SalaController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;
		}
		public IActionResult Index()
		{
			ViewData["Title"] = "Operazioni di modifica per le sale";
			return View();
		}

		public IActionResult Upsert(int? id)
		{
			Sala sala = new Sala();

			if (id == null)
			{
				//create product
				//ViewBag.CategoryList = CategoryList;
				//ViewData["CoverTypeList"] = CoverTypeList;
				ViewData["Title"] = "Crea una nuova sala";

				return View(sala);
			}
			else
			{
				sala = _unitOfWork.Sala.GetFirstOrDefault(u => u.Numero == id);
				ViewData["Title"] = "Aggiorna sala numero " + id;
				return View(sala);

				//update product
			}


		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(Sala obj, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				try
				{
					_unitOfWork.Sala.Add(obj);
					_unitOfWork.Save();
				}
				catch (Exception)
				{
					_unitOfWork.Sala.Update(obj);
					_unitOfWork.Save();
				}
				TempData["success"] = "Sala created successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var productList = _unitOfWork.Sala.GetAll();
			return Json(new { data = productList });
		}

		//POST
		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var obj = _unitOfWork.Sala.GetFirstOrDefault(u => u.Numero == id);
			if (obj == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}
			_unitOfWork.Sala.Remove(obj);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Delete Successful" });

		}
		#endregion
	}
}
