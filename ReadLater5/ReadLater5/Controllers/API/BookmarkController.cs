using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Entity;
using Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ReadLater5.Helpers;

namespace ReadLater5.Controllers.API
{
    [Authorize]
    public class BookmarkController : BaseApiController
    {
        public ReadLaterDataContext _db { get; }
        private readonly IDataGateway _datagate;
        public BookmarkController(ReadLaterDataContext db, IDataGateway dataGateway)
        {
            _db = db;
            _datagate = dataGateway;
        }
        [HttpGet("bookmarks")]
        public async Task<ActionResult<List<Bookmark>>> GetBookmarks()
        {
            return  Ok(await _db.Bookmark.Include(m => m.Category).ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookmark>> GetBookmark(int id)
        {
            var bookmark = await _db.Bookmark.Include(c => c.Category).FirstOrDefaultAsync(b => b.ID == id);
            if(bookmark == null)
                return NotFound("Bookmark not found");
            return Ok(bookmark);
        }
        [HttpPost("create")]
        public async Task<ActionResult> CreateBookmark(Bookmark bookmark)
        { 
            _db.Add(bookmark);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("update")]
        public async Task<ActionResult> UpdateBookmark(Bookmark bookmark)
        {
            //var db_bookmark= await _db.Bookmark.Include(m => m.Category).FirstOrDefaultAsync(b => b.ID == b.ID);
            //if (bookmark == null) return NotFound("Bookmark not found");
            _db.Update(bookmark);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteBookmark(int id)
        {
            var bookmark = await _db.Bookmark.FirstOrDefaultAsync(b => b.ID == id);
            if (bookmark == null) return NotFound("Bookmark not found");
            _db.Remove(bookmark);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("dashboard")]
        public ActionResult ActivityDashboard()
        {
            var generatedDashboard = new DashboardGenerator(_datagate).GenerateWidgetDashboard();

            return RedirectToAction("WidgetDashboard", "Activity",generatedDashboard);
        }
        //Import this line of code, for displaying the activity dashboard in your app
        // <iframe src="https://localhost:44326/api/bookmark/dashboard" width="500" height="500"></iframe>
    }
}
