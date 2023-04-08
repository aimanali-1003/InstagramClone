using DataLayer.Models;
using Instagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PostBusinessLayer
    {
        DBContext dBContext = new DBContext();
        public Post CreatePost(Post post, int userId)
        {
            User user = dBContext.Users.Find(userId);
            if (user != null)
            {
                post.userId = user.id;
                post.active = true;
                post.time = DateTime.Now;
                try
                {
                    Post newPost = dBContext.Posts.Add(post);
                    dBContext.SaveChanges();
                    return newPost;
                }
                catch
                {
                    return null;
                }
                
            }
            else
            {
                return null;
            }
        }
        public bool AddPostImages(int postId, List<string> allPaths)
        {
            foreach(var path in allPaths)
            {
                PostImages postImages = new PostImages();
                postImages.postId = postId;
                postImages.imageUrl = path;

                try
                {
                    PostImages pI = dBContext.PostImages.Add(postImages);
                    int result = dBContext.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        public Post GetPost(int postId)
        {
            return dBContext.Posts.Find(postId);
        }
        public Post EditPost(Post post, int postId, int userId)
        {
            try
            {
                User user = dBContext.Users.Find(userId);
                Post updatePost = dBContext.Posts.Find(postId);
                updatePost.description = post.description;
                updatePost.userId = user.id;
                updatePost.user = user;
                dBContext.SaveChanges();
                return updatePost;
            }catch (Exception)
            {
                return null;
            }
        }
        public bool DeletePost(int postId, int userId)
        {
            User user = dBContext.Users.Find(userId);
            Post post = dBContext.Posts.Find(postId);
            post.active = false;
            post.userId = user.id;
            post.user = user;
            dBContext.SaveChanges();
            return true;
        }
        public bool LikePost(int postId, int userId, bool currentlyLikedByLoggedUser)
        {
            try
            {
                PostLikes postLikes = new PostLikes { postId = postId, userId = userId };
                if (currentlyLikedByLoggedUser)
                {
                    dBContext.PostLikes.Add(postLikes);
                }
                else
                {
                    dBContext.PostLikes.Attach(postLikes);
                    dBContext.PostLikes.Remove(postLikes);
                }
                dBContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public List<Comment> GetPostComments(int postId) => dBContext.Comments.Where(x => x.postId == postId).ToList();
        public Comment GetCommentById(int commentId) => dBContext.Comments.Find(commentId);
        public bool CreateComment(string Desc, int postId, int userId)
        {
            try
            {
                Comment comment = new Comment
                {
                    description = Desc,
                    postId = postId,
                    userId = userId,
                    time = DateTime.Now
                };
                dBContext.Comments.Add(comment);
                dBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteComment(int commentId)
        {
            try
            {
                Comment comment = GetCommentById(commentId);
                dBContext.Comments.Attach(comment);
                dBContext.Comments.Remove(comment);
                dBContext.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool EditComment(int commentId, string Desc)
        {
            try
            {
                Comment comment = GetCommentById(commentId);
                comment.description = Desc;
                dBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
