using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicePlaningWebUI.Helpers;

namespace ServicePlaningWebUI.Objects
{
    public abstract class BaseFilteredPage : BasePage
    {
        private const string FilterLinksKey = "vskeyFilterLinks";
        private const string BtnSearchClientIdKey = "vskeyBtnSearchClientId";
        protected List<FilterLink> FilterLinks { get { if (ViewState[FilterLinksKey] != null) { return (List<FilterLink>)ViewState[FilterLinksKey]; } else { return null; } } set { ViewState[FilterLinksKey] = value; } }
        protected string BtnSearchClientId { get { if (ViewState[BtnSearchClientIdKey] != null) { return (string)ViewState[BtnSearchClientIdKey]; } else { return null; } } set { ViewState[BtnSearchClientIdKey] = value; } }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!IsPostBack)
            {
                FillFilterLinksDefaults();
                GetFilterQueryStringValues();
                FillFilterForm();
                RegisterFilterScripts();
            }
        }

        protected abstract void FillFilterLinksDefaults();

        protected void GetFilterQueryStringValues()
        {
            foreach (FilterLink filterLink in FilterLinks)
            {
                if (Request.QueryString[filterLink.ParamName] != null)
                {
                    filterLink.Value = Request.QueryString[filterLink.ParamName];
                }
            }
        }

        protected void GetFilterUserValues()
        {
            foreach (FilterLink filterLink in FilterLinks)
            {
                string value = null;

                switch (filterLink.ControlType)
                {
                    case "TextBox":
                        //TextBox txt =  FindControl(filterLink.ControlId) as TextBox;
                        //value = MainHelper.TxtGetText(ref txt);
                        value = Request.Form[filterLink.ControlId];
                        value = String.IsNullOrEmpty(value) ? null : value;
                        break;
                    case "DropDownList":
                        //DropDownList ddl = FindControl(filterLink.ControlId) as DropDownList;
                        //value = MainHelper.DdlGetSelectedValue(ref ddl, positiveOrNull: true);
                        value = Request.Form[filterLink.ControlId];
                        value = value != MainHelper.ddlEmptyValue && value != MainHelper.ddlSelectAllValue ? value : null;
                        break;
                    case "HiddenField":
                        //HiddenField hf = FindControl(filterLink.ControlId) as HiddenField;
                        //value = MainHelper.HfGetValue(ref hf);
                        value = Request.Form[filterLink.ControlId];
                        value = String.IsNullOrEmpty(value) ? null : value;
                        break;
                    case "RadioButtonList":
                        value = Request.Form[filterLink.ControlId];
                        value = String.IsNullOrEmpty(value) || value == MainHelper.ddlEmptyValue || value == MainHelper.ddlSelectAllValue ? null : value;
                        break;
                    case "CheckBoxList":
                        CheckBoxList cbl = (CheckBoxList)FindControl(filterLink.ControlId);
                        value = MainHelper.ChkListGetCheckedValuesString(ref cbl);//Request.Form[filterLink.ControlId];
                        value = String.IsNullOrEmpty(value) ? null : value;
                        break;
                }

                filterLink.Value = value;
            }
        }

        protected void FillFilterForm()
        {
            foreach (FilterLink filterLink in FilterLinks)
            {
                string value = filterLink.Value ?? filterLink.DefaultValue;

                switch (filterLink.ControlType)
                {
                    case "TextBox":
                        TextBox txt = FindControl(filterLink.ControlId) as TextBox;
                        MainHelper.TxtSetText(ref txt, value);
                        break;
                    case "DropDownList":
                        DropDownList ddl = FindControl(filterLink.ControlId) as DropDownList;
                        MainHelper.DdlSetSelectedValue(ref ddl, value);
                        break;
                    case "HiddenField":
                        HiddenField hf = FindControl(filterLink.ControlId) as HiddenField;
                        MainHelper.HfSetValue(ref hf, value);
                        break;
                    case "RadioButtonList":
                        RadioButtonList rbl = FindControl(filterLink.ControlId) as RadioButtonList;
                        MainHelper.RblSetValue(ref rbl, value);
                        break;
                    case "CheckBoxList":
                        CheckBoxList cbl = FindControl(filterLink.ControlId) as CheckBoxList;
                        string[] arrVal = value != null ? value.Split(',') : new[] { "" };
                        MainHelper.ChkListSetSelectedValues(ref cbl, arrVal);
                        break;
                }
            }
        }

        protected Dictionary<string, string> GetNewQueryStringParams()
        {
            Dictionary<string, string> newParams = new Dictionary<string, string>();

            foreach (FilterLink filterLink in FilterLinks)
            {
                if (!String.IsNullOrEmpty(filterLink.Value))
                {newParams.Add(filterLink.ParamName, filterLink.Value);}
            }

            return newParams;
        }

        protected void Search()
        {
            GetFilterUserValues();
            Dictionary<string, string> newQueryParams = GetNewQueryStringParams();
            RedirectWithParams(newQueryParams, false);
        }

        protected void RegisterFilterScripts()
        {
            //<Срабатывание по Enter>
            StringBuilder script = new StringBuilder();

            foreach (FilterLink filterLink in FilterLinks)
            {
                string clientId = (FindControl(filterLink.ControlId) as Control).ClientID;

                script.Append(String.Format(@"
var ctrl = document.getElementById('{1}');
ctrl.addEventListener('keyup', function (e) {{
    if (e.keyCode === 13) {{  //checks whether the pressed key is Enter
        document.getElementById('{0}').click();
    }}
}});", BtnSearchClientId, clientId));

                //switch (filterLink.ControlType)
                //{
                //    case "TextBox":
                //        TextBox txt = FindControl(filterLink.ControlId) as TextBox;
                //        MainHelper.TxtSetEmptyText(ref txt);
                //        break;
                //    case "DropDownList":
                //        DropDownList ddl = FindControl(filterLink.ControlId) as DropDownList;
                //        MainHelper.DdlSetEmptyOrSelectAllSelectedIndex(ref ddl);
                //        break;
                //}

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "filterSearchOnEnter", script.ToString(), true);
            //</Срабатывание по Enter>

            //<Очистка фильтра>
            script = new StringBuilder();
            script.AppendLine("function FilterClear() {");

            foreach (FilterLink filterLink in FilterLinks)
            {
                if (!filterLink.ClearValueOnClear) continue;

                string clientId = (FindControl(filterLink.ControlId) as Control).ClientID;

                switch (filterLink.ControlType)
                {
                    case "TextBox":
                        script.AppendLine("var txt" + filterLink.ParamName + "=document.getElementById('" + clientId +
                                          "').value = '" + filterLink.DefaultValue + "';");
                        
                        break;
                    case "DropDownList":
                        script.AppendLine("var ddl" + filterLink.ParamName + "=document.getElementById('" + clientId +
                                          "').selectedIndex = 0;");
                        break;
                    case "HiddenField":
                        script.AppendLine("var hf" + filterLink.ParamName + "=document.getElementById('" + clientId +
                                          "').value = '" + filterLink.DefaultValue + "';");
                        break;
                    case "RadioButtonList":
                        script.AppendLine("var rbl" + filterLink.ParamName + "=document.getElementById('" + clientId +
                                          "');$(rbl" + filterLink.ParamName + ").find(':radio').removeAttr('checked');");
                        //script.AppendLine("var x = 0;for(x = 0; x < rbl" + filterLink.ParamName + ".length; x++){rbl" + filterLink.ParamName + "[x].checked=false;}");
                        break;
                    case "CheckBoxList":
                        script.AppendLine("var chk" + filterLink.ParamName + "=document.getElementById('" + clientId +
                                          "');$(chk" + filterLink.ParamName + ").find(':checkbox').removeAttr('checked');");
                        //script.AppendLine("var x = 0;for(x = 0; x < rbl" + filterLink.ParamName + ".length; x++){rbl" + filterLink.ParamName + "[x].checked=false;}");
                        break;
                }

            }

            script.AppendLine("}");

            ScriptManager.RegisterStartupScript(this, GetType(), "filterClear", script.ToString(), true);
            //</Очистка фильтра>
        }


        //protected void GoToPage(int newPageIndex = 0, bool next = false, bool previous = false)
        //{
        //    if (newPageIndex <= 0)
        //    {
        //        string pageIndexQueryStringKey = "pind";

        //        string currPind = Request.QueryString[pageIndexQueryStringKey];

        //        int currP;
        //        int.TryParse(currPind, out currP); //Вернет 0 если там не цифра

        //        newPageIndex = next ? currP++ : currP;
        //        newPageIndex = previous ? currP-- : currP;

        //        if (newPageIndex < 0) newPageIndex = 0;
        //    }

        //    string queryParams = String.Format("pind={0}", newPageIndex);

        //    RedirectWithParams(queryParams);
        //}
    }
}