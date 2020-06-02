using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class CheckBoxViewModel
    {
        public bool IsActive { get; set; }
        public string Display { get; set; }
        public int PositionID { get; set; } // <positionID, companyID>
        public int CompanyID { get; set; }
    }
}
