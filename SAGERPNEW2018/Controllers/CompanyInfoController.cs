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
    public class CompanyInfoController : Controller
    {
        // GET: CompanyInfo
        [UserRightFilters]
        public ActionResult Index()
        {

            try
            {
                tblCompany a = new tblCompany();
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                return View(a);
            }
            catch (Exception ex)
            {
                return View();
            }


        }
        [UserRightFilters]
        public ActionResult create(tblCompany a)
        {
            try
            {
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                a.CompanyLogo = "~/AppFiles/Images/placeholder-avatar.jpg";

                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }
        
        }
        [UserRightFilters]
        public ActionResult delete(int id)
        {
            try
            {
                tblCompany a = new tblCompany();

                bool c = a.DeleteData(id);
                if (c)
                {
                    return RedirectToAction("Index");

                }
                TempData["Dependancy"] = "This Record Using In Another Table";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }


        }
        [UserRightFilters]

        public ActionResult Edit(string id)
        {
            try
            {
                string[] ID = id.Split('|');

                tblCompany a = new tblCompany();
                var obj = a.getAlldataByID(Convert.ToInt32(ID[0]));
                if (ID[1] == "0")
                {
                    a.IsView = true;
                }
                return View("create", obj);

            }
            catch (Exception ex)
            {
                return View("create");
            }
          
        }

        [UserRightFilters]

        public ActionResult Save(tblCompany model)
        {
            try
            {
                int check;
                if (model.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    model.CompanyLogo = "~/AppFiles/Images/" + fileName;
                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
                }


                if (model.id > 0)
                {
                    check = check = model.UpdateData(model);
                }
                else
                {

                    check = model.addata(model);
                }

                if (check > 0)
                {
                    return RedirectToAction("Index");

                }
                return RedirectToAction("create", model);

            }
            catch (Exception ex)
            {

                return View("create");
            }
        }

        public ActionResult Duplicate(string Name, int ID)
        {
            try
            {
                string json = "";
                var list = new tblCompany().checkDuplicate(ID, Name);
                if (list.Count() > 0)
                {
                    json = " Duplicate Record Found ";
                }
                return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
         

                return Json(JsonConvert.SerializeObject("Error"), JsonRequestBehavior.AllowGet);

            }


        }
    }
}