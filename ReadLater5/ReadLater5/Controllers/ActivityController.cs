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
using Entity.DTOs;
using Entity.DTOs.StatsData;
using ReadLater5.Models;
using ReadLater5.Helpers;

namespace ReadLater5.Controllers
{
    
    public class ActivityController : Controller
    {
        private readonly IDataGateway _datagate;

        public ReadLaterDataContext _db { get; }
        public ActivityController(ReadLaterDataContext db,IDataGateway dataGateway)
        {
            _db = db;
            _datagate = dataGateway;
        }
        [HttpPost]
        public async Task<ActionResult> StoreUserActivity([FromBody]int id)
        {
            await _datagate.StoreUserActivity(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok();
        }
        [HttpGet]
        public IActionResult ActivityDashboard()
        {
            var generatedDashboard = new DashboardGenerator(_datagate).GenerateDashboard();

            return View(generatedDashboard);
        }
        [HttpGet]
        public IActionResult WidgetDashboard()
        {
            var generatedDashboard = new DashboardGenerator(_datagate).GenerateWidgetDashboard();

            return PartialView(generatedDashboard);
        }
    }
}
