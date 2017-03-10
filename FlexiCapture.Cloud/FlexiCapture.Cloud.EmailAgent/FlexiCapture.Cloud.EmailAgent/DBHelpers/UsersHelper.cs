using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgent.DB;

namespace FlexiCapture.Cloud.EmailAgent.DBHelpers
{
    /// <summary>
    /// helper for users
    /// </summary>
    public static class UsersHelper
    {
        /// <summary>
        /// add user to database
        /// </summary>
        public static int AddUser(string userEmail, string userName)
        {
            try
            {
                using (FCCEmailAgentEntities db = new FCCEmailAgentEntities())
                {
                    Users user = new Users() {UserEmail = userEmail, UserName = userName};
                    db.Users.Add(user);
                    db.SaveChanges();
                    return user.Id;
                }
                
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static string GetToUserInfo()
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }
    }
}
