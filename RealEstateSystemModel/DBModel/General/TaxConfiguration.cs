using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class TaxConfiguration
    {
        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }

        public IEnumerable<string> DetailID { get; set; }
        public IEnumerable<string> AnualSalaryFrom { get; set; }
        public IEnumerable<string> AnualSalaryTo { get; set; }
        public IEnumerable<string> FixedAmount { get; set; }
        public IEnumerable<string> Percentage { get; set; }
        //public int  DetailID { get; set; }
        //public decimal AnualSalaryFrom { get; set; }
        //public decimal AnualSalaryTo { get; set; }
        //public decimal FixedAmount { get; set; }
        //public decimal Percentage { get; set; }
        public List<sp_getAllTaxConfig_Result> getAlltaxConfigdata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllTaxConfig().ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public int UpdateData(TaxConfiguration obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.TaxConfigurations.SingleOrDefault(x => x.TaxConfigurationID ==1);
                    if (result != null)
                    {
                        result.AllowanceDeductionID = obj.AllowanceDeductionID;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        int a = 0;
                        context.TaxConfigurationDetails.RemoveRange(context.TaxConfigurationDetails.Where(x => x.TaxConfiguration == 1));
                        context.SaveChanges();
                        foreach (var item in obj.DetailID)
                        {
                            TaxConfigurationDetail objdss = new TaxConfigurationDetail();
                            objdss.TaxConfiguration = 1;
                            objdss.AnualSalaryFrom = Convert.ToInt32(obj.AnualSalaryFrom.ToArray()[a]);
                            objdss.AnualSalaryTo = Convert.ToInt32(obj.AnualSalaryTo.ToArray()[a]);
                            objdss.FixedAmount = Convert.ToInt32(obj.FixedAmount.ToArray()[a]);
                            objdss.Percentage = Convert.ToDecimal(obj.Percentage.ToArray()[a]);
                            a++;
                            context.TaxConfigurationDetails.Add(objdss);
                            context.SaveChanges();

                        }

                        return result.TaxConfigurationID;


                    }
                    return result.TaxConfigurationID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public List<TaxConfiguration> getAlldata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.TaxConfigurations.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> FillDeduction()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var list = context.AllowancesDeductions.Where(x => x.Inactive == false && x.IsDeduction == true).ToList();
                    List<SelectListItem> listobj = new List<SelectListItem>();

                    foreach (var item in list)
                    {
                        listobj.Add(new SelectListItem { Text = item.AllowanceDeductionTitle, Value = item.AllowanceDeductionID.ToString() });
                    }


                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
