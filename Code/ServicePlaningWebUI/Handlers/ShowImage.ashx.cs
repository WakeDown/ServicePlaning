using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.Handlers
{
    /// <summary>
    /// Сводное описание для ShowImage1
    /// </summary>
    public class ShowImage1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string path = context.Request.QueryString["path"];

            NetworkCredential nc = GetNetCredential4Scan();

            using (
                WindowsImpersonationContextFacade impersonationContext =
                    new WindowsImpersonationContextFacade(nc))
            {
                //context.Response.Clear();
                //context.Response.ContentType = getContentType(path);

                byte[] imageByte = File.ReadAllBytes(path);

                if (imageByte != null && imageByte.Length > 0)
                {
                    context.Response.ContentType = getContentType(path);
                    context.Response.BinaryWrite(imageByte);
                }

                //context.Response.BinaryWrite(b);
                ////context.Response.WriteFile(path);
                //context.Response.End();
            }
        }

        private string getContentType(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp":
                    return "Image/bmp";
                case ".gif":
                    return "Image/gif";
                case ".jpg":
                    return "Image/jpeg";
                case ".png":
                    return "Image/png";
                case ".tif":
                    return "Image/tiff";
                default:
                    return "Image/png";
                    break;
            }
            return "";
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}