using Entity;
using Entity.DTOs.StatsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDataGateway
    {
        public List<UserActivityStats> GetUserActivityStats();
        public List<BookmarkStats> GetBookmarkStats();
        public List<BookmarkStats> GetBookmarkTodayStats();
        public  Task StoreUserActivity(int id, string userId);

    }
}
