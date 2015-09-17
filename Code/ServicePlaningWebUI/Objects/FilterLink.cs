using System;
using System.Web.UI;

namespace ServicePlaningWebUI.Objects
{
    [Serializable]
    public class FilterLink
    {
        public string ParamName { get; set; }
        //public System.Web.UI.Control Control { get; set; }
        //public string ControlClientId { get; set; }
        public string ControlType { get; private set; }
        public string ControlId { get; set; }
        public string Value { get; set; }
        public string DefaultValue { get; set; }
        public bool ClearValueOnClear { get; set; }

        public FilterLink(string paramName, string controlId, string defaultValue = null, bool clearValueOnClear = true)
        {
            ParamName = paramName;
            ControlId = controlId;
            DefaultValue = defaultValue;
            ClearValueOnClear = clearValueOnClear;
        }

        public FilterLink(string paramName, Control control, string defaultValue = null, bool clearValueOnClear = true)
            : this(paramName, control.UniqueID, defaultValue, clearValueOnClear)
        {
            ControlType = control.GetType().Name;
        }

        
    }
}