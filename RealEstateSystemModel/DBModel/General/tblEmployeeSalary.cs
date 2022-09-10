using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
   public partial class tblEmployeeSalary
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }
        public int DepartmentID { get; set; }



        public int addata(tblEmployeeSalary obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.tblEmployeeSalaries.Add(obj);
                    context.SaveChanges();
                    return obj.SalaryID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        public IEnumerable<SelectListItem> loadEmployee(int? departmentID,int? projectiD)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tblEmployees.Where(x => x.inactive == false && x.DepartmentID==departmentID &&x.ProjectID==projectiD).Select(x => new SelectListItem { Text = x.EmployeeName +" - "+ x.EmployeeNo, Value = x.EmployeeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int UpdateData(tblEmployeeSalary obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEmployeeSalaries.SingleOrDefault(x => x.SalaryID == obj.SalaryID);
                    if (result != null)
                    {
                        result.GrossSalary = obj.GrossSalary;
                        result.AccountNo = obj.AccountNo;
                        result.BankID = obj.BankID;
                        result.AccountTitle = result.AccountTitle;
                        result.AccountNo = result.AccountNo;
                        result.Inactive = obj.Inactive;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.SalaryID;
                    }
                    return result.SalaryID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public bool DeleteData(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEmployeeSalaries.SingleOrDefault(x => x.SalaryID == id);
                    if (result != null)
                    {
                        context.tblEmployeeSalaries.Remove(result);
                        context.SaveChanges();


                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public tblEmployeeSalary getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEmployeeSalaries.SingleOrDefault(x => x.SalaryID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<tblEmployeeSalary> getAllSalarydata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEmployeeSalaries.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<sp_GetAllemployeeSalaries_Result> getAllSalarydatabyProc(string where)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_GetAllemployeeSalaries(where).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<tblEmployeeSalary> checkDuplicate(int id, int title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblEmployeeSalaries.Where(x => x.EmployeeID == title   && x.SalaryID != id).ToList();

                    }
                    else
                    {
                        return context.tblEmployeeSalaries.Where(x => x.EmployeeID == title ).ToList();


                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
