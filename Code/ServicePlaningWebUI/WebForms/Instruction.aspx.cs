using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms
{
    public partial class Instruction : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Site p = Page.Master as Site;
            Label1.Text = p.User.DisplayName; //Page.Master.User.DisplayName;

            //Site myMasterPage = Page.Master as Site; 
        }
    }
}