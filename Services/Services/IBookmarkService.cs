using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookmarkService
    {
        public Task<Bookmark> CreateBookmark(Bookmark bookmark);
        public Task UpdateBookmark(Bookmark bookmark);
        public Task<List<Bookmark>> GetBookmarks(string userID);
        public Task<Bookmark> GetBookmark(int? id,string userid);
        public Task DeleteBookmark(Bookmark bookmark);
    }
}
