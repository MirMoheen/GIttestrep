using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
   public partial class tblDesignation
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }
        public string DepID { get; set; }



        public string geneartedesginationCode(int pid,int depid)
        {

            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var DesignationID = context.tblDesignations.Where(x => x.DepartmentID==depid &&  x.ProjectID==pid).Max(x => x.DesignationCode);
                    return  (Convert.ToInt32( DesignationID)+1).ToString("D2");
                }


            }
            catch (Exception ex)
            {

                return null;
            }


        }


        public int addata(tblDesignation obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;

                    obj.DesignationTitle = obj.DesignationTitle.ToUpper();
                    context.tblDesignations.Add(obj);
                    context.SaveChanges();
                    return obj.DesignationID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(tblDesignation obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblDesignations.SingleOrDefault(x => x.DesignationID == obj.DesignationID);
                    if (result != null)
                    {
                        result.DesignationTitle = obj.DesignationTitle.ToUpper();
                        result.OrderNO = obj.OrderNO;
                        result.inactive = obj.inactive;
                       // result.DesignationCode = obj.DesignationCode;
                        result.ProjectID = obj.ProjectID;
                        result.DepartmentID = obj.DepartmentID;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;
                        result.IsApprove = obj.IsApprove;
                        result.IsComplete = obj.IsComplete;
                        result.IsDepartment = obj.IsDepartment;
                        result.IsCancel = obj.IsCancel;


                        context.SaveChanges();
                        return result.DesignationID;
                    }
                    return result.DesignationID;
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
                    var result = context.tblDesignations.SingleOrDefault(x => x.DesignationID == id);
                    if (result != null)
                    {
                        context.tblDesignations.Remove(result);
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


        public tblDesignation getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblDesignations.SingleOrDefault(x => x.DesignationID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        
        public List<sp_getAllDesiginationnew_Result> getAllDesiginationdata(int pid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getAllDesiginationnew(pid).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
      
        //public IEnumerable<SelectListItem> LoadProjects(int pID)
        //{
        //    try
        //    {
        //        using (var context = new HRandPayrollDBEntities())
        //        {
        //            var list = context.tblProjects.Where(x => x.Inactive == false && x.ProjectID== pID).ToList();
        //            List<SelectListItem> listobj = new List<SelectListItem>();

        //            foreach (var item in list)
        //            {
        //                listobj.Add(new SelectListItem { Text = item.ProjectName, Value = item.ProjectID.ToString() });
        //            }


        //            return listobj;

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }
        //}
        public IEnumerable<SelectListItem> LoadDepartment(int ProjectID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var list = context.tblDepartments.Where(x => x.ProjectID == ProjectID &  x.Inactive == false).ToList();
                    List<SelectListItem> listobj = new List<SelectListItem>();

                    foreach (var item in list)
                    {
                        listobj.Add(new SelectListItem { Text = item.DepartmentName, Value = item.DepartmentID.ToString() });
                    }


                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> LoadDepartment()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var list = context.tblDepartments.Where(x => x.ProjectID == 0 & x.Inactive == false).ToList();
                    List<SelectListItem> listobj = new List<SelectListItem>();

                    foreach (var item in list)
                    {
                        listobj.Add(new SelectListItem { Text = item.DepartmentName, Value = item.DepartmentID.ToString() });
                    }



                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<tblDesignation> checkDuplicate(int id, string title,int pid,int dptid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblDesignations.Where(x => x.DesignationTitle == title && x.DesignationID != id && x.ProjectID==pid && x.DepartmentID ==dptid).ToList();

                    }
                    else
                    {
                        return context.tblDesignations.Where(x => x.DesignationTitle == title && x.ProjectID == pid && x.DepartmentID == dptid).ToList();


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
