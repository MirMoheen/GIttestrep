using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
  public partial  class CityInformtion
    {


        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }


        public int addata(CityInformtion obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.CityInformtions.Add(obj);
                    context.SaveChanges();
                    return obj.CityID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public int UpdateData(CityInformtion obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.CityInformtions.SingleOrDefault(x => x.CityID == obj.CityID);
                    if (result != null)
                    {
                        result.CityName = obj.CityName;
                        result.inactive = obj.inactive;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.CityID;
                    }
                    return result.CityID;
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
                    var result = context.CityInformtions.SingleOrDefault(x => x.CityID == id);
                    if (result != null)
                    {
                        context.CityInformtions.Remove(result);
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


        public CityInformtion getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.CityInformtions.SingleOrDefault(x => x.CityID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<CityInformtion> getAllCitydata()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.CityInformtions.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<CityInformtion> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.CityInformtions.Where(x => x.CityName == title && x.CityID != id).ToList();

                    }
                    else
                    {
                        return context.CityInformtions.Where(x => x.CityName == title).ToList();


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
