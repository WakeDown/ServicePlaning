using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicePlaningWebUI.WebForms.Contracts
{
    /// <summary>
    /// Сводное описание для FileHandler
    /// </summary>
    public class FileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string progName = context.Request.QueryString["f"];

            if (progName.Equals("ctrWiz"))
            {
                context.Response.ContentType = "application/xls";
                context.Response.AddHeader("content-disposition", "attachment; filename=Template.xlsm");
                context.Response.WriteFile("~/Content/Files/new_contract_claim.xlsm");
            }
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