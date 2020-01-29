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
    public class comptesController : Controller
    {
        private banqueEntities db = new banqueEntities();

        // GET: comptes
        public async Task<ActionResult> Index()
        {
            return View(await db.comptes.ToListAsync());
        }

        // GET: comptes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte compte = await db.comptes.FindAsync(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // GET: comptes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: comptes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "code_client,nom,prenom,email,adresse")] compte compte)
        {
            if (ModelState.IsValid)
            {
                db.comptes.Add(compte);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(compte);
        }

        // GET: comptes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte compte = await db.comptes.FindAsync(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // POST: comptes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "code_client,nom,prenom,email,adresse")] compte compte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compte).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(compte);
        }

        // GET: comptes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            compte compte = await db.comptes.FindAsync(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        // POST: comptes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            compte compte = await db.comptes.FindAsync(id);
            db.comptes.Remove(compte);
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
