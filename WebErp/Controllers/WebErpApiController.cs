using Newtonsoft.Json;
using Ninject;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebErp.Data;
using WebErp.Data.Repositories;
using WebErp.Models;
using System;
using WebErp.Extensions;
using System.Collections.Generic;

namespace WebErp.Controllers
{
    public class WebErpApiController<T> : ApiController, IInitializable where T : class, IModelBase, new()
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
        public ApiActionResult<T> Get(string id)
        {
            T entity = Repository.GetSingle(id);
            if (entity == null)
            {
                return this.ApiNotFound();
            }

            return this.ApiOk(entity);
        }


        [Route("{pageSize:int}/{pageNumber:int}/{orderBy:alpha}/{sortDirection:alpha?}")]
        public IQueryable<T> Get(int pageSize, int pageNumber, string orderBy, string sortDirection = "asc")
        {
            var totalCount = this.Repository.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            var query = this.Repository.All;

            if (QueryHelper.PropertyExists<T>(orderBy))
            {
                var orderByExpression = QueryHelper.GetPropertyExpression<T>(orderBy);

                if (sortDirection == "asc")
                    query = query.OrderBy(orderByExpression);
                else
                    query = query.OrderByDescending(orderByExpression);
            }
            else
            {
                query = query.OrderBy(c => c.ID);

            }

            var entities = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return entities;


        }

       /* public IQueryable<T> Post_Get([FromBody] GetModelBySearchRequest request)
        {
            return this.Get();
        }*/
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

        public virtual void Initialize()
        {

        }
    }

    public class ApiActionResult<T>
    {
        public int Count { get; private set; }
        public int Index { get; private set; }
        public T Result { get; private set; }
        public HttpStatusCode Status { get; private set; }
        //public HttpRequestMessage Request { get; private set; }

        public ApiActionResult(ApiController ctrl, HttpStatusCode status)
        {
            //Request = ctrl.Request;
            Status = status;
        }

        public ApiActionResult(ApiController ctrl, HttpStatusCode status, T result)
        {
            // Request = ctrl.Request;
            Status = status;
            Result = result;
        }

        public ApiActionResult(ApiController ctrl, HttpStatusCode status, T result, int index, int count)
        {
            // Request = ctrl.Request;
            Status = status;
            Result = result;
            Index = index;
            Count = count;

        }
    }
    /*
    public class ApiActionResult<T> : IHttpActionResult
    {
        public class ApiResult
        {
            public int Count { get; internal set; }
            public int Index { get; internal set; }
            public T Result { get; internal set; }
            public HttpStatusCode Status { get; internal set; }
        }


        public class JsonContent : HttpContent
        {
            private readonly string _result;

            public JsonContent(ApiResult result)
            {
                this._result = JsonConvert.SerializeObject(result);
                Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
            {

                var jw = new StreamWriter(stream);

                jw.Write(_result);

                jw.Flush();
                return Task.FromResult<object>(null);
            }

            protected override bool TryComputeLength(out long length)
            {
                length = _result.Length;
                return false;
            }
        }

        public ApiActionResult(ApiController ctrl, HttpStatusCode status)
        {
            Request = ctrl.Request;
            Result.Status = status;
        }


        public ApiActionResult(ApiController ctrl, HttpStatusCode status, T result, int index, int count)
        {
            Request = ctrl.Request;
            Result.Result = result;
            Result.Index = index;
            Result.Count = count;

        }

        public HttpRequestMessage Request { get; private set; }

        public ApiResult Result { get; set; } = new ApiResult();

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(Result.Status);
            response.Content = new JsonContent(Result);
            response.RequestMessage = Request;
            return response;
        }
    }*/

}