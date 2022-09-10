using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Filters
{


    public class HandleExceptionFilter: FilterAttribute, IExceptionFilter
    {
    
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)
            {
               filterContext.Result = new RedirectResult("~/Home/Error");

                filterContext.ExceptionHandled = true;
            }
        }

    }


}