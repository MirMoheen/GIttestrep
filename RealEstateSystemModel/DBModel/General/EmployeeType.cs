using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class EmployeeType
    {


        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }


        public int addata(EmployeeType obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.EmployeeTypes.Add(obj);
                    context.SaveChanges();
                    return obj.EmpoyeeTypeID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(EmployeeType obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.EmployeeTypes.SingleOrDefault(x => x.EmpoyeeTypeID == obj.EmpoyeeTypeID);
                    if (result != null)
                    {
                        result.EmployeeTypeName = obj.EmployeeTypeName;                       
                        result.inactive = obj.inactive;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.EmpoyeeTypeID;
                    }
                    return result.EmpoyeeTypeID;
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
                    var result = context.EmployeeTypes.SingleOrDefault(x => x.EmpoyeeTypeID == id);
                    if (result != null)
                    {
                        context.EmployeeTypes.Remove(result);
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


        public EmployeeType getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.EmployeeTypes.SingleOrDefault(x => x.EmpoyeeTypeID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<EmployeeType> getAllCompanydata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.EmployeeTypes.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<EmployeeType> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.EmployeeTypes.Where(x => x.EmployeeTypeName == title && x.EmpoyeeTypeID != id).ToList();

                    }
                    else
                    {
                        return context.EmployeeTypes.Where(x => x.EmployeeTypeName == title).ToList();


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
