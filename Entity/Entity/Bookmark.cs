using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Bookmark
    {
        public Bookmark()
        {
            this.CreateDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        [StringLength(maximumLength: 500)]
        public string URL { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Display(Name ="Category")]

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created")]
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}
