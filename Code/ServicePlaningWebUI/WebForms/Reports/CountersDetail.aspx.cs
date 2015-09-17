using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Models;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.WebForms.Reports
{
    public partial class CountersDetail : BasePage
    {
        protected const string ClaimEditorForm = "http://dsu-zip.un1t.group/Claims/Editor";

        private int Id
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["id"], out id);

                return id;
            }
        }

        private int IdContract
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["cid"], out id);

                return id;
            }
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                if (Id > 0)
                {
                    Device device = new Device(Id);

                    FillFormData(device);
                }

                //int contractId;
                //int.TryParse(User.Login, out contractId);//Вставляем только число id_contract 

                //sdsList.SelectParameters["id_contractor"].DefaultValue = contractId.ToString();
            }
        }

        private void FillFormData(Device device)
        {
            bool isEdit = Id > 0;

            Contract contract = new Contract(IdContract);

            lFormTitle.Text = !isEdit ? String.Empty : String.Format("Просмотр истории объема печати аппарата {1} №{0} (договор №{2})", device.SerialNum, device.Model, contract.Number);

            var dt = Db.Db.Srvpl.GetContractorDevice(Id, IdContract);

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];

                lDeviceAddress.Text = !isEdit
                    ? String.Empty
                    : String.Format("{0}{3} {1}{4} {2}", row["city"], row["address"], row["object_name"],
                        !String.IsNullOrEmpty(row["address"].ToString()) ? "," : String.Empty,
                        !String.IsNullOrEmpty(row["object_name"].ToString()) ? "," : String.Empty);
            }
        }

        protected void tblDeviceCounterHistory_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //if (e.Item.ItemIndex > 0)
                //{
                //    var prevItem = tblDeviceCounterHistory.Items[e.Item.ItemIndex - 1];

                //    //Считаем разницу Счетчик ЧБ
                //    var tdPrevCounter = prevItem.FindControl("tdCounter") as HtmlTableCell;
                //    if (tdPrevCounter != null)
                //    {
                //        int prevCounter;
                //        int.TryParse(tdPrevCounter.InnerText.Replace(" ", String.Empty).Trim(), out prevCounter);

                //        var tdCounter = e.Item.FindControl("tdCounter") as HtmlTableCell;

                //        if (tdCounter != null)
                //        {
                //            int counter;
                //            int.TryParse(tdCounter.InnerText.Replace(" ", String.Empty).Trim(), out counter);

                //            var tdPrevDiff = prevItem.FindControl("tdDiff") as HtmlTableCell;
                //            if (tdPrevDiff != null)
                //            {
                //                tdPrevDiff.InnerText = (prevCounter - counter).ToString("### ### ### ### ###");
                //            }
                //        }
                //    }

                //    //Считаем разницу Счетчик Цвет
                //    var tdPrevCounterColor = prevItem.FindControl("tdCounterColor") as HtmlTableCell;
                //    if (tdPrevCounterColor != null)
                //    {
                //        int prevCounterColor;
                //        int.TryParse(tdPrevCounter.InnerText.Replace(" ", String.Empty).Trim(), out prevCounterColor);

                //        var tdCounterColor = e.Item.FindControl("tdCounterColor") as HtmlTableCell;

                //        if (tdCounterColor != null)
                //        {
                //            int counterColor;
                //            int.TryParse(tdCounterColor.InnerText.Replace(" ", String.Empty).Trim(), out counterColor);

                //            var tdPrevDiffColor = prevItem.FindControl("tdDiffColor") as HtmlTableCell;
                //            if (tdPrevDiffColor != null)
                //            {
                //                tdPrevDiffColor.InnerText = (prevCounterColor - counterColor).ToString("### ### ### ### ###");
                //            }
                //        }
                //    }

                //    //Считаем разницу Счетчик общий
                //    var tdPrevCounterTotal = prevItem.FindControl("tdCounterTotal") as HtmlTableCell;
                //    if (tdPrevCounterTotal != null)
                //    {
                //        int prevCounterTotal;
                //        int.TryParse(tdPrevCounter.InnerText.Replace(" ", String.Empty).Trim(), out prevCounterTotal);

                //        var tdCounterTotal = e.Item.FindControl("tdCounterTotal") as HtmlTableCell;

                //        if (tdCounterTotal != null)
                //        {
                //            int counterTotal;
                //            int.TryParse(tdCounterTotal.InnerText.Replace(" ", String.Empty).Trim(), out counterTotal);

                //            var tdPrevDiffTotal = prevItem.FindControl("tdDiffTotal") as HtmlTableCell;
                //            if (tdPrevDiffTotal != null)
                //            {
                //                tdPrevDiffTotal.InnerText = (prevCounterTotal - counterTotal).ToString("### ### ### ### ###");
                //            }
                //        }
                //    }
                //}
            }
        }

        protected void btnShowHistory_OnClick(object sender, EventArgs e)
        {
            if (!rtrClimUnitsHistory.Visible)
            {
                int? idDevice = Db.Db.GetValueIntOrNull(Request.QueryString["id"]);
                var dt = Db.Db.Zipcl.GetClaimUnitHistoryList(idDevice);

                DataColumn col = dt.Columns.Add("BgCol", typeof(string));
                int claimId = -1;
                string color1 = "#E5E5E5";
                string color2 = "#ffffff";
                string curColor = String.Empty;

                foreach (DataRow dr in dt.Rows)
                {
                    int? curClaimId = Db.Db.GetValueIntOrNull(dr["id_claim"].ToString());

                    if (curClaimId.HasValue && curClaimId.Value != claimId)
                    {
                        curColor = curColor.Equals(color1) ? color2 : color1;
                        claimId = curClaimId.Value;
                    }

                    dr["BgCol"] = curColor;
                }

                rtrClimUnitsHistory.Visible = true;
                rtrClimUnitsHistory.DataSource = dt;
                rtrClimUnitsHistory.DataBind();
            }
            else
            {
                rtrClimUnitsHistory.Visible = false;
                rtrClimUnitsHistory.DataSource = null;
                rtrClimUnitsHistory.DataBind();
            }
        }
    }
}