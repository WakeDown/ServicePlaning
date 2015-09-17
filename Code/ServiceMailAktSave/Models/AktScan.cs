using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMailAktSave.Models
{
    class AktScan : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public bool CamesAdd { get; set; }
        public DateTime? DateCamesAdd { get; set; }
        public int? idAdder { get; set; }
        public int? idCreator { get; set; }

        public AktScan ()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public AktScan(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_akt_scan", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getAktScan", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_akt_scan"];
                Name = dr["name"].ToString();
                FileName = dr["file_name"].ToString();
                FullPath = dr["full_path"].ToString();
                CamesAdd = GetValueBool(dr["cames_add"].ToString());
                DateCamesAdd = GetValueDateTimeOrNull(dr["date_cames_add"].ToString());
                idAdder = GetValueIntOrNull(dr["id_adder"].ToString());
                idCreator = GetValueIntOrNull(dr["id_creator"].ToString());
            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_akt_scan", Value = Id, DbType = DbType.Int32 };
            SqlParameter pName = new SqlParameter() { ParameterName = "name", Value = Name, DbType = DbType.AnsiString };
            SqlParameter pFileName = new SqlParameter() { ParameterName = "file_name", Value = FileName, DbType = DbType.AnsiString };
            SqlParameter pFullPath = new SqlParameter() { ParameterName = "full_path", Value = FullPath, DbType = DbType.AnsiString };
            SqlParameter pCamesAdd = new SqlParameter() { ParameterName = "cames_add", Value = CamesAdd, DbType = DbType.Boolean };
            SqlParameter pDateCamesAdd = new SqlParameter() { ParameterName = "date_cames_add", Value = DateCamesAdd, DbType = DbType.DateTime };
            SqlParameter pidAdder = new SqlParameter() { ParameterName = "id_adder", Value = idAdder, DbType = DbType.Int32 };
            SqlParameter pidCreator = new SqlParameter() { ParameterName = "id_creator", Value = idCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveAktScan", pId, pName, pFileName, pFullPath, pCamesAdd, pDateCamesAdd, pidAdder, pidCreator);
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_akt_scan", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Srvpl.sp, "closeAktScan", pId);
        }

        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}
