using DataLayer.Models;
using Instagram;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class StoriesBusinessLayer
    {
        DBContext dBContext = new DBContext();
        public Stories GetStory(int storyId)
        {
            return dBContext.Stories.Find(storyId);
        }
        public List<Stories> GetAllFollowedStories(int userId)
        {
            List<int> followingUsers = dBContext.Followers.Where(x => x.FollowedBy == userId).ToList().Select(x => x.FollowedUser).ToList();
            return dBContext.Stories.Where(x => (followingUsers.Contains(x.userId) || x.userId == userId) && !x.isDeleted && DbFunctions.DiffHours(x.createdAt, DateTime.Now) < 24).ToList();
        }
        public bool UploadStory(string storyImagePath, int userId)
        {
            try
            {
                dBContext.Stories.Add(new Stories { storyImage = storyImagePath, userId = userId });
                dBContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }
        public bool DeleteStory(int storyId)
        {
            try
            {
                Stories story = dBContext.Stories.Find(storyId);
                story.isDeleted = true;
                dBContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
