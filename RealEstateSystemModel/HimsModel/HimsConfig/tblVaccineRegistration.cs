using HRandPayrollSystemModel.HimsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRandPayrollSystemModel.HimsModel
{

    public partial   class tblVaccineRegistration
    {



        public tblVaccineRegistration InsertVaccine(tblVaccineRegistration obj)
        {
            try
            {
                using (var context = new webserverEntities())
                {
                     context.tblVaccineRegistrations.Add(obj);
                    context.SaveChanges();

                    var result  =  context.VaccineStocks.FirstOrDefault();
                    if (result!=null)
                    {
                        result.AvaliableforPatient = result.AvaliableforPatient - 1;
                        result.Used = result.Used + 1;

                        context.SaveChanges();
                    }


                    return obj;

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public tblVaccineRegistration CheckCnic(string  cnic)
        {
            try
            {
                using (var context = new webserverEntities())
                {
                    return context.tblVaccineRegistrations.FirstOrDefault(x=>x.Cnic==cnic && x.isCancel==false) ;
                   

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public int? getAvaliableforPatient()
        {
            try
            {
                using (var context = new webserverEntities())
                {
                    return context.VaccineStocks.FirstOrDefault().AvaliableforPatient;


                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }






    }
}
