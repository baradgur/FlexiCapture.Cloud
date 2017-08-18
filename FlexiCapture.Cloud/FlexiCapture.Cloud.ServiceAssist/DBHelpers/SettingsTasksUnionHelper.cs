using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.ServiceAssist.DB;

namespace FlexiCapture.Cloud.ServiceAssist.DBHelpers
{
    /// <summary>
    /// Класс связывающий выполненный таск (ФТП) и сеттинг, с которым он выполнился
    /// </summary>
    public static class SettingsTasksUnionHelper
    {
        /// <summary>
        /// Добавляем новый элемент
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="settingId"></param>
        public static void AddNewItem(int taskId, int settingId)
        {
            try
            {
                if (taskId == 0 || settingId == 0)
                    throw new ArgumentNullException();

                using (var db = new FCCPortalEntities2())
                {
                    db.FTPSettingsTasksUnion.Add(new FTPSettingsTasksUnion()
                    {
                        TaskId = taskId,
                        SettingId = settingId
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static DB.FTPSettings GetSettingsByTaskId(int taskId)
        {
            try
            {
                if (taskId == 0)
                    throw new ArgumentNullException();

                using (var db = new FCCPortalEntities2())
                {
                    var query = (from stu in db.FTPSettingsTasksUnion
                        where stu.TaskId == taskId
                        join st in db.FTPSettings on stu.SettingId equals st.Id
                        select st).SingleOrDefault();

                    return query;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
