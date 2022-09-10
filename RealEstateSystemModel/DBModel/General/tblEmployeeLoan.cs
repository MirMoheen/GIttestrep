using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
  public partial  class tblEmployeeLoan
    {


        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }

        public int fromDepartmentID { get; set; }
        public int fromDesignation { get; set; }
        public decimal GrossSalry { get; set; }
        public string Total { get; set; }

        



        public IEnumerable<decimal> Amount { get; set; }
        public IEnumerable<string> LoanDetailDate { get; set; }

       

        public IEnumerable<tblEmployeeLoanCashDetail> detailistDetail { get; set; }



        public IEnumerable<SelectListItem> loadLoanType()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                 
                    listobj.AddRange(context.tblTypeLoans.Where(x => x.Inactive == false ).Select(x => new SelectListItem { Text = x.Title , Value = x.LoanTypeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> LoadPaymentMode()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                    listobj.Add(new SelectListItem { Text = "Salary", Value = "1" });
                    listobj.Add(new SelectListItem { Text = "Cash", Value = "2" });
            


                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

      

        public IEnumerable<SelectListItem> loadEmployee(int projectID  )
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tblEmployees.Where(x => x.inactive == false && x.ProjectID== projectID).Select(x => new SelectListItem { Text = x.EmployeeName + " - " + x.EmployeeNo, Value = x.EmployeeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public EmployeePlacement loadEmployeeData(int employeeid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.EmployeePlacements.FirstOrDefault(x => x.EmployeeID == employeeid);
                  

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<tblEmployeeLoanCashDetail> GetLoanDetail(int loanID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.tblEmployeeLoanCashDetails.Where(x => x.LoanID == loanID).ToList();


                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int addata(tblEmployeeLoan obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.tblEmployeeLoans.Add(obj);
                    context.SaveChanges();
                    return obj.LoanID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(tblEmployeeLoan obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEmployeeLoans.SingleOrDefault(x => x.LoanID == obj.LoanID);
                    if (result != null)
                    {
                        result.EmployeeId = obj.EmployeeId;

                        result.Remark = obj.Remark;
                        result.RequestedAmount = obj.RequestedAmount;
                        result.PaymentStartDate = obj.PaymentStartDate;
              
                        result.LoanDate = obj.LoanDate;
                        result.FixnPercentage = obj.FixnPercentage;

                        result.Inactive = obj.Inactive;
                        result.NoOfInstallments = obj.NoOfInstallments;
                        result.PerInstallment = obj.PerInstallment;
                        result.PaymentAmount = obj.PaymentAmount;


                        result.LoanType = obj.LoanType;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.LoanID;
                    }
                    return result.LoanID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public bool insertPayment(tblEmployeeLoan obj)
        {

            try
            {

                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {


                            var result = context.tblEmployeeLoans.SingleOrDefault(x => x.LoanID == obj.LoanID);
                            if (result!=null)
                            {



                                List<tblEmployeeLoanCashDetail> resultDetail = context.tblEmployeeLoanCashDetails.Where(x=>x.LoanID== obj.LoanID).ToList();
                                if (resultDetail != null)
                                {
                                    context.tblEmployeeLoanCashDetails.RemoveRange(context.tblEmployeeLoanCashDetails.Where(q => q.LoanID == obj.LoanID));

                                }


                                int a = 0;
                                foreach (var item in obj.Amount)
                                {
                                    tblEmployeeLoanCashDetail objdss = new tblEmployeeLoanCashDetail();
                                    objdss.Amount = item;
                                    objdss.LoanID = result.LoanID;
                                    objdss.EmployeeId = result.EmployeeId;
                                    objdss.EntryID = obj.EntryID;
                                    objdss.EntryIP = obj.EntryIP;
                                    objdss.LoanDetailDate = obj.LoanDetailDate.ToArray()[a];
                                    a++;
                                    context.tblEmployeeLoanCashDetails.Add(objdss);
                                    context.SaveChanges();

                                }





                                dbContextTransaction.Commit();

                            }


                          
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }
                    return true;
                }


            }
            catch (Exception)
            {

                return false;
            }

           
        }


        public bool DeleteData(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEmployeeLoans.SingleOrDefault(x => x.LoanID == id);
                    if (result != null)
                    {
                        context.tblEmployeeLoans.Remove(result);
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


        public tblEmployeeLoan getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEmployeeLoans.SingleOrDefault(x => x.LoanID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public DataTable getAllloandata(string where)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_getEmployeeLoandata", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@where", where);
                        

                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


      

        public List<tblEmployeeLoan> checkDuplicate(int id, int title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblEmployeeLoans.Where(x => x.EmployeeId == title && x.LoanID != id).ToList();

                    }
                    else
                    {
                        return context.tblEmployeeLoans.Where(x => x.EmployeeId == title).ToList();


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
