
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.DBModel
{
    public partial class GLUserGroup
    {


       public bool IsView { get; set; }
        public bool Isedit { get; set; }
        public bool Isdelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsNew { get; set; }
        public bool IsAsign { get; set; }
        public DataTable dtdetail { get; set; }
        public IEnumerable<sp_GLUserGroupDetail_Result> detailistGroup { get; set; }



        public bool DeleteData(int id)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.GLUserGroups.SingleOrDefault(x => x.GroupID == id);
                    if (result != null)
                    {
                        context.GLUserGroups.Remove(result);
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

        public List<UserForm> selectallForm()
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {


                    return context.UserForms.ToList();


                }
                }
            catch (Exception)
            {

                throw;
            }


        }
        public int UpdateData(GLUserGroup obj)
        {
            try
            {
              
                using (var context = new HRandPayrollDBEntities())
                {
                    var result = context.GLUserGroups.SingleOrDefault(x => x.GroupID == obj.GroupID);
                    if (result != null)
                    {
                        result.GroupTitle = obj.GroupTitle;
                        result.Description = obj.Description;
                         result.Entryby = obj.Entryby;
                        result.Inactive = obj.Inactive;
                        result.TimeStamp = DateTime.Now;
                        result.UserID = obj.UserID;
                      
                        context.SaveChanges();

                        List<GLUserGroupDetail> resultofDOC = getGroupdetail(obj.GroupID);
                        if (resultofDOC != null)
                        {
                            context.GLUserGroupDetails.RemoveRange(context.GLUserGroupDetails.Where(c => c.UserGroupID == obj.GroupID));

                        }
                        context.SaveChanges();

                        foreach (DataRow item in dtdetail.Rows)
                        {
                            GLUserGroupDetail detail = new GLUserGroupDetail();
                            // detail.Assign=dtdetail
                            detail.FormsID = Convert.ToInt32(item["FormID"]);
                            detail.UserGroupID = obj.GroupID;
                            detail.Assign = Convert.ToBoolean(item["Assign"]);
                            detail.IsEdit = Convert.ToBoolean(item["Edit"]);
                            detail.IsPrint = Convert.ToBoolean(item["Print"]);
                            detail.IsNew = Convert.ToBoolean(item["New"]);
                            detail.IsDelete = Convert.ToBoolean(item["Delete"]);
                            context.GLUserGroupDetails.Add(detail);
                            context.SaveChanges();


                        }


                    }
                    return result.GroupID;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        public List<sp_GLUserGroupDetail_Result> getGroupdetailbySp(int groupid)
        {

            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.sp_GLUserGroupDetail(groupid).ToList();

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<GLUserGroupDetail> getGroupdetail(int groupid)
        {

            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                  return  context.GLUserGroupDetails.Where(x => x.UserGroupID == groupid).ToList();

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<GLUserGroup> getGroupDataAll()
        {

            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.GLUserGroups.ToList();

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public GLUserGroup getGroupData(int groupid)
        {

            try
            {
                using (var context = new HRandPayrollDBEntities())
                {

                    return context.GLUserGroups.SingleOrDefault(x => x.GroupID == groupid);

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public int addata(GLUserGroup obj)
        {
            try
            {

                using (var context = new HRandPayrollDBEntities())
                {

                   var result = context.GLUserGroups.Add(obj);
                    context.SaveChanges();
                    //  return obj.AdmisssionID;

                    foreach (DataRow item in dtdetail.Rows)
                    {
                        GLUserGroupDetail detail = new GLUserGroupDetail();
                        // detail.Assign=dtdetail
                        detail.FormsID = Convert.ToInt32(item["FormID"]);
                        detail.UserGroupID = obj.GroupID;
                        detail.Assign = Convert.ToBoolean(item["Assign"]);
                        detail.IsEdit = Convert.ToBoolean(item["Edit"]);
                        detail.IsPrint = Convert.ToBoolean(item["Print"]);
                        detail.IsNew = Convert.ToBoolean(item["New"]);
                        detail.IsDelete = Convert.ToBoolean(item["Delete"]);
                        context.GLUserGroupDetails.Add(detail);
                        context.SaveChanges();

                    }

                }

                return obj.GroupID;

            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public List<GLUserGroup> checkDuplicate(int id, string title)
        {
            try
            {
                using (var context = new HRandPayrollDBEntities())
                {
                    if (id > 0)
                    {
                        return context.GLUserGroups.Where(x => x.GroupTitle == title && x.GroupID != id).ToList();

                    }
                    else
                    {
                        return context.GLUserGroups.Where(x => x.GroupTitle == title).ToList();


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
