using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace Instagram.Controllers
{
    [RoutePrefix("Post")]
    public class PostController : Controller
    {
        private readonly PostBusinessLayer _postBusinessLayer;

        public PostController() => _postBusinessLayer = new PostBusinessLayer();
        private int GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login", "User");
            }
            return Int16.Parse(User.Identity.Name);
        }
        public ActionResult Index() => View();

        [HttpGet]
        [Authorize]
        [Route("Create")]
        [ActionName("Post")]
        public ActionResult CreatePost_Get()
        {
            ViewBag.PostStatus = (string)TempData["PostStatus"];
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("Create")]
        [ActionName("Post")]
        public ActionResult CreatePost_Post(Post post)
        {
            if(post.description != null && post.description != "")
            {
                TempData["post"] = post;
                return RedirectToAction("UploadMultipleFiles", "FileUpload");
            }
            TempData["PostStatus"] = "Please add a description";
            return RedirectToAction("Post");
        }

        [HttpGet]
        [Route("Edit/{id}")]
        [ActionName("Edit")]
        public ActionResult EditPost_Get(int id)
        {
            Post post = _postBusinessLayer.GetPost(id);
            return View(post);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ActionName("Edit")]
        public ActionResult EditPost_Post(Post post, int id)
        {
            if(post.description == null || post.description == "")
            {
                ViewBag.EditStatus = "Description can't be empty";
                return View(post);
            }
            Post newPost = _postBusinessLayer.EditPost(post, id, GetUserId());
            if(newPost != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(post);
        }

        [HttpGet]
        [Route("Delete/{id}")]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            TempData["postId"] = id;
            return View();
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ActionName("Delete")]
        public ActionResult DeletePost()
        {
            if (TempData["postId"] != null)
            {
                bool result = _postBusinessLayer.DeletePost((int)TempData["postId"], GetUserId());
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ActionName("LikePost")]
        [Route("LikePost/{id}")]
        public ActionResult LikePost(int id)
        {
            bool result = _postBusinessLayer.LikePost(id, GetUserId(), true);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ActionName("UnLikePost")]
        [Route("UnLikePost/{id}")]
        public ActionResult UnLikePost(int id)
        {
            bool result = _postBusinessLayer.LikePost(id, GetUserId(), false);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("Comment")]
        [Route("Comment/{id}")]
        public ActionResult Comment_Get(int id)
        {
            List<Comment> comments = _postBusinessLayer.GetPostComments(id);
            TempData["postId"] = id;
            return View(comments);
        }

        [HttpPost]
        [ActionName("Comment")]
        [Route("Comment/{id}")]
        public ActionResult AddComment_Post(string Desc)
        {
            int postId = (int)TempData["postid"];
            bool result = _postBusinessLayer.CreateComment(Desc, postId, GetUserId());
            if (result)
            {
                TempData["CommentStatus"] = "Comment Added";
            }
            else
            {
                TempData["CommentStatus"] = "Error Adding Comment";
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ActionName("DeleteComment")]
        [Route("DeleteComment/{commentId}")]
        public ActionResult DeleteComment(int commentId)
        {
            bool result = _postBusinessLayer.DeleteComment(commentId);
            if (result)
            {
                TempData["CommentStatus"] = "Comment Deleted";
            }
            else
            {
                TempData["CommentStatus"] = "Error Deleting Comment";
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("EditComment")]
        [Route("EditComment/{commentId}")]
        public ActionResult EditComment_Get(int commentId)
        {
            TempData["commentId"] = commentId;
            return View(_postBusinessLayer.GetCommentById(commentId));
        }

        [HttpPost]
        [ActionName("EditComment")]
        [Route("EditComment/{id}")]
        public ActionResult EditComment_Post(int id, string description)
        {
            bool result = _postBusinessLayer.EditComment(id, description);
            if (result)
            {
                TempData["CommentStatus"] = "Comment Saved";
            }
            else
            {
                TempData["CommentStatus"] = "Error Saving Comment";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}