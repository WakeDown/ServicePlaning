using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Settings
{
    public partial class TariffEngeneer : BasePage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void Save()
        {
            foreach (RepeaterItem item in rtrFeatures.Items)
            {
                int id = Convert.ToInt32((item.FindControl("hfIdFeature") as HiddenField).Value);
                double price = Convert.ToDouble((item.FindControl("txtPrice") as TextBox).Text);
                int idCreator = User.Id;

                new TariffFeature() { Id = id, Price = price, IdCreator = idCreator }.Save();
            }
        }

        protected void btnSetTariff_Click(object sender, EventArgs e)
        {
            bool all = true;//chkAll.Checked;
            int idCreator = User.Id;

            Db.Db.Srvpl.SetNewDeviceTariff(all, idCreator);
        }
    }
}