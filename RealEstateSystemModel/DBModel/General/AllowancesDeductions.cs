using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
   public partial class AllowancesDeduction
    {


        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }


        public int addata(AllowancesDeduction obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.AllowancesDeductions.Add(obj);
                    context.SaveChanges();
                    return obj.AllowanceDeductionID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(AllowancesDeduction obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.AllowancesDeductions.SingleOrDefault(x => x.AllowanceDeductionID == obj.AllowanceDeductionID);
                    if (result != null)
                    {
                        result.AllowanceDeductionTitle = obj.AllowanceDeductionTitle;
                        result.OrderNo = obj.OrderNo;
                        result.Inactive = obj.Inactive;
                        result.ShortName = obj.ShortName;
                        result.IsBasicSalary = obj.IsBasicSalary;
                        result.IsDeduction = obj.IsDeduction;
                        result.Remarks = obj.Remarks;
                        result.ProjectID = obj.ProjectID;
                        result.Rate = obj.Rate;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.AllowanceDeductionID;
                    }
                    return result.AllowanceDeductionID;
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
                    var result = context.AllowancesDeductions.SingleOrDefault(x => x.AllowanceDeductionID == id);
                    if (result != null)
                    {
                        context.AllowancesDeductions.Remove(result);
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


        public AllowancesDeduction getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.AllowancesDeductions.SingleOrDefault(x => x.AllowanceDeductionID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<AllowancesDeduction> getAllCompanydata( int pID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.AllowancesDeductions.Where(x=>x.ProjectID==pID) .ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<AllowancesDeduction> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.AllowancesDeductions.Where(x => x.AllowanceDeductionTitle == title && x.AllowanceDeductionID != id).ToList();

                    }
                    else
                    {
                        return context.AllowancesDeductions.Where(x => x.AllowanceDeductionTitle == title).ToList();


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
