using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
  public partial   class tblFavorite
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }


        public int addata(tblFavorite obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    //  obj.CompID = new Login().GetUser().CompID;
                    context.tblFavorites.Add(obj);
                    context.SaveChanges();
                    return obj.FavoriteID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }








        public int UpdateData(tblFavorite obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblFavorites.SingleOrDefault(x => x.FavoriteID == obj.FavoriteID);
                    if (result != null)
                    {
                        result.FavText = obj.FavText;
                        result.FavRemarks = obj.FavRemarks;

                        result.inactive = obj.inactive;
                        result.ModifiedDate = obj.ModifiedDate;
                        result.ModifiedID = obj.ModifiedID;
                        result.ModifiedIP = obj.ModifiedIP;


                        context.SaveChanges();
                        return result.FavoriteID;
                    }
                    return result.FavoriteID;
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
                    var result = context.tblFavorites.SingleOrDefault(x => x.FavoriteID == id);
                    if (result != null)
                    {
                        result.inactive = true;
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


        public tblFavorite getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblFavorites.SingleOrDefault(x => x.FavoriteID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }





        public List<tblFavorite> getAlldata(int userid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblFavorites.Where(x=>x.UserID==userid) .ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> favlist(int userid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
           
                        //  listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                        listobj.AddRange(context.tblFavorites.Where(x => x.inactive == false && x.UserID== userid).Select(x => new SelectListItem { Text = x.FavText, Value = x.FavoriteID.ToString() }).ToList());

                   


                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }





        public List<tblFavorite> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblFavorites.Where(x => x.FavText == title && x.FavoriteID != id).ToList();

                    }
                    else
                    {
                        return context.tblFavorites.Where(x => x.FavText == title).ToList();


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
