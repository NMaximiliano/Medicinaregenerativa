using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicinaRegenerativa.Models;
using Microsoft.AspNet.Identity;

namespace MedicinaRegenerativa.Controllers
{
    public class TipoTurnosController : Controller
    {
        private DB_A06236_turnosMedicEntities db = new DB_A06236_turnosMedicEntities();

        // GET: TipoTurnos
        public ActionResult Index()
        {
            return View(db.TipoTurnos.ToList());
        }

        // GET: TipoTurnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTurnos tipoTurnos = db.TipoTurnos.Find(id);
            if (tipoTurnos == null)
            {
                return HttpNotFound();
            }
            return View(tipoTurnos);
        }

        // GET: TipoTurnos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoTurnos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTipoTurno,Descripcion,FechaCarga,UserId")] TipoTurnos tipoTurnos)
        {
            if (ModelState.IsValid)
            {
                tipoTurnos.UserId = User.Identity.GetUserId();
                tipoTurnos.FechaCarga = DateTime.Now;
                db.TipoTurnos.Add(tipoTurnos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoTurnos);
        }

        // GET: TipoTurnos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTurnos tipoTurnos = db.TipoTurnos.Find(id);
            if (tipoTurnos == null)
            {
                return HttpNotFound();
            }
            return View(tipoTurnos);
        }

        // POST: TipoTurnos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTipoTurno,Descripcion,FechaCarga,UserId")] TipoTurnos tipoTurnos)
        {
            if (ModelState.IsValid)
            {
                tipoTurnos.UserId = User.Identity.GetUserId();
                tipoTurnos.FechaCarga = DateTime.Now;
                db.Entry(tipoTurnos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoTurnos);
        }

        // GET: TipoTurnos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTurnos tipoTurnos = db.TipoTurnos.Find(id);
            if (tipoTurnos == null)
            {
                return HttpNotFound();
            }
            return View(tipoTurnos);
        }

        // POST: TipoTurnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoTurnos tipoTurnos = db.TipoTurnos.Find(id);
            db.TipoTurnos.Remove(tipoTurnos);
            db.SaveChanges();
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
