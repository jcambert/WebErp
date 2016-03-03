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
        IQueryable<Article> articles;
        public ArticleController()
        {



        }

        // GET api/values
        public IQueryable<Article> Get()
        {
            return articles;
        }

        [Route("First")]
        public Article GetFirst()
        {
            return articles.First();
        }

        [Route("Last")]
        public Article GetLast()
        {
            return articles.Last();
        }

        [Route("Next")]
        public Article GetNext(int index)
        {
            return Next(index, articles);
        }

        [Route("Previous")]
        public Article GetPrevious(int index)
        {
            return Previous(index, articles);
        }

        [Inject]
        public IModelBaseRepository<Article> Repository { get; set; }
        
        [Inject]
        public IContext Context { get; set; }

        protected bool HasPrevious(int index, IQueryable<Article> articles)
        {
            return index > 0;
        }

        protected Article Previous(int index, IQueryable<Article> articles)
        {
            if (HasPrevious(index, articles))
                return articles.ElementAt(--index);
            return articles.First();
        }

        protected bool HasNext(int index, IQueryable<Article> articles)
        {
            return (index + 1) < articles.Count();
        }

        protected Article Next(int index, IQueryable<Article> articles)
        {
            if (HasNext(index, articles))
                return articles.ElementAt(++index);
            return articles.Last();
        }

        public void Initialize()
        {
            articles = Repository.All;
            if (articles.Count() == 0) {
                var tmp = new List<Article>();
                for (int i = 1; i <= 300; i++)
                {
                    Repository.Add(new Article { Societe = 999, Code = "Code" + i });
                }
                
                Context.Commit();
                
                articles = Repository.All;
            }
        }

        public class NavigationState<T>
        {
            public NavigationState(T result)
            {
                this.Result = result;

            }
            T Result { get; set; }
        }
    }

}
