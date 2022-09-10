using HRandPayrollSystemModel.DBModel;
using SAGERPNEW2018.Domain;
using SAGERPNEW2018.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
    public class NavbarController : Controller
    {
        // GET: Navbar
        public ActionResult NavBarView()
        {
        
             sp_GetUserRightByUser_Result nav = new sp_GetUserRightByUser_Result();
           var User = new SystemLogin().GetUser();
            var ass = new SystemLogin().checkRightUser(User.Userid).Where(x => x.Assign == true).ToList();
            return PartialView("_Navigation", new SystemLogin().checkRightUser(User.Userid).Where(x=>x.Assign==true).ToList());

        }
    }
}