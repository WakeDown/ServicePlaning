using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.FriendlyUrls;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;
using WebGrease.Css.Extensions;

namespace ServicePlaningWebUI.WebForms.Devices
{
    public partial class List : BaseFilteredPage
    {
        protected const string sesDeviceIdsKey = "deviceIds";
        /// <summary>
        /// Имя query string параметра для списка выбранных значений
        /// </summary>
        private const string qsValues = "lstids";
        protected const string qspDeviceId = "devid";

        /*queryStringFilterParams
         model - Модель
         sernum - Серийный номер
         speed - Скорость печати
         imprint - отпечаток
         printype - тип печати
         cartype - тип картриджа
         counter - счетчик
         age - возраст
         insdate - дата установки
         islnk - привязан к договору
         */

        protected override void FillFilterLinksDefaults()
        {
            //Если заполненный, занчит уже с умолчаниями
            if (FilterLinks != null) return;

            FilterLinks = new List<FilterLink>();
            FilterLinks.Add(new FilterLink("model", ddlModel));
            FilterLinks.Add(new FilterLink("sernum", txtSerialNum));
            FilterLinks.Add(new FilterLink("speed", txtSpeed));
            FilterLinks.Add(new FilterLink("imprint", ddlDeviceImprint));
            FilterLinks.Add(new FilterLink("printype", ddlPrintType));
            FilterLinks.Add(new FilterLink("cartype", ddlCartridgeType));
            FilterLinks.Add(new FilterLink("counter", txtCounter));
            FilterLinks.Add(new FilterLink("age", txtAge));
            FilterLinks.Add(new FilterLink("insdate", txtInstalationDate));
            FilterLinks.Add(new FilterLink("mdlnme", txtModelSelection));
            FilterLinks.Add(new FilterLink("islnk", rblIsLinked));
            FilterLinks.Add(new FilterLink("rcnt", txtRowsCount, "10"));

            BtnSearchClientId = btnSearch.ClientID;
        }

        protected string FormUrl = FriendlyUrl.Href("~/Devices/Editor");
        protected string Contract2DevicesFormUrl = FriendlyUrl.Href("~/Contracts/Devices/Editor");
        protected string ContracDevtFormUrl = FriendlyUrl.Href("~/Contracts/Devices");
        protected string ContracFormUrl = FriendlyUrl.Href("~/Contracts/Editor");

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFilterLists();
            }

            base.Page_Load(sender, e);

            RegisterStartupScripts();
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFilterLists();
            }
        }

        private void FillFilterLists()
        {
            MainHelper.DdlFill(ref ddlModel, Db.Db.Srvpl.GetDeviceModelSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlDeviceImprint, Db.Db.Srvpl.GetDeviceImprintSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlPrintType, Db.Db.Srvpl.GetPrintTypeSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
            MainHelper.DdlFill(ref ddlCartridgeType, Db.Db.Srvpl.GetCartridgeTypeSelectionList(), true, MainHelper.ListFirstItemType.SelectAll);
        }


        //protected string GetFormUrl(string id = null)
        //{
        //    string url = FormUrl;

        //    if (id != null)
        //    {url = MainHelper.UrlQueryStringParamsReplace(FormUrl, String.Format("id={0}", id)) ;}

        //    return url;
        //}

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            int[] devId = { Convert.ToInt32((sender as LinkButton).CommandArgument) };
            Delete(devId);
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            Search();
        }

        //protected void btnFilterClear_OnClick(object sender, EventArgs e)
        //{
        //    FilterClear();
        //}

        protected void sdsList_OnSelected(object sender, SqlDataSourceStatusEventArgs e)
        {
            int count = e.AffectedRows;
            SetRowsCount(count);
        }

        private void SetRowsCount(int count = 0)
        {
            lRowsCount.Text = count.ToString();
        }

        private void RegisterStartupScripts()
        {
            //<Фильтрация списка по вводимому тексту>
            string script = String.Format(@"$(function() {{$('#{0}').filterByText($('#{1}'), true);}});", ddlModel.ClientID, txtModelSelection.ClientID);

            ScriptManager.RegisterStartupScript(this, GetType(), "filterModelListByName", script, true);
            //</Фильтрация списка>

            if (tblList.Rows.Count > 0)
            {
                //<Чекбокс с квадратиком (tristate checkbox)>
                HtmlContainerControl span = tblList.HeaderRow.FindControl("pnlTristate") as HtmlContainerControl;
                script = String.Format(@"$(function() {{initTriStateCheckBox('{0}', '{1}', false);}});", span.ClientID,
                    tblList.ClientID);

                ScriptManager.RegisterStartupScript(this, GetType(), "tristateCheckbox", script, true);
                //</Чекбокс>

                //<Отметки выбанных записей>
                script = String.Format(@"$(function() {{initSelectedMemmory('{0}', '{2}', '{1}', '{3}');  }});",
                    tblList.ClientID, lChecksCount.ClientID, sesDeviceIdsKey, hfCheckedDeviceIds.ClientID);

                ScriptManager.RegisterStartupScript(this, GetType(), "checkedRows", script, true);
                //вешаем обработчики
                btnCheckedIdsClear.Attributes.Add("onclick", "ClearCheckedRows();");

                //</вешаем обработчики>
                //</Отметки>
            }
        }

        protected int[] GetCheckedDeviceIds()
        {
            string devIds = hfCheckedDeviceIds.Value;

            string[] result = { };

            if (!String.IsNullOrEmpty(devIds))
            {
                result = devIds.Split(',');
            }

            int[] ids = result.Select(d => Convert.ToInt32(d)).ToArray();

            return ids;
        }

        protected void btnCheckedAdd2Contract_OnClick(object sender, EventArgs e)
        {
            int[] devIds = GetCheckedDeviceIds();
            Add2Contract(devIds);
        }

        private void Add2Contract(int[] devIds)
        {
            if (devIds.Any())
            {
                Dictionary<string, string> newQueryParams = new Dictionary<string, string>();

                if (devIds.Count() > 1)
                {
                    string dIds = string.Join(",", devIds);
                    newQueryParams.Add(qsValues, dIds);
                }
                else
                {
                    newQueryParams.Add(qspDeviceId, devIds[0].ToString());
                }

                // bctid - Back Contract ID
                string backContractId = Request.QueryString["bctid"];
                if (!String.IsNullOrEmpty(backContractId)) newQueryParams.Add("id", backContractId);

                RedirectWithParams(newQueryParams, false, Contract2DevicesFormUrl);
            }
        }

        protected void btnCheckedDelete_OnClick(object sender, EventArgs e)
        {
            int[] devIds = GetCheckedDeviceIds();
            Delete(devIds);
        }

        private void Delete(int[] devIds)
        {
            if (devIds.Any())
            {
                foreach (int devId in devIds)
                {
                    try
                    {
                        new Device().Delete(devId);

                    }
                    catch (Exception ex)
                    {
                        ServerMessageDisplay(new [] { phServerMessage }, ex.Message, true);
                    }
                }

                RedirectWithParams();
            }
        }

        protected void btnAdd2Contract_OnClick(object sender, EventArgs e)
        {
            int[] devId = {Convert.ToInt32(((LinkButton) sender).CommandArgument)};
            Add2Contract(devId);
        }

        protected string SetRowMark(string statusSysname)
        {
            string result = string.Empty;

            switch (statusSysname.ToUpper())
            {
                case "1":
                    result = "bg-warning";
                    break;
                case "0":
                    result = "bg-success";
                    break;
            }

            return result;
        }
    }
}