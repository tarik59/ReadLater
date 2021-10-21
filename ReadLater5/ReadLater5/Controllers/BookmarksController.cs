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

namespace ReadLater5.Controllers
{
    public class BookmarksController : Controller
    {
        private readonly ReadLaterDataContext _context;
        private IBookmarkService _bookmarkService;

        public BookmarksController(ReadLaterDataContext context,IBookmarkService bookmarkService)
        {
            _context = context;
            _bookmarkService = bookmarkService;
        }

        // GET: Bookmarks
        public async Task<IActionResult> Index()
        {
            var readLaterDataContext = await _bookmarkService.GetBookmarks(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(readLaterDataContext);
        }

        // GET: Bookmarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmark = await _bookmarkService.GetBookmark(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (bookmark == null)
            {
                return NotFound();
            }

            return View(bookmark);
        }

        // GET: Bookmarks/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "Name");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,URL,ShortDescription,CategoryId,CreateDate")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                bookmark.CreateDate = DateTime.Now;
                bookmark.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _bookmarkService.CreateBookmark(bookmark);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "Name", bookmark.CategoryId);
            return View(bookmark);
        }

        // GET: Bookmarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmark = await _bookmarkService.GetBookmark(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (bookmark == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "Name", bookmark.CategoryId);
            return PartialView(bookmark);
        }

        // POST: Bookmarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,URL,ShortDescription,CategoryId,CreateDate,user")] Bookmark bookmark)
        {
            if (id != bookmark.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookmark.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await _bookmarkService.UpdateBookmark(bookmark);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookmarkExists(bookmark.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "Name", bookmark.CategoryId);
            return View(bookmark);
        }

        // GET: Bookmarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookmark = await _bookmarkService.GetBookmark(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (bookmark == null)
            {
                return NotFound();
            }

            return PartialView(bookmark);
        }

        // POST: Bookmarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookmark = await _bookmarkService.GetBookmark(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (bookmark == null)
                return NotFound();
            await _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction(nameof(Index));
        }

        private bool BookmarkExists(int id)
        {
            return _context.Bookmark.Any(e => e.ID == id);
        }
    }
}
