using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class Position
    {
        public int PositionID { get; set; }
        [MaxLength(30)]
        [Required]
        public string PositionName { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        [JsonIgnore]
        public List<Application> Applications { get; set; }
    }
}
