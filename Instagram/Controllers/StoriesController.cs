using BusinessLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Instagram.Controllers
{
    [RoutePrefix("Story")]
    public class StoriesController : Controller
    {
        private readonly StoriesBusinessLayer _storiesBusinessLayer;
        public StoriesController()
        {
            _storiesBusinessLayer = new StoriesBusinessLayer();
        }
        private int GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login", "User");
            }
            return Int16.Parse(User.Identity.Name);
        }

        public ActionResult Index()
        {
            List<Stories> stories = _storiesBusinessLayer.GetAllFollowedStories(GetUserId());
            if (stories.Count <= 0)
            {
                ViewBag.StoriesStatus = "No Stories";
            }
            ViewBag.StoryUploadStatus = TempData["StoryStatus"];
            return PartialView(stories);
        }

        [HttpGet]
        [ActionName("Create")]
        [Route("Create")]
        public ActionResult Create_Get() {
            ViewBag.StoryStatus = (string)TempData["StoryStatus"];
            return View();
        }

        [HttpPost]
        [ActionName("StoryUpload")]
        public ActionResult StoryUpload(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path1 = Path.Combine(Server.MapPath("/UploadedFiles/StoryImages"), Path.GetFileName(file.FileName));
                        string path = "/UploadedFiles/StoryImages/" + Path.GetFileName(file.FileName);
                        file.SaveAs(path1);

                        if (!_storiesBusinessLayer.UploadStory(path, GetUserId()))
                        {
                            TempData["StoryStatus"] = "Error while uploading story";
                            return RedirectToAction("Create", "Stories");
                        }
                    }
                    else
                    {
                        TempData["StoryStatus"] = "Please Select an image";
                        return RedirectToAction("Create", "Stories");
                    }
                    TempData["StoryStatus"] = "Story uploaded successfully.";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TempData["StoryStatus"] = "Error while uploading story";
                    return RedirectToAction("Create", "Stories");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("Details")]
        [Route("Details/{storyId}")]
        public ActionResult Details_Get(int storyId)
        {
            return View(_storiesBusinessLayer.GetStory(storyId));
        }

        [HttpPost]
        [ActionName("DeleteStory")]
        [Route("DeleteStory/{storyId}")]
        public ActionResult DeleteStory(int storyId)
        {
            bool result = _storiesBusinessLayer.DeleteStory(storyId);
            if (result)
            {
                TempData["StoryStatus"] = "Story Deleted";
            }
            else
            {
                TempData["StoryStatus"] = "Error Deleting Story";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}