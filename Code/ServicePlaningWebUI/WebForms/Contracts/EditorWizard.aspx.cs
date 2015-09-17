using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Contracts
{
    public partial class NewContractWizard : BasePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                var res = OXml.GetMarkupValues(fileUpload.FileContent);

                foreach (var r in res)
                {
                    Label1.Text += String.Format("Key: {0} Val: {1} ;", r.Key, r.Value);
                }
            }
        }
        
    }
}