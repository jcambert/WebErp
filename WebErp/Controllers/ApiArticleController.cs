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
using WebErp.Extensions;
namespace WebErp.Controllers
{
    [RoutePrefix("api/Article")]
    public class ApiArticleController : WebErpApiController<Article>
    {
        //IQueryable<Article> articles;
        public ApiArticleController()
        {



        }




        public ApiActionResult<Article> Get(int index)
        {
            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            if (index >= l.Count())
                return this.ApiBadRequest();
            return this.ApiOk(l.Skip(index).First(), index, l.Count());

        }



        [Route("First")]
        public ApiActionResult<Article> GetFirst()
        {
            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            if (l.Count() == 0)
                return this.ApiBadRequest();

            return this.ApiOk(l.First(), 0, l.Count());
        }

        [Route("Last")]
        public ApiActionResult<Article> GetLast()
        {
            var l = from a in Repository.All orderby a.Societe, a.Code select a;
            if (l.Count() == 0)
                return this.ApiBadRequest();
            return this.ApiOk ( l.AsEnumerable().Last(), l.Count() - 1, l.Count());
        }

        [Route("Next")]
        public ApiActionResult<Article> GetNext(int index)
        {
            Article result;
            int newIndex;
            var articles = Repository.All;
            Next(index, articles, out result, out newIndex);
            return this.ApiOk( result, newIndex, articles.Count());
        }

        [Route("Previous")]
        public ApiActionResult<Article> GetPrevious(int index)
        {
            Article result;
            int newIndex;
            var articles = Repository.All;
            Previous(index, articles, out result, out newIndex);
            return this.ApiOk( result, newIndex, articles.Count());

        }



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

        /* public void Initialize()
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
         }*/

    }

}
