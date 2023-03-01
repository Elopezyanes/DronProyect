using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DonsApi.Models;

namespace DonsApi.Controllers
{
    public class dronsController : ApiController
    {
        ModelDron model;
        private dronesEntities db = new dronesEntities();

        public dronsController()
        {
            this.model = new ModelDron();
        }

        // GET: api/drons
        public IQueryable<dron> Getdron()
        {
            return db.dron;
        }

        // GET: api/drons/5
        [ResponseType(typeof(dron))]
        public IHttpActionResult Getdron(int id)
        {
            dron dron = db.dron.Find(id);
            if (dron == null)
            {
                return NotFound();
            }

            return Ok(dron);
        }

        //Get Drons Available 
        [HttpGet]
        [Route("api/drons/checkavailable")]
        public List<dron> GetDronsAvailables()
        {
            return this.model.GetDronByState();
        }

        //Get Batery Dron 
        [HttpGet]
        [Route("api/drons/baterycheck/{id}")]
        public string GetDronsBatery(int iddron)
        {
            return this.model.GetBateryDron(iddron);
        }

        //Get medication items loaded for a drone
        [HttpGet]
        [Route("api/drons/medicationload/{id}")]
        public List<string> GetMedicationbyDron(int iddron)
        {
            return this.model.GetMedicationByDron(iddron);
        }

        // PUT: api/drons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdron(int id, dron dron)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dron.id)
            {
                return BadRequest();
            }

            db.Entry(dron).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dronExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        //register a drone
        // POST: api/drons
        [ResponseType(typeof(dron))]
        public IHttpActionResult Postdron(dron dron)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.dron.Add(dron);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dron.id }, dron);
        }

        // DELETE: api/drons/5
        [ResponseType(typeof(dron))]
        public IHttpActionResult Deletedron(int id)
        {
            dron dron = db.dron.Find(id);
            if (dron == null)
            {
                return NotFound();
            }

            db.dron.Remove(dron);
            db.SaveChanges();

            return Ok(dron);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool dronExists(int id)
        {
            return db.dron.Count(e => e.id == id) > 0;
        }
    }
}