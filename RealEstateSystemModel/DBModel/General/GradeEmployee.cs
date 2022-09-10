using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
public partial    class GradeEmployee
    {


        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }


        public int addata(GradeEmployee obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.GradeEmployees.Add(obj);
                    context.SaveChanges();
                    return obj.GradeID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(GradeEmployee obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.GradeEmployees.SingleOrDefault(x => x.GradeID == obj.GradeID);
                    if (result != null)
                    {
                        result.GradeTitle = obj.GradeTitle;
                        result.inactive = obj.inactive;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.GradeID;
                    }
                    return result.GradeID;
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
                    var result = context.GradeEmployees.SingleOrDefault(x => x.GradeID == id);
                    if (result != null)
                    {
                        context.GradeEmployees.Remove(result);
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


        public GradeEmployee getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.GradeEmployees.SingleOrDefault(x => x.GradeID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<GradeEmployee> getAllgradedata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.GradeEmployees.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<GradeEmployee> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.GradeEmployees.Where(x => x.GradeTitle == title && x.GradeID != id).ToList();

                    }
                    else
                    {
                        return context.GradeEmployees.Where(x => x.GradeTitle == title).ToList();


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
