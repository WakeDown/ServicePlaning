using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Db;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class SpecPrice : Db.Db, IDbObject<int>
    {
        public int Id;
        public int IdContract;
        public int? IdNomenclature;
        public string NomenclatureName;
        public string CatalogNum;
        public decimal Price;
        public int? IdCreator;

        public SpecPrice()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract_spec_price", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getSrvplContractSpecPrice", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_contract_spec_price"];
                IdContract = (int) dr["id_srvpl_contract"];
                IdNomenclature = GetValueIntOrNull(dr["id_nomenclature"].ToString());
                NomenclatureName = dr["nomenclature_name"].ToString();
                CatalogNum = dr["catalog_num"].ToString();
                Price = (decimal)dr["price"];
                IdCreator = GetValueIntOrNull(dr["id_creator"].ToString());
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract_spec_price", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdContract = new SqlParameter() { ParameterName = "id_srvpl_contract", Value = IdContract, DbType = DbType.Int32 };
            SqlParameter pIdNomenclature = new SqlParameter() { ParameterName = "id_nomenclature", Value = IdNomenclature, DbType = DbType.AnsiString };
            SqlParameter pCatalogNum = new SqlParameter() { ParameterName = "catalog_num", Value = CatalogNum, DbType = DbType.AnsiString };
            SqlParameter pNomenclatureName = new SqlParameter() { ParameterName = "nomenclature_name", Value = NomenclatureName, DbType = DbType.AnsiString };
            SqlParameter pPrice = new SqlParameter() { ParameterName = "price", Value = Price, DbType = DbType.Decimal };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "saveSrvplContractSpecPrice", pId, pIdContract, pIdNomenclature, pNomenclatureName, pCatalogNum, pPrice, pIdCreator);
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_contract_spec_price", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Zipcl.sp, "closeSrvplContractSpecPrice", pId);
        }

        public void Delete(int id, int idCreator)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_city", Value = id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Zipcl.sp, "closeSrvplContractSpecPrice", pId, pIdCreator);
        }

        public static void GetNomenclatureName(string catalogNum, out string nomenclatureName, out int? idNomenclature)
        {
            nomenclatureName = String.Empty;
            idNomenclature = null;

            SqlParameter pCatalogNum = new SqlParameter() { ParameterName = "catalog_num", Value = catalogNum, DbType = DbType.AnsiString };

            DataTable dt = ExecuteQueryStoredProcedure(Zipcl.sp, "getSrvplContractSpecPriceNomenclatureName", pCatalogNum);

            if (dt.Rows.Count > 0)
            {
                nomenclatureName = dt.Rows[0]["nomenclature_name"].ToString();
                idNomenclature = GetValueIntOrNull(dt.Rows[0]["id_nomenclature"].ToString());
            }

        }
    }
}