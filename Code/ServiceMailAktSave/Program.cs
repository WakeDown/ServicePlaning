using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using ServiceMailAktSave.Models;
using ServiceMailAktSave.Objects;

namespace ServiceMailAktSave
{
    class Program
    {
        static void Main(string[] args)
        {
            Work();
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        static ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP2);

        /// <summary>
        /// Читаем письма в специальном ящике Exchenge Server, затем сохраняем в БД и удалеям псиьмо
        /// </summary>
        public static void Work()
        {
            //Место куда валятся письма с результатами сбора данных с КМТ
            string login = ConfigurationManager.AppSettings["login"];
            string pass = ConfigurationManager.AppSettings["pass"];
            string mail = ConfigurationManager.AppSettings["mail"];
            string archivePath = ConfigurationManager.AppSettings["documentAktScanPath"];
            string archivePathPetrenko = ConfigurationManager.AppSettings["documentAktScanPathPetrenko"];
            String[] allowedExtensions = ConfigurationManager.AppSettings["documentScanFormat"].Split('|');

            service.Credentials = new WebCredentials(login, pass);

            service.UseDefaultCredentials = false;

            service.AutodiscoverUrl(mail, RedirectionUrlValidationCallback);

            ItemView view = new ItemView(1000000000);//Чтобы ничего не пропустить берем миллиард записей сразу ))

            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, view);

            if (findResults.Any())
            {
                service.LoadPropertiesForItems(findResults, PropertySet.FirstClassProperties);

                foreach (Item myItem in findResults.Items)
                {

                    if (myItem.HasAttachments)
                    {
                        foreach (Attachment attach in myItem.Attachments)
                        {
                            if (attach is FileAttachment)
                            {

                                FileAttachment fileAttachment = attach as FileAttachment;//myItem.Attachments[0] as FileAttachment;
                                fileAttachment.Load();
                                String fileExtension = string.Empty;
                                //Загружуем файл в хранилище
                                try
                                {
                                    fileExtension = Path.GetExtension(fileAttachment.Name).ToLower().Replace(".", String.Empty);
                                }
                                catch (NullReferenceException exception)
                                {
                                    continue;
                                }

                                if (allowedExtensions.Contains(fileExtension))
                                {
                                    NetworkCredential nc = GetNetCredential4Scan();

                                    using (
                                        WindowsImpersonationContextFacade impersonationContext =
                                            new WindowsImpersonationContextFacade(nc))
                                    {
                                        //Преобразовываем имя
                                        bool isTiff = (fileExtension.Equals("tiff") || fileExtension.Equals("tif"));
                                        if (isTiff) fileExtension = "png";

                                        string newFileName;
                                        string fullPath;
                                        int i = 0;
                                        do
                                        {
                                            string postfix = i > 1 ? String.Format("[{0}]", i) : String.Empty;
                                            newFileName = String.Format("{0:####}{1:##}{2:##}{3:##}{4:##}{5:##}{7}.{6}", DateTime.Now.Year,
                                                DateTime.Now.Month, DateTime.Now.Day,
                                                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, fileExtension, postfix);
                                            fullPath = Path.Combine(archivePath, newFileName);
                                            i++;
                                        } while (File.Exists(fullPath));

                                        //string savePath = Path.Combine(archivePath, attach.Name);

                                        //Специально сохраняем еще в папку Алены Васильевны Петренко (так исторически сложилось)
                                        string pathPetrnko = Path.Combine(archivePathPetrenko, newFileName);

                                        if (isTiff)
                                        {
                                            using (MemoryStream ms = new MemoryStream(fileAttachment.Content))
                                            {
                                                Bitmap.FromStream(ms).Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                                                try
                                                {
                                                    //Специально сохраняем еще в папку Алены Васильевны Петренко (так исторически сложилось)
                                                    Bitmap.FromStream(ms)
                                                        .Save(pathPetrnko, System.Drawing.Imaging.ImageFormat.Png);
                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            fileAttachment.Load(fullPath);

                                            try
                                            {
                                                //Специально сохраняем еще в папку Алены Васильевны Петренко (так исторически сложилось)
                                                fileAttachment.Load(pathPetrnko);
                                            }
                                            catch
                                            {
                                            }
                                        }

                                        //Сохраняем ссылку на файл
                                        AktScan akt = new AktScan();
                                        akt.Name = attach.Name;
                                        akt.FullPath = fullPath;
                                        akt.FileName = newFileName;

                                        akt.Save();



                                    }

                                }
                            }
                        }
                    }

                    //Удаление письма
                    //DeleteMode.SoftDelete - The item or folder will be moved to the dumpster. Items and folders in the dumpster can be recovered.
                    myItem.Delete(DeleteMode.SoftDelete);

                }
            }

            //EmailMessage email = new EmailMessage(service);

            //email.ToRecipients.Add(login);

            //email.Subject = "HelloWorld";
            //email.Body = new MessageBody("This is the first email I've sent by using the EWS Managed API");

            //email.Send();
        }


        public static NetworkCredential GetNetCredential4Scan()
        {
            string accUserName = ConfigurationManager.AppSettings["accUserName4Scan"];
            string accUserPass = ConfigurationManager.AppSettings["accUserPass4Scan"];

            string domain = accUserName.Substring(0, accUserName.IndexOf("\\"));
            string name = accUserName.Substring(accUserName.IndexOf("\\") + 1);

            NetworkCredential nc = new NetworkCredential(name, accUserPass, domain);

            return nc;
        }
    }
}
