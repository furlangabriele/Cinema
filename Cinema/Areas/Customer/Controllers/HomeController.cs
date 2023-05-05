using Cinema.DataAccess.Repository;
using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
			_unitOfWork = unitOfWork;
		}

        public IActionResult Index()
        {
			//IEnumerable<Film> films = _unitOfWork.Film.GetAll();
			ViewData["Title"] = "HomePage";
			return View();
        }
		public IActionResult Details(string id)
		{
            var film = _unitOfWork.Film.GetFirstOrDefault(f => id == f.Titolo);
            ViewData["Title"] = film.Titolo;
            var spettacoloConFilm = _unitOfWork.Spettacolo.GetAll().Where(s => s.FkFilm == id);
            ViewBag.spettacoli = spettacoloConFilm;
			return View(film);
		}
		public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Recensioni(string id)
        {
            var recensioni = _unitOfWork.Valutazione.GetAll().Where(v => v.FkFilm == id);
            ViewData["navbar"] = false;
            return View(recensioni);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]    
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}