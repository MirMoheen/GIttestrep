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
    public class EmployeeController : Controller
    {
        // GET: Employee

        [UserRightFilters]
        public ActionResult Index()
        {
            tblEmployee a = new tblEmployee();
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);

          a.employedatalistObbj = new tblEmployee().getallemplyeedata("   and tblProjects.ProjectID=" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs+ "   ORDER BY Seniortylevel ASC ").ToList();



            return View(a);
        }

        [UserRightFilters]
        public ActionResult create(tblEmployee a)
        {
            a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
            a.Isedit = false;
            a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
            a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
            a.detailistnomine = a.getdetailistnomineData(-1);
            a.detailistDoc = a.getdetailistDocumentData(-1);
            a.detailistEduction = a.getdetailistEductionData(-1);
            a.detailistExpernce = a.getdetailistExprienceData(-1);
            a.EmployeePhotoPath = "~/AppFiles/Images/placeholder-avatar.jpg";
            a.DateOfBirth = DateTime.Now;
            a.DateofJoining = DateTime.Now;
            a.ProbationEnd = DateTime.Now;
            a.Machineid = 0000;


            return View(a);
        }


        [UserRightFilters]

        public ActionResult Edit(string id)
        {
            string[] ID = id.Split('|');

            tblEmployee a = new tblEmployee();
            var obj = a.getAlldataByID(Convert.ToInt32(ID[0]));
            //obj.getCustomerdata = a.getAllCustomerdata().FirstOrDefault(x => x.Id == Convert.ToInt32(ID[0]));
        
            
            obj.detailistnomine = a.getdetailistnomineData(Convert.ToInt32(ID[0]));
            obj.detailistDoc = a.getdetailistDocumentData(Convert.ToInt32(ID[0]));

            obj.detailistExpernce = a.getdetailistExprienceData(Convert.ToInt32(ID[0]));
            obj.detailistEduction = a.getdetailistEductionData(Convert.ToInt32(ID[0]));
            



            ViewData["Editmode"] = true;

            if (ID[1] == "0")
            {
                obj.IsView = true;
            }
            return View("create", obj);
        }




        [UserRightFilters]

        public ActionResult delete(string id)
        {
            tblEmployee obj = new tblEmployee();

             obj = obj.getAlldataByID(Convert.ToInt32(id));
            if (obj.EmployeeStatusDate==null)
            {
                obj.EmployeeStatusDate = DateTime.Now;

            }
            return View( obj);
        }


        public ActionResult EmployeeStatusUpdate(tblEmployee model)
        {
            var check = false;

            if (model.EmployeeID > 0)
            {
                check = model.updateEmpployeStatus(model);
                TempData["ActionMessage"] = true;
                return RedirectToAction("Index");

            }
        
            TempData["ActionMessage"] = false;


            return RedirectToAction("Index", model);
        }


        public ActionResult Save(tblEmployee model)
        {
            if (!string.IsNullOrEmpty(model.base64image))
            {
                model.EmployeePhotoPath = new tblEmployee().storeImagePath(model.base64image);
            }
            else
            {
                if (model.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    fileName = Guid.NewGuid().ToString().Substring(0, 4) + extension;
                    model.EmployeePhotoPath = "~/AppFiles/Images/" + model.EmployeeCNIC+ fileName;
                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), model.EmployeeCNIC + fileName));
                }
            }

          //  model.ProjectID = new SAGERPNEW2018.Models.SystemLogin().GetUser().companyID;
            model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
            model.EntryDatetime = DateTime.Now;
            model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
            model.ProjectID  = Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);


            var checkpath = CreatImagesPath(model);

            bool check;

            if (model.EmployeeID > 0)
            {
                check = model.UpdateData(model);
                TempData["ActionMessage"] = true;


                return RedirectToAction("Index");

            }
            else
            {
                model.EmployeeNo = model.genearteEmployeeNo(model.ProjectID,model.DepartmentID );
                var result = model.addata(model);
                if (result != null && result.EmployeeID > 0)
                {

                    //ReportDocument rptH = new ReportDocument();
                    //rptH.Load(Path.Combine(Server.MapPath("~/Reports"), "ReceiptPrint.rpt"));

                    //var resultlist = model.getReceptCustomerdatadt(model.Id);
                    //rptH.SetDataSource(resultlist);                    
                    //if (resultlist!=null)
                    //{
                    //    var host = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);                       
                    //    Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    //    File(stream, "application/pdf", "Receipt"+DateTime.Now.Millisecond+".pdf");
                    //}


                    TempData["ActionMessage"] = true;
                    return RedirectToAction("Index");

                }
            }
            TempData["ActionMessage"] = false;


            return RedirectToAction("Index", model);
        }



        public bool CreatImagesPath(tblEmployee model)
        {
            try
            {
                List<string> myCollection = new List<string>();
                if (model.DocpathFile != null)
                {
                    foreach (var item in model.DocpathFile)
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
                            string imagename = model.EmployeeCNIC+ DateTime.Now.Ticks.ToString();
                            var fileName = Path.GetFileName(item.FileName);
                            string ext = Path.GetExtension(fileName);
                            var path = Path.Combine(Server.MapPath("~/AppFiles/Docments/"), imagename + ext);
                            item.SaveAs(path);
                            string Savepath = "~/AppFiles/Docments/" + imagename + ext;
                            myCollection.Add(Savepath);
                        }
                    }
                }

                model.DocpathFile = myCollection;
            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }

        public JsonResult loadDesigantionBydepartment(int id)
        {
            var list = new tblEmployee().LoadDesignation(id);

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public JsonResult LoademployeeData(int id ,int status)
        {

            if (id > 0)
            {
                var list = new tblEmployee().getallemplyeedata(" and  tblEmployee.DepartmentID=" + id + " and tblEmployee.inactive=" + status + "   and tblProjects.ProjectID=" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs + "   ORDER BY Seniortylevel ASC ");
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else if (id == 0)
            {
                var list = new tblEmployee().getallemplyeedata(" and tblEmployee.inactive=" + status + "   and tblProjects.ProjectID=" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs + "   ORDER BY Seniortylevel ASC ");
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var list = new tblEmployee().getallemplyeedata("  and tblEmployee.inactive=" + status + "   and tblProjects.ProjectID=" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs + "   ORDER BY Seniortylevel ASC ");

                return Json(list, JsonRequestBehavior.AllowGet);
            }

       
        }

        public JsonResult getEmployeeNo( int DepartmentID)
        {
           var ProjectID = Convert.ToInt32( new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
            var EmployeeNo = new tblEmployee().genearteEmployeeNo(ProjectID, DepartmentID);

            return Json(EmployeeNo, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Duplicate( long MachineCode, string Name, Int32 ID   )
        {
            string json = "";
            var list = new tblEmployee().checkDuplicate(ID, Name);
            if (list.Count() > 0)
            {
                json = "CNIC Duplicate Record Found  ";
                return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);

            }

            var code = new tblEmployee().checkMachine(ID, MachineCode);
            if (code.Count() > 0)
            {
                json = "Machine Code Duplicate Record ";
                return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);

            }
            return Json(JsonConvert.SerializeObject(json), JsonRequestBehavior.AllowGet);
        }




    }
}