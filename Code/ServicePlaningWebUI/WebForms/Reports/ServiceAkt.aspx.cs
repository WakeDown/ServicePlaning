using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicePlaningWebUI.WebForms.Reports
{
    public partial class ServiceAkt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script = "$(document).ready(function() { window.print(); });";

            ScriptManager.RegisterStartupScript(this, GetType(), "print",
                            script, true);
            
        }
    }
}