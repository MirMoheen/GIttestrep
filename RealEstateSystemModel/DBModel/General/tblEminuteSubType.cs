using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
  public partial  class tblEminuteSubType
    {



        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }


        public int addata(tblEminuteSubType obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.tblEminuteSubTypes.Add(obj);
                    context.SaveChanges();
                    return obj.MinuteSubTypeID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(tblEminuteSubType obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEminuteSubTypes.SingleOrDefault(x => x.MinuteSubTypeID == obj.MinuteSubTypeID);
                    if (result != null)
                    {
                        result.MinuteSubType = obj.MinuteSubType;
                        result.MinuteTypeID = obj.MinuteTypeID;

                        result.inactive = obj.inactive;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.MinuteSubTypeID;
                    }
                    return result.MinuteSubTypeID;
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
                    var result = context.tblEminuteSubTypes.SingleOrDefault(x => x.MinuteSubTypeID == id);
                    if (result != null)
                    {
                        context.tblEminuteSubTypes.Remove(result);
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



        public IEnumerable<SelectListItem> loadMinutetype()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                   /// listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tblEminuteTypes.Where(x => x.inactive == false).Select(x => new SelectListItem { Text = x.MinuteType, Value = x.MinuteTypeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public tblEminuteSubType getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEminuteSubTypes.SingleOrDefault(x => x.MinuteSubTypeID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getminuteSubtype_Result> getAlldata(string where)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.sp_getminuteSubtype(where).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<tblEminuteSubType> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblEminuteSubTypes.Where(x => x.MinuteSubType == title && x.MinuteSubTypeID != id).ToList();

                    }
                    else
                    {
                        return context.tblEminuteSubTypes.Where(x => x.MinuteSubType == title).ToList();


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
