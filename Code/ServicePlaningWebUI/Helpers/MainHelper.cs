using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls.ModelBinding;
using Microsoft.SqlServer.Server;
using ServicePlaningWebUI.Objects;

namespace ServicePlaningWebUI.Helpers
{
    public static class MainHelper
    {
        #region Основные константы

        public enum ListFirstItemType
        {
            Nullable, SelectAll
        }

        private const string listDefaultDataValueField = "id";
        private const string listDefaultDataTextField = "name";
        private const string ddlEmptyText = "--выберите значение--";
        public const string ddlEmptyValue = "-1";
        public const string ddlSelectAllText = "--все--";
        public const string ddlSelectAllValue = "-13";
        public const string rblSelectAllText = "все";
        public const string rblSelectAllValue = "-13";

        #endregion

        #region Работа с контролами страницы

        #region Списки

        /// <summary>
        /// Устанавливает нулевое значение либо пустой элемент в списке выбора
        /// </summary>
        /// <param name="ddl">Список выбора</param>
        public static void DdlSetEmptyOrSelectAllSelectedIndex(ref DropDownList ddl, bool enable = true)
        {
            if (ddl.Items.Count > 0 && (ddl.Items[0].Value.Equals(ddlEmptyValue) || ddl.Items[0].Value.Equals(ddlSelectAllValue)))
            {
                ddl.SelectedIndex = 0;
            }
            else
            {
                ddl.SelectedIndex = -1;
            }

            ddl.Enabled = enable;
        }

        public static void LbSetEmptyOrSelectAllSelectedIndex(ref ListBox lb, bool enable = true)
        {
            if (lb.Items.Count > 0 && (lb.Items[0].Value.Equals(ddlEmptyValue) || lb.Items[0].Value.Equals(ddlSelectAllValue)))
            {
                lb.SelectedIndex = 0;
            }
            else
            {
                lb.SelectedIndex = -1;
            }

            lb.Enabled = enable;
        }

        public static void DdlSetSelectedValue(ref DropDownList ddl, object value, bool enable = true)
        {
            if (ddl.Items.Count > 0)
            {
                try
                {
                    ddl.SelectedValue = value.ToString();
                }
                catch (Exception exception)
                {
                    DdlSetEmptyOrSelectAllSelectedIndex(ref ddl);
                }
            }

            ddl.Enabled = enable;
        }

        public static void LbSetSelectedValue(ref ListBox lb, object value, bool enable = true)
        {
            if (lb.Items.Count > 0)
            {
                try
                {
                    lb.SelectedValue = value.ToString();
                }
                catch (Exception exception)
                {
                    LbSetEmptyOrSelectAllSelectedIndex(ref lb);
                }
            }

            lb.Enabled = enable;
        }

        public static string DdlGetSelectedValue(ref DropDownList ddl, bool positiveOrNull = false)
        {
            if (positiveOrNull && (ddl.SelectedValue == ddlEmptyValue || ddl.SelectedValue == ddlSelectAllValue))
            {
                return null;
            }

            string result = ddl.SelectedValue;
            return result;
        }

        public static string LbGetSelectedValue(ref ListBox lb, bool positiveOrNull = false)
        {
            if (positiveOrNull && (lb.SelectedValue == ddlEmptyValue || lb.SelectedValue == ddlSelectAllValue))
            {
                return null;
            }

            string result = lb.SelectedValue;
            return result;
        }

        public static int? DdlGetSelectedValueInt(ref DropDownList ddl, bool positiveOrNull = false)
        {
            string resStr = DdlGetSelectedValue(ref ddl, positiveOrNull);

            if (!positiveOrNull || !String.IsNullOrEmpty(resStr))
            {
                int result = Convert.ToInt32(resStr);
                return result;
            }

            return null;
        }

        public static int DdlGetSelectedValueInt(ref DropDownList ddl)
        {
            int result = Convert.ToInt32(DdlGetSelectedValue(ref ddl));
            return result;
        }

        public static int LbGetSelectedValueInt(ref ListBox lb)
        {
            int result = Convert.ToInt32(LbGetSelectedValue(ref lb));
            return result;
        }
        

        public static string DdlGetSelectedText(ref DropDownList ddl)
        {
            string result = ddl.SelectedItem.Text;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="list"></param>
        /// <param name="appendFirstItem">Добваить нулевое значение</param>
        public static void DdlFill(ref DropDownList ddl, object list, bool appendFirstItem = false, ListFirstItemType firstItemType = ListFirstItemType.Nullable, string valueField = listDefaultDataValueField, string textField = listDefaultDataTextField)
        {
            ddl.Items.Clear();
            bool appDatBouItems = ddl.AppendDataBoundItems;//Запоминаем текущее значение, чтобы потом восстановить

            if (appendFirstItem)
            {
                ddl.AppendDataBoundItems = true;
                ListItem li = new ListItem();

                switch (firstItemType)
                {
                    case ListFirstItemType.Nullable:
                        li.Text = ddlEmptyText;
                        li.Value = ddlEmptyValue;
                        break;
                    case ListFirstItemType.SelectAll:
                        li.Text = ddlSelectAllText;
                        li.Value = ddlSelectAllValue;
                        break;
                }

                ddl.Items.Add(li);
            }

            ddl.DataTextField = textField;
            ddl.DataValueField = valueField;
            ddl.DataSource = list;
            ddl.DataBind();

            if (appendFirstItem)
            {
                ddl.AppendDataBoundItems = appDatBouItems;//Восстанавливаем запомненное значение
            }
        }

        public static void LbFill(ref ListBox lb, object list, bool appendFirstItem = false, ListFirstItemType firstItemType = ListFirstItemType.Nullable)
        {
            lb.Items.Clear();
            bool appDatBouItems = lb.AppendDataBoundItems;//Запоминаем текущее значение, чтобы потом восстановить

            if (appendFirstItem)
            {
                lb.AppendDataBoundItems = true;
                ListItem li = new ListItem();

                switch (firstItemType)
                {
                    case ListFirstItemType.Nullable:
                        li.Text = ddlEmptyText;
                        li.Value = ddlEmptyValue;
                        break;
                    case ListFirstItemType.SelectAll:
                        li.Text = ddlSelectAllText;
                        li.Value = ddlSelectAllValue;
                        break;
                }

                lb.Items.Add(li);
            }

            lb.DataTextField = listDefaultDataTextField;
            lb.DataValueField = listDefaultDataValueField;
            lb.DataSource = list;
            lb.DataBind();

            if (appendFirstItem)
            {
                lb.AppendDataBoundItems = appDatBouItems;//Восстанавливаем запомненное значение
            }
        }

        public static void DdlFill(ref DropDownList ddl, object list, object defaultValue)
        {
            DdlFill(ref ddl, list);
            DdlSetSelectedValue(ref ddl, defaultValue);
        }

        public static void RblFill(ref RadioButtonList rbl, object list, bool appendFirstItem = false, ListFirstItemType firstItemType = ListFirstItemType.SelectAll)
        {
            rbl.Items.Clear();
            bool appDatBouItems = rbl.AppendDataBoundItems;//Запоминаем текущее значение, чтобы потом восстановить

            if (appendFirstItem)
            {
                rbl.AppendDataBoundItems = true;
                ListItem li = new ListItem();

                switch (firstItemType)
                {
                    case ListFirstItemType.SelectAll:
                        li.Text = rblSelectAllText;
                        li.Value = rblSelectAllValue;
                        break;
                }

                rbl.Items.Add(li);
            }

            rbl.DataTextField = listDefaultDataTextField;
            rbl.DataValueField = listDefaultDataValueField;
            rbl.DataSource = list;
            rbl.DataBind();

            if (appendFirstItem)
            {
                rbl.AppendDataBoundItems = appDatBouItems;//Восстанавливаем запомненное значение
            }
        }

        #endregion

        #region Текстовые поля

        /// <summary>
        /// Устанавливает пустое значение в текстовое поле
        /// </summary>
        /// <param name="txt">Текстовое поле</param>
        public static void TxtSetEmptyText(ref TextBox txt, bool enable = true)
        {
            txt.Text = string.Empty;
            txt.Enabled = enable;
        }

        public static void TxtSetText(ref TextBox txt, object text, bool enable = true)
        {
            if (text == null) text = string.Empty;
            txt.Text = text.ToString();
            txt.Enabled = enable;
        }

        public static void TxtSetDate(ref TextBox txt, object text, bool enable = true, bool shortYear = false)
        {
            if (text == null) text = string.Empty;
            txt.Text = String.Format("{0:dd/MM/yyyy}", text);

            if (shortYear)
            {
                txt.Text = String.Format("{0:dd/MM/yy}", text);
            }

            txt.Enabled = enable;
        }

        public static string TxtGetText(ref TextBox txt)
        {
            string result = txt.Text.Trim();
            if (String.IsNullOrEmpty(result) || String.IsNullOrWhiteSpace(result)) result = null;
            return result;
        }

        public static DateTime TxtGetTextDateTime(ref TextBox txt)
        {
            string text = TxtGetText(ref txt);
            DateTime result;
            result = DateTime.Parse(text);
            return result;
        }

        public static DateTime? TxtGetTextDateTime(ref TextBox txt, bool positiveOrNull)
        {
            string text = TxtGetText(ref txt);
            if (!positiveOrNull || !String.IsNullOrEmpty(text))
            {
                DateTime result;
                result = DateTime.Parse(text);
                return result;
            }
            return null;
        }

        public static decimal TxtGetTextDecimal(ref TextBox txt)
        {
            string text = TxtGetText(ref txt);
            Decimal result = Convert.ToDecimal(text);
            return result;
        }

        public static decimal? TxtGetTextDecimal(ref TextBox txt, bool positiveOrNull)
        {
            string text = TxtGetText(ref txt);

            if (positiveOrNull && String.IsNullOrEmpty(text)) return null;

            Decimal result = Convert.ToDecimal(text);
            return result;
        }

        public static int TxtGetTextInt32(ref TextBox txt)
        {
            string text = TxtGetText(ref txt);
            int result = Convert.ToInt32(text);
            return result;
        }

        public static int? TxtGetTextInt32(ref TextBox txt, bool positiveOrNull, bool returnZeroIfError = false)
        {
            string text = TxtGetText(ref txt);

            if (positiveOrNull && String.IsNullOrEmpty(text)) return null;

            int result;

            if (returnZeroIfError)
            {
                try
                {
                    result = Convert.ToInt32(text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            result = Convert.ToInt32(text);
            return result;
        }

        #endregion

        #region Скрытые поля

        public static void HfSetEmptyValue(ref HiddenField hf)
        {
            hf.Value = string.Empty;
        }

        public static void HfSetValue(ref HiddenField hf, object text)
        {
            if (text == null) text = string.Empty;
            hf.Value = text.ToString();
        }

        public static string HfGetValue(ref HiddenField hf)
        {
            string result = hf.Value;
            if (String.IsNullOrEmpty(result) || String.IsNullOrWhiteSpace(result)) result = null;
            return result;
        }

        public static int HfGetValueInt32(ref HiddenField hf)
        {
            string text = HfGetValue(ref hf);
            int result = Convert.ToInt32(text);
            return result;
        }

        #endregion

        #region Галочки

        public static void ChkSetValue(ref CheckBox chk, object value)
        {
            bool val = false;

            try
            {
                val = (bool)value;
            }
            catch (FormatException)
            {
                val = false;
            }

            chk.Checked = val;
        }

        public static bool ChkGetValueBool(ref CheckBox chk)
        {
            bool result = chk.Checked;
            return result;
        }

        public static int ChkGetValueInt(ref CheckBox chk)
        {
            int result = chk.Checked ? 1 : 0;
            return result;
        }

        public static string ChkGetValueIntStr(ref CheckBox chk)
        {
            int result = ChkGetValueInt(ref chk);
            return result.ToString();
        }

        public static void ChkListFill(ref CheckBoxList chkl, object list)
        {
            chkl.DataTextField = listDefaultDataTextField;
            chkl.DataValueField = listDefaultDataValueField;
            chkl.DataSource = list;
            chkl.DataBind();
        }

        public static string[] ChkListGetCheckedValues(ref CheckBoxList chkl)
        {
            List<string> result = new List<string>();

            foreach (ListItem item in chkl.Items)
            {
                if (item.Selected)
                {
                    result.Add(item.Value);
                }
            }

            return result.ToArray();
        }

        public static int[] ChkListGetCheckedValuesInt(ref CheckBoxList chkl)
        {
            List<int> result = new List<int>();

            string[] arr = ChkListGetCheckedValues(ref chkl);

            foreach (string s in arr)
            {
                result.Add(Convert.ToInt32(s));
            }

            return result.ToArray();
        }

        public static string ChkListGetCheckedValuesString(ref CheckBoxList chkl)
        {
            string result = String.Empty;


            string[] arr = ChkListGetCheckedValues(ref chkl);

            result = string.Join(",", arr);

            return result;
        }

        public static void ChkListSetSelectedValues(ref CheckBoxList chkl, object[] values, bool enable = true, bool clearOtherValues = false)
        {
            foreach (ListItem item in chkl.Items)
            {
                if (clearOtherValues)
                {
                    item.Selected = false;
                }

                if (values != null)
                {
                    foreach (object value in values)
                    {
                        if (item.Value.Equals(value.ToString()))
                        {
                            item.Selected = true;
                            break; //value.ToString().Equals(item.Value);
                        }
                    }
                }
            }

            //if (chkl.Items.Count > 0)
            //{
            //    foreach (object value in values)
            //    {
            //        chkl.Items.FindByValue(value.ToString()).Selected = true;
            //    }
            //}

            chkl.Enabled = enable;
        }

        public static void RblSetValue(ref RadioButtonList rbl, string value, bool enable = true)
        {
            rbl.SelectedValue = value;
            rbl.Enabled = enable;
        }

        public static bool RblGetValueBool(ref RadioButtonList rbl)
        {
            string val = rbl.SelectedValue;
            return val.Equals("1");
        }

        public static bool? RblGetValueBool(ref RadioButtonList rbl, bool positiveOrNull)
        {
            string val = rbl.SelectedValue;

            if (!val.Equals("1") && !val.Equals("0") && positiveOrNull) return null;

            return val.Equals("1");
        }

        #endregion

        #region Кнопки

        public static string GetSenderCommandArgument(object sender)
        {
            string result;

            try
            {
                if (sender is LinkButton)
                {
                    result = (sender as LinkButton).CommandArgument;
                }
                else if (sender is ImageButton)
                {
                    result = (sender as ImageButton).CommandArgument;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception exception)
            {
                result = string.Empty;
            }

            return result;
        }

        public static int GetSenderCommandArgumentInt(object sender)
        {
            int result = -1;
            string ca = GetSenderCommandArgument(sender);
            if (!String.IsNullOrEmpty(ca))
            {
                result = Convert.ToInt32(ca);
            }
            return result;
        }

        #endregion

        #region URL

        //private const string replaceUrlItem = @"/WebForms";
        //private const string replaceUrlItem2 = @"\WebForms";//Как-то раз так случилось, избегаем


        //private static string UrlPrepare(string url)
        //{
        //    //Удаляем папочку которую скрыли в роутинг мапе, хотя и с ней будет норм работать (для красоты)
        //    return url.Replace(replaceUrlItem2, String.Empty).Replace(replaceUrlItem, String.Empty);
        //}

        private static Dictionary<string, string> UrlQueryStringParamsSplit(string queryParams)
        {
            //TODO: Добавить проверку на валидность самой сторки регулярным выражением ?id=1&bd=2
            var qParams = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(queryParams))
            {
                string[] qp = queryParams.Split('&');

                foreach (string p in qp)
                {
                    string[] kv = p.Split('=');
                    string key = kv[0];
                    string val = kv.Count() > 1 ? kv[1] : String.Empty;

                    qParams.Add(key, val);
                }
            }

            return qParams;
        }

        public static string UrlQueryStringParamsJoin(Dictionary<string, string> queryParams)
        {
            string result = string.Empty;

            string[] arrParams = new string[queryParams.Count];
            int i = 0;

            foreach (KeyValuePair<string, string> param in queryParams)
            {
                arrParams[i] = String.Format("{0}={1}", param.Key, param.Value);
                ; i++;
            }

            result = String.Join("&", arrParams);

            return result;
        }

        public static string UrlQueryStringParamsJoin(string[] queryParams)
        {
            string result = string.Empty;

            //string[] arrParams = new string[queryParams.Count];
            //int i = 0;

            //foreach (KeyValuePair<string, string> param in queryParams)
            //{
            //    arrParams[i] = String.Format("{0}={1}", param.Key, param.Value);
            //    ; i++;
            //}

            result = String.Join("&", queryParams);

            return result;
        }

        public static string UrlQueryStringParamsMerge(string currUrl, Dictionary<string, string> newQueryParams, string newAbsolutePath = null)
        {
            string[] arrUrl = currUrl.Split('?');
            string newUrl = newAbsolutePath ?? arrUrl[0];//Отделяем часть без queryString
            string currQueryString = String.Empty;
            Dictionary<string, string> currQueryParams = new Dictionary<string, string>();

            if (arrUrl.Count() > 1)
            {
                currQueryString = arrUrl[1];
                currQueryParams = UrlQueryStringParamsSplit(currQueryString);
            }

            if (currQueryParams.Count > 0)
            {
                //Dictionary<string, string> queryParams = currQueryParams;

                foreach (KeyValuePair<string, string> currPar in currQueryParams)
                {
                    if (newQueryParams.ContainsKey(currPar.Key))
                    {
                        //Если значение параметра другое, то заменяем иначе оставляем
                        if (!currPar.Value.Equals(newQueryParams[currPar.Key]))
                        {
                            newQueryParams[currPar.Key] = newQueryParams[currPar.Key];
                        }
                    }
                    else
                    {
                        newQueryParams.Add(currPar.Key, currQueryParams[currPar.Key]);
                    }
                }
            }

            string newQueryString = UrlQueryStringParamsJoin(newQueryParams);
            if (!String.IsNullOrEmpty(newQueryString))
            {
                newUrl = String.Format("{0}?{1}", newUrl, newQueryString);
            }

            return newUrl;
        }

        public static string UrlQueryStringParamsMerge(string currUrl, string newQueryParams, string newAbsolutePath = null)
        {
            var queryParams = UrlQueryStringParamsSplit(newQueryParams);

            return UrlQueryStringParamsMerge(currUrl, queryParams, newAbsolutePath);
        }

        //public static string UrlQueryStringParamsMerge(string currUrl, string[] newQueryParams, string newAbsolutePath = null)
        //{
        //    string newQueryParamsJoin = String.Join("&", newQueryParams);
        //    var queryParams = UrlQueryStringParamsSplit(newQueryParamsJoin);

        //    return UrlQueryStringParamsMerge(currUrl, queryParams, newAbsolutePath);
        //}

        public static string GetUrlQueryParamValue(ref HttpRequest request, string paramName)
        {
            string value = string.Empty;

            if (request.QueryString[paramName] != null)
            {
                value = request.QueryString[paramName];
            }

            return value;
        }

        #endregion

        #endregion
    }
}