using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using SAGERPNEW2018.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class TaxConfigController : Controller
    {
        // GET: TaxConfig
        [UserRightFilters]
        public ActionResult Index()
        {
            try
            {
                TaxConfiguration a = new TaxConfiguration();
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                var obj = a.getAlldata();
                a.AllowanceDeductionID = obj[0].AllowanceDeductionID;
               
                return View(a);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult Save(TaxConfiguration model)
        {
            int check = 0;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
            model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().IP;           
            
            check = model.UpdateData(model);
           
            return View("Index", model);
        }
    }
}