using Cinema.DataAccess.Repository.IRepository;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Cinema.Models;
using Cinema.Models.ViewModel;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AnaliticaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AnaliticaController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            int guadagnitotali = 0;
            var listaTickets = _unitOfWork.Biglietto.GetAll(); 
            foreach(var item in listaTickets)
            {
                var spettacolo = _unitOfWork.Spettacolo.GetFirstOrDefault(s => s.Id == item.SpettacoloId);
                if ((spettacolo.Data.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber) <= 7) 
                {
                    var sala = _unitOfWork.Sala.GetFirstOrDefault(s => s.Numero == spettacolo.FkSala);
                    if (sala.Isense && item.Fila >= 5)
                    {
                        guadagnitotali += 7;
                    }
                    else
                    {
                        guadagnitotali += 5;
                    }
                }
            }
            return View(guadagnitotali);
        }
        public IActionResult AnaliticaFilm()
        {
            return View();
        }
        public IActionResult AnaliticaSala()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAnaliticaFilm()
        {
            HashSet<AnaliticaFilmVM> analiticaFilms = new HashSet<AnaliticaFilmVM>();
            var listaTickets = _unitOfWork.Biglietto.GetAll();
            var listaFilms = _unitOfWork.Film.GetAll();
            foreach (var film in listaFilms)
            {
                int guadagnitotali = 0;
                foreach (var item in listaTickets)
                {
                    var spettacolo = _unitOfWork.Spettacolo.GetFirstOrDefault(s => s.Id == item.SpettacoloId);
                    if (spettacolo.FkFilm.Equals(film.Titolo))
                    {
                        var sala = _unitOfWork.Sala.GetFirstOrDefault(s => s.Numero == spettacolo.FkSala);
                        if (sala.Isense && item.Fila >= 5)
                        {
                            guadagnitotali += 7;
                        }
                        else
                        {
                            guadagnitotali += 5;
                        }
                    }
                }
                analiticaFilms.Add(new AnaliticaFilmVM
                {
                    Film = film.Titolo,
                    Guadagni = guadagnitotali,
                });
            }
            return Json(new { data = analiticaFilms });
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAnaliticaSala()
        {
            HashSet<AnaliticaSalaVM> analiticaFilms = new HashSet<AnaliticaSalaVM>();
            var listaTickets = _unitOfWork.Biglietto.GetAll();
            var listaSala = _unitOfWork.Sala.GetAll();
            foreach (var sala in listaSala)
            {
                int guadagnitotali = 0;
                foreach (var item in listaTickets)
                {
                    var spettacolo = _unitOfWork.Spettacolo.GetFirstOrDefault(s => s.Id == item.SpettacoloId);
                    if (spettacolo.FkSala.Equals(sala.Numero))
                    {
                        if (sala.Isense && item.Fila >= 5)
                        {
                            guadagnitotali += 7;
                        }
                        else
                        {
                            guadagnitotali += 5;
                        }
                    }
                }
                analiticaFilms.Add(new AnaliticaSalaVM
                {
                    Sala = sala.Numero,
                    Guadagni = guadagnitotali,
                });
            }
            return Json(new { data = analiticaFilms });
        }
    }
}
