using Entity.DTOs;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Helpers
{
    public class DashboardGenerator
    {
        private readonly IDataGateway _datagate;
        public DashboardGenerator(IDataGateway datagate)
        {
            _datagate = datagate;
        }
        public UserActivityDto GenerateDashboard()
        {
            var userActivityStats = _datagate.GetUserActivityStats();

            var mostPopularBookmarks = _datagate.GetBookmarkStats();

            UserActivityDto userActivity = new UserActivityDto()
            {
                userActivityStats = userActivityStats,
                mostPopularBookmarks = mostPopularBookmarks
            };
            return userActivity;
        }
        public UserActivityDto GenerateWidgetDashboard()
        {
            var mostPopularBookmarks = _datagate.GetBookmarkTodayStats();

            UserActivityDto userActivity = new UserActivityDto()
            {
                mostPopularBookmarks = mostPopularBookmarks
            };
            return userActivity;
        }
    }
}
