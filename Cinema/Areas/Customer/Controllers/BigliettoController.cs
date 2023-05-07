using Cinema.DataAccess.Repository;
using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using Cinema.Models.ViewModel;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BigliettoController : Controller
    {
        private readonly ILogger<BigliettoController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BigliettoController(ILogger<BigliettoController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult Index(int id)
        {
            var bigliettiSpettacolo = _unitOfWork.Biglietto.GetAll().Where(b => b.SpettacoloId == id);
            List<string> postiDisponibili = new List<string>();
            for (int i = 1; i < 8; i++)
            {
                for (int j = 1; j < 15; j++)
                {
                    if (bigliettiSpettacolo.Where(b => b.Fila == i && b.Posto == j).Count() == 0)
                    {
                        postiDisponibili.Add($"{i}_{j}");
                    }
                }
            }
            var posti = postiDisponibili.Select(p => new SelectListItem
            {
                Text = $"Posto: {p.Split('_')[0]} Fila: {p.Split('_')[1]}",
                Value = p
            });
            TempData["spettacolo"] = id;
            return View(posti);
        }
        [HttpPost]
        public IActionResult CreaBiglietto(string posto_fila, int id_spettacolo)
        {
            try
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
                var arr1 = posto_fila.Split(';');
                foreach (var item in arr1)
                {
                    var arr = item.Split('-');
                    var biglietto = new Biglietto
                    {
                        SpettacoloId = id_spettacolo,
                        ApplicationUserId = applicationUserId,
                        Posto = int.Parse(arr[1]),
                        Fila = int.Parse(arr[0])
                    };
                    _unitOfWork.Biglietto.Add(biglietto);
                    _unitOfWork.Save();
                }
                return Redirect($"/Customer/Carrello/");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult IMieiBiglietti()
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
            var listabiglietti = _unitOfWork.Biglietto.GetAll().Where(b => b.ApplicationUserId == applicationUserId );
            var spettacoli = _unitOfWork.Spettacolo.GetAll();
            var myVM = new MieiBigliettiVM
            {
                bigliettos = listabiglietti.OrderByDescending(b => b.Id),
                Spettacolos = spettacoli
            };
            return View(myVM);
        }
        //POST
        [HttpDelete]
        [AllowAnonymous]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Biglietto.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Biglietto.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
    }
}