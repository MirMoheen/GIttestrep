using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HRandPayrollSystemModel.DBModel
{
   public partial class tblCompany
    {


        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }


        public int addata(tblCompany obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.tblCompanies.Add(obj);
                    context.SaveChanges();
                    return obj.id;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(tblCompany obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblCompanies.SingleOrDefault(x => x.id == obj.id);
                    if (result != null)
                    {
                        result.CompanyName = obj.CompanyName;
                        result.CompanyLogo = obj.CompanyLogo;
                        result.CompanyDesc = obj.CompanyDesc;
                        result.CompanyAddress = obj.CompanyAddress;
                        result.PostCode = obj.PostCode;
                        result.Phone = obj.Phone;
                        result.Fax = obj.Fax;
                        result.ModifyBy = obj.ModifyBy;
                        result.ModifyDate = obj.ModifyDate;
                      


                        context.SaveChanges();
                        return result.id;
                    }
                    return result.id;
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
                    var result = context.tblCompanies.SingleOrDefault(x => x.id == id);
                    if (result != null)
                    {
                        context.tblCompanies.Remove(result);
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


        public tblCompany getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblCompanies.SingleOrDefault(x => x.id == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<tblCompany> getAllCompanydata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblCompanies.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<tblCompany> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblCompanies.Where(x => x.CompanyName == title && x.id != id).ToList();

                    }
                    else
                    {
                        return context.tblCompanies.Where(x => x.CompanyName == title).ToList();


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
