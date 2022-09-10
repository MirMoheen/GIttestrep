using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.FixedModel
{
    public partial class tblAssetService
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }

        public string CurrentLogEmployeedetail { get; set; }

        public IEnumerable<tblAssetService> tas { get; set; }

        public IEnumerable<HttpPostedFileBase> DocFile { get; set; }
        public IEnumerable<string> listdocpath { get; set; }
        public IEnumerable<AssetDocDetail> detailistDoc { get; set; }


        public IEnumerable<string> lisdescription { get; set; }




        public IEnumerable<string> EServiceType { get; set; }

        public IEnumerable<string> ERemarks { get; set; }

        public string dateformat { get; set; }

        public IEnumerable<SelectListItem> loadType(int? AssetTypeID)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "" });
                    if (AssetTypeID != null && AssetTypeID > 0)

                        listobj.AddRange(context.Fixed_Asset_Category.Where(x => x.Cat_Id == AssetTypeID && x.Status == false).OrderBy(x => x.Cat_Name).Select(x => new SelectListItem { Text = x.Cat_Name, Value = x.Cat_Id.ToString() }).ToList());

                    else
                        listobj.AddRange(context.Fixed_Asset_Category.Where(x => x.Status == false).OrderBy(x => x.Cat_Name).Select(x => new SelectListItem { Text = x.Cat_Name, Value = x.Cat_Id.ToString() }).ToList());

                    // .OrderBy(x => x.Text)
                    // Where(x => x.Status == null)
                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> loadSubtypebyTypeID(int? typeId)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {


                    List<SelectListItem> listobj = new List<SelectListItem>();
                      listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "" });

                    listobj.AddRange(context.Fixed_Asset_SubCategory.Where(x => x.Cat_Id == typeId).OrderBy(x => x.Sub_Cat_Name).Select(x => new SelectListItem { Text = x.Sub_Cat_Name, Value = x.Sub_Cat_Id.ToString() }).ToList());
                    //  x.status == true &&
                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public IEnumerable<SelectListItem> loadTagID(int? typeId)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {


                    List<SelectListItem> listobj = new List<SelectListItem>();
                      listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "" });

                    listobj.AddRange(context.tblAssets.Where(x => x.SubCategoryId == typeId).OrderBy(x => x.AssetId).Select(x => new SelectListItem { Text = x.AssetId, Value = x.id.ToString() }).ToList());
                    //  x.status == true &&
                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

   

        public IEnumerable<SelectListItem> loadServiceType()
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.ServiceConfigurations.Select(x => new SelectListItem { Text = x.ServiceName, Value = x.ConfigId.ToString() }).OrderBy(x => x.Text).ToList());
                    // .OrderBy(x => x.Text)
                    // Where(x => x.Status == null)
                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public string GenerateServiceCode(string prefix)
        {
            try
            {

                using (var context = new FixedAssetEntities())
                {


                    var resultList = context.sp_ServiceCode().ToList();

                    return prefix + "-" + resultList[0].Value.ToString("D4");


                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public int addata(tblAssetService obj)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {


                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {

                            if (obj.EServiceType != null)
                            {

                                List<tblAssetService> Listobjdss = new List<tblAssetService>();
                                int a = 0;
                                foreach (var item in obj.EServiceType)
                                {
                                    tblAssetService objtas = new tblAssetService();

                                    //objtas.ServiceID = obj.ServiceID;
                                    objtas.ServiceCode = obj.ServiceCode;
                                    objtas.AssetTypeID = obj.AssetTypeID;
                                    objtas.AssetSubTypeID = obj.AssetSubTypeID;
                                    objtas.AssetTagNoID = obj.AssetTagNoID;

                                    objtas.EntryDate = obj.EntryDate;
                                    objtas.UserID = obj.UserID;
                                    objtas.IP = obj.IP;

                                    objtas.ServiceType = obj.EServiceType.ToArray()[a];
                                    objtas.ServiceRemarks = obj.ERemarks.ToArray()[a];

                                    objtas.MaintenanceDate = obj.MaintenanceDate;


                                    Listobjdss.Add(objtas);
                                    a++;
                                }

                                context.tblAssetServices.AddRange(Listobjdss);
                                context.SaveChanges();
                            }

                            List<AssetDocDetail> Listobjadd = new List<AssetDocDetail>();

                            if (obj.lisdescription != null)
                            {

                                int b = 0;
                                foreach (var item in obj.listdocpath)
                                {
                                    if (item != "")
                                    {
                                        //tblEminuteDocDetail objdss = new tblEminuteDocDetail();
                                        AssetDocDetail objdss = new AssetDocDetail();
                                        ////objdss.Description = lisdescription.ToArray()[a];
                                        objdss.ServiceID = obj.AssetTagNoID;
                                        objdss.AssetDescription = "'" + lisdescription.ToArray()[b] + "'" + " Uploaded by : " + obj.CurrentLogEmployeedetail + "";


                                        objdss.PathDoc = obj.listdocpath.ToArray()[b];

                                        Listobjadd.Add(objdss);
                                        //context.AssetDocDetails.Add(objdss);
                                        //context.SaveChanges();

                                        b++;
                                    }
                                }
                                context.AssetDocDetails.AddRange(Listobjadd);
                                context.SaveChanges();
                            }

                            context.SaveChanges();



                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }




                    return 1;
                }




            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        //public List<sp_ServiceList_Result> loadServicesAsset()
        //{
        //    try
        //    {
        //        using (var context = new FixedAssetEntities())
        //        {

        //            return context.sp_ServiceList().ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {


        //        return null;
        //    }
        //}

        public tblAssetService getAlldataByID(int id)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {
                    return context.tblAssetServices.SingleOrDefault(x => x.ServiceID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

       
        public List<tblAssetService> getdetailistDocumentData(int code)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    return context.tblAssetServices.Where(x => x.AssetTagNoID == code).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<AssetDocDetail> getdocinfo(int code)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    return context.AssetDocDetails.Where(x => x.ServiceID == code).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

       


        public int UpdateAssetService(tblAssetService obj)
        {
            using (var context = new FixedAssetEntities())
            {

               

                List<AssetDocDetail> listobjdss = new List<AssetDocDetail>();
                int a = 0;

                foreach (var item in obj.listdocpath)
                {
                    AssetDocDetail objdss = new AssetDocDetail();

                    if (item != "")

                    {

                        //objdss.AssetDescription = lisdescription.ToArray()[a].Contains("Uploaded by :") ? lisdescription.ToArray()[a] : lisdescription.ToArray()[a] + " Uploaded by : ADMIN IT ";
                        objdss.AssetDescription = lisdescription.ToArray()[a] + " Uploaded by : " + obj.CurrentLogEmployeedetail + "";


                        objdss.ServiceID = obj.AssetTagNoID;
                        objdss.PathDoc = obj.listdocpath.ToArray()[a];
                        listobjdss.Add(objdss);
                        a++;
                    }

                }
                context.AssetDocDetails.AddRange(listobjdss);
                context.SaveChanges();

                return 1;
            }

        }


        public List<tblAsset> getasset(int id)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    return context.tblAssets.Where(x => x.id == id).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<AssetDocDetail> getdetailistDocumentData1(int? code)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                    return context.AssetDocDetails.Where(x => x.ServiceID == code).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<tblAsset> loadBootstrapModal(int? typeId)
        {
            try
            {
                using (var context = new FixedAssetEntities())
                {

                     return context.tblAssets.Where(z => z.id == typeId).ToList();


                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


    }
}
