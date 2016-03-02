using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebErp.Models;

namespace WebErp.Controllers
{
    [RoutePrefix("api/Article")]
    public class ArticleController : ApiController
    {
        List<Article> articles;
        public ArticleController()
        {
            articles = new List<Article>();
            for (int i = 1; i <= 300; i++)
            {
                articles.Add(new Article { Societe = 999, Code = "Code"+i });
            }
            

        }

        // GET api/values
        public IQueryable<Article> Get()
        {
            return articles.AsQueryable();
        }
    }
}
