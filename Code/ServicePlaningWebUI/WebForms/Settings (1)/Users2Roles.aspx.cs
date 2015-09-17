using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Settings
{
    public partial class Users2Roles : BasePage
    {
        private string serviceAdminRightGroup = ConfigurationManager.AppSettings["serviceAdminRightGroup"];

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                FillLists();
            }
        }

        private void FillLists()
        {
            rtrUsers.DataSource = Db.Db.Users.GetUsersSelectionList(serviceAdminRightGroup);
            rtrUsers.DataBind();
        }

        protected void rtrUsers_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ddlUserRole = e.Item.FindControl("ddlUserRole") as DropDownList;

            if (ddlUserRole != null)
            {
                MainHelper.DdlFill(ref ddlUserRole, Db.Db.Srvpl.GetUserRoleSelectionList(), true);
                int idUser = Convert.ToInt32((e.Item.FindControl("hfIdUser") as HiddenField).Value);
                int idUserRole = new User2UserRole(idUser).IdUserRole;
                MainHelper.DdlSetSelectedValue(ref ddlUserRole, idUserRole);
            }
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
            foreach (RepeaterItem item in rtrUsers.Items)
            {
                DropDownList ddlUserRole = (item.FindControl("ddlUserRole") as DropDownList);

                int idUserRole = MainHelper.DdlGetSelectedValueInt(ref ddlUserRole);

                int idUser = Convert.ToInt32((item.FindControl("hfIdUser") as HiddenField).Value);
                int idCreator = User.Id;

                new User2UserRole() { IdUserRole = idUserRole, IdUser = idUser, IdCreator = idCreator }.Save();
            }
        }
    }
}