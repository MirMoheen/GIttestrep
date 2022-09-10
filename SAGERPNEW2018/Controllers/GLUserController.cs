
using Newtonsoft.Json;
using HRandPayrollSystemModel.DBModel;
using SAGERPNEW2018.Filters;
using SAGERPNEW2018.Models;
using System;

using System.IO;
using System.Linq;

using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    [HandleExceptionFilter]
    public class GLUserController : Controller
    {
        // GET: GLUser
        [UserRightFilters]
        public ActionResult Index()
        {
            GLUser a = new GLUser();

            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
        }
        [UserRightFilters]
        public ActionResult create(GLUser a)
        {
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
            a.PhotoPath = "~/AppFiles/Images/placeholder-avatar.jpg";

            return View(a);
        }
        [UserRightFilters]
        public ActionResult delete(int id)
        {
            GLUser a = new GLUser();

            bool c = a.DeleteData(id);
            if (c)
            {
                return RedirectToAction("Index");

            }
            TempData["Dependancy"] = "This Record Using In Another Table";
            return RedirectToAction("Index");
        }
        [UserRightFilters]

        public ActionResult Edit(string id)
        {
            string[] ID = id.Split('|');

            GLUser a = new GLUser();
            var obj= a.getAlldataByID(Convert.ToInt32(ID[0]));


            ViewData["Editmode"] = true;
            if (ID[1] == "0")
            {
                a.IsView = true;
            }
            return View("create", obj);
        }

        [UserRightFilters]

        public ActionResult Save(GLUser model)
        {
            int check;
            if (model.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                string extension = Path.GetExtension(model.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                model.PhotoPath = "~/AppFiles/Images/" + fileName;
                model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
            }

            model.companyID = new SystemLogin().Getcompany().id;
            model.Entryby = new SystemLogin().GetUser().system +"-"+ new  SystemLogin().GetUser().Userid.ToString();
            model.ProjectIDs = string.Join(",", model.SelectProjectIDs);

            if (model.SelectEmployees!=null)
            {
                 model.AllowFwdEmployee = string.Join(",", model.SelectEmployees);
            }
            if (model.SelectEmployeesTransfer != null)
            {
                model.TransferEmplyeeIDs = string.Join(",", model.SelectEmployeesTransfer);
            }
            if (model.Userid > 0)
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

        public ActionResult Duplicate(string Name, Int32 ID)
        {
            string json = "";
            var list = new GLUser().checkDuplicate(ID, Name);
            if (list.Count() > 0)
            {
                json = " Duplicate Record Found ";
            }
            return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);
        }


        public JsonResult loadEmployeeBydepartment(int id)
        {


            var list = new GLUser().loadEmployee(id);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}