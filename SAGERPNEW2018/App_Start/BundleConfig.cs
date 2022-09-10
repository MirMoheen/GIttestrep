using System.Web;
using System.Web.Optimization;

namespace SAGERPNEW2018
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            
            
            
            //// dataTables 
            //            bundles.Add(new ScriptBundle("~/Content/dataTables1").Include(
            //          "~/Content/dataTables1/datatables.min.js",
            //          "~/Scripts/datatable/Buttons-1.5.1/js/buttons.colVis.js"
                    
            //          ));

            //// dataTables css styles
            //bundles.Add(new StyleBundle("~/Content/dataTables/dataTablesStyles").Include(
            //        //  "~/Content/dataTables/datatables.min.css",
            //          "~/Scripts/datatable/Buttons-1.5.1/css/buttons.dataTables.min.css",
            //          "~/Scripts/DataTables/Buttons-1.5.1/css/buttons.bootstrap4.min.css",
            //          "~/Scripts/DataTables/DataTables-1.10.16/css/dataTables.bootstrap4.min.css",
            //           "~/Scripts/DataTables/Buttons-1.5.1/css/buttons.jqueryui.min.css",
            //           "~/Scripts/DataTables/Buttons-1.5.1/css/buttons.bootstrap.min.css",
            //           "~/Scripts/DataTables/Responsive-2.2.1/css/responsive.dataTables.min.css",
                       
            //           "~/Scripts/DataTables/Buttons-1.5.1/css/buttons.semanticui.min.css"));


       //     @*< link href = "~/Scripts/datatable/Buttons-1.5.1/css/buttons.dataTables.min.css" rel = "stylesheet" />
   
       //< link href = "~/Scripts/DataTables/Buttons-1.5.1/css/buttons.bootstrap4.min.css" rel = "stylesheet" />
      
       //   < link href = rel = "stylesheet" />
         
       //      < link href = "~/Scripts/DataTables/Buttons-1.5.1/css/buttons.semanticui.min.css" rel = "stylesheet" />
            
       //         < link href =  rel = "stylesheet" />
               
       //            < link href = rel = "stylesheet" />
                  
       //               < link href =  rel = "stylesheet" /> *@


     //          < script src = "/Scripts/DataTables-1.10.16/media/js/jquery.dataTables.js" ></ script >
 
     //< script src = "/Scripts/DataTables-1.10.16/media/js/jquery.dataTables.min.js" ></ script >
  
     // < script src = "/Scripts/Buttons-master/js/buttons.dataTables.js" ></ script >
   
     //  < script src = "/Scripts/Buttons-master/js/dataTables.buttons.js" ></ script >
    
     //   < script src = "/Scripts/Buttons-master/js/buttons.bootstrap4.js" ></ script >
     
     //    < script src = "/Scripts/datatable/JSZip-2.5.0/jszip.min.js" ></ script >
      
     //     < script src = "/Scripts/datatable/pdfmake-0.1.32/pdfmake.min.js" ></ script >
       
     //      < script src = "/Scripts/datatable/pdfmake-0.1.32/vfs_fonts.js" ></ script >
        
     //       < script src = "/Scripts/datatable/Buttons-1.5.1/js/buttons.print.js" ></ script >
         
     //        < script src = "/Scripts/datatable/Buttons-1.5.1/js/buttons.html5.js" ></ script >
          
     //         < script src = "/Scripts/datatable/Buttons-1.5.1/js/dataTables.buttons.js" ></ script >
           
     //          < script src = "~/Scripts/datatable/Buttons-1.5.1/js/buttons.colVis.js" ></ script > *@


        }
    }
}

