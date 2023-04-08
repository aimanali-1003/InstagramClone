using DataLayer.Models;
using DataLayer.ViewModels;
using Instagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class HomeBusinessLayer
    {
        DBContext dBContext = new DBContext();
        public List<PostWithImages> GetAllPosts(int userId)
        {
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            User user = userBusinessLayer.GetUser(userId);
            if(user != null)
            {
                List<Followers> followers = dBContext.Followers.Where(x => x.FollowedBy == user.id).ToList();
                List<Post> allPosts = new List<Post>();

                for (int i = 0; i < followers.Count; i++)
                {
                    int newId = followers[i].FollowedUser;
                    List<Post> singleUserPosts = dBContext.Posts.Where(x => (x.userId == newId) && x.active == true).ToList();
                    allPosts.AddRange(singleUserPosts);
                }
                List<Post> singleUserPosts1 = dBContext.Posts.Where(x => (x.userId == userId) && x.active == true).ToList();
                allPosts.AddRange(singleUserPosts1);
                List<PostWithImages> postWithImages = new List<PostWithImages>();

                List<int> postIds = allPosts.Select(x => x.id).ToList();
                List<PostLikes> postLikes = dBContext.PostLikes.Where(x => postIds.Contains(x.postId)).ToList();
                List<PostImages> postImages = dBContext.PostImages.Where(x => postIds.Contains(x.postId)).ToList();

                foreach(Post _post in allPosts)
                {
                    PostWithImages postWithImages1 = new PostWithImages();
                    postWithImages1.images = postImages.Where(x => x.postId == _post.id).ToList();
                    postWithImages1.post = _post;
                    postWithImages1.likes = postLikes.Where(x => x.postId == _post.id).ToList();
                    postWithImages.Add(postWithImages1);
                }

                return postWithImages;
            }
            else
            {
                return null;
            }
        }
        public List<PostImages> GetPostImages(int id)
        {
            return dBContext.PostImages.Where(x => x.postId == id).ToList();
        }
    }
}
