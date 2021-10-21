
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.StatsData
{
    public class UserActivityStats
    {
        public int SumActivity { get; set; }
        public string Username { get; set; }
    }
    public class BookmarkStats
    {

        public string URL { get; set; }
        public string ShortDescription { get; set; }

        public DateTime CreateDate { get; set; }
        public Category category { get; set; }
        public int SumActivity { get; set; }
    }
}
