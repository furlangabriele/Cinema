using Cinema.DataAccess.Repository.IRepository;
using Cinema.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
    }
}
