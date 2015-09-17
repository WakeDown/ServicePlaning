using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ServicePlaningWebUI.Objects
{
    [Serializable]
    public class AdGroup
    {
        public string SID { get; set; }
        public string Name { get; set; } 
    }
}