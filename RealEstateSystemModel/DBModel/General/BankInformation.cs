using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
  public partial  class BankInformation
    {



        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }


        public int addata(BankInformation obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.BankInformations.Add(obj);
                    context.SaveChanges();
                    return obj.BankID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(BankInformation obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.BankInformations.SingleOrDefault(x => x.BankID == obj.BankID);
                    if (result != null)
                    {
                        result.BankName = obj.BankName;
                        result.inactive = obj.inactive;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.BankID;
                    }
                    return result.BankID;
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
                    var result = context.BankInformations.SingleOrDefault(x => x.BankID == id);
                    if (result != null)
                    {
                        context.BankInformations.Remove(result);
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


        public BankInformation getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.BankInformations.SingleOrDefault(x => x.BankID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<BankInformation> getAllbankdata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.BankInformations.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<BankInformation> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.BankInformations.Where(x => x.BankName == title && x.BankID != id).ToList();

                    }
                    else
                    {
                        return context.BankInformations.Where(x => x.BankName == title).ToList();


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
