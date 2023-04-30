using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public IActionResult Index(int? FkSala, string? FkFilm, DateOnly Data, TimeOnly Orario)
        {
            var bigliettiSpettacolo = _unitOfWork.Biglietto.GetAll().Where(b => b.FkSala == FkSala && FkFilm == b.FkFilm && Data == b.Data && Orario == b.Orario);
            List<string> postiDisponibili = new List<string>();
            for (int i = 1; i < 8; i++)
            {
                for (int j = 1; j < 15; j++)
                {
                    if(bigliettiSpettacolo.Where(b => b.Fila == i && b.Posto == j).Count() == 0)
                    {
                        postiDisponibili.Add($"{j}_{i}");
                    }
                }
            }
            var posti = postiDisponibili.Select(p => new SelectListItem
            {
                Text = $"Posto: {p.Split('_')[0]} Fila: {p.Split('_')[1]}",
                Value = p
            });
            TempData["Sala"] = (int)FkSala;
            TempData["Film"] = FkFilm;
            TempData["Ora"] = Orario.ToString();
            TempData["Data"] = Data.ToString();
            return View(posti);
        }
        [Authorize(Roles = SD.Role_Customer)]
        public IActionResult CreaBiglietto(string posto_fila)
        {
            try
            {
                var arr = posto_fila.Split('_');
                var userIdentity = User.Identity;
                var claimsIdentity = (ClaimsIdentity)userIdentity;
                var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
                string applicationUserId = null;
                if (claim == null)
                {
                    return Redirect("/");
                }
                applicationUserId = claim.Value;
                var biglietto = new Biglietto
                {
                    FkSala = (int)TempData["Sala"],
                    FkFilm = (string)TempData["Film"],
                    Data = DateOnly.Parse((string)TempData["Data"]),
                    Orario = TimeOnly.Parse((string)TempData["Ora"]),
                    ApplicationUserId = applicationUserId,
                    Posto = int.Parse(arr[0]),
                    Fila = int.Parse(arr[1])
                };
                if (_unitOfWork.Biglietto.GetAll().Where(b => b.ApplicationUserId == applicationUserId && b.FkSala == biglietto.FkSala && biglietto.FkFilm == b.FkFilm && biglietto.Data == b.Data && biglietto.Orario == b.Orario).Count() >= 4)
                {
                    TempData["message"] = "Hai già 4 biglietti del seguente spettacolo non ne puoi acquistare altri";
                }
                else
                {
                    _unitOfWork.Biglietto.Add(biglietto);
                    _unitOfWork.Save();
                }
                return Redirect($"/Customer/Biglietto?FkSala={biglietto.FkSala}&FkFilm={biglietto.FkFilm}&Orario={biglietto.Orario}&Data={biglietto.Data}");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
