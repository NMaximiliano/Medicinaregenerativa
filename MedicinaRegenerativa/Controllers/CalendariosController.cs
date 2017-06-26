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
    public class CalendariosController : Controller
    {
        private DB_A06236_turnosMedicEntities db = new DB_A06236_turnosMedicEntities();

        // GET: Calendarios
        public ActionResult Index()
        {
            var calendario = db.Calendario.Include(c => c.Turnos);
            return View(calendario.ToList());
        }

        // GET: Calendarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendario calendario = db.Calendario.Find(id);
            if (calendario == null)
            {
                return HttpNotFound();
            }
            return View(calendario);
        }

        // GET: Calendarios/Create
        public ActionResult Create()
        {
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Hora");
            return View();
        }

        // POST: Calendarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCalendario,Fecha,Hora,idTurno,FechaCarga,UserId")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                calendario.UserId = User.Identity.GetUserId();
                calendario.FechaCarga = DateTime.Now;
                db.Calendario.Add(calendario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Hora", calendario.idTurno);
            return View(calendario);
        }

        // GET: Calendarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendario calendario = db.Calendario.Find(id);
            if (calendario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Hora", calendario.idTurno);
            return View(calendario);
        }

        // POST: Calendarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCalendario,Fecha,Hora,idTurno,FechaCarga,UserId")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                calendario.UserId = User.Identity.GetUserId();
                calendario.FechaCarga = DateTime.Now;
                db.Entry(calendario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Hora", calendario.idTurno);
            return View(calendario);
        }

        // GET: Calendarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendario calendario = db.Calendario.Find(id);
            if (calendario == null)
            {
                return HttpNotFound();
            }
            return View(calendario);
        }

        // POST: Calendarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calendario calendario = db.Calendario.Find(id);
            db.Calendario.Remove(calendario);
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
