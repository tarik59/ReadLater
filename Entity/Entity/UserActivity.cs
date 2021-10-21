using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UserActivity
    {
        [Key]
        public int ID { get; set; }
        public Bookmark Bookmark { get; set; }
        public ApplicationUser User { get; set; }
        public int Activity { get; set; }
        [ForeignKey("Bookmark")]
        public int BookmarkId { get; set; }
        [ForeignKey("User")]
        public string userId { get; set; }
        public DateTime? StoredDateTime { get; set; }
    }
}
