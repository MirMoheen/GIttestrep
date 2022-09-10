using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
  public partial class tblProject
    {
        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }


        public string genearteprojectCode()
        {

            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var projectid = context.tblProjects.Max(x => x.ProjectID) + 1;
                    return projectid.ToString("D2");
                }


            }
            catch (Exception ex)
            {

                return null;
            }


        }


        public int addata(tblProject obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.tblProjects.Add(obj);
                    context.SaveChanges();
                    return obj.ProjectID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(tblProject obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblProjects.SingleOrDefault(x => x.ProjectID == obj.ProjectID);
                    if (result != null)
                    {
                        result.ProjectName = obj.ProjectName;
                        result.OrderNo = obj.OrderNo;
                        //result.ProjectCode = obj.ProjectCode;
                        result.Inactive = obj.Inactive;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.ProjectID;
                    }
                    return result.ProjectID;
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
                    var result = context.tblProjects.SingleOrDefault(x => x.ProjectID == id);
                    if (result != null)
                    {
                        context.tblProjects.Remove(result);
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


        public tblProject getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblProjects.SingleOrDefault(x => x.ProjectID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<tblProject> getAllCompanydata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblProjects.OrderBy(x => x.OrderNo).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<tblProject> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblProjects.Where(x => x.ProjectName == title && x.ProjectID != id).ToList();

                    }
                    else
                    {
                        return context.tblProjects.Where(x => x.ProjectName == title).ToList();


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
