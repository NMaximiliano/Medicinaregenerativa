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
using System.Data.Entity.Validation;

namespace MedicinaRegenerativa.Controllers
{

    public class PacientesController : Controller
    {
        private DB_A06236_turnosMedicEntities db = new DB_A06236_turnosMedicEntities();

        // GET: Pacientes
        public ActionResult Index()
        {
            //            HttpContext.Session["culture"] = "es-ES";            
            ViewBag.PacientesNombres = new SelectList(db.Pacientes, "idPaciente", "NombreCompleto"); ;
            return View(db.Pacientes.OrderBy(x => x.NombreCompleto).ToList());            
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {


            ViewBag.idObraSocial = new SelectList(db.ObrasSociales, "idObraSocial", "Descripcion");
            HttpContext.Session["culture"] = "es-ES";
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPaciente, NombreCompleto,Documento,Direccion,Telefono,FechaNacimiento, mail, idObraSocial, cuit, ciudad, pais, provincia, observaciones, nroSocioObraSocial")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                pacientes.UserId = User.Identity.GetUserId();
                pacientes.FechaCarga = DateTime.Now;
                db.Pacientes.Add(pacientes);

                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
                catch (System.Data.Entity.Core.EntityCommandCompilationException ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
                catch (System.Data.Entity.Core.UpdateException ex)
                {
                    Console.WriteLine(ex.InnerException);
                }

                catch (System.Data.Entity.Infrastructure.DbUpdateException ex) //DbContext
                {
                    Console.WriteLine(ex.InnerException);
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    throw;
                }
                return RedirectToAction("Index");
            }

            return View(pacientes);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.idObraSocial = new SelectList(db.ObrasSociales, "idObraSocial", "Descripcion", pacientes.idObraSocial);
            return View(pacientes);
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPaciente,NombreCompleto,Documento,Direccion,Telefono,FechaNacimiento,FechaCarga,UserId, mail, idObraSocial, cuit, ciudad, pais, provincia, observaciones, nroSocioObraSocial")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                pacientes.UserId = User.Identity.GetUserId();
                pacientes.FechaCarga = DateTime.Now;
                db.Entry(pacientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idObraSocial = new SelectList(db.ObrasSociales, "idObraSocial", "Descripcion", pacientes.idObraSocial);
            return View(pacientes);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacientes pacientes = db.Pacientes.Find(id);
            db.Pacientes.Remove(pacientes);
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

        public ActionResult ObtenerCiudades(string term)
        {
            var ListadoPacientes = db.Pacientes.Where(x => x.ciudad.Contains(term)).Select(c => new { ciudad = c.ciudad }).AsEnumerable();
            return Json(ListadoPacientes.Select(x => x.ciudad).Distinct(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerPaises(string term)
        {
            var ListadoPacientes = db.Pacientes.Where(x => x.pais.Contains(term)).Select(c => new { pais = c.pais }).AsEnumerable();
            return Json(ListadoPacientes.Select(x => x.pais).Distinct(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerProvincias(string term)
        {
            var ListadoPacientes = db.Pacientes.Where(x => x.provincia.Contains(term)).Select(c => new { provincia = c.provincia }).AsEnumerable();
            return Json(ListadoPacientes.Select(x => x.provincia).Distinct(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ObtenerCiudade(string Prefix)
        {
            //Note : you can bind same list from database  
            var ListadoPacientes = db.Pacientes.Where(x => x.ciudad.Contains(Prefix)).Select(c => new { ciudad = c.ciudad }).ToList();
            //    return Json(ListadoPacientes, JsonRequestBehavior.AllowGet);
            return Json(new { success = true, Pacientes = ListadoPacientes }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Autocomplete(string term)
        {
            //var items = new[] { "Apple", "Pear", "Banana", "Pineapple", "Peach" };

            //var filteredItems = items.Where(
            //    item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
            //    );
            var ListadoPacientes = db.Pacientes.Where(x => x.ciudad.Contains(term)).Select(c => new { ciudad = c.ciudad }).AsEnumerable();
            return Json(ListadoPacientes.Select(x => x.ciudad).Distinct(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult ObtenerPacientes(int PacientesNombres)
        {
            var pacientes = db.Pacientes.Where(x => x.idPaciente == PacientesNombres);
            return PartialView("_ListadoPacientes", pacientes);
        }
    }

}
