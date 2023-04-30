using Cinema.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                        postiDisponibili.Add($"{j}-{i}");
                    }
                }
            }
            var posti = postiDisponibili.Select(p => new SelectListItem
            {
                Text = p,
                Value = p
            });
            return View(posti);
        }
    }
}
