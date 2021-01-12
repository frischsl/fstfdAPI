using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fstFood.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int postID { get; set; }
        public string title { get; set; }
        public string mealPlanURL { get; set; }
        public string comment { get; set; }
        public int userID { get; set; }
        public string imageUrl { get; set; }
        public string type { get; set; }
        public DateTime postedAt { get; set; }


    }
}
