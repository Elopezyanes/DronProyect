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
    public class modelsController : ApiController
    {
        private dronesEntities db = new dronesEntities();

        // GET: api/models
        public IQueryable<model> Getmodel()
        {
            return db.model;
        }

        // GET: api/models/5
        [ResponseType(typeof(model))]
        public IHttpActionResult Getmodel(int id)
        {
            model model = db.model.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // PUT: api/models/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmodel(int id, model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.id)
            {
                return BadRequest();
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!modelExists(id))
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

        // POST: api/models
        [ResponseType(typeof(model))]
        public IHttpActionResult Postmodel(model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.model.Add(model);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = model.id }, model);
        }

        // DELETE: api/models/5
        [ResponseType(typeof(model))]
        public IHttpActionResult Deletemodel(int id)
        {
            model model = db.model.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            db.model.Remove(model);
            db.SaveChanges();

            return Ok(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool modelExists(int id)
        {
            return db.model.Count(e => e.id == id) > 0;
        }
    }
}