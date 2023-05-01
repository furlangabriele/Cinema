using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models.ViewModel;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CarrelloController : Controller
    {
        private readonly ILogger<CarrelloController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CarrelloController(ILogger<CarrelloController> logger, IUnitOfWork unitOfWork)
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
            var biglietti_utente = _unitOfWork.Biglietto.GetAll().Where(b => b.ApplicationUserId == applicationUserId && b.Pagato == false);
            var spettacoli = _unitOfWork.Spettacolo.GetAll();
            int prezzo = biglietti_utente.Count() * 5;
            var carrello = new CarrelloVM
            {
                Biglietti = biglietti_utente,
                Prezzo= prezzo,
                Spettacoli = spettacoli
            };
            return View(carrello);
        }
    }
}
