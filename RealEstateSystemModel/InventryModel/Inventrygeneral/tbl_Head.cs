using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.InventryModel
{
    public partial class tbl_Head
    {

        public List<SelectListItem> getbudgetAccount(int project)
        {
            try
            {
                using (var context = new InventoryEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.Alshifa_jv_Desc_Fill("2020").Select(x => new SelectListItem { Text = x.AccDes.Trim().ToString(), Value = x.id.ToString() }).ToList());


                    return listobj;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<SelectListItem> gethead(int costcenter)
        {
            try
            {
                using (var context = new InventoryEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tbl_Costcenterbudgetheads.Where(x => x.status == false && x.Costcenterid== costcenter ).Select(x => new SelectListItem { Text = x.HeadName.ToString(), Value = x.ID.ToString() }).ToList());


                    return listobj;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public tbl_Costcenter getcostonId(int costcentrid)
        {
            try
            {
                using (var context = new InventoryEntities())
                {
                    // && x.ProjectID == projectID


                    
                //    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                  return  context.tbl_Costcenter.Where(x => x.status == false && x.ID== costcentrid).FirstOrDefault();


                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<SelectListItem> getheadsab( int projectID)
        {
            try
            {
                using (var context = new InventoryEntities())
                {
                    // && x.ProjectID == projectID


                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tbl_Costcenter.Where(x => x.status == false && x.Projectid == projectID).Select(x => new SelectListItem { Text = x.Name.ToString(), Value = x.ID.ToString() }).ToList());


                    return listobj;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string getheadname(int hID)
        {
            try
            {
                using (var context = new InventoryEntities())
                {


                    return context.tbl_Costcenter.FirstOrDefault(x => x.status == false && x.ID == hID).Name;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public string getheadsabname(int subid)
        {
            try
            {
                using (var context = new InventoryEntities())
                {


                    return context.tbl_Costcenterbudgetheads.FirstOrDefault(x => x.status == false && x.ID == subid).HeadName;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<SelectListItem> getheadsabAll(int projectID)
        {
            try
            {
                using (var context = new InventoryEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tbl_Costcenterbudgetheads.Where(x => x.status == false && x.ProjectID == projectID).Select(x => new SelectListItem { Text = x.HeadName.ToString(), Value = x.ID.ToString() }).ToList());


                    return listobj;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<SelectListItem> getFinYear(string hospitalCode)
        {
            try
            {
                using (var context = new InventoryEntities())
                {


                    return context.Get_BudgetFinYears(hospitalCode).Select(x => new SelectListItem { Text = x.fyear.ToString(), Value = x.code.ToString() }).ToList();



                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public tbl_Costcenterbudgetheadamount getheaddetails(int headID, int subhead, string fyear)
        {
            try
            {
                using (var context = new InventoryEntities())
                {


                    return context.tbl_Costcenterbudgetheadamount.FirstOrDefault(x => x.HeadID == headID  && x.FY == fyear && x.status == false);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public getBudgetProvisionamount_Result getCostCenterwiseheaddetails(string headID, string subhead, string fyear)
        {
            try
            {
                using (var context = new InventoryEntities())
                {


                    return context.getBudgetProvisionamount(headID, subhead, fyear).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public decimal getProvion(int headID, int subhead, string fyear)
        {
            try
            {
                using (var context = new InventoryEntities())
                {




                    return Convert.ToDecimal(context.tbl_AddBudget.Where(x => x.headID == headID &&  x.fY == fyear && x.status == false && x.adjustment == true).Sum(x => x.ThisExpense));
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }



        public List< budgetHeadLedger_Result> getExpensebudget(string headID, string subhead, string from = "01/01/2050", string to = "01/01/2050", int fy=2021)
        {
            try
            {
                using (var context = new InventoryEntities())
                {


                    return context.budgetHeadLedger(headID, subhead, from, to,fy).ToList();
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public List<SelectListItem> getproject()
        {
            try
            {
                using (var context = new InventoryEntities())
                {


                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.projecttbls.Select(x => new SelectListItem { Text = x.name.ToString(), Value = x.id.ToString() }).ToList());




                    return listobj;



                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




    }
}
