using SAGERPNEW2018.Filters;
using HRandPayrollSystemModel.FixedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using SAGERPNEW2018.ServiceReference1;
using System.Configuration;
using System.Globalization;
using System.IO;



namespace SAGERPNEW2018.Controllers
{
    public class AssetServiceController : Controller
    {
        // GET: AssetService
        public ActionResult Index()
        {
            return View();
        }




        public ActionResult ViewList(string id)
        {
            tblAssetService a = new tblAssetService();
            try
            {


                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                TempData["St"] = id;

                Session["AssetList"] = id;
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }


       [UserRightFilters]
        public ActionResult Create()
        {

            tblAssetService a = new tblAssetService();
            try
            {

                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                ViewData["Editmode"] = false;
                a.ServiceCode = a.GenerateServiceCode("S");
                a.detailistDoc = a.getdetailistDocumentData1(-1);

                a.MaintenanceDate = DateTime.Now;





                a.CompletionDate = DateTime.Now;
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }


       [UserRightFilters]

        public ActionResult Save(tblAssetService model, FormCollection collection)
        {
            int check;

                var checkpath = CreateImagesPath(model);

                model.EntryDate = DateTime.Now;
                model.MaintenanceDate = DateTime.Now;
                model.AssetTypeID = Convert.ToInt32(collection["AssetTypeID"].ToString());
                //model.AssetTypeID = model.AssetTypeID;
                model.AssetSubTypeID = Convert.ToInt32(collection["Subtype"].ToString());
                model.AssetTagNoID = Convert.ToInt32(collection["tagID"].ToString());

                model.ServiceCode = model.GenerateServiceCode("S");// Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));
                model.UserID = Convert.ToString(new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid);
                model.IP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;

                var employedata = new HRandPayrollSystemModel.DBModel.tblEminuteInfo().LoadEmployeeInfo(new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID);
                // model.CurrentLogEmployeedetail = employedata.DepartmentName + "-" + employedata.Initiatedby;
                model.CurrentLogEmployeedetail = employedata.Initiatedby;

                //model.detailistDoc1 = model.getdetailistDocumentData1(0);

                check = model.addata(model);
            


            //  model.currentUserEmployeeID = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;

            if (check > 0)
            {
                ViewData["Message"] = "Success";
                return RedirectToAction("Create", "AssetService");

            }
            return RedirectToAction("create", model);
        }



        public JsonResult getSubtype(int type)
        {

            var result = new tblAssetService().loadSubtypebyTypeID(type); // .OrderBy(x => x.Text)

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getTagNo(int type)
        {

            var result = new tblAssetService().loadTagID(type); // .OrderBy(x => x.Text)

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //FOR Store Procedure
        //public JsonResult LoadAssetServices()
        //{
        //    var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;
        //    var check = new tblAssetService().loadServicesAsset();

        //    return Json(check, JsonRequestBehavior.AllowGet);
        //}



        public JsonResult loadAssets(int id)
        {


            var list = new tblAssetService().loadTagID(id);


            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult loadFixedAssetLog(int AssetTag)
        {
            tblAssetService a = new tblAssetService();
           
            var result = new tblAssetService().getdetailistDocumentData(AssetTag);

            foreach (var item in result)
            {
                item.dateformat = item.MaintenanceDate.Value.ToString("dd-MMM-yyyy");
            }



            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult loadFixedAssetLog1(int AssetTag)
        {
            // var useid = new SAGERPNEW2018.Models.SystemLogin().GetUser().EmployeeID;

            var result = new tblAssetService().getdocinfo(AssetTag);


            return Json(result, JsonRequestBehavior.AllowGet);

        }



        public JsonResult loadModalData(int AssetTag1)
        {
         
            var result = new tblAssetService().loadBootstrapModal(AssetTag1);


            return Json(result, JsonRequestBehavior.AllowGet);

        }



        public bool CreateImagesPath(tblAssetService model)
        {
            try
            {
                List<string> myCollection = new List<string>();
                if (model.listdocpath != null)
                {
                    foreach (var item in model.listdocpath)
                    {
                        if (item != null)
                        {
                            myCollection.Add(item);
                        }
                    }
                }
                if (model.DocFile != null)
                {
                    foreach (var item in model.DocFile)  //3rd change
                    {
                        if (item != null)

                        {
                            var Random = Guid.NewGuid().ToString("n").Substring(0, 8);

                            string imagename = model.ServiceID + "i" + Random;
                            var fileName = Path.GetFileName(item.FileName);
                            string ext = Path.GetExtension(fileName);
                            var path = Path.Combine(Server.MapPath("~/AppFiles/AssetAttachment/"), imagename + ext);
                            item.SaveAs(path);
                            string Savepath = "~/AppFiles/AssetAttachment/" + imagename + ext;
                            myCollection.Add(Savepath);
                        }
                    }
                }

                model.listdocpath = myCollection;
            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }

    }
}