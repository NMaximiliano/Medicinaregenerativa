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
    public class HistorialPacientesController : Controller
    {
        private DB_A06236_turnosMedicEntities db = new DB_A06236_turnosMedicEntities();

        // GET: HistorialPacientes
        public ActionResult Index()
        {
            var historialPacientes = db.HistorialPacientes.Include(h => h.Pacientes).Include(h => h.Turnos);
            return View(historialPacientes.ToList());
        }

        // GET: HistorialPacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialPacientes historialPacientes = db.HistorialPacientes.Find(id);
            if (historialPacientes == null)
            {
                return HttpNotFound();
            }
            return View(historialPacientes);
        }

        // GET: HistorialPacientes/Create
        public ActionResult Create()
        {
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto");
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Hora");
            return View();
        }

        // POST: HistorialPacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idHistorialPaciente,Historia,MedicacionRecibida,idPaciente,idTurno,FechaCarga,UserId")] HistorialPacientes historialPacientes)
        {
            if (ModelState.IsValid)
            {
                historialPacientes.UserId = User.Identity.GetUserId();
                historialPacientes.FechaCarga = DateTime.Now;
                db.HistorialPacientes.Add(historialPacientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto", historialPacientes.idPaciente);
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Hora", historialPacientes.idTurno);
            return View(historialPacientes);
        }

        // GET: HistorialPacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialPacientes historialPacientes = db.HistorialPacientes.Find(id);
            if (historialPacientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto", historialPacientes.idPaciente);
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Hora", historialPacientes.idTurno);
            return View(historialPacientes);
        }

        // POST: HistorialPacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idHistorialPaciente,Historia,MedicacionRecibida,idPaciente,idTurno,FechaCarga,UserId")] HistorialPacientes historialPacientes)
        {
            if (ModelState.IsValid)
            {
                historialPacientes.UserId = User.Identity.GetUserId();
                historialPacientes.FechaCarga = DateTime.Now;
                db.Entry(historialPacientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto", historialPacientes.idPaciente);
            ViewBag.idTurno = new SelectList(db.Turnos, "idTurno", "Hora", historialPacientes.idTurno);
            return View(historialPacientes);
        }

        // GET: HistorialPacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistorialPacientes historialPacientes = db.HistorialPacientes.Find(id);
            if (historialPacientes == null)
            {
                return HttpNotFound();
            }
            return View(historialPacientes);
        }

        // POST: HistorialPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistorialPacientes historialPacientes = db.HistorialPacientes.Find(id);
            db.HistorialPacientes.Remove(historialPacientes);
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
