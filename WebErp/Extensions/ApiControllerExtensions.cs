using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using WebErp.Controllers;
using WebErp.Models;

namespace WebErp.Extensions
{
    internal static class ApiControllerExtensions
    {
        public static ApiActionResult<T> ApiOk<T>(this WebErpApiController<T> ctrl,T entity, int index, int count) where T :class,IModelBase,new()
        {
            return new ApiActionResult<T>(ctrl, HttpStatusCode.OK, entity,index,count);
        }

        public static ApiActionResult<T> ApiOk<T>(this WebErpApiController<T> ctrl, T entity) where T : class, IModelBase, new()
        {
            return new ApiActionResult<T>(ctrl, HttpStatusCode.OK, entity);
        }

        public static ApiActionResult<T> ApiBadRequest<T>(this WebErpApiController<T> ctrl) where T : class, IModelBase, new()
        {
            return new ApiActionResult<T>(ctrl, HttpStatusCode.BadRequest);
        }

        public static ApiActionResult<T> ApiNotFound<T>(this WebErpApiController<T> ctrl) where T : class, IModelBase, new()
        {
            return new ApiActionResult<T>(ctrl, HttpStatusCode.NotFound);
        }
    }
}