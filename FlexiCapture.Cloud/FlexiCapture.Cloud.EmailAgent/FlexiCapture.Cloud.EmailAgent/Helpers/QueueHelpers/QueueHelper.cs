using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FlexiCapture.Cloud.EmailAgent.DB;
using FlexiCapture.Cloud.EmailAgent.DBHelpers;
using FlexiCapture.Cloud.EmailAgent.Helpers.EmailGeneratorHelpers;
using FlexiCapture.Cloud.EmailAgent.Models;

namespace FlexiCapture.Cloud.EmailAgent.Helpers.QueueHelpers
{
    public static class QueueHelper
    {
        /// <summary>
        /// generate mass email task
        /// </summary>
        private static void GenerateGroupEmails(QueuePackageModel model)
        {
            try
            {
                if (model.Task.DeliveryDateTime == null) return;
                int taskId = TaskHelper.AddTask((DateTime) model.Task.DeliveryDateTime);
                
                    foreach (var emailModel in model.Emails)
                    {
                        EmailHelper.AddEmailToDbByEmailModel(taskId, emailModel);
                    }
                
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }

        }

        /// <summary>
        /// generate single emails
        /// </summary>
        /// <param name="model"></param>
        private static void GenerateSingleEmails(QueuePackageModel model)
        {
            try
            {
                foreach (var emailModel in model.Emails)
                {
                    int taskId = TaskHelper.AddTask((DateTime)emailModel.Task.DeliveryDateTime);
                    EmailHelper.AddEmailToDbByEmailModel(taskId, emailModel);
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }

        }
        /// <summary>
        /// parse queue
        /// </summary>
        public static void ParseQueues(List<Queue> queues )
        {
            try
            {
                foreach (var queue in queues)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof (QueuePackageModel));
                    QueuePackageModel model = new QueuePackageModel();
                    using (var reader = new StringReader(queue.EmailContent))
                    {
                        model = (QueuePackageModel)serializer.Deserialize(reader);
                        if (model.Task.DeliveryDateTime == null)
                        {
                            GenerateSingleEmails(model);
                        }
                        {
                            GenerateGroupEmails(model);
                            
                        }

                    }
                    DBHelpers.QueueHelper.UpdateStateQueue(queue.Id,3);
                }
            }
            catch (Exception exception)
            {
                string innerException = exception.InnerException == null ? "" : exception.InnerException.Message;
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                LogHelper.AddLog("Error in method: " + methodName + "; Exception: " + exception.Message + " Innner Exception: " +
                                 innerException);
            }

        }
    }
}
