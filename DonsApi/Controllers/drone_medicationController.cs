using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DonsApi.Models;

namespace DonsApi.Controllers
{
    public class drone_medicationController : ApiController
    {

        ModelDron model;
        private dronesEntities db = new dronesEntities();

        public drone_medicationController()
        {
            this.model = new ModelDron();
        }

        // GET: api/drone_medication
        public IQueryable<drone_medication> Getdrone_medication()
        {
            return db.drone_medication;
        }

        // GET: api/drone_medication/5
        [ResponseType(typeof(drone_medication))]
        public IHttpActionResult Getdrone_medication(int id)
        {
            drone_medication drone_medication = db.drone_medication.Find(id);
            if (drone_medication == null)
            {
                return NotFound();
            }

            return Ok(drone_medication);
        }



        // PUT: api/drone_medication/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdrone_medication(int id, drone_medication drone_medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drone_medication.id)
            {
                return BadRequest();
            }

            db.Entry(drone_medication).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!drone_medicationExists(id))
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



        //Load drone with medicines     
        // POST: api/drone_medication
        [ResponseType(typeof(drone_medication))]
        public IHttpActionResult Postdrone_medication(drone_medication drone_medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (this.model.Down25(drone_medication.id))
            {
               
                return BadRequest("Batery Down, please wait to charge this dron");
            }

            if ((this.model.GetWeightByDron(drone_medication.id_dron)) <= (this.model.GetWeightByMedication(drone_medication.id_medicamento) + this.model.GetTotalWeightByDron(drone_medication.id_dron)))
            {
                
                return BadRequest("The drone can no longer carry more weight");
            }


            db.drone_medication.Add(drone_medication);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drone_medication.id }, drone_medication);
        }

        // DELETE: api/drone_medication/5
        [ResponseType(typeof(drone_medication))]
        public IHttpActionResult Deletedrone_medication(int id)
        {
            drone_medication drone_medication = db.drone_medication.Find(id);
            if (drone_medication == null)
            {
                return NotFound();
            }

            db.drone_medication.Remove(drone_medication);
            db.SaveChanges();

            return Ok(drone_medication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool drone_medicationExists(int id)
        {
            return db.drone_medication.Count(e => e.id == id) > 0;
        }
    }
}