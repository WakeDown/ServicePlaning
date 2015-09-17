using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace ServicePlaningWebUI.WebForms.Service
{
    /// <summary>
    /// Сводное описание для Service
    /// </summary>
    [WebService(Namespace = "http://unitgroup.ru/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        [ScriptMethod]
        public static string CheckCounter(
            //string counter
            )
        {
            string message = "Вы уверены что правильно внесли счетчик? ";// + counter;

            return message;
        }

        [ScriptMethod]
        public static string Check()
        {
            string message = "Вы уверены что правильно внесли счетчик? ";

            return message;
        }
    }
}
