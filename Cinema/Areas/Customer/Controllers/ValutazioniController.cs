using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using Cinema.Models.ViewModel;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ValutazioniController : Controller
    {
        private readonly ILogger<ValutazioniController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ValutazioniController(ILogger<ValutazioniController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult Index()
        {
            var userIdentity = User.Identity;
            var claimsIdentity = (ClaimsIdentity)userIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            string applicationUserId = null;
            if (claim == null)
            {
                return Redirect("/");
            }
            applicationUserId = claim.Value;
            var biglietti_utente = _unitOfWork.Biglietto.GetAll().Where(b => b.ApplicationUserId == applicationUserId && b.Pagato == true);
            var idspettacolo = new List<int>();
            foreach (var b in biglietti_utente)
            {
                if (!idspettacolo.Contains(b.SpettacoloId))
                {
                    idspettacolo.Add(b.SpettacoloId);
                }
            }
            var spettacoli_user = _unitOfWork.Spettacolo.GetAll().Where(s => idspettacolo.Contains(s.Id));
            var films = new List<Film>();
            foreach (var s in spettacoli_user)
            {
                var film = _unitOfWork.Film.GetAll().Where(f => f.Titolo == s.FkFilm).First();
                if (!films.Contains(film))
                {
                    films.Add(film);
                }
            }
            var valutazioni = _unitOfWork.Valutazione.GetAll().Where(v => v.ApplicationUserId == applicationUserId);
            var vm = new MieiFilmVM
            {
                Valutazioni = valutazioni,
                Films = films,
            };
            return View(vm);
        }
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult NuovaRecensione(string? id)
        {
            var userIdentity = User.Identity;
            var claimsIdentity = (ClaimsIdentity)userIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            string applicationUserId = null;
            if (claim == null)
            {
                return Redirect("/");
            }
            applicationUserId = claim.Value;
            var valutazione = new Valutazione()
            {
                FkFilm = id,
                ApplicationUserId = applicationUserId,
            };
            return View(valutazione);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NuovaRecensione(Valutazione obj)
        {
            _unitOfWork.Valutazione.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "valutazione created successfully";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult ModificaRecensione(string? id)
        {
            var userIdentity = User.Identity;
            var claimsIdentity = (ClaimsIdentity)userIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            string applicationUserId = null;
            if (claim == null)
            {
                return Redirect("/");
            }
            applicationUserId = claim.Value;
            var valscelta = _unitOfWork.Valutazione.GetAll().Where(v => v.FkFilm == id && v.ApplicationUserId == applicationUserId).FirstOrDefault();
            if( valscelta == null)
            {
                return RedirectToAction("Index");
            }
            return View(valscelta);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificaRecensione(Valutazione obj)
        {
            _unitOfWork.Valutazione.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "valutazione created successfully";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult EliminaRecensione(string? id)
        {
            var userIdentity = User.Identity;
            var claimsIdentity = (ClaimsIdentity)userIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            string applicationUserId = null;
            if (claim == null)
            {
                return Redirect("/");
            }
            applicationUserId = claim.Value;
            var valutazione = new Valutazione()
            {
                FkFilm = id,
                ApplicationUserId = applicationUserId,
            };
            _unitOfWork.Valutazione.Remove(valutazione);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
