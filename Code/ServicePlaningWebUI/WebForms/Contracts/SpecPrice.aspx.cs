using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Contracts
{
    public partial class SpecPrice : BasePage
    {
        private int Id
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                return id;
            }
        }

        protected string FormTitle;

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                FormTitle = "Спеццены для договора";

                if (Id > 0)
                {
                    Contract contract = new Contract(Id);
                    FillFormData(contract);
                }
            }
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillLists();
            }
        }

        private void FillFormData(Contract contract)
        {
            bool isEdit = Id > 0;

            FormTitle = !isEdit ? "Спеццены для договора" : String.Format("Спеццены для договора №{0}", contract.Number);
        }

        private void FillLists()
        {
            int? idContract = Id > 0 ? new int?(Id) : null;

            MainHelper.DdlFill(ref ddlContracts, Db.Db.Srvpl.GetContractSelectionList(idContract), idContract == null);
        }

        protected void tblList_DataBound(object sender, EventArgs e)
        {
            DataView dv = (DataView)sdsList.Select(DataSourceSelectArguments.Empty);
            lRowsCount.Text = dv.Count.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                tblList.DataBind();
                //RedirectWithParams();
            }
            catch (Exception ex)
            {
                ServerMessageDisplay(new[] { phServerMessage }, ex.Message, true);
            }
        }

        private void Save()
        {
            Models.SpecPrice specPrice = GetFormData();
            specPrice.Save();
        }

        private Models.SpecPrice GetFormData()
        {
            var specPrice = new Models.SpecPrice();

            //specPrice.Id = Id;
            specPrice.IdContract = Id;
            specPrice.IdNomenclature = MainHelper.HfGetValueInt32(ref hfIdNomenclatureNum);
            specPrice.NomenclatureName = MainHelper.TxtGetText(ref txtNomenclatureName);
            specPrice.Price = MainHelper.TxtGetTextDecimal(ref txtPrice);
            specPrice.CatalogNum = MainHelper.TxtGetText(ref txtCatalogNum);
            specPrice.IdCreator = User.Id;

            return specPrice;
        }

        protected void txtCatalogNum_OnTextChanged(object sender, EventArgs e)
        {
            string catalogNum = MainHelper.TxtGetText(ref txtCatalogNum);
            string nomenclatureName;
            int? idNomenclature;

            Models.SpecPrice.GetNomenclatureName(catalogNum, out nomenclatureName, out idNomenclature);
            
            MainHelper.TxtSetText(ref txtNomenclatureName, nomenclatureName);
            MainHelper.HfSetValue(ref hfIdNomenclatureNum, idNomenclature);
        }

        protected void ddlContracts_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int idContract = MainHelper.DdlGetSelectedValueInt(ref ddlContracts);

            RedirectWithParams(String.Format("id={0}", idContract), false);
        }
    }
}