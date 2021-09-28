using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsInCompany.Model.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Patronymic { get; set; }

        [Required] 
        public string Organization { get; set; }

        [Required] 
        public string VisitGoal { get; set; }

        [Required] 
        public string Attendant { get; set; }

        public string Note { get; set; }

        [Required] 
        public string EntryTime { get; set; }

        public string ExitTime { get; set; }
    }
}
