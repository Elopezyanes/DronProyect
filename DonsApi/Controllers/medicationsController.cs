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
    public class medicationsController : ApiController
    {
        private dronesEntities db = new dronesEntities();

        // GET: api/medications
        public IQueryable<medication> Getmedication()
        {
            return db.medication;
        }

        // GET: api/medications/5
        [ResponseType(typeof(medication))]
        public IHttpActionResult Getmedication(int id)
        {
            medication medication = db.medication.Find(id);
            if (medication == null)
            {
                return NotFound();
            }

            return Ok(medication);
        }

        // PUT: api/medications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmedication(int id, medication medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medication.id)
            {
                return BadRequest();
            }

            db.Entry(medication).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!medicationExists(id))
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

        // POST: api/medications
        [ResponseType(typeof(medication))]
        public IHttpActionResult Postmedication(medication medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.medication.Add(medication);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = medication.id }, medication);
        }

        // DELETE: api/medications/5
        [ResponseType(typeof(medication))]
        public IHttpActionResult Deletemedication(int id)
        {
            medication medication = db.medication.Find(id);
            if (medication == null)
            {
                return NotFound();
            }

            db.medication.Remove(medication);
            db.SaveChanges();

            return Ok(medication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool medicationExists(int id)
        {
            return db.medication.Count(e => e.id == id) > 0;
        }
    }
}