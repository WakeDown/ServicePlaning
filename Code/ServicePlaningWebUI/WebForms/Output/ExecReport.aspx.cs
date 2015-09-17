using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Objects;
using ServicePlaningWebUI.WebForms.Masters;

namespace ServicePlaningWebUI.WebForms.Output
{
    public partial class ExecReport : BasePage
    {
        protected new void Page_PreLoad(object sender, EventArgs e)
        {
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            
        }

        protected void tblListExecServAdmins_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Footer)
            //{
            //    SetExecServAdminsSum();
            //}
        }

        //protected void SetExecServAdminsSum()
        //{
        //    //SetCountExec();

        //    DataView dv = (DataView)sdsListExecByServAdmins.Select(DataSourceSelectArguments.Empty);

        //    string plan_cnt = dv.Table.Compute("Sum(plan_cnt)", "").ToString();
        //    string done_cnt = dv.Table.Compute("Sum(done_cnt)", "").ToString();
        //    string residue = dv.Table.Compute("Sum(residue)", "").ToString();

        //    var lPlanSum = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("lPlanSum") as Literal;
        //    var lDoneSum = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("lDoneSum") as Literal;
        //    var lResitudeSum = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("lResitudeSum") as Literal;

        //    if (lPlanSum != null)
        //    {
        //        lPlanSum.Text = plan_cnt;
        //    }

        //    if (lDoneSum != null)
        //    {
        //        lDoneSum.Text = done_cnt;
        //    }

        //    if (lResitudeSum != null)
        //    {
        //        lResitudeSum.Text = residue;
        //    }

        //    var lDonePercentSum = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("lDonePercentSum") as Literal;
        //    var footerChart = tblListExecServAdmins.Controls[tblListExecServAdmins.Controls.Count - 1].Controls[0].FindControl("footerChart") as HtmlContainerControl;

        //    if (footerChart != null && lDonePercentSum != null)
        //    {
        //        int sumDonePercent = Convert.ToInt32((Convert.ToDecimal(done_cnt) / Convert.ToDecimal(plan_cnt)) * 100);
        //        footerChart.Style.Value = String.Format("width: {0}%", sumDonePercent);
        //        lDonePercentSum.Text = String.Format("{0}%", sumDonePercent);
        //        //footerChart.InnerText = String.Format("{0}%", sumDonePercent);
        //    }
        //}
    }
}