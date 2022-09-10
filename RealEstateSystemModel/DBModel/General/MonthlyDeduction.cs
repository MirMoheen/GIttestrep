using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class MonthlyDeduction
    {
        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }
        public IEnumerable<MonthlyDeductionDetail> MonthlyDeductionDetaillist { get; set; }
        public string DepartmentTableComboJson { get; set; }
        public string DeductionTableComboJson { get; set; }
        public IEnumerable<string> Department { get; set; }
        public IEnumerable<string> Employee { get; set; }
        public IEnumerable<string> Deduction { get; set; }
        public IEnumerable<string> Amount { get; set; }


        public List<MonthlyDeduction> getAllCompanydata(int Pid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.MonthlyDeductions.Where(x => x.ProjectID == Pid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<MonthlyDeductionDetail> getDetailData(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.MonthlyDeductionDetails.Where(x => x.MonthlyDeductionID == id).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public MonthlyDeduction getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.MonthlyDeductions.SingleOrDefault(x => x.MonthlyDeductionID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<tblDepartment> LoadAllDepartment(int Pid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblDepartments.Where(x => x.ProjectID == Pid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<AllowancesDeduction> LoadAllDeduction(int Pid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.AllowancesDeductions.Where(x => x.ProjectID == Pid && x.IsDeduction == true).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public int saveData(MonthlyDeduction obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            context.MonthlyDeductions.Add(obj);
                            context.SaveChanges();

                            int a = 0;

                            foreach (var item in obj.Department)
                            {
                                MonthlyDeductionDetail objdss = new MonthlyDeductionDetail();
                                objdss.MonthlyDeductionID = obj.MonthlyDeductionID;
                                objdss.DepartmentID = Convert.ToInt32(obj.Department.ToArray()[a]);
                                objdss.EmployeeID = Convert.ToInt32(obj.Employee.ToArray()[a]);
                                objdss.AllowanceDeductionID = Convert.ToInt32(obj.Deduction.ToArray()[a]);
                                objdss.Amount = Convert.ToDecimal(obj.Amount.ToArray()[a]);

                                a++;
                                context.MonthlyDeductionDetails.Add(objdss);
                                context.SaveChanges();

                            }
                            dbContextTransaction.Commit();

                            return obj.MonthlyDeductionID;


                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
      

        public int UpdateData(MonthlyDeduction obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {


                            var result = context.MonthlyDeductions.SingleOrDefault(x => x.MonthlyDeductionID == obj.MonthlyDeductionID);
                            if (result != null)
                            {
                                result.Remarks = obj.Remarks;
                                result.Inactive = obj.Inactive;

                                context.SaveChanges();

                            }
                    
                  

                            List<MonthlyDeductionDetail> resultofDetail = context.MonthlyDeductionDetails.Where(x=>x.MonthlyDeductionID == obj.MonthlyDeductionID).ToList();
                            if (resultofDetail != null)
                            {
                                context.MonthlyDeductionDetails.RemoveRange(context.MonthlyDeductionDetails.Where(q => q.MonthlyDeductionID == obj.MonthlyDeductionID));

                            }
                            context.SaveChanges();


                            int a = 0;

                            foreach (var item in obj.Department)
                            {
                                MonthlyDeductionDetail objdss = new MonthlyDeductionDetail();
                                objdss.MonthlyDeductionID = obj.MonthlyDeductionID;
                                objdss.DepartmentID = Convert.ToInt32(obj.Department.ToArray()[a]);
                                objdss.EmployeeID = Convert.ToInt32(obj.Employee.ToArray()[a]);
                                objdss.AllowanceDeductionID = Convert.ToInt32(obj.Deduction.ToArray()[a]);
                                objdss.Amount = Convert.ToDecimal(obj.Amount.ToArray()[a]);

                                a++;
                                context.MonthlyDeductionDetails.Add(objdss);
                                context.SaveChanges();

                            }



                            dbContextTransaction.Commit();

                            return result.MonthlyDeductionID;
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public List<tblEmployee> LoadAllemployees(int Pid, int departmentid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEmployees.Where(x => x.ProjectID == Pid && x.inactive == false && x.DepartmentID == departmentid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
