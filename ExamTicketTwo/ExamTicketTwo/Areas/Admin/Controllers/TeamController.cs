using ExamTicketTwo.DAL;
using ExamTicketTwo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamTicketTwo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {

        public readonly AppDbContext _context;
        public readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<TeamMember> members = await _context.TeamMembers.ToListAsync();
            return View(members);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMemberCreateDto teamMember)
        {
            if (!ModelState.IsValid)
            {
                return View(teamMember);
            }
            bool isExsisFullName = await _context.TeamMembers.AnyAsync(c => c.FullName.ToLower().Trim() == teamMember.FullName.ToLower().Trim());
            bool isExsisProfession = await _context.TeamMembers.AnyAsync(c => c.Profession.ToLower().Trim() == teamMember.Profession.ToLower().Trim());
            bool isExsisFacebook = await _context.TeamMembers.AnyAsync(c => c.FacebookUrl.ToLower().Trim() == teamMember.FacebookUrl.ToLower().Trim());
            bool isExsisTwitter = await _context.TeamMembers.AnyAsync(c => c.TwitterUrl.ToLower().Trim() == teamMember.TwitterUrl.ToLower().Trim());
            bool isExsisInstagram = await _context.TeamMembers.AnyAsync(c => c.InstagramUrl.ToLower().Trim() == teamMember.InstagramUrl.ToLower().Trim());

            if (isExsisFullName)
            {
                ModelState.AddModelError("FullName", "Already exsist");
                return View();
            }
            if (isExsisProfession) { ModelState.AddModelError("Profession", "Already exsist"); }
            if (isExsisFacebook) { ModelState.AddModelError("Facebook", "Already exsist"); }
            if (isExsisTwitter) { ModelState.AddModelError("Twitter", "Already exsist"); }
            if (isExsisInstagram) { ModelState.AddModelError("Instagram", "Already exsist"); }

            if (teamMember.FormFile is null)
            {
                ModelState.AddModelError("FormFile", "Image can not be null");
                return View();
            }
            if (!teamMember.FormFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("FormFile", "enter image");
                return View();
            }
            if (teamMember.FormFile.Length / 1024 / 1024 >= 5)
            {
                ModelState.AddModelError("FormFile", "dont input image big 5MB");
                return View();
            }

            string FileName = Guid.NewGuid() + teamMember.FormFile.FileName;
            string FolderName = ("/assets/img/team/");
            string FullPath = _env.WebRootPath + FolderName + FileName;
            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create))
            {
                teamMember.FormFile.CopyTo(fileStream);
            }
            string ImageUrl = FileName;
            TeamMember member = new TeamMember(teamMember.FullName, teamMember.Profession, ImageUrl, teamMember.TwitterUrl, teamMember.FacebookUrl, teamMember.InstagramUrl, teamMember.LinkedinUrl);
            await _context.TeamMembers.AddAsync(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            TeamMember? teamMember = _context.TeamMembers.Find(id);
            if (teamMember == null) { return NotFound(); }
            return View(teamMember);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TeamMember teamMember)
        {
            TeamMember? exsisTeamMember = _context.TeamMembers.Find(teamMember.Id);
            if (exsisTeamMember == null) { return NotFound(); }
            if (!ModelState.IsValid)
            {
                return View();
            }

            exsisTeamMember.FullName = teamMember.FullName;
            exsisTeamMember.Profession = teamMember.Profession;
            exsisTeamMember.FacebookUrl = teamMember.FacebookUrl;
            exsisTeamMember.TwitterUrl = teamMember.TwitterUrl;
            exsisTeamMember.InstagramUrl = teamMember.InstagramUrl;


            if (teamMember.FormFile is null)
            {
                ModelState.AddModelError("FormFile", "Image can not be null");
                return View(teamMember);
            }
            if (!teamMember.FormFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("FormFile", "enter image");
                return View(teamMember);
            }
            if (teamMember.FormFile.Length / 1024 / 1024 >= 5)
            {
                ModelState.AddModelError("FormFile", "dont input image big 5MB");
                return View(teamMember);
            }

            string FileName = Guid.NewGuid() + teamMember.FormFile.FileName;
            string FolderName = ("/assets/img/team/");
            string FullPath = _env.WebRootPath + FolderName + FileName;
            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create))
            {
                teamMember.FormFile.CopyTo(fileStream);
            }
            exsisTeamMember.ImageUrl = FileName;


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            TeamMember? teamMember = _context.TeamMembers.Find(id);
            if (teamMember == null) { return NotFound(); }

            _context.TeamMembers.Remove(teamMember);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
