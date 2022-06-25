using Microsoft.AspNetCore.Mvc;
using Resto_Backend.DAL;
using Resto_Backend.ViewModels;
using System.Linq;

namespace Resto_Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM=new HomeVM();
            homeVM.Breakfast = _context.Breakfasts.ToList();
            return View(homeVM);
        }
    }
}
