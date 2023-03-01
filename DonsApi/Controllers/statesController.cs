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
    public class statesController : ApiController
    {
        private dronesEntities db = new dronesEntities();

        // GET: api/states
        public IQueryable<state> Getstate()
        {
            return db.state;
        }

        // GET: api/states/5
        [ResponseType(typeof(state))]
        public IHttpActionResult Getstate(int id)
        {
            state state = db.state.Find(id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }

        // PUT: api/states/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstate(int id, state state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != state.id)
            {
                return BadRequest();
            }

            db.Entry(state).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!stateExists(id))
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

        // POST: api/states
        [ResponseType(typeof(state))]
        public IHttpActionResult Poststate(state state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.state.Add(state);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = state.id }, state);
        }

        // DELETE: api/states/5
        [ResponseType(typeof(state))]
        public IHttpActionResult Deletestate(int id)
        {
            state state = db.state.Find(id);
            if (state == null)
            {
                return NotFound();
            }

            db.state.Remove(state);
            db.SaveChanges();

            return Ok(state);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool stateExists(int id)
        {
            return db.state.Count(e => e.id == id) > 0;
        }
    }
}