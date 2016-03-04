using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebErp.Data;
using WebErp.Data.Repositories;
using WebErp.Models;

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

        [Route("First")]
        public ApiResult<Article> GetFirst()
        {
            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            if (l.Count() == 0)
                return new ApiResult<Article>(null, 0, 0);
            return new ApiResult<Article>(l.First(), 0, l.Count());
        }

        [Route("Last")]
        public ApiResult<Article> GetLast()
        {
            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            if (l.Count() == 0)
                return new ApiResult<Article>(null, 0, 0);
            return new ApiResult<Article>(l.AsEnumerable().Last(), l.Count()-1, l.Count());
        }

        [Route("Next")]
        public ApiResult< Article> GetNext(int index)
        {
            Article result;
            int newIndex;
            var articles = Repository.All;
            Next(index, articles,out result,out newIndex);
            return new ApiResult<Article>(result, newIndex,articles.Count());
        }

        [Route("Previous")]
        public ApiResult<Article> GetPrevious(int index)
        {
            Article result;
            int newIndex;
            var articles = Repository.All;
            Previous(index, articles, out result, out newIndex);
            return new ApiResult<Article>(result, newIndex, articles.Count());
            
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
            return (index ) < articles.Count();
        }

        protected void Next(int index, IQueryable<Article> articles,out Article article,out int newIndex)
        {
           
            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            if (HasNext(index, articles)) {
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
            if (articles.Count() == 0) {
                var tmp = new List<Article>();
                for (int i = 1; i <= 300; i++)
                {
                    Context.ArticleSet.Add(new Article { Societe = 999, Code = "Code" + i,Libelle="Libelle "+i });
                }
                
                Context.Commit();
                
                articles = Repository.All;
            }
        }

        public class ApiResult<T>
        {
            public ApiResult(T result,int index,int count)
            {
                this.Result = result;
                this.Index = index;
                this.Count = count;
            }
            public int Count { get; private set; }
            public int Index { get; private set; }
            public T Result { get; private set; }

        }
    }

}
