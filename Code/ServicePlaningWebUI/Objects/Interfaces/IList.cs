using System.Data;

namespace ServicePlaningWebUI.Objects.Interfaces
{
    interface IList
    {
        DataTable GetSelectionList();
    }
}
