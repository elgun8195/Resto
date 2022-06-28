using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Resto_Backend.DAL;
using Resto_Backend.Extensions;
using Resto_Backend.Helpers;
using Resto_Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resto_Backend.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class BreakfastController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BreakfastController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Breakfast> breakfasts = _context.Breakfasts.ToList();
            return View(breakfasts);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Breakfast breakfast = await _context.Breakfasts.FindAsync(id);
            if (breakfast == null)
            {
                return NotFound();
            }
            _context.Breakfasts.Remove(breakfast);
            Helper.DeleteImage(_env, "img", breakfast.ImageUrl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Breakfast breakfast)
        {
            if (ModelState["Photo"].ValidationState==Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!breakfast.Photo.IsImage())
            {
                 ModelState.AddModelError("Photo","Photo only");
            }

            if (breakfast.Photo.CheckSize(20000))
            {
                 ModelState.AddModelError("Photo","Sekilin olcusu 20mb-dan boyuk ola bilmez");
            }

            string filename=await breakfast.Photo.SaveImage(_env, "img");

            Breakfast db = new Breakfast();

            db.ImageUrl = filename;
            db.Name=breakfast.Name;
            db.Desc=breakfast.Desc;
            db.Price=breakfast.Price;
            
            _context.Breakfasts.Add(db);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Breakfast breakfast = _context.Breakfasts.Find(id);
            if (breakfast==null)
            {
                return NotFound();
            }

            return View(breakfast);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Breakfast breakfast,int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!breakfast.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Photo only");
            }

            if (breakfast.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 20mb-dan boyuk ola bilmez");
            }

            Breakfast existName = _context.Breakfasts.FirstOrDefault(x=>x.Name.ToLower()==breakfast.Name.ToLower());
            Breakfast db =await _context.Breakfasts.FindAsync(id);
            if (existName != null)
            {
                if (db!=existName)
                {
                    ModelState.AddModelError("Name","Name Already Exist");
                    return View();
                }
            }
            if (db==null)
            {
                return NotFound();
            }
            string filename =await breakfast.Photo.SaveImage(_env,"img");

            db.ImageUrl = filename;
            db.Name = breakfast.Name;
            db.Desc = breakfast.Desc;
            db.Price = breakfast.Price;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            Breakfast breakfast = _context.Breakfasts.Find(id);
            if (breakfast==null)
            {
                return NotFound();
            }
            return View(breakfast);
        }
    }
}
