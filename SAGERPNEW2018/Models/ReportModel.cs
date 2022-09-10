
using HRandPayrollSystemModel.DBModel;
using HRandPayrollSystemModel.InventryModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAGERPNEW2018.Models
{
    public class ReportModel
    {
        
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int EmployeeID { get; set; }
        public int DesignationID { get; set; }
        public int DepartmentID { get; set; }
        public int projectID { get; set; }
        public int Month { get; set; }
        public int year { get; set; }





        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }



        public DataTable GetEminuteData(string QuryStatus, DateTime fromdate, DateTime todate, string deptid, string type)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_eminutedetailwiseReport", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@Status", QuryStatus);
                        adapt.SelectCommand.Parameters.AddWithValue("@datefrom", fromdate);
                        adapt.SelectCommand.Parameters.AddWithValue("@dateto", todate);
                        adapt.SelectCommand.Parameters.AddWithValue("@deptid", deptid);
                        adapt.SelectCommand.Parameters.AddWithValue("@type", type);



                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }




        public List<sp_getFixedAssitlistMinute_Result> getlistofFixedAsset(int Project, int department, int type, int Subtype)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    try
                    {
                        Subtype =Convert.ToInt32(  context.tblEminuteSubTypes.FirstOrDefault(x => x.MinuteSubTypeID == Subtype).FixassetSubtypeId);
                    }
                    catch (Exception)
                    {

                        Subtype = 0;
                    }
                   
                     return context.sp_getFixedAssitlistMinute(Project, department, type, Subtype).ToList();
                   

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        //public IEnumerable<SelectListItem> loadAccountsAccount()
        //{
        //    try
        //    {
        //        using (var context = new HRandPayrollDBEntities())
        //        {

        //            List<SelectListItem> listobj = new List<SelectListItem>();
        //            listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

        //            listobj.AddRange(context.ChartofAccounts.Where(x => x.AccountType == 2).Select(x => new SelectListItem { Text = x.AccountNo + " - " + x.AccountName, Value = x.AccountID.ToString() }).ToList());

        //            return listobj;

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }
        //}






        public DataTable GetRecoveryData(string id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_RptRecoverySummryReport", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@PaymentCodeID", id);


                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public DataSet GetRecoveryDateData(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_getRecoveryDate", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@code", id);


                        DataSet ds = new DataSet();
                        adapt.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public DataTable GetPaymentCollection(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_GetPaymentByIDData", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@paymentID", id);
                      
                        
                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public DataTable GetDataCashCollection(DateTime from, DateTime to)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_GetCashCollectionData", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@fromdate", from);
                        adapt.SelectCommand.Parameters.AddWithValue("@Todate", to);

                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public DataTable GetPaymmentDetailDatewise(DateTime from, DateTime to)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_GetPaymentEntryDetailReport", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@fromdate", from);
                        adapt.SelectCommand.Parameters.AddWithValue("@Todate", to);

                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public DataSet GetEmployeeSalarySlip(ReportModel obj)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_getEmployeeSalarySlip", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
                        adapt.SelectCommand.Parameters.AddWithValue("@projectID", obj.projectID);
                        adapt.SelectCommand.Parameters.AddWithValue("@yaer", obj.year);
                        adapt.SelectCommand.Parameters.AddWithValue("@month", obj.Month);
                        adapt.SelectCommand.Parameters.AddWithValue("@employeeID", obj.EmployeeID);


                        DataSet dt = new DataSet();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetAccountLedger(DateTime from, DateTime to , int accountID)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
                    using (SqlCommand cmd = new SqlCommand("sp_GeneralLegderReport", conn))
                    {
                        cmd.CommandTimeout = 1500;
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapt.SelectCommand.Parameters.AddWithValue("@datefrom", from);
                        adapt.SelectCommand.Parameters.AddWithValue("@dateTo", to);
                        adapt.SelectCommand.Parameters.AddWithValue("@AccoutId", accountID);


                        DataTable dt = new DataTable();
                        adapt.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<sp_budgetSummryForEminute_Result> getbudgetreport(int projectid, int headid, int subhead)
        {
            try
            {
                using (var context = new InventoryEntities())
                {

                    return context.sp_budgetSummryForEminute(projectid, headid, subhead).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //public DataTable GetDataSessionWiseReport(int PartID,int sessionID)
        //{
        //    try
        //    {
        //        using (var context = new TradeProEntities())
        //        {
        //            using (SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString.ToString()))
        //            using (SqlCommand cmd = new SqlCommand("sp_rptSessionWiseReport", conn))
        //            {
        //                cmd.CommandTimeout = 1500;
        //                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
        //                adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
        //                adapt.SelectCommand.Parameters.AddWithValue("@sessionId", sessionID);
        //                adapt.SelectCommand.Parameters.AddWithValue("@partId", PartID);



        //                DataTable dt = new DataTable();
        //                adapt.Fill(dt);
        //                return dt;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        // public DataTable selectallTrail(string where = "")
        // {
        //     SqlConnection con = new SqlConnection(constring);
        //     DataTable dt = new DataTable();

        //     SqlDataAdapter da = new SqlDataAdapter(@"     
        //                    select a.*, b.dr,b.cr  ,GLCompany.Address,GLCompany.Title,GLCompany.website,GLCompany.Phone from

        //                  (
        //                 select secondParent.* ,  b.GLCAID as thirdParentID ,  b.GLCode as thirdParentCode ,  b.GLTitle as thirdParentTitle  from(
        //                 select  b.GLCAID as SecondParentID ,  b.GLCode as SecondParentCode ,  b.GLTitle as SecondParentTitle ,    parenttable.* from(select A.GLCAID as FirstParentID,A.GLCode as FirstParentCode,a.GLTitle as FirstParentTitle  from GLChartOFAccount a where isParent=0) parenttable 
        //                 left join GLChartOFAccount b on parenttable.FirstParentID=b.isParent) 

        //                 secondParent left join GLChartOFAccount b on b.isParent=secondParent.SecondParentID 
        //                 )  a inner join GLvDetail b on  a.thirdParentID=b.GlAccountID  
        //                 inner join GLvMAIN on GLvMAIN.vID=b.vID 
        //                 inner join GLCompany on GLCompany.Companyid=GLvMAIN.Comp_Id




        //" + where, con);

        //     da.Fill(dt);
        //     return dt;
        // }


        //public DataTable SearchselectallTrail(string where = "")
        //{
        //    SqlConnection con = new SqlConnection(constring);
        //    DataTable dt = new DataTable();

        //    SqlDataAdapter da = new SqlDataAdapter(@"
        //                   select a.*, b.dr,b.cr  from

        //                 (
        //                select secondParent.* ,  b.GLCAID as thirdParentID ,  b.GLCode as thirdParentCode ,  b.GLTitle as thirdParentTitle  from(
        //                select  b.GLCAID as SecondParentID ,  b.GLCode as SecondParentCode ,  b.GLTitle as SecondParentTitle ,    parenttable.* from(select A.GLCAID as FirstParentID,A.GLCode as FirstParentCode,a.GLTitle as FirstParentTitle  from GLChartOFAccount a where isParent=0) parenttable 
        //                inner join GLChartOFAccount b on parenttable.FirstParentID=b.isParent) 

        //                secondParent inner join GLChartOFAccount b on b.isParent=secondParent.SecondParentID 
        //                )  a inner join GLvDetail b on  a.thirdParentID=b.GlAccountID  
        //                inner join GLvMAIN on GLvMAIN.vID=b.vID 
        //           " + where, con);

        //    da.Fill(dt);
        //    return dt;
        //}




        //    public DataTable selectall(string where = "", string date = "")
        //    {
        //        SqlConnection con = new SqlConnection(constring);
        //        DataTable dt = new DataTable();

        //        SqlDataAdapter da = new SqlDataAdapter(@"sELECT        GLvDetail.GlAccountID, GLvDetail.DTypeID, GLvDetail.DvaluesID, GLvDetail.Narration, GLvDetail.ChequeNo, GLvDetail.ChequeDate, GLvDetail.dr, GLvDetail.cr, GLChartOFAccount.GLCode, GLChartOFAccount.GLTitle, 
        //                     GLvMAIN.vDate,ISNULL(dimensionValues.DValue, '') as dimensionValues , GLvMAIN.vNO, GLvMAIN.vType, GLvMAIN.vID, GLvMAIN.Comp_Id, GLvMAIN.FiscalID, GLvMAIN.vUserID, aaa.dr as GroupTotalDr, aaa.Cr as GroupTotalCr, GLVoucherType.Title 
        //                ,	 format( GLvMAIN.vDate,'dd-MMMM-yyyy') as vdate , ( GLvDetail.dr -GLvDetail.cr + blance.asae) as runingbal, 


        //	case  
        //	when  blance.asae > 0  then CONCAT(  blance.asae,' Dr') else CONCAT(blance.asae ,' Cr' )  end as openingbalnce,
        //GLCompany.Title AS company,GLCompany.Address, GLCompany.Email, GLCompany.Phone, GLCompany.website
        //	 FROM          GLvMAIN INNER JOIN

        //                  GLCompany ON GLvMAIN.Comp_Id = GLCompany.Companyid  INNER JOIN
        //                     GLvDetail ON GLvMAIN.vID = GLvDetail.vID INNER JOIN
        //                     GLChartOFAccount ON GLvDetail.GlAccountID = GLChartOFAccount.GLCAID LEFT OUTER JOIN
        //                     dimensionValues ON GLvDetail.DvaluesID = dimensionValues.DValuesID left outer join 

        //		    GLVoucherType ON GLvMAIN.vType = GLVoucherType.Voucherid INNER JOIN
        //		 (SELECT        GLvDetail.GlAccountID,sum( CONVERT(numeric, GLvDetail.dr)) as dr,sum( CONVERT(numeric, GLvDetail.cr)) as Cr


        //		  FROM  GLvMAIN INNER JOIN
        //                     GLvDetail ON GLvMAIN.vID = GLvDetail.vID 
        //		GROUP BY GLvDetail.GlAccountID
        //	 )aaa on aaa.GlAccountID= GLvDetail.GlAccountID  inner join

        //	 (SELECT        GLvDetail.GlAccountID,sum( CONVERT(numeric, GLvDetail.dr)) as dr, sum( CONVERT(numeric, GLvDetail.cr)) as Cr,

        //	   ( sum( CONVERT(numeric, GLvDetail.dr)) )- (sum( CONVERT(numeric, GLvDetail.cr)))as asae

        //	FROM            GLvMAIN INNER JOIN
        //							 GLvDetail ON GLvMAIN.vID = GLvDetail.vID 


        //							 where  GLvMAIN.vDate < '" + date + @"'
        //	GROUP BY GLvDetail.GlAccountID) blance  on blance.GlAccountID=GLvDetail.GlAccountID " + where, con);

        //        da.Fill(dt);
        //        return dt;
        //    }


        //   public DataTable Searchselectall(string where = "", string date="")
        //   {
        //       SqlConnection con = new SqlConnection(constring);
        //       DataTable dt = new DataTable();

        //           SqlDataAdapter da = new SqlDataAdapter(@"sELECT        GLvDetail.GlAccountID, GLvDetail.DTypeID, GLvDetail.DvaluesID, GLvDetail.Narration, GLvDetail.ChequeNo, GLvDetail.ChequeDate, GLvDetail.dr, GLvDetail.cr, GLChartOFAccount.GLCode, GLChartOFAccount.GLTitle, 
        //                    GLvMAIN.vDate,ISNULL(dimensionValues.DValue, '') as dimensionValues , GLvMAIN.vNO, GLvMAIN.vType, GLvMAIN.vID, GLvMAIN.Comp_Id, GLvMAIN.FiscalID, GLvMAIN.vUserID, aaa.dr as GroupTotalDr, aaa.Cr as GroupTotalCr, GLVoucherType.Title 
        //               ,	 format( GLvMAIN.vDate,'dd-MMMM-yyyy') as vdate , ( GLvDetail.dr -GLvDetail.cr + blance.asae) as runingbal, 


        //case  
        //when  blance.asae > 0  then CONCAT(  blance.asae,' Dr') else CONCAT(blance.asae ,' Cr' )  end as openingbalnce
        // FROM          GLvMAIN INNER JOIN
        //                    GLvDetail ON GLvMAIN.vID = GLvDetail.vID INNER JOIN
        //                    GLChartOFAccount ON GLvDetail.GlAccountID = GLChartOFAccount.GLCAID LEFT OUTER JOIN
        //                    dimensionValues ON GLvDetail.DvaluesID = dimensionValues.DValuesID left outer join 
        //	    GLVoucherType ON GLvMAIN.vType = GLVoucherType.Voucherid INNER JOIN
        //	 (SELECT        GLvDetail.GlAccountID,sum( CONVERT(numeric, GLvDetail.dr)) as dr,sum( CONVERT(numeric, GLvDetail.cr)) as Cr


        //	  FROM  GLvMAIN INNER JOIN
        //                    GLvDetail ON GLvMAIN.vID = GLvDetail.vID 
        //	GROUP BY GLvDetail.GlAccountID
        // )aaa on aaa.GlAccountID= GLvDetail.GlAccountID  inner join

        // (SELECT        GLvDetail.GlAccountID,sum( CONVERT(numeric, GLvDetail.dr)) as dr, sum( CONVERT(numeric, GLvDetail.cr)) as Cr,

        //   ( sum( CONVERT(numeric, GLvDetail.dr)) )- (sum( CONVERT(numeric, GLvDetail.cr)))as asae

        //FROM            GLvMAIN INNER JOIN
        //						 GLvDetail ON GLvMAIN.vID = GLvDetail.vID 

        //						 where  GLvMAIN.vDate < '" + date + @"'
        //GROUP BY GLvDetail.GlAccountID) blance  on blance.GlAccountID=GLvDetail.GlAccountID" + where, con);

        //       da.Fill(dt);
        //       return dt;
        //   }


        //public IEnumerable<SelectListItem> LoadDimensionValue(string where = "")
        //{
        //    SqlConnection con = new SqlConnection(constring);
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter("select * from dimensionValues" + where, con);
        //    da.Fill(dt);
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Text = "-Dimension Value-", Value = "0" });
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        list.Add(new SelectListItem { Text = item["DValue"].ToString(), Value = item["DValuesID"].ToString() });
        //    }

        //    return list;
        //}
    }
}



//SqlDataAdapter da = new SqlDataAdapter(@"SELECT        GLvDetail.GlAccountID, GLvDetail.DTypeID, GLvDetail.DvaluesID, GLvDetail.Narration, GLvDetail.ChequeNo, GLvDetail.ChequeDate, GLvDetail.dr, GLvDetail.cr, GLChartOFAccount.GLCode, GLChartOFAccount.GLTitle, 
//                         GLvMAIN.vDate, ISNULL(dimensionValues.DValue, '') AS dimensionValues, GLvMAIN.vNO, GLvMAIN.vID, GLvMAIN.FiscalID, GLvMAIN.vUserID, dimensionValues.DValue, GLvMAIN.vType, GLVoucherType.Title, 
//                         GLCompany.Title AS company,GLCompany.Address, GLCompany.Email, GLCompany.Phone, GLCompany.website
//                            FROM            GLvMAIN INNER JOIN
//                         GLvDetail ON GLvMAIN.vID = GLvDetail.vID INNER JOIN
//                         GLChartOFAccount ON GLvDetail.GlAccountID = GLChartOFAccount.GLCAID INNER JOIN
//                         GLVoucherType ON GLvMAIN.vType = GLVoucherType.Voucherid INNER JOIN
//                         GLCompany ON GLvMAIN.Comp_Id = GLCompany.Companyid LEFT OUTER JOIN
//                         dimensionValues ON GLvDetail.DvaluesID = dimensionValues.DValuesID " + where, con);

//da.Fill(dt);