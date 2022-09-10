using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
  public partial  class EmployeePlacement
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }



        // detail Allowance
        public IEnumerable<int> AllowanceID { get; set; }
        public IEnumerable<decimal> AllowanceRate { get; set; }

        public IEnumerable<decimal> AllowanceAmount { get; set; }

        public IEnumerable<int> IsCheck { get; set; }



        // detail Deduction
        public IEnumerable<int> DeductionID { get; set; }
        public IEnumerable<decimal> DeductionAmount { get; set; }

        public IEnumerable<int> deductionIsCheck { get; set; }




        public int addata(EmployeePlacement obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            //  obj.CompID = new Login().GetUser().CompID;
                            context.EmployeePlacements.Add(obj);
                            context.SaveChanges();
                            

                            int a = 0;
                            foreach (var item in obj.IsCheck)
                            {
                                EmployeePlacementAllowancesDetail objdss = new EmployeePlacementAllowancesDetail();
                                objdss.PlacementID = obj.PlacementID;
                                objdss.EmployeeID = obj.EmployeeID;
                                objdss.AllowanceID = item;
                                objdss.AllowanceRate = obj.AllowanceRate.ToArray()[a];
                                objdss.AllowanceAmount = obj.AllowanceAmount.ToArray()[a];
                                objdss.IsCheck = true;
                              a++;
                                context.EmployeePlacementAllowancesDetails.Add(objdss);
                                context.SaveChanges();

                            }


                            int b = 0;
                           var AllowancesDeductionsList = context.AllowancesDeductions.ToList();
                            foreach (var item in obj.deductionIsCheck)
                            {
                                EmployeePlacementDeductionDetail objdoc = new EmployeePlacementDeductionDetail();

                                objdoc.PlacementID = obj.PlacementID;
                                objdoc.EmployeeID = obj.EmployeeID;
                                objdoc.DeductionID = item;

                                objdoc.DeductionAmount = AllowancesDeductionsList.FirstOrDefault(x => x.AllowanceDeductionID == item).Rate==0? obj.DeductionAmount.ToArray()[b] : AllowancesDeductionsList.FirstOrDefault(x => x.AllowanceDeductionID == item).Rate;
                                objdoc.IsCheck = true;

                                b++;
                                context.EmployeePlacementDeductionDetails.Add(objdoc);
                                context.SaveChanges();

                            }



                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return obj.PlacementID;
                }



            
            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        public IEnumerable<SelectListItem> loadEmployee(int? departmentID, int? projectiD)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                    var employeesIDs = context.EmployeePlacements.Where(x => x.ProjectID == projectiD).Select(x => x.EmployeeID).ToList();

                    List<long> longs = employeesIDs.Select(i => (long)i).ToList();
                    listobj.AddRange(context.tblEmployees.Where(x => x.inactive == false && x.DepartmentID == departmentID && x.ProjectID == projectiD  && !longs.Contains(x.EmployeeID)).Select(x => new SelectListItem { Text = x.EmployeeName + " - " + x.EmployeeNo, Value = x.EmployeeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public int UpdateData(EmployeePlacement obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {


                            var result = context.EmployeePlacements.SingleOrDefault(x => x.PlacementID == obj.PlacementID);
                            if (result != null)
                            {
                                result.GrossSalary = obj.GrossSalary;
                                result.BankID = obj.BankID;
                                result.DesiginationID = obj.DesiginationID;
                                result.BankAccountTitle = result.BankAccountTitle;
                                result.BankAccountNo = result.BankAccountNo;
                                result.Inactive = obj.Inactive;
                                result.ModifiedDate = obj.ModifiedDate;
                                result.ModifiedID = obj.ModifiedID;
                             //   result.ProbationCompletedDtae = obj.ProbationCompletedDtae;
                                result.TaxRebait = obj.TaxRebait;
                            ///    result.isConfirm = obj.isConfirm;
                                result.PlacemantDate = obj.PlacemantDate;
                                result.ModifiedIP = obj.ModifiedIP;
                                context.SaveChanges();
                              
                            }


                            context.SP_InsertLogEmployeePlacementAllowancesDetail(obj.PlacementID);


                            int a = 0;
                            foreach (var item in obj.IsCheck)
                            {
                                EmployeePlacementAllowancesDetail objdss = new EmployeePlacementAllowancesDetail();
                                objdss.PlacementID = obj.PlacementID;
                                objdss.EmployeeID = obj.EmployeeID;
                                objdss.AllowanceID = item;
                                objdss.AllowanceRate = obj.AllowanceRate.ToArray()[a];
                                objdss.AllowanceAmount = obj.AllowanceAmount.ToArray()[a];
                                objdss.IsCheck = true;
                                a++;
                                context.EmployeePlacementAllowancesDetails.Add(objdss);
                                context.SaveChanges();

                            }

                            context.SP_InsertLogEmployeePlacementDeductionDetail(obj.PlacementID);



                            //insert image path 
                            int b = 0;
                            var AllowancesDeductionsList = context.AllowancesDeductions.ToList();

                            foreach (var item in obj.deductionIsCheck)
                            {
                                EmployeePlacementDeductionDetail objdoc = new EmployeePlacementDeductionDetail();

                                objdoc.PlacementID = obj.PlacementID;
                                objdoc.EmployeeID = obj.EmployeeID;
                                objdoc.DeductionID = item;

                                objdoc.DeductionAmount = AllowancesDeductionsList.FirstOrDefault(x => x.AllowanceDeductionID == item).Rate == 0 ? obj.DeductionAmount.ToArray()[b] : AllowancesDeductionsList.FirstOrDefault(x => x.AllowanceDeductionID == item).Rate;

                                objdoc.IsCheck = true;

                                b++;
                                context.EmployeePlacementDeductionDetails.Add(objdoc);
                                context.SaveChanges();

                            }






                            dbContextTransaction.Commit();

                            return result.PlacementID;
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



        public bool DeleteData(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.EmployeePlacements.SingleOrDefault(x => x.PlacementID == id);
                    if (result != null)
                    {
                        context.EmployeePlacements.Remove(result);
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


        public EmployeePlacement getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.EmployeePlacements.SingleOrDefault(x => x.PlacementID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getDeductionEmployeePlacement_Result> getDeductionPlacementByID(int id,int projectid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getDeductionEmployeePlacement(id, projectid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<sp_getAllowancesEmployeePlacement_Result> getAllowancelacementByID(int id,int projectId)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllowancesEmployeePlacement(id,projectId).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<AllowancesDeduction> getAllowancesDeduction( int projectID, bool  isdeduction)
        {
            try
            {
                AllowancesDeduction objAD = new AllowancesDeduction();
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.AllowancesDeductions.Where(x=>x.Inactive == false&& x.IsDeduction== isdeduction && x.ProjectID== projectID).ToList();
                //    objAD.Rate = result.Sum(x => x.Rate);
                //    objAD.AllowanceDeductionTitle = "Total";
                //    objAD.OrderNo = 1000;

                //    result.Add(objAD);
                //return    result.OrderBy(x=>x.OrderNo).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public decimal GetTaxCalculaton(int deductioniD, decimal salary)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    decimal tax = 0;
                    var resultTaxConfigurations=  context.TaxConfigurations.Where(x=>x.AllowanceDeductionID== deductioniD).FirstOrDefault();
                    if (resultTaxConfigurations!=null)
                    {
                        var resultTaxConfigurationDetail = context.TaxConfigurationDetails.Where(x => x.TaxConfiguration == resultTaxConfigurations.TaxConfigurationID && salary >= x.AnualSalaryFrom/12 && salary <= x.AnualSalaryTo/12).FirstOrDefault();

                        if (resultTaxConfigurationDetail!=null)
                        {
                            var salaryslapTotal = (salary * 12) - (resultTaxConfigurationDetail.AnualSalaryFrom + resultTaxConfigurationDetail.FixedAmount);
                            var yearlyTax = ((salaryslapTotal / 100) * resultTaxConfigurationDetail.Percentage);
                            var MonthlyTax = yearlyTax / 12;
                            tax=Math.Round(Convert.ToDecimal(MonthlyTax), 0);

                        }

                    }

                    return tax;

                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public List<EmployeePlacement> getAllSalarydata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.EmployeePlacements.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<sp_getemployeePlacement_Result> sp_employeePlacement(string where)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getemployeePlacement(where).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<EmployeePlacement> checkDuplicate(int id, int title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.EmployeePlacements.Where(x => x.EmployeeID == title && x.PlacementID != id).ToList();

                    }
                    else
                    {
                        return context.EmployeePlacements.Where(x => x.EmployeeID == title).ToList();


                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public bool UpdateAllowancePlacement( int projectID)
        {

            try
            {


                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                           var NewAllowanceresult = getAllowancesDeduction(projectID, false);
                           var CheckHundred= NewAllowanceresult.Sum(x => x.Rate);
                            if (CheckHundred!=100)
                            {
                                return false;
                            }


                            if (NewAllowanceresult != null)
                            {

                                var EmployeePlacementsResult = context.EmployeePlacements.Where(x => x.ProjectID == projectID).ToList();
                                var employeeListID = EmployeePlacementsResult.Select(x => x.EmployeeID).ToList();
                                var EmployeePlacementAllowancesDetails =  context.EmployeePlacementAllowancesDetails.Where(s => employeeListID.Contains(s.EmployeeID)).ToList();
                                context.sp_deleteEmployeePlacementAllowancesDetailByProjectID(projectID);
                                List<EmployeePlacementAllowancesDetail> listEmployeeAllowanceDetail = new List<EmployeePlacementAllowancesDetail>();
                                foreach (var item in EmployeePlacementsResult)
                                {

                                     var EmployeePlacementAllowancesDetailsResult = EmployeePlacementAllowancesDetails.Where(x => x.EmployeeID == item.EmployeeID).ToList();
                                    if (EmployeePlacementAllowancesDetailsResult!=null)
                                    {
                                        foreach (var itemDetail in NewAllowanceresult)
                                        {

                                         
                                        
                                            EmployeePlacementAllowancesDetail objEmployeeAllowanceDetail = new EmployeePlacementAllowancesDetail();
                                            objEmployeeAllowanceDetail.AllowanceID = itemDetail.AllowanceDeductionID;
                                            objEmployeeAllowanceDetail.AllowanceRate = itemDetail.Rate;
                                            var CreateAmount = (item.GrossSalary / 100) * itemDetail.Rate;
                                            objEmployeeAllowanceDetail.AllowanceAmount = CreateAmount;
                                            objEmployeeAllowanceDetail.PlacementID = item.PlacementID;
                                            objEmployeeAllowanceDetail.EmployeeID = item.EmployeeID;
                                            objEmployeeAllowanceDetail.IsCheck = true;
                                            listEmployeeAllowanceDetail.Add(objEmployeeAllowanceDetail);

                                        }


                                    }

                                }
                                context.EmployeePlacementAllowancesDetails.AddRange(listEmployeeAllowanceDetail);
                                context.SaveChanges();


                                dbContextTransaction.Commit();
                            }


                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }



    }
}
