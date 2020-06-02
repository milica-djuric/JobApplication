using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        [MaxLength(30)]
        [Required]
        public string CompanyName { get; set; }
        [JsonIgnore]
        public List<Position> Positions { get; set; }
    }
}
