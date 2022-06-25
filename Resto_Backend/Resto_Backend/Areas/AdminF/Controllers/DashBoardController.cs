using Microsoft.AspNetCore.Mvc;

namespace Resto_Backend.Areas.AdminF.Controllers
{
    [Area("Adminf")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
