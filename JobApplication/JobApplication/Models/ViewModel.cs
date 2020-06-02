using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class ViewModel
    {
        public List<CheckBoxViewModel> PositionTypesCheckBoxes { get; set; }
        public Candidate Candidate { get; set; }
        public IList<Position> Positions { get; set; }

    }
}
