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
    public class PresupuestosController : Controller
    {
        private DB_A06236_turnosMedicEntities db = new DB_A06236_turnosMedicEntities();

        // GET: Presupuestos
        public ActionResult Index()
        {
            var presupuestos = db.Presupuestos.Include(p => p.EstadosPresupuestos).Include(p => p.Pacientes).Include(p => p.TipoTurnos).Include(p => p.AspNetUsers);
            return View(presupuestos.ToList());
        }

        // GET: Presupuestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuestos presupuestos = db.Presupuestos.Find(id);
            if (presupuestos == null)
            {
                return HttpNotFound();
            }
            return View(presupuestos);
        }

        // GET: Presupuestos/Create
        public ActionResult Create()
        {
            
            ViewBag.idEstadoPresupuesto = new SelectList(db.EstadosPresupuestos, "idEstadoPresupuesto", "Descripcion");
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto");
            ViewBag.idTipoMoneda = new SelectList(db.TiposMoneda, "idTipoMoneda", "Descripcion");
            ViewBag.idTipoTurno = new SelectList(db.TipoTurnos, "idTipoTurno", "Descripcion");
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Presupuestos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPresupuestos,Precio,Cuotas,Observaciones,idPaciente,idTipoTurno,idEstadoPresupuesto,FechaCarga,UserId, idTipoMoneda")] Presupuestos presupuestos)
        {
            if (ModelState.IsValid)
            {
                presupuestos.UserId = User.Identity.GetUserId();
                presupuestos.FechaCarga = DateTime.Now;
                db.Presupuestos.Add(presupuestos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEstadoPresupuesto = new SelectList(db.EstadosPresupuestos, "idEstadoPresupuesto", "Descripcion", presupuestos.idEstadoPresupuesto);
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto", presupuestos.idPaciente);
            ViewBag.idTipoMoneda = new SelectList(db.TiposMoneda, "idTipoMoneda", "Descripcion", presupuestos.idTipoMoneda);
            ViewBag.idTipoTurno = new SelectList(db.TipoTurnos, "idTipoTurno", "Descripcion", presupuestos.idTipoTurno);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", presupuestos.UserId);
            return View(presupuestos);
        }

        // GET: Presupuestos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuestos presupuestos = db.Presupuestos.Find(id);
            if (presupuestos == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.idEstadoPresupuesto = new SelectList(db.EstadosPresupuestos, "idEstadoPresupuesto", "Descripcion", presupuestos.idEstadoPresupuesto);
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto", presupuestos.idPaciente);
            ViewBag.idTipoMoneda = new SelectList(db.TiposMoneda, "idTipoMoneda", "Descripcion", presupuestos.idTipoMoneda);
            ViewBag.idTipoTurno = new SelectList(db.TipoTurnos, "idTipoTurno", "Descripcion", presupuestos.idTipoTurno);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", presupuestos.UserId);
            return View(presupuestos);
        }

        // POST: Presupuestos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPresupuestos,Precio,Cuotas,Observaciones,idPaciente,idTipoTurno,idEstadoPresupuesto,FechaCarga,UserId, idTipoMoneda")] Presupuestos presupuestos)
        {
            if (ModelState.IsValid)
            {
                presupuestos.UserId = User.Identity.GetUserId();
                presupuestos.FechaCarga = DateTime.Now;
                db.Entry(presupuestos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstadoPresupuesto = new SelectList(db.EstadosPresupuestos, "idEstadoPresupuesto", "Descripcion", presupuestos.idEstadoPresupuesto);
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto", presupuestos.idPaciente);
            ViewBag.idTipoMoneda = new SelectList(db.TiposMoneda, "idTipoMoneda", "Descripcion", presupuestos.idTipoMoneda);
            ViewBag.idTipoTurno = new SelectList(db.TipoTurnos, "idTipoTurno", "Descripcion", presupuestos.idTipoTurno);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", presupuestos.UserId);
            return View(presupuestos);
        }

        // GET: Presupuestos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuestos presupuestos = db.Presupuestos.Find(id);
            if (presupuestos == null)
            {
                return HttpNotFound();
            }
            return View(presupuestos);
        }

        // POST: Presupuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Presupuestos presupuestos = db.Presupuestos.Find(id);
            db.Presupuestos.Remove(presupuestos);
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
