using BusinessLayer;
using DataLayer.Models;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeBusinessLayer _homeBusinessLayer;
        public HomeController()
        {
            _homeBusinessLayer = new HomeBusinessLayer();
        }
        private int GetUserId() {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login", "User");
            }
            return Int16.Parse(User.Identity.Name);
        }
        // GET: Home
        public ActionResult Index()
        {
            if (User.Identity.Name != null && User.Identity.Name != "")
            {
                ViewBag.Status = TempData["CommentStatus"];
                ViewBag.StoryStatus = TempData["StoryStatus"];
                List<PostWithImages> posts = _homeBusinessLayer.GetAllPosts(GetUserId());
                return View(posts);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public ActionResult PostImages(int id)
        {
            List<PostImages> posts = _homeBusinessLayer.GetPostImages(id);
            return View(posts);
        }
        [HttpGet]
        [ActionName("Search")]
        [Route("Search")]
        public ActionResult Search_Get() => View();

        [HttpPost]
        [ActionName("Search")]
        [Route("Search")]
        public ActionResult Search_Post(string search) => View(new UserBusinessLayer().FindUsers(search));

        [HttpPost]
        [ActionName("CloseNotification")]
        public ActionResult CloseNotification()
        {
            ViewBag.StoryStatus = null;
            ViewBag.Status = null;
            return RedirectToAction("Index", "Home");
        }
    }
}