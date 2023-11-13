using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VET_ANIMAL.WEB.Controllers
{
    public class HistorialClinico : Controller
    {
        // GET: HistorialClinico
        public ActionResult Index()
        {
            TempData["menu"] = "";
            return View();
        }

        // GET: HistorialClinico/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HistorialClinico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistorialClinico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HistorialClinico/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HistorialClinico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HistorialClinico/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HistorialClinico/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
