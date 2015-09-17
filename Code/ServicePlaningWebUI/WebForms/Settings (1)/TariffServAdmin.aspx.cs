using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Settings
{
    public partial class PaymentTariff : BasePage
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
            foreach (RepeaterItem item in rtrPaymentTariffs.Items)
            {
                int idUserRole = Convert.ToInt32((item.FindControl("hfIdUserRole") as HiddenField).Value);
                double price = Convert.ToDouble((item.FindControl("txtPrice") as TextBox).Text);
                int idCreator = User.Id;

                new Models.PaymentTariff() { IdUserRole = idUserRole, Price = price, IdCreator = idCreator }.Save();
            }
        }
    }
}