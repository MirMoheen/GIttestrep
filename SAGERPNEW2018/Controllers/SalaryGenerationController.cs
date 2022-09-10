using HRandPayrollSystemModel.DBModel;
using Newtonsoft.Json;
using SAGERPNEW2018.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Controllers
{
    public class SalaryGenerationController : Controller
    {
        // GET: SalaryGeneration
        [UserRightFilters]
        public ActionResult Index()
        {
            try
            {
                tblSalaryGeneration a = new tblSalaryGeneration();
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

        public ActionResult Generate(string id)
        {

            try
            {
                string[] IDo = id.Split('|');
                DateTime newdate = new  DateTime(Convert.ToInt32( IDo[1]), Convert.ToInt32( IDo[0]),1);
                tblSalaryGeneration a = new tblSalaryGeneration();
                if (a.getAllCurrentData(newdate.Month, newdate.Year).Count > 0)
                { a.lbmsg = "Salary is already generated for this month"; }
                else
                {


                  var LoanRemiangCHeck=  a.getAllRemaingLoanAmount(Convert.ToInt32(new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs));
                    a.lbmsg = "Salary Generated Successfully now Click Save Button to post the Salary";
                    a.listmontlySalary = a.getAllSalarydata(newdate.Month, newdate.Year);
                    DataTable dt = ToDataTable(a.listmontlySalary.ToList());
          
                    DataView view = new DataView(dt);
                    DataTable distinctValues = view.ToTable(true, "ShortName", "ST");
                    DataView dv = distinctValues.DefaultView;
                    dv.Sort = "ST";
                    distinctValues = dv.ToTable();


                    DataTable dtnew = new DataTable();
                    dtnew.Columns.Add("SR");
                    dtnew.Columns.Add("ProjectID");
                    dtnew.Columns.Add("ProjectName");
                    dtnew.Columns.Add("EmployeeID");
                    dtnew.Columns.Add("EmployeeName");
                    dtnew.Columns.Add("DepartmentID");
                    dtnew.Columns.Add("Department Name");
                    dtnew.Columns.Add("DesiginationID");
                    dtnew.Columns.Add("Desigination Name");

                    DataTable dtdis = distinctValues.Select("ST='Allowances'").CopyToDataTable();
                    for (int i = 0; i < dtdis.Rows.Count; i++)
                    {
                        dtnew.Columns.Add(dtdis.Rows[i][0].ToString());
                    }
                    dtnew.Columns.Add("GrossSalary");
                    DataTable dtdis2 = distinctValues.Select("ST='Deduction'").CopyToDataTable();
                    for (int i = 0; i < dtdis2.Rows.Count; i++)
                    {
                        dtnew.Columns.Add(dtdis2.Rows[i][0].ToString());
                    }

                    DataView view1 = new DataView(dt);
                    DataTable distinctValuesEmployees = view1.ToTable(true, "EmployeeID", "employeeName");
                    for (int i = 0; i < distinctValuesEmployees.Rows.Count; i++)
                    {

                        DataTable dtselect = dt.Select(" EmployeeID =" + distinctValuesEmployees.Rows[i][0].ToString()).CopyToDataTable();
                        dtnew.Rows.Add(i + 1, dtselect.Rows[0]["ProjectID"].ToString(), dtselect.Rows[0]["projectName"].ToString(), dtselect.Rows[0]["EmployeeID"].ToString(), dtselect.Rows[0]["employeeName"].ToString(), dtselect.Rows[0]["departmentid"].ToString(), dtselect.Rows[0]["DepartmentName"].ToString(), dtselect.Rows[0]["desiginationID"].ToString(), dtselect.Rows[0]["DesignationTitle"].ToString());
                        for (int x = 0; x < distinctValues.Rows.Count; x++)
                        {
                            for (int z = 0; z < dtselect.Rows.Count; z++)
                            {
                                if (distinctValues.Rows[x][0].ToString() == dtselect.Rows[z]["ShortName"].ToString())
                                {
                                    if (distinctValues.Rows[x][0].ToString() == "Loan")
                                    {
                                        if (LoanRemiangCHeck.FirstOrDefault(q => q.EmployeeId == Convert.ToInt32(dtselect.Rows[0]["EmployeeID"])).remaningAmount > 0)
                                        {
                                            dtnew.Rows[dtnew.Rows.Count - 1][distinctValues.Rows[x][0].ToString()] = dtselect.Rows[z]["AllowanceAmount"].ToString();

                                        }
                                    }
                                    else
                                    {

                                        dtnew.Rows[dtnew.Rows.Count - 1][distinctValues.Rows[x][0].ToString()] = dtselect.Rows[z]["AllowanceAmount"].ToString();
                                    }
                                 }
                            }

                        }
                    }

                    foreach (var item in LoanRemiangCHeck)
                    {

                    }


                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        for (int x = 0; x < dtnew.Columns.Count; x++)
                        {
                            if (dtnew.Rows[i][x].ToString() == "")
                            {
                                dtnew.Rows[i][x] = "0.00";
                            }
                        }
                    }

                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        double gross = 0;
                        for (int x = 0; x < dtnew.Columns.Count; x++)
                        {
                            if (x > 8 && Convert.ToDouble(dtnew.Rows[i][x].ToString()) > 0)
                            {
                                gross += Convert.ToDouble(dtnew.Rows[i][x].ToString());
                            }
                        }

                        dtnew.Rows[i]["GrossSalary"] = gross;
                    }
                    a.dtforsalary = dtnew;
                }
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                a.month = Convert.ToInt32( IDo[0]);
                a.Year = Convert.ToInt32(IDo[1]);
                a.SalaryGenerationDate = newdate;
                a.SalaryID = 0;
               
                return View("create", a);

            }
            catch (Exception ex)
            {
                return View("create");
            }
        }
        [UserRightFilters]
        public ActionResult create(tblSalaryGeneration a)
        {
            try
            {
                a.IsNew = Convert.ToBoolean(TempData["IsNew"]);
                a.Isedit = Convert.ToBoolean(TempData["IsEdit"]);
                a.Isdelete = Convert.ToBoolean(TempData["IsDelete"]);
                a.IsPrint = Convert.ToBoolean(TempData["IsPrint"]);
                a.SalaryGenerationDate = DateTime.Now;
                
                return View(a);
            }
            catch (Exception ex)
            {

                return View(a);
            }

        }
        public ActionResult Save(tblSalaryGeneration model)
        {
            try
            {
                int check = 0;

                if (model.SalaryID > 0)
                {
                    model.ModifiedDate = DateTime.Now;
                    model.ModifiedID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.ModifiedIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    //  check = check = model.UpdateData(model);
                }
                else
                {
                    // Session["listMonthlysalary"] = a.listmontlySalary;
                    model.listmontlySalary = TempData["listmontlySalaryof"] as List<sp_getMonthSalary_Result>;
                    model.EntryDate = DateTime.Now;
                    model.SalaryGenerationDate = new DateTime(model.Year, model.month, 1);

                    model.EntryID = new SAGERPNEW2018.Models.SystemLogin().GetUser().Userid;
                    model.EntryIP = new SAGERPNEW2018.Models.SystemLogin().GetUser().system;
                    model.ProjectID = Convert.ToInt32("0" + new SAGERPNEW2018.Models.SystemLogin().GetUser().ProjectIDs);
                    check = model.saveData(model);
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

        public ActionResult GetDDListData(string salaryDate)
        {
            tblSalaryGeneration a = new tblSalaryGeneration();
            var datatable = a.getAllSalarydata(4, 2020);
            DataTable dt = ToDataTable( a.getAllSalarydata(4, 2020));

            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "ShortName");

            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("SR");
            dtnew.Columns.Add("ProjectID");
            dtnew.Columns.Add("ProjectName");
            dtnew.Columns.Add("EmployeeID");
            dtnew.Columns.Add("EmployeeName");
            dtnew.Columns.Add("DepartmentID");
            dtnew.Columns.Add("DepartmntName");
            dtnew.Columns.Add("DesiginationID");
            dtnew.Columns.Add("DesiginationName");
            dtnew.Columns.Add("GrossSalary");
            for (int i = 0; i < distinctValues.Rows.Count; i++)
            {
                dtnew.Columns.Add(distinctValues.Rows[i][0].ToString());
            }
            DataView view1 = new DataView(dt);
            DataTable distinctValuesEmployees = view1.ToTable(true, "EmployeeID", "employeeName");
            for (int i = 0; i < distinctValuesEmployees.Rows.Count; i++)
            {
                
                DataTable dtselect = dt.Select(" EmployeeID ="+ distinctValuesEmployees.Rows[i][0].ToString()).CopyToDataTable();
                dtnew.Rows.Add(i + 1,  dtselect.Rows[0]["ProjectID"].ToString(), dtselect.Rows[0]["projectName"].ToString(), dtselect.Rows[0]["EmployeeID"].ToString(), dtselect.Rows[0]["employeeName"].ToString(), dtselect.Rows[0]["departmentid"].ToString(), dtselect.Rows[0]["DepartmentName"].ToString(), dtselect.Rows[0]["desiginationID"].ToString(), dtselect.Rows[0]["DesignationTitle"].ToString(), dtselect.Rows[0]["GrossSalary"].ToString());
                for (int x = 0; x < distinctValues.Rows.Count; x++)
                {
                    for (int z = 0; z < dtselect.Rows.Count; z++)
                    {
                        if (distinctValues.Rows[x][0].ToString() == dtselect.Rows[z]["ShortName"].ToString())
                        {
                            dtnew.Rows[dtnew.Rows.Count-1][distinctValues.Rows[x][0].ToString()] = dtselect.Rows[z]["AllowanceAmount"].ToString();
                        }
                    }
                   
                }
            }

            string JsonResult = "";
            JsonResult = JsonConvert.SerializeObject(dtnew);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}