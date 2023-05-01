using Cinema.DataAccess.Repository.IRepository;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
    }
}
