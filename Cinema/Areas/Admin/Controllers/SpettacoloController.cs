using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using Cinema.Models.ViewModel;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SpettacoloController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public SpettacoloController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Operazioni di modifica per gli spettacoli";
            return View();
        }

        public IActionResult Upsert(int? fkSala, string? fkFilm, DateOnly data, TimeOnly orario)
        {
            SpettacoloVM spettacolo = new SpettacoloVM
            {
                Spettacolo = new (),
                FilmList = _unitOfWork.Film.GetAll().Select(item => new SelectListItem
                {
                    Text = item.Titolo,
                    Value= item.Titolo
                }),
                SalaList = _unitOfWork.Sala.GetAll().Select(item => new SelectListItem
                {
                    Text = item.Numero.ToString(),
                    Value = item.Numero.ToString()
                }),
            };

            if (fkFilm == null)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                ViewData["Title"] = "Crea una nuova sala";
                TempData["modifica"] = false;
                return View(spettacolo);
            }
            else
            {
                spettacolo.Spettacolo = _unitOfWork.Spettacolo.GetFirstOrDefault(u => fkSala == u.FkSala && fkFilm == u.FkFilm && orario == u.Orario && data == u.Data);
                ViewData["Title"] = $"Aggiorna spettacolo del {data} di {fkFilm} in sala {fkSala}";
                TempData["old_film"] = spettacolo.Spettacolo.FkFilm;
                TempData["old_sala"] = spettacolo.Spettacolo.FkSala;
                TempData["old_orario"] = spettacolo.Spettacolo.Orario.ToString();
                TempData["old_data"] = spettacolo.Spettacolo.Data.ToString();
                TempData["modifica"] = true;
                return View(spettacolo);

                //update product
            }


        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(SpettacoloVM obj)
        {
            if (ModelState.IsValid)
            {
                if (!(bool)TempData["modifica"])
                {
                    _unitOfWork.Spettacolo.Add(obj.Spettacolo);
                    _unitOfWork.Save();
                    TempData["success"] = "Spettacolo created successfully";
                }
                else
                {
                    _unitOfWork.Spettacolo.Update(obj.Spettacolo, new Spettacolo
                    {
                        FkFilm= (string)TempData["old_film"],
                        FkSala = (int)TempData["old_sala"],
                        Orario= TimeOnly.Parse((string)TempData["old_orario"]),
                        Data = DateOnly.Parse((string)TempData["old_data"])
                    });
                    _unitOfWork.Save();
                    TempData["success"] = "Spettacolo modificato successfully";
                }
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Spettacolo.GetAll();
            return Json(new { data = productList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? fkSala, string? fkFilm, DateOnly data, TimeOnly orario)
        {
            var obj = _unitOfWork.Spettacolo.GetFirstOrDefault(u => fkSala==u.FkSala && fkFilm == u.FkFilm && orario == u.Orario && data == u.Data);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Spettacolo.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
