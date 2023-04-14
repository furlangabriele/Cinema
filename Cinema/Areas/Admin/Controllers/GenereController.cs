using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenereController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenereController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Genere> objCategoryList = _unitOfWork.Genere.GetAll();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genere obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Genere.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Genere created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Genere.GetFirstOrDefault(u => u.Genere1 == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            TempData["old"] = categoryFromDbFirst.Genere1;

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genere obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Genere.Update(new Genere { Genere1 = (string)TempData["old"] }, obj);
                _unitOfWork.Save();
                TempData["success"] = "Genere updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Genere.GetFirstOrDefault(u => u.Genere1 == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string? id)
        {
            var obj = _unitOfWork.Genere.GetFirstOrDefault(u => u.Genere1 == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Genere.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Genere deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
