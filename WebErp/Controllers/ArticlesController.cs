using Ninject;
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
using WebErp.Data;
using WebErp.Data.Repositories;
using WebErp.Models;

namespace WebErp.Controllers
{
    [RoutePrefix("api/articles")]
    public class ArticlesController : WebErpApiController<Article>
    {
      
        
    }
}