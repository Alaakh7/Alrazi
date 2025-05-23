using Alrazi.Models;
using Alrazi.Services;
using Alrazi.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Alrazi.Controllers
{
    public class HomeController : Controller
    {
        private readonly AccountService accountService;
        private readonly WebsiteService websiteService;

        public HomeController(AccountService accountService, WebsiteService websiteService)
        {
            this.accountService = accountService;
            this.websiteService = websiteService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await websiteService.GetLastBlog(3));
        }

        [HttpPost]
        public async Task AddContact(string fullName,  string phoneNumber, string message)
        {
            await websiteService.AddMessage(new Models.ContactMessage
            {
                FullName = fullName,
                Message = message,
                SendDate = DateTime.UtcNow.AddHours(3),
                PhoneNumber = phoneNumber,
            });
        }

        public async Task<List<Blog>> GetLastBlogs()
        {
            return await websiteService.GetLastBlog(3);
        }

        public async Task<List<Blog>> GetLastPinnedBlogs()
        {
            return await websiteService.GetLastPinnedBlog(3);
        }

        [HttpGet("Blogs")]
        public async Task<IActionResult> Blogs()
        {
            return View(await websiteService.GetBlogs());
        }

        public async Task<IActionResult> Blog(int id)
        {
            ViewData["LastBlogs"] = await websiteService.GetLastBlog(3);
            return View(await websiteService.GetBlog(id));
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (HttpContext.HasSession())
                return RedirectToAction("Panel");
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (HttpContext.HasSession())
            {
                return RedirectToAction("Panel");
            }
            var data = await accountService.Login(username, password);
            if (!string.IsNullOrWhiteSpace(data.Item2))
            {
                ViewBag.ErrorMessage = data.Item2;
                return View();
            }
            HttpContext.SetSession(data.Item1);
            return RedirectToAction("Panel");
        }

        [HttpGet("Panel")]
        public IActionResult Panel()
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            return View();
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("Add-Blog")]
        public IActionResult AddBlog()
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost("Add-Blog")]
        public async Task<IActionResult> AddBlog(string title, string details, string video)
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            int blogId = await websiteService.AddBlog(title, details, Request.Form.Files.ToList(), video);
            ViewBag.Done = true;
            ViewBag.Message = "تم النشر بنجاح";
            var absoluteUrl = $"{Request.Scheme}://{Request.Host}{Url.Content($"~/Home/Blog/{blogId}")}";
            ViewBag.PostLink = absoluteUrl;
            return View();
        }

        [HttpGet("Get-Blogs")]
        public async Task<IActionResult> GetBlogs()
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            return View(await websiteService.GetBlogs());
        }

        [HttpGet("DeleteBlog/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            await websiteService.RemoveBlog(id);
            return Redirect("~/Get-Blogs");
        }

        [HttpGet("ChangePin/{id}")]
        public async Task<IActionResult> ChangePin(int id)
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            await websiteService.ChangePin(id);
            return Redirect("~/Get-Blogs");
        }

        [HttpGet("Get-Contacts")]
        public async Task<IActionResult> GetContacts()
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            return View(await websiteService.GetContacts(false));
        }

        [HttpGet("Get-Archived-Contacts")]
        public async Task<IActionResult> GetArchivedContacts()
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            return View(await websiteService.GetContacts(true));
        }

        [HttpGet("Get-Contact/{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            return View(await websiteService.GetContact(id));
        }

        [HttpGet("ReadContact/{id}")]
        public async Task<IActionResult> ReadContact(int id)
        {
            if (!HttpContext.HasSession())
                return RedirectToAction("Index");
            await websiteService.UpdateContact(id);
            return Redirect($"~/Get-Archived-Contacts");
        }

    }
}