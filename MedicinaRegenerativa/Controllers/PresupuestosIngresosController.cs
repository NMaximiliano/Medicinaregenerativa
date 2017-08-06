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
    public class PresupuestosIngresosController : Controller
    {
        private DB_A06236_turnosMedicEntities db = new DB_A06236_turnosMedicEntities();

        // GET: PresupuestosIngresos
        public ActionResult Index()
        {
            
            var presupuestosIngresos = db.PresupuestosIngresos.Include(p => p.MediosPago).Include(p => p.Presupuestos).Include(p => p.TiposMoneda).Include(p => p.AspNetUsers);
            return View(presupuestosIngresos.ToList());
        }

        // GET: PresupuestosIngresos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosIngresos presupuestosIngresos = db.PresupuestosIngresos.Find(id);
            if (presupuestosIngresos == null)
            {
                return HttpNotFound();
            }
            return View(presupuestosIngresos);
        }

        // GET: PresupuestosIngresos/Create
        public ActionResult Create(int? presupuesto)
        {
            if (presupuesto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Presupuestos presupuestos = db.Presupuestos.Find(presupuesto);
            if (presupuestos == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.Presupuesto = presupuestos;
            ViewBag.idMedioPago = new SelectList(db.MediosPago, "idMedioPago", "Descripcion");
            ViewBag.idPresupuestos = new SelectList(db.Presupuestos, "idPresupuestos", "Observaciones",presupuestos.idPresupuestos);
            ViewBag.idTipoMoneda = new SelectList(db.TiposMoneda, "idTipoMoneda", "Descripcion");
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: PresupuestosIngresos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPresupuestoIngreso,Monto,Cuota,Observaciones,idPresupuestos,idTipoMoneda,idMedioPago,FacturaNro,FechaCarga,UserId")] PresupuestosIngresos presupuestosIngresos)
        {
            if (ModelState.IsValid)
            {
                presupuestosIngresos.UserId = User.Identity.GetUserId();
                presupuestosIngresos.FechaCarga = DateTime.Now;
                db.PresupuestosIngresos.Add(presupuestosIngresos);
                db.SaveChanges();
                return RedirectToAction("Details", "Presupuestos",new { id = presupuestosIngresos.idPresupuestos });
                
            }

            ViewBag.idMedioPago = new SelectList(db.MediosPago, "idMedioPago", "Descripcion", presupuestosIngresos.idMedioPago);
            ViewBag.idPresupuestos = new SelectList(db.Presupuestos, "idPresupuestos", "Observaciones", presupuestosIngresos.idPresupuestos);
            ViewBag.idTipoMoneda = new SelectList(db.TiposMoneda, "idTipoMoneda", "Descripcion", presupuestosIngresos.idTipoMoneda);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", presupuestosIngresos.UserId);
            return View(presupuestosIngresos);
        }

        // GET: PresupuestosIngresos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosIngresos presupuestosIngresos = db.PresupuestosIngresos.Find(id);
            Presupuestos presupuestos = db.Presupuestos.Find(presupuestosIngresos.idPresupuestos);
            if (presupuestosIngresos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Presupuesto = presupuestos;
            ViewBag.idMedioPago = new SelectList(db.MediosPago, "idMedioPago", "Descripcion", presupuestosIngresos.idMedioPago);
            ViewBag.idPresupuestos = new SelectList(db.Presupuestos, "idPresupuestos", "Observaciones", presupuestosIngresos.idPresupuestos);
            ViewBag.idTipoMoneda = new SelectList(db.TiposMoneda, "idTipoMoneda", "Descripcion", presupuestosIngresos.idTipoMoneda);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", presupuestosIngresos.UserId);
            return View(presupuestosIngresos);
        }

        // POST: PresupuestosIngresos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPresupuestoIngreso,Monto,Cuota,Observaciones,idPresupuestos,idTipoMoneda,idMedioPago,FacturaNro,FechaCarga,UserId")] PresupuestosIngresos presupuestosIngresos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presupuestosIngresos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Presupuestos", new { id = presupuestosIngresos.idPresupuestos });
            }
            ViewBag.idMedioPago = new SelectList(db.MediosPago, "idMedioPago", "Descripcion", presupuestosIngresos.idMedioPago);
            ViewBag.idPresupuestos = new SelectList(db.Presupuestos, "idPresupuestos", "Observaciones", presupuestosIngresos.idPresupuestos);
            ViewBag.idTipoMoneda = new SelectList(db.TiposMoneda, "idTipoMoneda", "Descripcion", presupuestosIngresos.idTipoMoneda);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", presupuestosIngresos.UserId);
            return View(presupuestosIngresos);
        }

        // GET: PresupuestosIngresos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresupuestosIngresos presupuestosIngresos = db.PresupuestosIngresos.Find(id);
            if (presupuestosIngresos == null)
            {
                return HttpNotFound();
            }
            
            return View(presupuestosIngresos);
        }

        // POST: PresupuestosIngresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PresupuestosIngresos presupuestosIngresos = db.PresupuestosIngresos.Find(id);
            db.PresupuestosIngresos.Remove(presupuestosIngresos);
            db.SaveChanges();
            return RedirectToAction("Details", "Presupuestos", new { id = presupuestosIngresos.idPresupuestos });            
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
