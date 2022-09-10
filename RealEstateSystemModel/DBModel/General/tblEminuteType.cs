using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
  public partial  class tblEminuteType
    {


        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }


        public int addata(tblEminuteType obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.tblEminuteTypes.Add(obj);
                    context.SaveChanges();
                    return obj.MinuteTypeID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(tblEminuteType obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEminuteTypes.SingleOrDefault(x => x.MinuteTypeID == obj.MinuteTypeID);
                    if (result != null)
                    {
                        result.MinuteType = obj.MinuteType;
                        result.inactive = obj.inactive;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;
                        result.TechPerson = obj.TechPerson;

                        context.SaveChanges();
                        return result.MinuteTypeID;
                    }
                    return result.MinuteTypeID;
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
                    var result = context.tblEminuteTypes.SingleOrDefault(x => x.MinuteTypeID == id);
                    if (result != null)
                    {
                        context.tblEminuteTypes.Remove(result);
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


        public tblEminuteType getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEminuteTypes.SingleOrDefault(x => x.MinuteTypeID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


     


        public List<tblEminuteType> getAlldata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEminuteTypes.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<tblEminuteType> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblEminuteTypes.Where(x => x.MinuteType == title && x.MinuteTypeID != id).ToList();

                    }
                    else
                    {
                        return context.tblEminuteTypes.Where(x => x.MinuteType == title).ToList();


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
