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
    public class QuotationsController : ApiController
    {
        private TechFixDB1Entities db = new TechFixDB1Entities();

        // GET: api/Quotations
        public IQueryable<Quotation> GetQuotations()
        {
            return db.Quotations;
        }

        // GET: api/Quotations/5
        [ResponseType(typeof(Quotation))]
        public IHttpActionResult GetQuotation(int id)
        {
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return NotFound();
            }

            return Ok(quotation);
        }

        // PUT: api/Quotations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuotation(int id, Quotation quotation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quotation.QuotationID)
            {
                return BadRequest();
            }

            db.Entry(quotation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuotationExists(id))
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

        // POST: api/Quotations
        [ResponseType(typeof(Quotation))]
        public IHttpActionResult PostQuotation(Quotation quotation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quotations.Add(quotation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (QuotationExists(quotation.QuotationID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = quotation.QuotationID }, quotation);
        }

        // DELETE: api/Quotations/5
        [ResponseType(typeof(Quotation))]
        public IHttpActionResult DeleteQuotation(int id)
        {
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return NotFound();
            }

            db.Quotations.Remove(quotation);
            db.SaveChanges();

            return Ok(quotation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuotationExists(int id)
        {
            return db.Quotations.Count(e => e.QuotationID == id) > 0;
        }
    }
}