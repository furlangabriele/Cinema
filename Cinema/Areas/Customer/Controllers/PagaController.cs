using Cinema.DataAccess.Repository.IRepository;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PagaController : Controller
    {
        private readonly ILogger<PagaController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public PagaController(ILogger<PagaController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult Index(int id)
        {
            return View(id);
        }
        public IActionResult Pagato(int id)
        {
            var obj = _unitOfWork.Biglietto.GetAll().Where(b => b.Id == id).First();
            obj.Pagato = true;
            _unitOfWork.Biglietto.Update(obj);
            _unitOfWork.Save();
            return Redirect("/Customer/Valutazioni");
        }
        public IActionResult PagaTutto()
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
            var obj = _unitOfWork.Biglietto.GetAll().Where(b => b.ApplicationUserId == applicationUserId && !b.Pagato);
            foreach (var item in obj)
            {
                item.Pagato = true;
                _unitOfWork.Biglietto.Update(item);
                _unitOfWork.Save();
            }
            return Redirect("/Customer/Valutazioni");
        }
    }
}
