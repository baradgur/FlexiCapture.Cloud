using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FlexiCapture.Cloud.EmailAgent.Models;

namespace FlexiCapture.Cloud.EmailAgentTester.Helpers
{
    /// <summary>
    ///     tester helper
    /// </summary>
    public static class TesterHelper
    {
        /// <summary>
        ///     test method to send single emails
        /// </summary>
        private static void SendSingleEmail()
        {
            try
            {
                QueuePackageModel package = new QueuePackageModel();
                var models = new List<EmailModel>();
                models.Add(new EmailModel
                {
                   EmailContentLine = "#username#=Илья;#emailtitle#=Тестовый заголовок письма для Ильи;#buttontitle#=Кликни меня!;#sendername#=Ваш сосед - человек-паук;#linkto#=http://www.netvix.by;#linktitle#=Go To Netvix!!!",
                    ToEmails = "fccemailagent_tester@netvix.by",
                    FromEmail = "fccemailagent@netvix.by",
                    Subject = "Test email from FCC Email Agent",
                    TypeId = 2,
                   Task = new QueuePackageTaskModel() { DeliveryDateTime = DateTime.Now}
                });
                //models.Add(new EmailModel
                //{
                //    EmailContentLine = "#username#=Alex Hvedkovich;#emailtitle#=Тестовый заголовок письма;#buttontitle#=Кнопка ТЫЦ!;#sendername#=Ваш сосед - человек-паук;#linkto#=http://www.netvix.by;#linktitle#=Go To Netvix!!!",
                //    ToEmails = "vyadzmak@gmail.com",
                //    FromEmail = "fccemailagent@netvix.by",
                //    Subject = "Test email from FCC Email Agent to vyadzmak",
                //    TypeId = 2
                //});


                //models.Add(new EmailModel
                //{
                //    EmailContentLine = "#username#=Alex Hvedkovich;#emailtitle#=Тестовый заголовок письма;#buttontitle#=Кнопка ТЫЦ!;#sendername#=Ваш сосед - человек-паук;#linkto#=http://www.netvix.by;#linktitle#=Go To Netvix!!!",
                //    ToEmails = "vyadzmak@netvix.by",
                //    FromEmail = "fccemailagent@netvix.by",
                //    Subject = "Test email from FCC Email Agent to vyadzmak",
                //    TypeId = 2
                //});
               // package.Task.DeliveryDateTime = DateTime.Now;
                package.Emails.AddRange(models);

                var model = new QueueModel();
                model.StateId = 1;

                var settings = new XmlWriterSettings();
                settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                var formatter = new XmlSerializer(typeof(QueuePackageModel));
                string content = "";

                using (StringWriter textWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                    {
                        formatter.Serialize(xmlWriter, package);
                    }
                    content = textWriter.ToString();

                }

                model.EmailContent = content;
                DBHelper.AddQueueToDb(model);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        ///     run tests
        /// </summary>
        public static void RunTests()
        {
            try
            {
                Console.WriteLine("Send single email ...");
                SendSingleEmail();
                Console.WriteLine("Single email sent");
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                var inner = "";

                if (exception.InnerException != null)
                {
                    inner = "; Inner exception: " + exception.InnerException.Message;
                }
                Console.WriteLine("ERROR: " + exception.Message + inner);
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }
    }
}