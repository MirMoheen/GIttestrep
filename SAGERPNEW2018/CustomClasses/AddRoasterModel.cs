using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAGERPNEW2018.CustomClasses
{
    public class AddRoasterModel
    {
        public string city { get; set; }
        public int Docid { get; set; }
        public string startTime { get; set; }
        public int TotalPatient { get; set; }
        public int MinutesPerPatient { get; set; }
        public string ScDate { get; set; }
    }
}