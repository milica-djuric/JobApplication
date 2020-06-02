using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class Application
    {
        public int CandidateID { get; set; }
        public Candidate Candidate { get; set; }
        public int PositionID { get; set; }
        public int CompanyID { get; set; }
        public Position Position { get; set; }

        public override bool Equals(object obj)
        {
            Application a = (Application)obj;
            return a.CandidateID == CandidateID && a.PositionID == PositionID && a.CompanyID == CompanyID;
        }
    }
}
