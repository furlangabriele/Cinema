using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using Cinema.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FilmController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FilmController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Operazioni di modifica dei film";
            return View();
        }
        public IActionResult Upsert(string? id)
        {
            FilmVM filmVM = new()
            {
                Film = new(),
                GenereList = _unitOfWork.Genere.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Genere1,
                    Value = i.Genere1
                }),
            };

            if (id == null)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
                ViewData["Title"] = "Crea un nuovo film";

				return View(filmVM);
            }
            else
            {
                filmVM.Film = _unitOfWork.Film.GetFirstOrDefault(u => u.Titolo == id);
                ViewData["Title"] = "Aggiorna " + id;
				return View(filmVM);

                //update product
            }


        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(FilmVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\films");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Film.UrlImmPub != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Film.UrlImmPub.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Film.UrlImmPub = @"\images\films\" + fileName + extension;

                }
                try
                {
					_unitOfWork.Film.Add(obj.Film);
					_unitOfWork.Save();
				}
                catch (Exception)
                {
					_unitOfWork.Film.Update(obj.Film);
					_unitOfWork.Save();
                }
                TempData["success"] = "Film created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Film.GetAll();
            return Json(new { data = productList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(string? id)
        {
            var obj = _unitOfWork.Film.GetFirstOrDefault(u => u.Titolo == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.UrlImmPub.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Film.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
