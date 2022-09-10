using SAGERPNEW2018.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Filters
{
    public class UserRightFilters : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HttpContext ctx = HttpContext.Current;
            //if (ctx.Session != null)
            //{
            //    // check if a new session id was generated
            //    if (ctx.Session.IsNewSession)
            //    {
            //        // If it says it is a new session, but an existing cookie exists, then it must
            //        // have timed out
            //        string sessionCookie = ctx.Request.Headers["UserCookie"];
            //        if ((null != sessionCookie))
            //        {
            //            ctx.Response.Redirect("~/");
            //        }
            //    }
            //}

            var resultUser = new SAGERPNEW2018.Models. SystemLogin().GetUser();
            if (resultUser != null)
            {
                var UserRightsData = new SystemLogin().checkRightUser(resultUser.Userid);
                var ResultRole = UserRightsData.FirstOrDefault(x => x.controller == filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);// && x.action == filterContext.ActionDescriptor.ActionName);

             ///   var ResultRole = ResultRoleresult.FirstOrDefault(x => x.action == filterContext.ActionDescriptor.ActionName);
                //  var ResultRole = UserRightsData.FirstOrDefault(x => x.controller == filterContext.ActionDescriptor.ControllerDescriptor.ControllerName && x.action == filterContext.ActionDescriptor.ActionName);


                if (Convert.ToBoolean(ResultRole.Assign))
                {
                    //filterContext.Result = new RedirectResult("~/Home/Login", true);
                    filterContext.Controller.TempData["IsNew"] = ResultRole.Isnew;
                    filterContext.Controller.TempData["IsEdit"] = ResultRole.IsEdit;
                    filterContext.Controller.TempData["IsDelete"] = ResultRole.IsDelete;
                    filterContext.Controller.TempData["IsPrint"] = ResultRole.IsPrint;
                 


                    return;
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Home/Login", true);


                    filterContext.Controller.TempData["LoginFailed"] = "Access denied You Have No Rights To Perform This Action";
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Home/Login", true);
                filterContext.Controller.TempData["LoginFailed"] = "Access Denied You Have No Rights To Perform This Action";


                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

}