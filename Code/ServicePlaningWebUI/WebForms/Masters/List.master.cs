﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicePlaningWebUI.WebForms.Masters
{
    public partial class List : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterStartupScripts();

            if (Request.QueryString["hidenav"] != null)
            {
                accordion.Style.Add("display", "none");
            }
        }

        private void RegisterStartupScripts()
        {
            //<Память для фильтра на раскрытие/закрытие>
            string script = String.Format(@"$(function() {{ initFilterExpandMemmory('{0}', '{1}') }});", "filterHead", "filterPanel");

            ScriptManager.RegisterStartupScript(this, GetType(), "filterExpandMemmory", script, true);
            //</Память для фильтра на раскрытие/закрытие>
        }
    }
}