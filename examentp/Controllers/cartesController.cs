using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using examentp.Models;

namespace examentp.Controllers
{
    public class cartesController : Controller
    {
        private banqueEntities db = new banqueEntities();

        // GET: cartes
        public async Task<ActionResult> Index()
        {
            var cartes = db.cartes.Include(c => c.compte);
            return View(await cartes.ToListAsync());
        }

        // GET: cartes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carte carte = await db.cartes.FindAsync(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            return View(carte);
        }

        // GET: cartes/Create
        public ActionResult Create()
        {
            ViewBag.code_client = new SelectList(db.comptes, "code_client", "nom");
            return View();
        }

        // POST: cartes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "code_carte,code_client,num_carte,date_exp")] carte carte)
        {
            if (ModelState.IsValid)
            {
                db.cartes.Add(carte);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.code_client = new SelectList(db.comptes, "code_client", "nom", carte.code_client);
            return View(carte);
        }

        // GET: cartes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carte carte = await db.cartes.FindAsync(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            ViewBag.code_client = new SelectList(db.comptes, "code_client", "nom", carte.code_client);
            return View(carte);
        }

        // POST: cartes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "code_carte,code_client,num_carte,date_exp")] carte carte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carte).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.code_client = new SelectList(db.comptes, "code_client", "nom", carte.code_client);
            return View(carte);
        }

        // GET: cartes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carte carte = await db.cartes.FindAsync(id);
            if (carte == null)
            {
                return HttpNotFound();
            }
            return View(carte);
        }

        // POST: cartes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            carte carte = await db.cartes.FindAsync(id);
            db.cartes.Remove(carte);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
