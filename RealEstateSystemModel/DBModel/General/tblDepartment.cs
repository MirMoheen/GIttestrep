using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class tblDepartment
    {
        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }
        
        public IEnumerable<string> SelectType { get; set; }


        public string geneartedepartmentCode( int pid)
        {

            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var DepartmentID =  context.tblDepartments.Where(x => x.ProjectID==pid).Max(x => x.DepartmentCode) ;
                   return  (Convert.ToInt32( DepartmentID)+1).ToString("D2");
                }


            }
            catch (Exception ex)
            {

                return null;
            }


        }



        public int addata(tblDepartment obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    obj.DepartmentName = obj.DepartmentName.ToUpper();
                    context.tblDepartments.Add(obj);
                    context.SaveChanges();
                    return obj.DepartmentID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(tblDepartment obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblDepartments.SingleOrDefault(x => x.DepartmentID == obj.DepartmentID);
                    if (result != null)
                    {
                        result.DepartmentName = obj.DepartmentName.ToUpper();
                        result.OrderNo = obj.OrderNo;
                        result.Inactive = obj.Inactive;
                        result.ProjectID = obj.ProjectID;
                   //     result.DepartmentCode = obj.DepartmentCode;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;
                        result.AllowTypeIDs = obj.AllowTypeIDs;



                        context.SaveChanges();
                        return result.DepartmentID;
                    }
                    return result.DepartmentID;
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
                    var result = context.tblDepartments.SingleOrDefault(x => x.DepartmentID == id);
                    if (result != null)
                    {
                        context.tblDepartments.Remove(result);
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


        public tblDepartment getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblDepartments.SingleOrDefault(x => x.DepartmentID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //public List<sp_getAllDepartments_Result> getAllDepartmentdata()
        //{
        //    try
        //    {
        //        using (var context = new HRandPayrollDBEntities())
        //        {
        //            return context.sp_getAllDepartments().ToList();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }
        //}
        public List<tblDepartment> getAllDepartmentdata(int ProjID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblDepartments.Where(X=> X.ProjectID == ProjID).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> LoadProjects()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var list = context.tblProjects.Where(x => x.Inactive == false).ToList();
                    List<SelectListItem> listobj = new List<SelectListItem>();

                    foreach (var item in list)
                    {
                        listobj.Add(new SelectListItem { Text = item.ProjectName, Value = item.ProjectID.ToString() });
                    }


                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<tblDepartment> checkDuplicate(int id, string title,int pid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblDepartments.Where(x => x.DepartmentName == title && x.DepartmentID != id && x.ProjectID==pid).ToList();

                    }
                    else
                    {
                        return context.tblDepartments.Where(x => x.DepartmentName == title && x.ProjectID == pid).ToList();


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
