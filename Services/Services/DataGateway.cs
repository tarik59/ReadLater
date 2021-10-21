using Data;
using Entity;
using Entity.DTOs.StatsData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class DataGateway:IDataGateway
    {
        public ReadLaterDataContext _db { get; }
        public DataGateway(ReadLaterDataContext db)
        {
            _db = db;
        }
        public List<UserActivityStats> GetUserActivityStats()
        {
            return _db.UserActivities
                .Include(u => u.User)
                .Include(b => b.Bookmark)
                .GroupBy(u => u.User.UserName)
                .Select(s => new UserActivityStats
                {
                    Username = s.Key,
                    SumActivity = s.Sum(a => a.Activity)
                }).ToList();
        }
        public List<BookmarkStats> GetBookmarkStats()
        {
            return _db.UserActivities
                .Include(b => b.Bookmark)
                .GroupBy(u => new { u.Bookmark.CreateDate, u.Bookmark.ShortDescription, u.Bookmark.URL })
                .Select(s => new BookmarkStats
                {
                    CreateDate = s.Key.CreateDate,
                    URL = s.Key.URL,
                    ShortDescription = s.Key.ShortDescription,
                    SumActivity = s.Sum(x => x.Activity)
                })
                .OrderByDescending(x => x.SumActivity)
                .Take(3)
                .ToList();
        }
        
        public List<BookmarkStats> GetBookmarkTodayStats()
        {
            return _db.UserActivities
                .Include(b => b.Bookmark)
                .Where(w=>  w.StoredDateTime.Value.Day==DateTime.Now.Day)
                .GroupBy(u => new { u.Bookmark.CreateDate, u.Bookmark.ShortDescription, u.Bookmark.URL })
                .Select(s => new BookmarkStats
                {
                    CreateDate = s.Key.CreateDate,
                    URL = s.Key.URL,
                    ShortDescription = s.Key.ShortDescription,
                    SumActivity = s.Sum(x => x.Activity)
                })
                .OrderByDescending(x => x.SumActivity)
                .ThenByDescending(x=>x.CreateDate)
                .Take(3)
                .ToList();
        }
        public async Task StoreUserActivity(int id,string userId)
        {
            var activity = _db.UserActivities.FirstOrDefault(b => b.BookmarkId == id && b.userId == userId);
            if (activity != null)
            {
                activity.Activity++;
                 _db.Update(activity);
                 await _db.SaveChangesAsync();
            }
            else
            {
                UserActivity userActivity = new UserActivity()
                {
                    userId = userId,
                    BookmarkId = id,
                    Activity = 1,
                    StoredDateTime = DateTime.Now
                };
                await _db.AddAsync(userActivity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
