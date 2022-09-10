using SAGERPNEW2018.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SAGERPNEW2018.Domain
{
    public class Data
    {

       // public IEnumerable<Navbar> navbarItems()
      //  {
            //var user = new Login().GetUser();
            //var userRight = new Login().GetDataUserRights();
            //var DeshBoard=userRight.Select(x => x.Formid == 3);

            //string[] userdata = new Login().userinfofromCookie();
            //DataTable dt = new DataTable();
            //dt = new Login().checkRightUser(" where GLUser.Userid='" + userdata[1] + "' ");
            //// dt = (DataTable)HttpContext.Current.Session["rightdt"];
            //var menu = new List<Navbar>();
            //DataRow[] row;
            //row = dt.Select("FormID='3'");
            //if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //{
            //    menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-home", status = true, isParent = false, parentId = 0 });

            //}
            //row = dt.Select("FormID='4'");
            //if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //{
            //    menu.Add(new Navbar { Id = 2, nameOption = row[0][14].ToString(), imageClass = "fa fa-usd", status = true, isParent = true, parentId = 0 });

            //    row = dt.Select("FormID='29'");
            //    if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //    {
            //        menu.Add(new Navbar { Id = 32, nameOption = row[0][14].ToString(), controller = "AdmissionInformation", action = "Index", status = true, isParent = false, parentId = 2 });

            //    }
            //    row = dt.Select("FormID='30'");
            //    if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //    {
            //        menu.Add(new Navbar { Id = 33, nameOption = row[0][14].ToString(), controller = "PackageInfo", action = "Index", status = true, isParent = false, parentId = 2 });

            //    }


            //}
            //row = dt.Select("FormID='20'");
            //if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //{

            //    menu.Add(new Navbar { Id = 16, nameOption = "Setup", imageClass = "nav-parent fa fa-gear", status = true, isParent = true, parentId = 0 });

            //    row = dt.Select("FormID='18'");
            //    if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //    {
            //        menu.Add(new Navbar { Id = 17, nameOption = "User Group", controller = "UserGroup", action = "Index", status = true, isParent = false, parentId = 16 });

            //    }

            //    row = dt.Select("FormID='19'");
            //    if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //    {
            //        menu.Add(new Navbar { Id = 18, nameOption = "GL User", controller = "GLUser", action = "Index", status = true, isParent = false, parentId = 16 });

            //    }

            //    row = dt.Select("FormID='27'");
            //    if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //    {
            //        menu.Add(new Navbar { Id = 1, nameOption = row[0][14].ToString(), controller = "SessionStudent", action = "Index", status = true, isParent = false, parentId = 16 });

            //    }


            //    row = dt.Select("FormID='28'");
            //    if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //    {
            //        menu.Add(new Navbar { Id = 1, nameOption = row[0][14].ToString(), controller = "StudentSubect", action = "Index", status = true, isParent = false, parentId = 16 });

            //    }


            //}

            //row = dt.Select("FormID='31'");
            //if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //{
            //    menu.Add(new Navbar { Id = 31, nameOption = row[0][14].ToString(), imageClass = "fa fa-file", status = true, isParent = true, parentId = 0 });


            //    row = dt.Select("FormID='32'");
            //    if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //    {
            //        menu.Add(new Navbar { Id = 32, nameOption = row[0][14].ToString(), controller = "GLReports", action = "SessionWiseReport", status = true, isParent = false, parentId = 31 });

            //    }

            //    row = dt.Select("FormID='33'");
            //    if (row.Count() > 0 && Convert.ToBoolean(row[0]["Assign"]))
            //    {
            //        menu.Add(new Navbar { Id = 32, nameOption = row[0][14].ToString(), controller = "GLReports", action = "ChallanPrint", status = true, isParent = false, parentId = 31 });

            //    }
            //}
            ////menu.Add(new Navbar { Id = 1, nameOption = "Dashboard", controller = "Home", action = "Index", imageClass = "fa fa-home", status = true, isParent = false, parentId = 0 });
            ////menu.Add(new Navbar { Id = 2, nameOption = "Accounts", imageClass = "nav-parent  ", status = true, isParent = true, parentId = 0 });
            ////menu.Add(new Navbar { Id = 3, nameOption = "Voucher Type", controller = "GLVoucherType", action = "Index", status = true, isParent = false, parentId = 2 });
            ////menu.Add(new Navbar { Id = 4, nameOption = "Voucher Information", controller = "GLvMAIN", action = "Index", status = true, isParent = false, parentId = 2 });
            ////menu.Add(new Navbar { Id = 5, nameOption = "Dimension Values", controller = "dimensionValues", action = "Index", status = true, isParent = false, parentId = 2 });
            ////menu.Add(new Navbar { Id = 6, nameOption = "Dimension Type", controller = "dimensionType", action = "Index", status = true, isParent = false, parentId = 2 });
            ////menu.Add(new Navbar { Id = 7, nameOption = "Chart OF Account", controller = "GLChartOFAccount", action = "Index", status = true, isParent = false, parentId = 2 });
            ////menu.Add(new Navbar { Id = 8, nameOption = "Voucher Posting", controller = "GLvMAIN", action = "Post", status = true, isParent = false, parentId = 2 });
            ////menu.Add(new Navbar { Id = 9, nameOption = "Periods", controller = "GLPeriods", action = "Index", status = true, isParent = false, parentId = 2 });
            ////menu.Add(new Navbar { Id = 10, nameOption = "Report", imageClass = " nav-parent nav-active", status = true, isParent = false, parentId = 2 });
            ////menu.Add(new Navbar { Id = 11, nameOption = "GL Report", controller = "GLReports", status = true, isParent = false, parentId = 10 });
            ////menu.Add(new Navbar { Id = 12, nameOption = "Trail Report", controller = "GLReports", status = true, isParent = false, parentId = 10 });


          //  return menu.ToList();


       // }
    }
}