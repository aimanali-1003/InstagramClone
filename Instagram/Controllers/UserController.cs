using DataLayer.Models;
using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using System.Security.Cryptography;
using System.Text;

namespace Instagram.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserBusinessLayer _userBusinessLayer;

        public UserController()
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

        public ActionResult Index() => View();

        [AllowAnonymous]
        [HttpGet]
        [Route("~/Login")]
        [ActionName("Login")]
        public ActionResult Login_Get()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("~/Login")]
        [ActionName("Login")]
        public ActionResult Login_Post(User authModel)
        {
            User loggedUser = _userBusinessLayer.testLogin(authModel);
            if (loggedUser != null)
            {
                FormsAuthentication.SetAuthCookie(loggedUser.id.ToString(), false);
                Session["profilePic"] = loggedUser.profilePic;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.UserStatus = "Email or Password incorrect";
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Signup")]
        [ActionName("Signup")]
        public ActionResult Signup_Get()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Signup")]
        [ActionName("Signup")]
        public ActionResult Signup_Post(User authModel)
        {
            if (ModelState.IsValid)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(authModel.password);
                Byte[] encodedBytes = md5.ComputeHash(originalBytes);

                authModel.password = BitConverter.ToString(encodedBytes);
                TempData["User"] = authModel;
                return RedirectToAction("Index", "FileUpload");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [ActionName("Profile")]
        [Route("Profile/{id}")]
        [HttpGet]
        public ActionResult ViewProfile(int id)
        {
            if(TempData["UserStatus"] != null)
            {
                ViewBag.UserStatus = TempData["UserStatus"];
            }
            return View(new UserBusinessLayer().GetUserProfile(id, GetUserId()));
        }

        [ActionName("Follow")]
        [Route("Profile/{id}")]
        [HttpPost]
        public ActionResult FollowUser(int id)
        {
            
            bool result = _userBusinessLayer.FollowUser(GetUserId(), id);
            if (result)
            {
                TempData["UserStatus"] = "User Followed";
            }
            return RedirectToAction("Profile");
        }

        [ActionName("UnFollow")]
        [HttpPost]
        public ActionResult UnFollowUser(int id)
        {
            
            bool result = _userBusinessLayer.UnFollowUser(GetUserId(), id);
            if (result)
            {
                TempData["UserStatus"] = "User Unfollowed";
            }
            return RedirectToAction("Profile");
        }

        [ActionName("MakePublic")] //
        [HttpPost]
        public ActionResult MakePublic(int id)
        {
            
            bool result = _userBusinessLayer.ChangePrivacy(id, false);
            if (result)
            {
                TempData["UserStatus"] = "Profile set to Public";
            }
            return RedirectToAction("Profile");
        }

        [ActionName("MakePrivate")] //
        [HttpPost]
        public ActionResult MakePrivate(int id)
        {
            
            bool result = _userBusinessLayer.ChangePrivacy(id, true);
            if (result)
            {
                TempData["UserStatus"] = "Profile set to Private";
            }
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [Route("CloseNotification/{profileId}")]
        public ActionResult CloseNotification(int profileId)
        {
            TempData["UserStatus"] = null;
            return RedirectToAction("Profile", new { id = profileId });
        }
    }
}
