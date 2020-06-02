using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class Candidate
    {
        [Key]
        public int CandidateID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [MaxLength(30)]
        public string NameSurename { get; set; }
        [Required(ErrorMessage = "Enter email address in format 'example@mail.com'!")]
        [MaxLength(30)]
        public string Email { get; set; }
        [JsonIgnore]
        public List<Application> Applications { get; set; }

        [ScaffoldColumn(false)]
        public string Positions
        {
            get
            {
                if (Applications != null)
                {
                    string s = "";
                    foreach (Application a in Applications)
                    {
                        s += $"{a.Position.PositionName}, ";
                    }
                    return (s.Length == 0) ? s : s.Substring(0, s.Length - 2);
                }
                else
                {
                    return "";
                }
            }
        }

    }
}
