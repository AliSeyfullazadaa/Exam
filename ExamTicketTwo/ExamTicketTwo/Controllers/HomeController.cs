using ExamTicketTwo.DAL;
using ExamTicketTwo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ExamTicketTwo.Controllers
{
    public class HomeController : Controller
    {

        public readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<TeamMember> members = await _context.TeamMembers.ToListAsync();
            return View(members);
        }



    }
}