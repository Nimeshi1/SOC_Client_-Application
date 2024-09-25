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
using TechFixWebApi;

namespace TechFixWebApi.Controllers
{
    public class RequestQuotationsController : ApiController
    {
        private TechFixDB1Entities db = new TechFixDB1Entities();

        // GET: api/RequestQuotations
        public IQueryable<RequestQuotation> GetRequestQuotations()
        {
            return db.RequestQuotations;
        }

        // GET: api/RequestQuotations/5
        [ResponseType(typeof(RequestQuotation))]
        public IHttpActionResult GetRequestQuotation(int id)
        {
            RequestQuotation requestQuotation = db.RequestQuotations.Find(id);
            if (requestQuotation == null)
            {
                return NotFound();
            }

            return Ok(requestQuotation);
        }

        // PUT: api/RequestQuotations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequestQuotation(int id, RequestQuotation requestQuotation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestQuotation.RequestQuotationID)
            {
                return BadRequest();
            }

            db.Entry(requestQuotation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestQuotationExists(id))
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

        // POST: api/RequestQuotations
        [ResponseType(typeof(RequestQuotation))]
        public IHttpActionResult PostRequestQuotation(RequestQuotation requestQuotation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestQuotations.Add(requestQuotation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RequestQuotationExists(requestQuotation.RequestQuotationID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = requestQuotation.RequestQuotationID }, requestQuotation);
        }

        // DELETE: api/RequestQuotations/5
        [ResponseType(typeof(RequestQuotation))]
        public IHttpActionResult DeleteRequestQuotation(int id)
        {
            RequestQuotation requestQuotation = db.RequestQuotations.Find(id);
            if (requestQuotation == null)
            {
                return NotFound();
            }

            db.RequestQuotations.Remove(requestQuotation);
            db.SaveChanges();

            return Ok(requestQuotation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestQuotationExists(int id)
        {
            return db.RequestQuotations.Count(e => e.RequestQuotationID == id) > 0;
        }
    }
}