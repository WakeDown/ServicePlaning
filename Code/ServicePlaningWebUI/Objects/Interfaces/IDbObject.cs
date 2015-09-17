using System;

namespace ServicePlaningWebUI.Objects.Interfaces
{
    interface IDbObject<in T>
    {
        void Get(T id);
        void Save();
        void Delete(T id);
        void Delete(T id, int idCreator);
    }
}
