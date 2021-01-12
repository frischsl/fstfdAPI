using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fstFood.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userID { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string guid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime joined { get; set; }

    }
}
