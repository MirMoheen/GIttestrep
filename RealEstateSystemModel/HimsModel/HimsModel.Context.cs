﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRandPayrollSystemModel.HimsModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class webserverEntities : DbContext
    {
        public webserverEntities()
            : base("name=webserverEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblVaccineRegistration> tblVaccineRegistrations { get; set; }
        public virtual DbSet<VaccineStock> VaccineStocks { get; set; }
        public virtual DbSet<tblAppUser> tblAppUsers { get; set; }
        public virtual DbSet<tblRosterforAppointment> tblRosterforAppointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<patient_registration> patient_registration { get; set; }
        public virtual DbSet<cl_ClinicName> cl_ClinicName { get; set; }
        public virtual DbSet<tblRosterforAppointmentDetail> tblRosterforAppointmentDetails { get; set; }
    
        public virtual ObjectResult<Nullable<decimal>> Insert_Patient_Record(Nullable<int> hid, Nullable<int> cid, Nullable<System.DateTime> ddate, Nullable<long> male, Nullable<long> female, Nullable<long> chlidmale, Nullable<long> chlidfemale, Nullable<long> operations, Nullable<long> medicines, Nullable<long> glasses, Nullable<long> uid, string notes, Nullable<long> operAF, Nullable<long> operCM, Nullable<long> operCF, Nullable<bool> isSchoolScreening, Nullable<int> provinceId, Nullable<int> districtId, Nullable<int> cityId, Nullable<long> maleCataract, Nullable<long> femaleCataract, Nullable<long> childMaleCataract, Nullable<long> childFemaleCataract, Nullable<long> maleOtherC, Nullable<long> femaleOtherC, Nullable<long> mChildOtherC, Nullable<long> fChildOtherC, string campSponsor, Nullable<long> regAMT, Nullable<long> donationAMT, Nullable<long> conjunctivitis, Nullable<long> opacity, Nullable<long> pterygium, Nullable<long> squint, Nullable<long> glaucoma, Nullable<long> others)
        {
            var hidParameter = hid.HasValue ?
                new ObjectParameter("Hid", hid) :
                new ObjectParameter("Hid", typeof(int));
    
            var cidParameter = cid.HasValue ?
                new ObjectParameter("Cid", cid) :
                new ObjectParameter("Cid", typeof(int));
    
            var ddateParameter = ddate.HasValue ?
                new ObjectParameter("ddate", ddate) :
                new ObjectParameter("ddate", typeof(System.DateTime));
    
            var maleParameter = male.HasValue ?
                new ObjectParameter("male", male) :
                new ObjectParameter("male", typeof(long));
    
            var femaleParameter = female.HasValue ?
                new ObjectParameter("female", female) :
                new ObjectParameter("female", typeof(long));
    
            var chlidmaleParameter = chlidmale.HasValue ?
                new ObjectParameter("chlidmale", chlidmale) :
                new ObjectParameter("chlidmale", typeof(long));
    
            var chlidfemaleParameter = chlidfemale.HasValue ?
                new ObjectParameter("chlidfemale", chlidfemale) :
                new ObjectParameter("chlidfemale", typeof(long));
    
            var operationsParameter = operations.HasValue ?
                new ObjectParameter("operations", operations) :
                new ObjectParameter("operations", typeof(long));
    
            var medicinesParameter = medicines.HasValue ?
                new ObjectParameter("medicines", medicines) :
                new ObjectParameter("medicines", typeof(long));
    
            var glassesParameter = glasses.HasValue ?
                new ObjectParameter("glasses", glasses) :
                new ObjectParameter("glasses", typeof(long));
    
            var uidParameter = uid.HasValue ?
                new ObjectParameter("uid", uid) :
                new ObjectParameter("uid", typeof(long));
    
            var notesParameter = notes != null ?
                new ObjectParameter("Notes", notes) :
                new ObjectParameter("Notes", typeof(string));
    
            var operAFParameter = operAF.HasValue ?
                new ObjectParameter("OperAF", operAF) :
                new ObjectParameter("OperAF", typeof(long));
    
            var operCMParameter = operCM.HasValue ?
                new ObjectParameter("OperCM", operCM) :
                new ObjectParameter("OperCM", typeof(long));
    
            var operCFParameter = operCF.HasValue ?
                new ObjectParameter("OperCF", operCF) :
                new ObjectParameter("OperCF", typeof(long));
    
            var isSchoolScreeningParameter = isSchoolScreening.HasValue ?
                new ObjectParameter("IsSchoolScreening", isSchoolScreening) :
                new ObjectParameter("IsSchoolScreening", typeof(bool));
    
            var provinceIdParameter = provinceId.HasValue ?
                new ObjectParameter("ProvinceId", provinceId) :
                new ObjectParameter("ProvinceId", typeof(int));
    
            var districtIdParameter = districtId.HasValue ?
                new ObjectParameter("DistrictId", districtId) :
                new ObjectParameter("DistrictId", typeof(int));
    
            var cityIdParameter = cityId.HasValue ?
                new ObjectParameter("CityId", cityId) :
                new ObjectParameter("CityId", typeof(int));
    
            var maleCataractParameter = maleCataract.HasValue ?
                new ObjectParameter("maleCataract", maleCataract) :
                new ObjectParameter("maleCataract", typeof(long));
    
            var femaleCataractParameter = femaleCataract.HasValue ?
                new ObjectParameter("FemaleCataract", femaleCataract) :
                new ObjectParameter("FemaleCataract", typeof(long));
    
            var childMaleCataractParameter = childMaleCataract.HasValue ?
                new ObjectParameter("ChildMaleCataract", childMaleCataract) :
                new ObjectParameter("ChildMaleCataract", typeof(long));
    
            var childFemaleCataractParameter = childFemaleCataract.HasValue ?
                new ObjectParameter("ChildFemaleCataract", childFemaleCataract) :
                new ObjectParameter("ChildFemaleCataract", typeof(long));
    
            var maleOtherCParameter = maleOtherC.HasValue ?
                new ObjectParameter("MaleOtherC", maleOtherC) :
                new ObjectParameter("MaleOtherC", typeof(long));
    
            var femaleOtherCParameter = femaleOtherC.HasValue ?
                new ObjectParameter("FemaleOtherC", femaleOtherC) :
                new ObjectParameter("FemaleOtherC", typeof(long));
    
            var mChildOtherCParameter = mChildOtherC.HasValue ?
                new ObjectParameter("MChildOtherC", mChildOtherC) :
                new ObjectParameter("MChildOtherC", typeof(long));
    
            var fChildOtherCParameter = fChildOtherC.HasValue ?
                new ObjectParameter("FChildOtherC", fChildOtherC) :
                new ObjectParameter("FChildOtherC", typeof(long));
    
            var campSponsorParameter = campSponsor != null ?
                new ObjectParameter("campSponsor", campSponsor) :
                new ObjectParameter("campSponsor", typeof(string));
    
            var regAMTParameter = regAMT.HasValue ?
                new ObjectParameter("RegAMT", regAMT) :
                new ObjectParameter("RegAMT", typeof(long));
    
            var donationAMTParameter = donationAMT.HasValue ?
                new ObjectParameter("DonationAMT", donationAMT) :
                new ObjectParameter("DonationAMT", typeof(long));
    
            var conjunctivitisParameter = conjunctivitis.HasValue ?
                new ObjectParameter("Conjunctivitis", conjunctivitis) :
                new ObjectParameter("Conjunctivitis", typeof(long));
    
            var opacityParameter = opacity.HasValue ?
                new ObjectParameter("Opacity", opacity) :
                new ObjectParameter("Opacity", typeof(long));
    
            var pterygiumParameter = pterygium.HasValue ?
                new ObjectParameter("Pterygium", pterygium) :
                new ObjectParameter("Pterygium", typeof(long));
    
            var squintParameter = squint.HasValue ?
                new ObjectParameter("Squint", squint) :
                new ObjectParameter("Squint", typeof(long));
    
            var glaucomaParameter = glaucoma.HasValue ?
                new ObjectParameter("Glaucoma", glaucoma) :
                new ObjectParameter("Glaucoma", typeof(long));
    
            var othersParameter = others.HasValue ?
                new ObjectParameter("others", others) :
                new ObjectParameter("others", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Insert_Patient_Record", hidParameter, cidParameter, ddateParameter, maleParameter, femaleParameter, chlidmaleParameter, chlidfemaleParameter, operationsParameter, medicinesParameter, glassesParameter, uidParameter, notesParameter, operAFParameter, operCMParameter, operCFParameter, isSchoolScreeningParameter, provinceIdParameter, districtIdParameter, cityIdParameter, maleCataractParameter, femaleCataractParameter, childMaleCataractParameter, childFemaleCataractParameter, maleOtherCParameter, femaleOtherCParameter, mChildOtherCParameter, fChildOtherCParameter, campSponsorParameter, regAMTParameter, donationAMTParameter, conjunctivitisParameter, opacityParameter, pterygiumParameter, squintParameter, glaucomaParameter, othersParameter);
        }
    
        public virtual int AddDoctorRoasterDetail(Nullable<int> masterid, Nullable<System.DateTime> enterydate, string roastertime, Nullable<System.DateTime> roasterdate, Nullable<int> enteryid, string enteryIp, Nullable<int> cancelid, string cancelip, Nullable<System.DateTime> cancelDate, Nullable<int> city, Nullable<int> docid)
        {
            var masteridParameter = masterid.HasValue ?
                new ObjectParameter("Masterid", masterid) :
                new ObjectParameter("Masterid", typeof(int));
    
            var enterydateParameter = enterydate.HasValue ?
                new ObjectParameter("Enterydate", enterydate) :
                new ObjectParameter("Enterydate", typeof(System.DateTime));
    
            var roastertimeParameter = roastertime != null ?
                new ObjectParameter("Roastertime", roastertime) :
                new ObjectParameter("Roastertime", typeof(string));
    
            var roasterdateParameter = roasterdate.HasValue ?
                new ObjectParameter("Roasterdate", roasterdate) :
                new ObjectParameter("Roasterdate", typeof(System.DateTime));
    
            var enteryidParameter = enteryid.HasValue ?
                new ObjectParameter("Enteryid", enteryid) :
                new ObjectParameter("Enteryid", typeof(int));
    
            var enteryIpParameter = enteryIp != null ?
                new ObjectParameter("EnteryIp", enteryIp) :
                new ObjectParameter("EnteryIp", typeof(string));
    
            var cancelidParameter = cancelid.HasValue ?
                new ObjectParameter("Cancelid", cancelid) :
                new ObjectParameter("Cancelid", typeof(int));
    
            var cancelipParameter = cancelip != null ?
                new ObjectParameter("Cancelip", cancelip) :
                new ObjectParameter("Cancelip", typeof(string));
    
            var cancelDateParameter = cancelDate.HasValue ?
                new ObjectParameter("CancelDate", cancelDate) :
                new ObjectParameter("CancelDate", typeof(System.DateTime));
    
            var cityParameter = city.HasValue ?
                new ObjectParameter("City", city) :
                new ObjectParameter("City", typeof(int));
    
            var docidParameter = docid.HasValue ?
                new ObjectParameter("Docid", docid) :
                new ObjectParameter("Docid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddDoctorRoasterDetail", masteridParameter, enterydateParameter, roastertimeParameter, roasterdateParameter, enteryidParameter, enteryIpParameter, cancelidParameter, cancelipParameter, cancelDateParameter, cityParameter, docidParameter);
        }
    }
}