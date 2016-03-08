using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WebErp.Data;
using WebErp.Data.Repositories;
using WebErp.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;

namespace WebErp.Controllers
{
    [RoutePrefix("api/Article")]
    public class ArticleController : ApiController, IInitializable
    {
        //IQueryable<Article> articles;
        public ArticleController()
        {



        }



        // GET api/values
        public IQueryable<Article> Get()
        {
            return Repository.All;
        }

        public ApiActionResult<Article> Get(int index)
        {
            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            return new ApiActionResult<Article>(this, l.Skip(index).First(), 0, l.Count());
        }

        [Route("First")]
        public ApiActionResult<Article> GetFirst()
        {
            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            if (l.Count() == 0)
                return new ApiActionResult<Article>(this, null, 0, 0);
            return new ApiActionResult<Article>(this, l.First(), 0, l.Count());
        }

        [Route("Last")]
        public ApiActionResult<Article> GetLast()
        {
            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            if (l.Count() == 0)
                return new ApiActionResult<Article>(this, null, 0, 0);
            return new ApiActionResult<Article>(this, l.AsEnumerable().Last(), l.Count() - 1, l.Count());
        }

        [Route("Next")]
        public ApiActionResult<Article> GetNext(int index)
        {
            Article result;
            int newIndex;
            var articles = Repository.All;
            Next(index, articles, out result, out newIndex);
            return new ApiActionResult<Article>(this, result, newIndex, articles.Count());
        }

        [Route("Previous")]
        public ApiActionResult<Article> GetPrevious(int index)
        {
            Article result;
            int newIndex;
            var articles = Repository.All;
            Previous(index, articles, out result, out newIndex);
            return new ApiActionResult<Article>(this, result, newIndex, articles.Count());

        }

        [Inject]
        public IModelBaseRepository<Article> Repository { get; set; }

        [Inject]
        public IContext Context { get; set; }

        protected bool HasPrevious(int index, IQueryable<Article> articles)
        {
            return index > 0;
        }

        protected void Previous(int index, IQueryable<Article> articles, out Article article, out int newIndex)
        {
            var l = from a in articles orderby a.Societe, a.Code select a;
            if (HasPrevious(index, articles))
            {
                var tmp = index - 1;
                article = l.Skip(tmp).First();
                newIndex = tmp;
                return;
            }
            article = l.First();
            newIndex = index;

        }

        protected bool HasNext(int index, IQueryable<Article> articles)
        {
            return (index) < articles.Count();
        }

        protected void Next(int index, IQueryable<Article> articles, out Article article, out int newIndex)
        {

            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            if (HasNext(index, articles))
            {
                var tmp = index + 1;
                article = l.Skip(index).First();
                newIndex = tmp;
                return;
            }
            article = l.AsEnumerable().Last<Article>();// Skip(l.Count() -1).First();
            newIndex = index;
        }

        public void Initialize()
        {
            var articles = Repository.All;
            if (articles.Count() == 0)
            {
                var tmp = new List<Article>();
                for (int i = 1; i <= 300; i++)
                {
                    Context.Articles.Add(new Article { Societe = 999, Code = "Code" + i, Libelle = "Libelle " + i });
                }

                Context.Commit();

                articles = Repository.All;

            }
        }

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
                Result.Status = status;
            }


            public ApiActionResult(ApiController ctrl, T result, int index, int count)
            {
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
        }
    }

}
