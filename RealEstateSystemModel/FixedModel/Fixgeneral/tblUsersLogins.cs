using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.FixedModel
{
  public partial class tblUsersLogin
    {


        public tblUsersLogin UserLogin(string Username, string password)
        {
            try
            {
                using (var context = new FixedAssetEntities ())
                {
                    return context.tblUsersLogins.FirstOrDefault(u => u.fldLoginName == Username && u.fldLoginPassword == password);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



      



        public sp_getdataforandroid_Result GetDataForAssetbyCode(string code)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {
                    return context.sp_getdataforandroid(code).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                return null;
            }   
        }



        public List<Assetlist> GetDataRoomName(string name)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {
                    return context.sp_getDataRoomWise(name).Select(x=> new Assetlist { AssetId=x.AssetId, AssetName=x.AssetName,Room=x.Room,Verify=x.LogVarifyStatus,Remarks=x.VerifyRemarks }).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public tblAsset updateverification(long userid ,string code,bool status,string ip,string details )
        {

            using (var context=new FixedAssetEntities())
            {
             var result=   context.tblAssets.FirstOrDefault(x => x.AssetId == code);
                if (result != null)
                {

                    result.LogVarifyStatus = status;
                    result.LogVarifiedBy = userid;
                    result.LogVarifiedDate = DateTime.Now;
                    result.LogVarifiedIp = ip;
                    result.VerifyRemarks = details;

                    context.SaveChanges();

                    return result;

                }
                else
                {

                    return null;

                }
            }




        }


        public IEnumerable<SelectListItem> loadProject()
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.Hospitals.Select(x => new SelectListItem { Text = x.Name, Value = x.HospNo.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> loadDepartment(string projectid)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- All --", Value = "0" });

                    listobj.AddRange(context.Departments.Where(x=>x.HospNo == projectid).Select(x => new SelectListItem { Text = x.DEPT_NAME, Value = x.Sno.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public IEnumerable<SelectListItem> loadcategory( )
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- All --", Value = "0" });

                    listobj.AddRange(context.Fixed_Asset_Category.Select(x => new SelectListItem { Text = x.Cat_Name, Value = x.Cat_Id.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<sp_getFixedAssitlistMinuteApi_Result> loadfixedlist(int projectID, int department, int typeid, int subtype)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {
                    return context.sp_getFixedAssitlistMinuteApi(projectID, department, typeid, subtype).ToList();


                }
            }
            catch (Exception ex)
            {

                return new List<sp_getFixedAssitlistMinuteApi_Result>();
            }
        }
      


        public IEnumerable<SelectListItem> loadSubcategory( int catid)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- All --", Value = "0" });

                    listobj.AddRange(context.Fixed_Asset_SubCategory.Where(x=>x.Cat_Id==catid) .Select(x => new SelectListItem { Text = x.Sub_Cat_Name, Value = x.Sub_Cat_Id.ToString() }).ToList());

                    return listobj.OrderBy(x=>x.Text);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




    }


    public class Assetlist
    {

        public string AssetName { get; set; }
        public string AssetId { get; set; }
        public string Room { get; set; }
        public string Remarks { get; set; }
        public bool Verify { get; set; }



    }
}
