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
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return -1;
            }

        }

        /// <summary>
        /// check user in db and add if not exists 
        /// </summary>
        /// <returns></returns>
        public static int CheckExistsUserByEmail(string email)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    email = email.ToLower();
                    email = email.Replace(" ", string.Empty);

                    var user = db.Users.FirstOrDefault(x => x.UserEmail == email);

                    if (user == null)
                    {
                        Users users = new Users() {UserEmail = email};
                        db.Users.Add(users);
                        db.SaveChanges();
                        return users.Id;
                    }
                    else
                    {
                        return user.Id;
                    }
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return -1;
            }

        }
        /// <summary>
        /// get to user inforation by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Users GetToUserInfo(int userId)
        {
            try
            {
                using (var db = new FCCEmailAgentEntities())
                {
                    return db.Users.FirstOrDefault(x=>x.Id==userId);    
                }
                
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
                return null;
            }

        }

        /// <summary>
        /// delete user from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string DeleteUser(int id)
        {
            try
            {
                return "OK";

            }
            catch (Exception e)
            {
                return "Error";
            }
        }
    }
}
