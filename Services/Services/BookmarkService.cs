using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookmarkService: IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;

        public BookmarkService(ReadLaterDataContext readLaterDataContext)
        {

            _ReadLaterDataContext = readLaterDataContext;
        }
        public async Task<Bookmark> CreateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Bookmark.Add(bookmark);
           await _ReadLaterDataContext.SaveChangesAsync();
            return bookmark;
        }
        public async Task UpdateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Update(bookmark);
            await _ReadLaterDataContext.SaveChangesAsync();
        }
        public async Task<List<Bookmark>> GetBookmarks(string userID)
        {
            return await _ReadLaterDataContext.Bookmark.Include(b => b.Category).Include(u=>u.user).Where(user=>user.UserId==userID).ToListAsync();
        }
        public async Task<Bookmark> GetBookmark(int? id,string userid)
        {
            return await _ReadLaterDataContext.Bookmark.Include(b => b.Category).SingleOrDefaultAsync(m => m.ID == id && m.UserId==userid);
        }
        public async Task DeleteBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Bookmark.Remove(bookmark);
            await _ReadLaterDataContext.SaveChangesAsync();

        }

    }
}
