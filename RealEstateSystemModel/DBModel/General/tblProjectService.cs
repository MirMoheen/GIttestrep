using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class tblProjectService
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }


        public List<sp_allServices_Result> getalldata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_allServices().ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public long AddServiceData(tblProjectService obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            context.tblProjectServices.Add(obj);
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return 1;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public tblProjectService getalldataedit(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblProjectServices.FirstOrDefault(x => x.Service_ID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public long UpdateService(tblProjectService obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = context.tblProjectServices.FirstOrDefault(x => x.Service_ID == obj.Service_ID);
                            if (result != null)
                            {

                                result.Modifed_Date = obj.Modifed_Date;
                                result.Modifed_ID = obj.Modifed_ID;
                                result.Modified_IP = obj.Modified_IP;
                                result.ServiceDetail = obj.ServiceDetail;
                                result.Status = obj.Status;
                                context.SaveChanges();
                            }


                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return 1;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public List<tblProjectService> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblProjectServices.Where(x => x.ServiceDetail == title && x.Service_ID != id).ToList();

                    }
                    else
                    {
                        return context.tblProjectServices.Where(x => x.ServiceDetail == title).ToList();


                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> GetRelatedTo()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "Software", Value = "Software" });
                    listobj.Add(new SelectListItem { Text = "Support", Value = "Support" });


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
