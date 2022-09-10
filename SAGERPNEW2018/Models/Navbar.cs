using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Management;

namespace SAGERPNEW2018.Models
{
    //public class Navbar
    //{
    //    public int Id { get; set; }
    //    public string nameOption { get; set; }
    //    public string controller { get; set; }
    //    public string action { get; set; }
    //    public string area { get; set; }
    //    public string imageClass { get; set; }
    //    public string activeli { get; set; }
    //    public bool status { get; set; }
    //    public int parentId { get; set; }
    //    public bool isParent { get; set; }
    //    public string Username { get; set; }

    //}
    public class Loginform
    {
      //  string constring = ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;

        public string Username { get; set; }
        public string Password { get; set; }
        public int Companyid { get; set; }
        public string ProjectID { get; set; }

        public bool type { get; set; }

        //public IEnumerable<SelectListItem> loadBranch(string where = "")
        //{
            
        //    ////ServiceController service = new ServiceController("MyServiceName");

        //    ////if ((service.Status.Equals(ServiceControllerStatus.Stopped)) ||

        //    ////    (service.Status.Equals(ServiceControllerStatus.StopPending)))

        //    ////    service.Start();

        //    ////else service.Stop();

        //    ////ServiceController controller = new ServiceController();
        //    ////controller.MachineName = "Machine1";
        //    ////controller.ServiceName = "MSSQL$SQLEXPRESS";

        //    ////if (controller.Status == ServiceControllerStatus.Running)
        //    ////    controller.Stop();

        //    ////controller.WaitForStatus(ServiceControllerStatus.Stopped);

        //    //var sc = new ServiceController("MyService", "MyRemoteMachine");
        //    //sc.Start();
        //    //sc.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running);
        //    //sc.Stop();
        //    //sc.WaitForStatus(ServiceControllerStatus.Stopped);

        //    SqlConnection con = new SqlConnection(constring);
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter("select * from GLCompany" + where, con);
        //    da.Fill(dt);
        //    List<SelectListItem> list = new List<SelectListItem>();
        //   /// list.Add(new SelectListItem { Text = "--Please Select Branch--", Value = "0" });
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        list.Add(new SelectListItem { Text = item["title"].ToString(), Value = item["Companyid"].ToString() });
        //    }

        //    return list;
        //}

        //public IEnumerable<SelectListItem> loadFiscal(string where = "")
        //{
        //    SqlConnection con = new SqlConnection(constring);
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter("select   CONCAT(format( StartYear,'yyyy'),'-',format(EndYear,'yyyy')) as Years, FiscalID from GLFiscalYear " + where, con);
        //    da.Fill(dt);
        //    List<SelectListItem> list = new List<SelectListItem>();
        // //   list.Add(new SelectListItem { Text = "--Please Select Year--", Value = "0" });
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        list.Add(new SelectListItem { Text = item["Years"].ToString(), Value = item["FiscalID"].ToString() });
        //    }

        //    return list;
        //}


    }
}