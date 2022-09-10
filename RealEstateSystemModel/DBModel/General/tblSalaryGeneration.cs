using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
   public partial class tblSalaryGeneration
    {
        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public string lbmsg { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }
        public int month { get; set; }
        public int Year { get; set; }
        public DataTable dtforsalary { get; set; }
        public IEnumerable<sp_getMonthSalary_Result> listmontlySalary { get; set; }

        public IEnumerable<SelectListItem> loadmonth()
        {
            List<SelectListItem> list = new List<SelectListItem>();
             
                list.Add(new SelectListItem{Text = "Jan", Value = "1"});
            list.Add(new SelectListItem { Text = "Feb", Value = "2" });
            list.Add(new SelectListItem { Text = "Mar", Value = "3" });
            list.Add(new SelectListItem { Text = "Apr", Value = "4" });
            list.Add(new SelectListItem { Text = "May", Value = "5" });
            list.Add(new SelectListItem { Text = "Jun", Value = "6" });
            list.Add(new SelectListItem { Text = "July", Value = "7" });
            list.Add(new SelectListItem { Text = "Aug", Value = "8" });
            list.Add(new SelectListItem { Text = "Sep", Value = "9" });
            list.Add(new SelectListItem { Text = "Oct", Value = "10" });
            list.Add(new SelectListItem { Text = "Nov", Value = "11" });
            list.Add(new SelectListItem { Text = "Dec", Value = "12" });
            return list;
        }

        public List<sp_getSalaryExist_Result> getAllCurrentData(int month,int year)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getSalaryExist(month,year).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_RemaingLoancheck_Result> getAllRemaingLoanAmount(int projectid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_RemaingLoancheck(projectid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> loadyear()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "2020", Value = "2020" });
            list.Add(new SelectListItem { Text = "2021", Value = "2021" });
            list.Add(new SelectListItem { Text = "2022", Value = "2022" });
           
            return list;
        }
        public List<tblSalaryGeneration> getAllCompanydata(int Pid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblSalaryGenerations.Where(x => x.ProjectID == Pid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public int saveData(tblSalaryGeneration obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    context.tblSalaryGenerations.Add(obj);
                    context.SaveChanges();

                    int a = 0;

                    foreach (var item in obj.listmontlySalary)
                    {
                        tblSalaryGenerationDetail objdss = new tblSalaryGenerationDetail();
                        objdss.SalaryID = obj.SalaryID;
                        objdss.DepartmentID = Convert.ToInt32(item.departmentid);
                        objdss.EmployeeID = Convert.ToInt32(item.EmployeeID);
                        objdss.DesiginationID = Convert.ToInt32(item.desiginationID);
                        objdss.Amount = Convert.ToDecimal(item.AllowanceAmount);
                        objdss.AllowanceDeductionID = Convert.ToInt32(item.AllowanceID);

                        a++;
                        context.tblSalaryGenerationDetails.Add(objdss);
                        context.SaveChanges();

                    }

                    return obj.SalaryID;




                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public List<sp_getMonthSalary_Result> getAllSalarydata(int month,int year)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getMonthSalary(month,year).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
