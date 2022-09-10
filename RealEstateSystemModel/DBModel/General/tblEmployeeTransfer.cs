using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
  public partial  class tblEmployeeTransfer
    {

        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public string system { get; set; }
        public string IP { get; set; }

        public int fromProjectID { get; set; }
        public int fromDepartmentID { get; set; }
        public int fromDesignation { get; set; }




        public bool updatedata(tblEmployeeTransfer obj)
        {

            try
            {



                using (var db = new HRandPayrollDBEntities())
                {


                    var result= db.tblEmployees.FirstOrDefault(x => x.EmployeeID == obj.EmployeeID);
                    tblEmployeeTransfer objtransfer = new tblEmployeeTransfer();
                    objtransfer.ProjectID = result.ProjectID;
                    objtransfer.DepartmentID = result.DepartmentID;
                    objtransfer.Designation =  result.DesignationID.ToString();
                    objtransfer.EmpCode = result.EmployeeNo;
                    objtransfer.EmployeeID =  Convert.ToInt32( result.EmployeeID);
                    objtransfer.EntryDate = DateTime.Now;
                    objtransfer.EntryIP = obj.EntryIP;
                    objtransfer.EntryID = obj.EntryID;
                    db.tblEmployeeTransfers.Add(objtransfer);
                    db.SaveChanges();

                    var resultUpdate = db.tblEmployees.FirstOrDefault(x => x.EmployeeID == obj.EmployeeID);

                    resultUpdate.EmployeeNo = obj.EmpCode;
                    resultUpdate.DepartmentID = obj.DepartmentID;
                    resultUpdate.DesignationID = Convert.ToInt32( obj.Designation);
                    resultUpdate.ProjectID = obj.ProjectID;

                    db.SaveChanges();
                    return true;

                }


            }
            catch (Exception ex)
            {

                return false;
            }
        }


    }
}
