using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using BusinessLayer;
using DataLayer.Models;

namespace Instagram.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly UserBusinessLayer _userBusinessLayer;

        public FileUploadController()
        {
            _userBusinessLayer = new UserBusinessLayer();
        }
        private int GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login", "User");
            }
            return Int16.Parse(User.Identity.Name);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (ModelState.IsValid && TempData.ContainsKey("User"))
            {
                try
                {
                    User user = (User)TempData["User"];
                    if (file != null)
                    {
                        string path1 = Path.Combine(Server.MapPath("/UploadedFiles/ProfilePics"), Path.GetFileName(file.FileName));
                        string path = "/UploadedFiles/ProfilePics/" + Path.GetFileName(file.FileName);
                        file.SaveAs(path1);
                        UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                        user.profilePic = path;
                        Status result = _userBusinessLayer.CreateUser(user);
                        if (!result.status)
                        {
                            ViewBag.FileStatus = "Error while creating user";
                            return View();
                        }
                        else
                        {
                            ViewBag.FileStatus = "Created User user";
                            return RedirectToAction("Login", "User");
                        }
                    }
                    else
                    {
                        ViewBag.FileStatus = "Please select a file.";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ViewBag.FileStatus = ex.Message;
                    return View();
                }
            }
            else
            {
                if(file == null)
                {
                    ViewBag.FileStatus = "Please Select a file";
                    return View();
                }
                else
                {
                    ViewBag.FileStatus = "Some error occured please try again later";
                    return View();
                }
            }
        }
        [HttpGet]
        [ActionName("UploadMultipleFiles")]
        public ActionResult UploadMultipleFiles_Get()
        {
            ViewBag.FileStatus = (string)TempData["FileStatus"];
            return View();
        }
        [HttpPost]
        [ActionName("UploadMultipleFiles")]
        public ActionResult UploadMultipleFiles_Post(List<HttpPostedFileBase> postedFiles)
        {
            if (TempData.ContainsKey("post") && ModelState.IsValid)
            {
                Post post = (Post)TempData["post"];
                if(post.description == null || post.description == "")
                {
                    TempData["FileStatus"] = "No Description";
                    return RedirectToAction("UploadMultipleFiles", new { post = post });
                }
                List<string> allPath = new List<string>();
                string path = Server.MapPath("/UploadedFiles/PostImages/");
                foreach (HttpPostedFileBase file in postedFiles)
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        file.SaveAs(path + fileName);
                        string path1 = "/UploadedFiles/PostImages/" + Path.GetFileName(file.FileName);
                        allPath.Add(path1);
                    }
                    else
                    {
                        TempData["FileStatus"] = "Please select a file.";
                        return RedirectToAction("UploadMultipleFiles");
                    }

                }
                PostBusinessLayer postBusinessLayer = new PostBusinessLayer();
                Post newPost = postBusinessLayer.CreatePost(post, GetUserId());
                if(newPost == null)
                {
                    TempData["FileStatus"] = "Error creating post";
                    return RedirectToAction("UploadMultipleFiles");
                }
                else
                {
                    postBusinessLayer.AddPostImages(post.id, allPath);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["FileStatus"] = "Error Creating Post";
                return RedirectToAction("UploadMultipleFiles");
            }
        }
    }
}
