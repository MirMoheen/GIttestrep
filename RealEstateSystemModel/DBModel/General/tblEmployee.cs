using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class tblEmployee
    {


        public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }


        public string  FormatProbationEnddate { get; set; }
        public string FormatTerminatedate { get; set; }
        public string FormatLeftdate { get; set; }






        // detail Nominee
        public IEnumerable<string> NomineeName { get; set; }
        public IEnumerable<string> FatherName { get; set; }

        public IEnumerable<string> CNIC { get; set; }

        public IEnumerable<string> Address { get; set; }

        public IEnumerable<string> Mobile { get; set; }

        public IEnumerable<string> Relation { get; set; }

        public IEnumerable<EmployeeNomineeDetail> detailistnomine { get; set; }


     

        //docment detail
        public IEnumerable<EmployeeDocumentDetail> detailistDoc { get; set; }
        public IEnumerable<HttpPostedFileBase> DocFile { get; set; }
        public IEnumerable<string> Docdescription { get; set; }
        public IEnumerable<string> DocpathFile { get; set; }

        //experince detail
        public IEnumerable<EmployeeExprienceDetail> detailistExpernce { get; set; }

        public IEnumerable<string> FirmName { get; set; }
        public IEnumerable<string> StartingDate { get; set; }
        public IEnumerable<string> EndingDate { get; set; }
        public IEnumerable<string> Remarks { get; set; }

        // education detail
        public IEnumerable<EmployeeEducationDetail> detailistEduction { get; set; }

        public IEnumerable<string> Degree { get; set; }
        public IEnumerable<string> Institute { get; set; }
        public IEnumerable<string> Boarduniversity { get; set; }
        public IEnumerable<string> ObtainMarks { get; set; }
        public IEnumerable<string> TotalMarks { get; set; }
        public IEnumerable<string> PassingYear { get; set; }
       


        public string base64image { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
        //public sp_getCustomerdata_Result getCustomerdata { get; set; }
   
        public IEnumerable<sp_getAllEmployeeData_Result> employedatalistObbj { get; set; }
        public IEnumerable<SelectListItem> LoadDesignation(int departmentId)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    if (departmentId == 0)
                    {
                         listobj.AddRange(context.tblDesignations.Where(x=> x.inactive == false).OrderBy(x=>x.OrderNO).Select(x => new SelectListItem { Text = x.DesignationTitle, Value = x.DesignationID.ToString() }).ToList());

                    }
                    else
                    {
                        listobj.AddRange(context.tblDesignations.Where(x => x.DepartmentID == departmentId && x.inactive == false).OrderBy(x => x.OrderNO).Select(x => new SelectListItem { Text = x.DesignationTitle, Value = x.DesignationID.ToString() }).ToList());
                    }

                    return listobj;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public IEnumerable<SelectListItem> loadEmployee(int? Desigantion)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tblEmployees.Where(x => x.inactive == false && x.DesignationID == Desigantion).Select(x => new SelectListItem { Text = x.EmployeeName + " - " + x.EmployeeNo, Value = x.EmployeeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


    



        public IEnumerable<SelectListItem> loadProject()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.tblProjects.Where(x => x.Inactive == false).Select(x => new SelectListItem { Text = x.ProjectName, Value = x.ProjectID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> loadDepartment(int projectid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                  
                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- All --", Value = "0" });

                    listobj.AddRange(context.tblDepartments.Where(x => x.Inactive == false && x.ProjectID== projectid).Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> loadDepartmentAll(int projectid)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "All", Value = "0" });

                    listobj.AddRange(context.tblDepartments.Where(x => x.Inactive == false && x.ProjectID == projectid).Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.DepartmentID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> LoadEmployeetype()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.EmployeeTypes.Where(x => x.inactive == false).Select(x => new SelectListItem { Text = x.EmployeeTypeName, Value = x.EmpoyeeTypeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public IEnumerable<SelectListItem> LoadBloodgroup()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                    listobj.Add(new SelectListItem { Text = "O-", Value = "O-" });
                    listobj.Add(new SelectListItem { Text = "O+", Value = "O+" });
                    listobj.Add(new SelectListItem { Text = "A-", Value = "A-" });
                    listobj.Add(new SelectListItem { Text = "A+", Value = "A+" });
                    listobj.Add(new SelectListItem { Text = "B-", Value = "B-" });
                    listobj.Add(new SelectListItem { Text = "B+", Value = "B+" });
                    listobj.Add(new SelectListItem { Text = "AB-", Value = "AB-" });
                    listobj.Add(new SelectListItem { Text = "AB+", Value = "AB+" });
                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> LoadEmployeStatus()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                   listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                    listobj.Add(new SelectListItem { Text = "Resigned", Value = "Resigned" });
                    listobj.Add(new SelectListItem { Text = "Terminate", Value = "Terminate" });
                   


                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public IEnumerable<SelectListItem> LoadEmployeClearStatus()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                     listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                    listobj.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
                    listobj.Add(new SelectListItem { Text = "Clear", Value = "Clear" });
                    listobj.Add(new SelectListItem { Text = "Partial", Value = "Partial" });
                    listobj.Add(new SelectListItem { Text = "Full", Value = "Full" });

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> LoadSeniorLevel()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });
                    listobj.Add(new SelectListItem { Text = "1th", Value = "1" });
                    listobj.Add(new SelectListItem { Text = "2nd", Value = "2" });
                    listobj.Add(new SelectListItem { Text = "3rd", Value = "3" });
                    listobj.Add(new SelectListItem { Text = "4th", Value = "4" });
                    listobj.Add(new SelectListItem { Text = "5th", Value = "5" });
                    
                  
                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<SelectListItem> loadGrade()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.GradeEmployees.Where(x => x.inactive == false).Select(x => new SelectListItem { Text = x.GradeTitle, Value = x.GradeID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }





        public IEnumerable<SelectListItem> loadBanks()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.BankInformations.Where(x => x.inactive == false).Select(x => new SelectListItem { Text = x.BankName, Value = x.BankID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public IEnumerable<SelectListItem> loadCities()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    List<SelectListItem> listobj = new List<SelectListItem>();
                    listobj.Add(new SelectListItem { Text = "-- Please Select --", Value = "0" });

                    listobj.AddRange(context.CityInformtions.Where(x => x.inactive == false).Select(x => new SelectListItem { Text = x.CityName, Value = x.CityID.ToString() }).ToList());

                    return listobj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public List<sp_getAllEmployeeData_Result> getallemplyeedata(string where)
        {

            try
            {

                using (var context = new HRandPayrollDBEntities())
                {


                    return context.sp_getAllEmployeeData(where).ToList();
                }

            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public string genearteEmployeeNo(int? projectID, int? departmentid)
        {

            try
            {


                using (var context = new HRandPayrollDBEntities())
                {
                    string projectorderNo = context.tblProjects.FirstOrDefault(x => x.ProjectID == projectID).ProjectCode.ToString();
                    string deptorderNo = context.tblDepartments.FirstOrDefault(x => x.DepartmentID == departmentid).DepartmentCode.ToString();
                   var reuslts= context.tblEmployees.ToList();
                    if (reuslts != null && reuslts.Count >0)
                    {
                        int employeeNextID = Convert.ToInt32(context.tblEmployees.Max(x => x.EmployeeID)) + 1;
                        return projectorderNo + deptorderNo + employeeNextID.ToString("D4");

                    }
                    else
                    {
                        return projectorderNo + deptorderNo + 1.ToString("D4");
                    }
                  
                }


            }
            catch (Exception ex)
            {

                return null;
            }


        }


        public List<EmployeeNomineeDetail> getdetailistnomineData(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.EmployeeNomineeDetails.Where(x => x.EmployeeID == id).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<EmployeeDocumentDetail> getdetailistDocumentData(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.EmployeeDocumentDetails.Where(x => x.EmployeeID == id).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<EmployeeEducationDetail> getdetailistEductionData(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.EmployeeEducationDetails.Where(x => x.EmployeeID == id).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<EmployeeExprienceDetail> getdetailistExprienceData(int? id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.EmployeeExprienceDetails.Where(x => x.EmployeeID == id).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string storeImagePath(string base64image)
        {

            String path = HttpContext.Current.Server.MapPath("~/AppFiles/Images/"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = Guid.NewGuid().ToString().Substring(0, 4) + ".jpg";
            var t = base64image.Substring(23);  // remove data:image/png;base64,
            string Temppath = "~/AppFiles/Images/" + imageName;
            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(t);

            File.WriteAllBytes(imgPath, imageBytes);

            return Temppath;

            // if (string.IsNullOrEmpty(base64image))
            //     return "";

            // var t = base64image.Substring(23);  // remove data:image/png;base64,

            // byte[] bytes = Convert.FromBase64String(t);

            // Image image;
            // using (MemoryStream ms = new MemoryStream(bytes))
            // {
            //     image = Image.FromStream(ms);
            // }

            // var randomFileName = Guid.NewGuid().ToString().Substring(0, 4) + ".png";
            // var fullPath = Path.Combine(HttpContext.Current. Server.MapPath("~/AppFiles/Images/"), randomFileName);
            // image.Save("\\AppFiles\\Images\\b354.png", System.Drawing.Imaging.ImageFormat.Png);

            //string asdasd= "~/AppFiles/Images/" + randomFileName;
            // image.Save(Path.Combine((HttpContext.Current.Server.MapPath("~/AppFiles/Images/")), randomFileName));

            ///  return fullPath;

        }



        public tblEmployee addata(tblEmployee obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                     
                            context.tblEmployees.Add(obj);
                            context.SaveChanges();

                            int a = 0;
                            foreach (var item in obj.NomineeName)
                            {
                                EmployeeNomineeDetail objdss = new EmployeeNomineeDetail();
                                objdss.NomineeName = item;
                                objdss.EmployeeID = Convert.ToInt32(obj.EmployeeID);
                                objdss.FatherName = obj.FatherName.ToArray()[a];
                                objdss.Mobile = obj.Mobile.ToArray()[a];
                                objdss.CNIC = obj.CNIC.ToArray()[a];


                                objdss.Address = obj.Address.ToArray()[a];
                                objdss.Relation = obj.Relation.ToArray()[a];
                                a++;
                                context.EmployeeNomineeDetails.Add(objdss);
                                context.SaveChanges();

                            }


                            //insert image path 
                            int b = 0;
                            foreach (var item in obj.DocpathFile)
                            {
                                EmployeeDocumentDetail objdoc = new EmployeeDocumentDetail();

                                objdoc.DescriptionDoc = obj.Docdescription == null ? "" : obj.Docdescription.ToArray()[b];
                                objdoc.EmployeeID = Convert.ToInt32(obj.EmployeeID);
                                objdoc.DocImagePath = obj.DocpathFile.ToArray()[b];
                                b++;
                                context.EmployeeDocumentDetails.Add(objdoc);
                                context.SaveChanges();

                            }

                            ///experince
                            int c = 0;
                            foreach (var item in obj.FirmName)
                            {
                                EmployeeExprienceDetail objcexpr = new EmployeeExprienceDetail();

                                objcexpr.FirmName = item;
                                objcexpr.EmployeeID = Convert.ToInt32(obj.EmployeeID);
                                objcexpr.Firmaddress = obj.FirmName == null ? "" : obj.FirmName.ToArray()[c];
                                objcexpr.StartingDate = Convert.ToDateTime( obj.StartingDate.ToArray()[c]);
                                objcexpr.EndingDate = Convert.ToDateTime( obj.EndingDate.ToArray()[c]);
                                objcexpr.Remarks = obj.Remarks == null ? "" : obj.Remarks.ToArray()[c];

                                c++;
                                context.EmployeeExprienceDetails.Add(objcexpr);
                                context.SaveChanges();

                            }

                            //eduction


                            ///experince
                            int d = 0;
                            foreach (var item in obj.Degree)
                            {
                                EmployeeEducationDetail objeduction = new EmployeeEducationDetail();

                                objeduction.Degree = item;
                                objeduction.EmployeeID = Convert.ToInt32(obj.EmployeeID);

                                objeduction.Boarduniversity = obj.Boarduniversity == null ? "" : obj.Boarduniversity.ToArray()[d];
                                objeduction.Institute = obj.Institute == null ? "" : obj.Institute.ToArray()[d];
                                objeduction.ObtainMarks = obj.ObtainMarks == null ? "" : obj.ObtainMarks.ToArray()[d];
                                objeduction.TotalMarks = obj.TotalMarks == null ? "" : obj.TotalMarks.ToArray()[d];
                                objeduction.PassingYear = obj.PassingYear == null ? "" : obj.PassingYear.ToArray()[d];


                                d++;
                                context.EmployeeEducationDetails.Add(objeduction);
                                context.SaveChanges();

                            }


                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }


                

                    return obj;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<tblEmployee> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblEmployees.Where(x => x.EmployeeCNIC == title && x.EmployeeID != id).ToList();

                    }
                    else
                    {
                        return context.tblEmployees.Where(x => x.EmployeeCNIC == title).ToList();


                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<tblEmployee> checkMachine(int id, long Machine)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.tblEmployees.Where(x => x.Machineid == Machine && x.EmployeeID != id).ToList();

                    }
                    else
                    {
                        return context.tblEmployees.Where(x => x.Machineid == Machine).ToList();


                    }

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public tblEmployee getAlldataByID(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    return context.tblEmployees.SingleOrDefault(x => x.EmployeeID == id);

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public bool UpdateData(tblEmployee obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var result = context.tblEmployees.SingleOrDefault(x => x.EmployeeID == obj.EmployeeID);
                            if (result != null)
                            {
                                result.EmployeeNo = obj.EmployeeNo;
                         
                                result.EmployeeName = obj.EmployeeName;
                                result.Relationship = obj.Relationship;
                                result.RelativeName = obj.RelativeName;
                                result.EmployeeCNIC = obj.EmployeeCNIC;
                                result.Nationality = obj.Nationality;
                                result.DateOfBirth = obj.DateOfBirth;
                                result.PresentAddress1 = obj.PresentAddress1;
                                result.PresentAddress2 = obj.PresentAddress2;
                                result.PresntCountry = obj.PresntCountry;
                                result.PresentCity = obj.PresentCity;
                                result.PermanentAddress1 = obj.PermanentAddress1;
                                result.PermanentAddress2 = obj.PermanentAddress2;
                                result.PermanentCountry = obj.PermanentCountry;
                                result.PermanentCity = obj.PermanentCity;
                                result.OfficePhone = obj.OfficePhone;
                                result.ResPhone = obj.ResPhone;
                                result.MobileNo = obj.MobileNo;
                                result.Fax = obj.Fax;
                                result.Email = obj.Email;
                                result.EmployeePhotoPath = obj.EmployeePhotoPath;
                                result.IsMinute = obj.IsMinute;
                                result.EMIno = obj.EMIno;



                                result.DateofJoining = obj.DateofJoining;
                                result.Machineid = obj.Machineid;
                                result.DepartmentID = obj.DepartmentID;
                                result.DesignationID = obj.DesignationID;
                                result.MatrialStatus = obj.MatrialStatus;
                                result.Gender = obj.Gender;
                                result.Bloodgroup = obj.Bloodgroup;
                                result.EmergencyContact = obj.EmergencyContact;
                                result.EmergencyPersonName = obj.EmergencyPersonName;
                                result.Seniortylevel = obj.Seniortylevel;
                                result.GradeID = obj.GradeID;
                                result.inactive = obj.inactive;
                                result.ModifiedID = obj.EntryID;
                                result.ModifiedDatetime = DateTime.Now;
                                result.ModifiedIP = obj.ModifiedIP;


                                context.SaveChanges();

                            

                                List<EmployeeNomineeDetail> resultofDOC = getdetailistnomineData(Convert.ToInt32(obj.EmployeeID));
                                if (resultofDOC != null)
                                {
                                    context.EmployeeNomineeDetails.RemoveRange(context.EmployeeNomineeDetails.Where(q => q.EmployeeID == obj.EmployeeID));

                                }
                                context.SaveChanges();
                                int a = 0;
                                foreach (var item in obj.NomineeName)
                                {
                                    EmployeeNomineeDetail objdss = new EmployeeNomineeDetail();
                                    objdss.NomineeName = item;
                                    objdss.EmployeeID = Convert.ToInt32(obj.EmployeeID);
                                    objdss.FatherName = obj.FatherName.ToArray()[a];
                                    objdss.Mobile = obj.Mobile.ToArray()[a];
                                    objdss.CNIC = obj.CNIC.ToArray()[a];


                                    objdss.Address = obj.Address.ToArray()[a];
                                    objdss.Relation = obj.Relation.ToArray()[a];
                                    a++;
                                    context.EmployeeNomineeDetails.Add(objdss);
                                    context.SaveChanges();

                                }


                                List<EmployeeDocumentDetail> resultDOC = getdetailistDocumentData(Convert.ToInt32(obj.EmployeeID));
                                if (resultDOC != null)
                                {
                                    context.EmployeeDocumentDetails.RemoveRange(context.EmployeeDocumentDetails.Where(q => q.EmployeeID == obj.EmployeeID));

                                }

                                int b = 0;
                                foreach (var item in obj.DocpathFile)
                                {
                                    EmployeeDocumentDetail objdoc = new EmployeeDocumentDetail();

                                    objdoc.DescriptionDoc = obj.Docdescription == null ? "" : obj.Docdescription.ToArray()[b];
                                    objdoc.EmployeeID = Convert.ToInt32(obj.EmployeeID);
                                    objdoc.DocImagePath = obj.DocpathFile.ToArray()[b];
                                    b++;
                                    context.EmployeeDocumentDetails.Add(objdoc);
                                    context.SaveChanges();

                                }


                                List<EmployeeExprienceDetail> resultExp = getdetailistExprienceData(Convert.ToInt32(obj.EmployeeID));
                                if (resultExp != null)
                                {
                                    context.EmployeeExprienceDetails.RemoveRange(context.EmployeeExprienceDetails.Where(q => q.EmployeeID == obj.EmployeeID));

                                }
                                int c = 0;
                                foreach (var item in obj.FirmName)
                                {
                                    EmployeeExprienceDetail objcexpr = new EmployeeExprienceDetail();

                                    objcexpr.FirmName = item;
                                    objcexpr.EmployeeID = Convert.ToInt32(obj.EmployeeID);
                                    objcexpr.Firmaddress = obj.FirmName == null ? "" : obj.FirmName.ToArray()[c];
                                    objcexpr.StartingDate = Convert.ToDateTime(obj.StartingDate.ToArray()[c]);
                                    objcexpr.EndingDate = Convert.ToDateTime(obj.EndingDate.ToArray()[c]);
                                    objcexpr.Remarks = obj.Remarks == null ? "" : obj.Remarks.ToArray()[c];

                                    c++;
                                    context.EmployeeExprienceDetails.Add(objcexpr);
                                    context.SaveChanges();

                                }



                                List<EmployeeEducationDetail> resultedu = getdetailistEductionData(Convert.ToInt32(obj.EmployeeID));
                                if (resultedu != null)
                                {
                                    context.EmployeeEducationDetails.RemoveRange(context.EmployeeEducationDetails.Where(q => q.EmployeeID == obj.EmployeeID));

                                }
                                int d = 0;
                                foreach (var item in obj.Degree)
                                {
                                    EmployeeEducationDetail objeduction = new EmployeeEducationDetail();

                                    objeduction.Degree = item;
                                    objeduction.EmployeeID = Convert.ToInt32(obj.EmployeeID);

                                    objeduction.Boarduniversity = obj.Boarduniversity == null ? "" : obj.Boarduniversity.ToArray()[d];
                                    objeduction.Institute = obj.Institute == null ? "" : obj.Institute.ToArray()[d];
                                    objeduction.ObtainMarks = obj.ObtainMarks == null ? "" : obj.ObtainMarks.ToArray()[d];
                                    objeduction.TotalMarks = obj.TotalMarks == null ? "" : obj.TotalMarks.ToArray()[d];
                                    objeduction.PassingYear = obj.PassingYear == null ? "" : obj.PassingYear.ToArray()[d];


                                    d++;
                                    context.EmployeeEducationDetails.Add(objeduction);
                                    context.SaveChanges();

                                }


                            }

                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback(); //Required according to MSDN article 
                            throw; //Not in MSDN article, but recommended so the exception still bubbles up
                        }
                    }
                    return true;

                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }



        public tblEmployee getEmployeeData(int empid )
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                    return  context.tblEmployees.SingleOrDefault(x => x.EmployeeID == empid);
                   
                  
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public bool UpdateconfirmData(tblEmployee obj)
        //{
        //    try
        //    {

        //        using (var context = new HRandPayrollDBEntities())
        //        {
        //            var result = context.tblEmployees.SingleOrDefault(x => x.EmployeeID == obj.EmployeeID);
        //            if (result != null)
        //            {
        //                if (result.ProbationEnd==null)
        //                {
        //                    result.ProbationEnd = obj.ProbationEnd;
        //                }


        //                if (obj.IsConfirm )
        //                {
        //                    result.IsConfirm = obj.IsConfirm;

        //                    result.EmployeetypeID = obj.EmployeetypeID;


        //                }

        //              if (obj.IsTerminate && result.TerminateDate == null)
        //                {
        //                    result.IsTerminate = obj.IsTerminate;
        //                    result.EmployeetypeID = obj.EmployeetypeID;


        //                }
        //               if (obj.IsLeft && result.LeftDate == null)
        //                {
        //                    result.IsLeft = obj.IsLeft;
        //                    result.LeftDate = obj.LeftDate;
        //                    result.EmployeetypeID = obj.EmployeetypeID;


        //                }



        //                context.SaveChanges();

        //                return true;

        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public bool updateEmpployeStatus(tblEmployee obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.tblEmployees.SingleOrDefault(x => x.EmployeeID == obj.EmployeeID);
                    if (result != null)
                    {
                        result.EmployeeStatusDate = obj.EmployeeStatusDate;
                        result.RemarksStatus = obj.RemarksStatus;
                        result.EmployeeStatus = obj.EmployeeStatus;
                        result.EmpClearanceStatus = obj.EmpClearanceStatus;
                        result.inactive = true;


                        context.SaveChanges();

                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
