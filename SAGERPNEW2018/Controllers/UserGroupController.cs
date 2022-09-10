using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using SAGERPNEW2018.Filters;
using SAGERPNEW2018.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class UserGroupController : Controller
    {
        // GET: UserGroup
    
      
        [UserRightFilters]

        public ActionResult Index(GLUserGroup a)
        {
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);

        }
        [UserRightFilters]

        public ActionResult create(GLUserGroup a)
        {
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
            return View(a);
            
        }


        [UserRightFilters]

        public ActionResult delete(int id)
        {
          
            new GLUserGroup().DeleteData(id);
            return RedirectToAction("Index");
        }
        [UserRightFilters]

        public ActionResult Edit(string id)
        {
            string[] ID = id.Split('|');

         
            GLUserGroup a = new GLUserGroup();
            a=a.getGroupData(Convert.ToInt32( ID[0]));
            a.detailistGroup = a.getGroupdetailbySp(Convert.ToInt32( ID[0]));
          
            if (ID[1] == "0")
            {
                a.IsView = true;
            }
            return View("create", a);
        }

        [HttpPost]
        [UserRightFilters]

        public ActionResult Save(GLUserGroup model, FormCollection form)
        {
            DataTable dtdetail = new DataTable();
            dtdetail.Columns.Add("UserGroupID", typeof(System.Int32));
            dtdetail.Columns.Add("Assign", typeof(System.Boolean));
            dtdetail.Columns.Add("FormID", typeof(System.Int32));
            dtdetail.Columns.Add("Edit", typeof(System.Boolean));
            dtdetail.Columns.Add("Delete", typeof(System.Boolean));
            dtdetail.Columns.Add("New", typeof(System.Boolean));
            dtdetail.Columns.Add("Print", typeof(System.Boolean));



            if (!string.IsNullOrEmpty(form["GroupdetailDatatable"].ToString()))
            {
                string[] TestDetailArray = form["GroupdetailDatatable"].Split(',');

                for (int i = 0; i < TestDetailArray.Length; i++)
                {
                    string[] localArray = TestDetailArray[i].Split('|');
                    DataRow dr = dtdetail.NewRow();

                    dr["UserGroupID"] = 0;
                    if (!string.IsNullOrEmpty(localArray[0].ToString()) && !localArray[0].Contains("null"))
                    {
                        dr[1] = localArray[0];

                    }

                    if (!string.IsNullOrEmpty(localArray[1].ToString()) && !localArray[1].Contains("null"))
                    {
                        dr[2] = localArray[1];

                    }
                    if (!string.IsNullOrEmpty(localArray[2].ToString()) && !localArray[2].Contains("null"))
                    {
                        dr[3] = localArray[2];

                    }
                    if (!string.IsNullOrEmpty(localArray[3].ToString()) && !localArray[3].Contains("null"))
                    {
                        dr[4] = localArray[3];

                    }
                    if (!string.IsNullOrEmpty(localArray[4].ToString()) && !localArray[4].Contains("null"))
                    {
                        dr[5] = localArray[4];

                    }
                    if (!string.IsNullOrEmpty(localArray[5].ToString()) && !localArray[5].Contains("null"))
                    {
                        dr[6] = localArray[5];

                    }
                    dtdetail.Rows.Add(dr);
                }
            }

            model.dtdetail = dtdetail;
            int a;
            if (model.GroupID > 0)
            {
                model.Entryby = new SystemLogin().GetUser().system;
              a=  model.UpdateData(model);
            }
            else
            {
              
                model.UserID = Convert.ToInt32(new SystemLogin().GetUser().Userid);
                model.Entryby = new SystemLogin().GetUser().system;
                model.TimeStamp = DateTime.Now;

                a = model.addata(model);
            }

            if (a>0)
            {
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("create", model);

            }

        }


        [UserRightFilters]

        public JsonResult Duplicate(string Name, int ID)
        {
            string json = "";
            var list = new GLUserGroup().checkDuplicate(ID, Name);
            if (list.Count() > 0)
            {
                json = " Duplicate Record Found ";
            }
            return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);
        }

     
        private  sp_GetUserRightByUser_Result loadrighta()
        {
            var User = new SystemLogin().GetUser();
           return new SystemLogin().checkRightUser(User.Userid).Where(x => x.Userid ==User.Userid && x.Formid==18).FirstOrDefault();

            //DataTable dtright = new Login().checkRightUser(" where GLUser.Userid='" + userdata[1] + "' and UserForms.Formid='18' ");
            //return dtright;
        }
    }
}