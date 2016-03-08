using Ninject;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebErp.Data;
using WebErp.Data.Repositories;
using WebErp.Models;

namespace WebErp.Controllers
{
    public class  WebErpApiController<T> :ApiController where T :class,IModelBase,new()
    {

        [Inject]
        public IModelBaseRepository<T> Repository { get; set; }

        [Inject]
        public IContext db { get; set; }


        // GET: api/Articles
        public IQueryable<T> Get()
        {
            return db.Set<T>();
        }

        // GET: api/Articles/5
        //[ResponseType(typeof(T))]
        public IHttpActionResult Get(string id)
        {
            T entity = Repository.GetSingle(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(string id, T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entity.ID)
            {
                return BadRequest();
            }

            db.Entry(entity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
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

        // POST: api/Articles
        //[ResponseType(typeof(Article))]
        public IHttpActionResult Post(T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Set<T>().Add(entity);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EntityExists(entity.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = entity.ID }, entity);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult Delete(string id)
        {
            T entity = Repository.GetSingle(id); ;
            if (entity == null)
            {
                return NotFound();
            }

            db.Set<T>().Remove(entity);
            db.SaveChanges();

            return Ok(entity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntityExists(string id)
        {
            return db.Set<T>().Count(e => e.ID == id) > 0;
        }
    }
}