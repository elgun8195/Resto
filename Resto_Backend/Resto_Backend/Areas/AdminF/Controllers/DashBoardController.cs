using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resto_Backend.Helpers;

namespace Resto_Backend.Areas.AdminF.Controllers
{
    [Area("Adminf")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
