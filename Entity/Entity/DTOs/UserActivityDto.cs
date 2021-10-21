using Entity.DTOs.StatsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class UserActivityDto
    {
        public UserActivityDto()
        {
            userActivityStats = Enumerable.Empty<UserActivityStats>();
            mostPopularBookmarks = Enumerable.Empty<BookmarkStats>();
        }

        public IEnumerable<UserActivityStats> userActivityStats { get; set; }
        public IEnumerable<BookmarkStats> mostPopularBookmarks { get; set; }
    }
}
