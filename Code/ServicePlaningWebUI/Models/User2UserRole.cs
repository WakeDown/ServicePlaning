using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class User2UserRole : Db.Db, IDbObject<int>
    {
        public int IdUserRole { get; set; }
        public int IdUser { get; set; }
        public int IdCreator { get; set; }

         public User2UserRole()
        {
            if (IdUserRole > 0)
            {
                Get(IdUserRole);
            }
        }

        public User2UserRole(int idUser)
        {
            Get(idUser);
        }

        public void Get(int idUser)
        {
            SqlParameter pIdUser = new SqlParameter() { ParameterName = "id_user", Value = idUser, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getUser2UserRole", pIdUser);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                IdUserRole = (int) dr["id_user_role"];
                IdUser = (int)dr["id_user"];
            }
        }

        public void Save()
        {
            SqlParameter pIdUserRole = new SqlParameter() { ParameterName = "id_user_role", Value = IdUserRole, DbType = DbType.Int32 };
            SqlParameter pIdUser = new SqlParameter() { ParameterName = "id_user", Value = IdUser, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveUser2UserRole", pIdUserRole, pIdUser, pIdCreator);
        }

        public void Delete(int id)
        {
        }

        public void Delete(int id, int idCreator)
        {
        }
    }
}