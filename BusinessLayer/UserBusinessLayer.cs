using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using DataLayer.ViewModels;
using Instagram;

namespace BusinessLayer
{
    public class UserBusinessLayer
    {
        DBContext dBContext = new DBContext();
        Status status = new Status();
        public User GetUser(int id)
        {
            return dBContext.Users.Find(id);
        }
        public Status CreateUser(User user)
        {
            try
            {
                User usernameTest = CheckUsernameExists(user.username);
                if(usernameTest == null)
                {
                    User emailTest = checkEmailExists(user.email);
                    if(emailTest == null)
                    {
                        dBContext.Users.Add(user);
                        dBContext.SaveChanges();
                        status.status = true;
                        status.message = "Successfully Registered";
                    }
                    else
                    {
                        status.status = false;
                        status.message = "Email already registered";
                    }
                }
                else
                {
                    status.status = false;
                    status.message = "Username already registered";
                }
                return status;
            }
            catch (Exception)
            {
                status.status = false;
                status.message = "";
                return status;
            }
        }
        public User CheckUsernameExists (string username)
        {
            var query = dBContext.Users.AsQueryable();
            query = query.Where(user => user.username == username && user.active);
            List<User> search = query.ToList();
            if(search.Count() > 0)
            {
                return search[0];
            }
            else
            {
                return null;
            }
        }
        public User checkEmailExists(string email)
        {
            var query = dBContext.Users.AsQueryable();
            query = query.Where(user => user.email == email && user.active);
            List<User> search = query.ToList();
            if (search.Count() > 0)
            {
                return search[0];
            }
            else
            {
                return null;
            }
        }
        public User testLogin(User user)
        {
            User tempUser = checkEmailExists(user.email);
            if(tempUser != null)
            {

                MD5 md5 = new MD5CryptoServiceProvider();
                Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(user.password);
                Byte[] encodedBytes = md5.ComputeHash(originalBytes);

                string encPassword = BitConverter.ToString(encodedBytes);
                if (tempUser.password == encPassword && tempUser.active)
                {
                    return tempUser;
                }
            }
            return null;
        }
        public bool UpdateProfilePic(string userEmail, string path)
        {
            User userToChange = dBContext.Users.FirstOrDefault(user => user.email == userEmail && user.active);
            if (userToChange != null)
            {
                userToChange.profilePic = path;
                dBContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<User> FindUsers(string search)
        {
            return dBContext.Users.Where(x => x.username.Contains(search) && x.active).ToList();
        }
        public List<PostWithImages> GetAllUserPosts(int userId)
        {
            List<Post> singleUserPosts = dBContext.Posts.Where(x => (x.userId == userId) && x.active == true).ToList();
            List<int> postIds = singleUserPosts.Select(x => x.id).ToList();
            List<PostImages> postImages = dBContext.PostImages.Where(x => postIds.Contains(x.postId)).ToList();
            List<PostWithImages> postWithImages = new List<PostWithImages>();

            foreach(Post _post in singleUserPosts)
            {
                PostWithImages postWithImages1 = new PostWithImages();
                postWithImages1.images = postImages.Where(x => x.postId == _post.id).ToList();
                postWithImages1.post = _post;
                postWithImages.Add(postWithImages1);
            }
            return postWithImages;
        }
        public bool CheckFollowing(int user1Id, int user2Id)
        {
            List<Followers> followers = dBContext.Followers.Where(x => x.FollowedUser == user2Id && x.FollowedBy == user1Id).ToList();
            if(followers.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ProfileData GetUserProfile(int userId, int myId)
        {
            ProfileData profileData = new ProfileData();
            profileData.user = GetUser(userId);
            profileData.postWithImages = GetAllUserPosts(userId);
            profileData.following = CheckFollowing(myId, userId);

            return profileData;
        }

        public bool FollowUser(int user1, int user2)
        {
            try
            {
                Followers followers = new Followers();
                followers.FollowedBy = user1;
                followers.FollowedUser = user2;
                dBContext.Followers.Add(followers);
                dBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UnFollowUser(int user1, int user2)
        {
            try
            {
                Followers followers = new Followers();
                followers.FollowedBy = user1;
                followers.FollowedUser = user2;
                dBContext.Followers.Attach(followers);
                dBContext.Followers.Remove(followers);
                dBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ChangePrivacy(int userId, bool cond)
        {
            try
            {
                User user = dBContext.Users.Find(userId);
                user.accountPrivacy = cond;
                dBContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
