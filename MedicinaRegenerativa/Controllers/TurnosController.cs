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
    public class TurnosController : Controller
    {
        private DB_A06236_turnosMedicEntities db = new DB_A06236_turnosMedicEntities();

        // GET: Turnos
        public ActionResult Index()
        {
            var turnos = db.Turnos.Include(t => t.Pacientes).Include(t => t.TipoTurnos);
            return View(turnos.ToList());
        }

        // GET: Turnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnos turnos = db.Turnos.Find(id);
            if (turnos == null)
            {
                return HttpNotFound();
            }
            return View(turnos);
        }

        // GET: Turnos/Create
        [HttpGet]
        public ActionResult Create(string fecha)
        {
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto");
            ViewBag.idTipoTurno = new SelectList(db.TipoTurnos, "idTipoTurno", "Descripcion");
            Turnos Turnos = new Turnos();
            Turnos.Fecha = Convert.ToDateTime(fecha);
            ViewData.Model = Turnos;
            return View();
        }
        [HttpPost]
        public ActionResult CreateNuevo(string fechaBusqueda)
        {                        
            return RedirectToAction("Create","Turnos", new { fecha = fechaBusqueda });            
        }


        // POST: Turnos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTurno,Fecha,Hora,TiempoReservado,Observaciones,idPaciente,idTipoTurno,FechaCarga,UserId")] Turnos turnos)
        {
            if (ModelState.IsValid)
            {
                turnos.UserId = User.Identity.GetUserId();
                turnos.FechaCarga = DateTime.Now;
                db.Turnos.Add(turnos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto", turnos.idPaciente);
            ViewBag.idTipoTurno = new SelectList(db.TipoTurnos, "idTipoTurno", "Descripcion", turnos.idTipoTurno);
            return View(turnos);
        }
        [HttpPost]
        public /*JsonResult */ActionResult  buscarXFecha(List<String> fecha)
        {
            // some code
           // string fechaIngresada = fecha.ToString();
            string fechaIngresada = fecha[0].ToString();
            DateTime fechaIng = Convert.ToDateTime(fechaIngresada);
           
            
            var turnos = db.Turnos.Where(x => x.Fecha == fechaIng).Select(c => new { Fecha = c.Fecha.ToString(), Hora = c.Hora, Observaciones = c.Observaciones, TiempoReserva = c.TiempoReservado, Paciente = c.Pacientes.NombreCompleto, TipoTurno = c.TipoTurnos.Descripcion })
                        .ToList(); 
            return Json(new { success = true, message = "fecha: " + fechaIngresada , turnos = turnos }/*, JsonRequestBehavior.AllowGet*/);
        }
        // GET: Turnos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnos turnos = db.Turnos.Find(id);
            if (turnos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto", turnos.idPaciente);
            ViewBag.idTipoTurno = new SelectList(db.TipoTurnos, "idTipoTurno", "Descripcion", turnos.idTipoTurno);
            return View(turnos);
        }

        // POST: Turnos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTurno,Fecha,Hora,TiempoReservado,Observaciones,idPaciente,idTipoTurno,FechaCarga,UserId")] Turnos turnos)
        {
            if (ModelState.IsValid)
            {
                turnos.UserId = User.Identity.GetUserId();
                turnos.FechaCarga = DateTime.Now;
                db.Entry(turnos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPaciente = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto", turnos.idPaciente);
            ViewBag.idTipoTurno = new SelectList(db.TipoTurnos, "idTipoTurno", "Descripcion", turnos.idTipoTurno);
            return View(turnos);
        }

        // GET: Turnos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turnos turnos = db.Turnos.Find(id);
            if (turnos == null)
            {
                return HttpNotFound();
            }
            return View(turnos);
        }

        // POST: Turnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turnos turnos = db.Turnos.Find(id);
            db.Turnos.Remove(turnos);
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
