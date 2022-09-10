using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class Employeearrier
    {
        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }
        public IEnumerable<EmployeearrierDetail> MonthlyDeductionDetaillist { get; set; }
        public string DepartmentTableComboJson { get; set; }
        public string DeductionTableComboJson { get; set; }
        public IEnumerable<string> Department { get; set; }
        public IEnumerable<string> Employee { get; set; }
        public IEnumerable<string> Deduction { get; set; }
        public IEnumerable<string> Amount { get; set; }


        public List<Employeearrier> getAllCompanydata(int Pid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.Employeearriers.Where(x => x.ProjectID == Pid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<EmployeearrierDetail> getDetailData(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.EmployeearrierDetails.Where(x => x.EmployeearrierID == id).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Employeearrier getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.Employeearriers.SingleOrDefault(x => x.EmployeearrierID == id);

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
                    return context.AllowancesDeductions.Where(x => x.ProjectID == Pid && x.IsDeduction == false).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<AllowancesDeduction> LoadAllDeductionZero(int Pid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.AllowancesDeductions.Where(x => x.ProjectID == Pid && x.IsDeduction == false && x.Rate==0).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public int saveData(Employeearrier obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            context.Employeearriers.Add(obj);
                            context.SaveChanges();

                            int a = 0;

                            foreach (var item in obj.Department)
                            {
                                EmployeearrierDetail objdss = new EmployeearrierDetail();
                                objdss.EmployeearrierID = obj.EmployeearrierID;
                                objdss.DepartmentID = Convert.ToInt32(obj.Department.ToArray()[a]);
                                objdss.EmployeeID = Convert.ToInt32(obj.Employee.ToArray()[a]);
                                objdss.AllowanceDeductionID = Convert.ToInt32(obj.Deduction.ToArray()[a]);
                                objdss.Amount = Convert.ToDecimal(obj.Amount.ToArray()[a]);

                                a++;
                                context.EmployeearrierDetails.Add(objdss);
                                context.SaveChanges();

                            }
                            dbContextTransaction.Commit();

                            return obj.EmployeearrierID;


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


        public int UpdateData(Employeearrier obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {


                            var result = context.Employeearriers.SingleOrDefault(x => x.EmployeearrierID == obj.EmployeearrierID);
                            if (result != null)
                            {
                                result.Remarks = obj.Remarks;
                                result.Inactive = obj.Inactive;

                                context.SaveChanges();

                            }



                            List<EmployeearrierDetail> resultofDetail = context.EmployeearrierDetails.Where(x => x.EmployeearrierID == obj.EmployeearrierID).ToList();
                            if (resultofDetail != null)
                            {
                                context.EmployeearrierDetails.RemoveRange(context.EmployeearrierDetails.Where(q => q.EmployeearrierID == obj.EmployeearrierID));

                            }
                            context.SaveChanges();


                            int a = 0;

                            foreach (var item in obj.Department)
                            {
                                EmployeearrierDetail objdss = new EmployeearrierDetail();
                                objdss.EmployeearrierID = obj.EmployeearrierID;
                                objdss.DepartmentID = Convert.ToInt32(obj.Department.ToArray()[a]);
                                objdss.EmployeeID = Convert.ToInt32(obj.Employee.ToArray()[a]);
                                objdss.AllowanceDeductionID = Convert.ToInt32(obj.Deduction.ToArray()[a]);
                                objdss.Amount = Convert.ToDecimal(obj.Amount.ToArray()[a]);

                                a++;
                                context.EmployeearrierDetails.Add(objdss);
                                context.SaveChanges();

                            }



                            dbContextTransaction.Commit();

                            return result.EmployeearrierID;
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
