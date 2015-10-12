using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using ServicePlaningWebUI.Models;

namespace ServicePlaningWebUI.Db
{
    public partial class Db
    {
        public class Users
        {
            #region Константы

            private const string sp = "ui_users";

            #endregion

            public static User GetUserBySid(string sid)
            {
                User user;

                SqlParameter pSid = new SqlParameter() { ParameterName = "user_sid", Value = sid, DbType = DbType.AnsiString };
                DataTable dt = ExecuteQueryStoredProcedure(sp, "getUserBySid", pSid);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    int id = (int)dr["id_user"];
                    string login = dr["login"].ToString();
                    string userSid = dr["sid"].ToString();
                    string fullName = dr["full_name"].ToString();
                    string displayName = dr["display_name"].ToString();
                    string mail = dr["mail"].ToString();
                    bool enabled = (bool)dr["enabled"];

                    if (String.IsNullOrEmpty(displayName)) displayName = fullName;

                    user = new User(id, login, fullName, displayName, mail) { Enabled = enabled, AdSid = userSid };


                    SqlParameter pUserId = new SqlParameter()
                    {
                        ParameterName = "id_user",
                        Value = user.Id,
                        DbType = DbType.Int32
                    };
                    dt = ExecuteQueryStoredProcedure(sp, "getEtUserByUserId", pUserId);

                    EtalonUser etUser;

                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];

                        int etId = (int)dr["id_et_user"];
                        string etLogin = dr["et_login"].ToString();
                        string etPassword = dr["et_password"].ToString();
                        string etDisplayName = dr["et_display_name"].ToString();
                        string adSid = dr["ad_sid"].ToString();

                        etUser = new EtalonUser(etId, etLogin, etPassword, etDisplayName) { AdSid = adSid };
                    }
                    else
                    {
                        string etDisplayName = "Не зарегистрирован";
                        etUser = new EtalonUser() { DisplayName = etDisplayName };
                    }

                    user.EtUser = etUser;
                }
                else
                {
                    user = new User();
                }

                return user;
            }

            public static DataTable GetUsersSelectionList()
            {
                DataTable dt = new DataTable();

                dt = ExecuteQueryStoredProcedure(sp, "getUsersSelectionList");
                return dt;
            }

            public static DataTable GetUsersSelectionList(string groupName, DataTable dtUsers = null, string groupSid = null)
            {
                if (dtUsers == null)
                {
                    dtUsers = GetUsersSelectionList();
                }

                //List<string> logins = new List<string>();

                string userGroupSid = string.Empty;
                string programName = WebConfigurationManager.AppSettings["progName"];

                SqlParameter pProgramName = new SqlParameter() { ParameterName = "program_name", Value = programName, DbType = DbType.AnsiString };
                SqlParameter pRightName = new SqlParameter() { ParameterName = "sys_name", Value = groupName, DbType = DbType.AnsiString };

                if (String.IsNullOrEmpty(groupSid))
                {
                    DataTable dtGroupSid = ExecuteQueryStoredProcedure(sp, "getUserGroupSid", pProgramName, pRightName);
                    if (dtGroupSid.Rows.Count > 0)
                    {
                        userGroupSid = dtGroupSid.Rows[0]["sid"].ToString();
                    }
                }
                else
                {
                    userGroupSid = groupSid;
                }


                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, System.DirectoryServices.ActiveDirectory.Domain.GetCurrentDomain().Name);
                GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Sid, userGroupSid);

                var members = grp.GetMembers(false);

                DataTable dt;
                List<string> sids = new List<string>();

                foreach (Principal member in members)
                {
                    sids.Add("'" + member.Sid.ToString() + "'");
                }

                //foreach (DataRow dr in dtUsers.Rows)
                //{
                //    string login = dr["login"].ToString();

                //    if (CheckUserRights(login, groupName, userGroupSid))
                //    {
                //        logins.Add(String.Format("'{0}'", login));
                //    }
                //}

                //DataTable dt;

                if (sids.Any())
                {
                    //string expression = String.Format("login in ({0})", String.Join(",", logins));

                    string expression = String.Format("sid in ({0})", String.Join(",", sids));

                    var res = dtUsers.Select(expression, "name asc");

                    if (res != null && res.Count() > 0)
                    {
                        dt = res.CopyToDataTable();
                    }
                    else
                    {
                        dt=new DataTable();
                    }

                }
                else
                {
                    dt = new DataTable();
                    //dt = dtUsers;
                }

                return dt;
            }

            public static bool CheckUserRights(string userLogin, string rightName=null, string groupSid = null)
            {
 bool flag = false;
                string sid = String.Empty;
                if (String.IsNullOrEmpty(groupSid))
                {
                    string programName = WebConfigurationManager.AppSettings["progName"];
                    SqlParameter pProgramName = new SqlParameter()
                    {
                        ParameterName = "program_name",
                        Value = programName,
                        DbType = DbType.AnsiString
                    };
                    SqlParameter pRightName = new SqlParameter()
                    {
                        ParameterName = "sys_name",
                        Value = rightName,
                        DbType = DbType.AnsiString
                    };

                    DataTable dt = new DataTable();

                    dt = ExecuteQueryStoredProcedure(sp, "getUserGroupSid", pProgramName, pRightName);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        sid = dr["sid"].ToString();
                    }
                }
                else
                {
                    sid = groupSid;
                }

                //if (dt.Rows.Count > 0)
                //{
                //    DataRow dr = dt.Rows[0];

                //    string sid = dr["sid"].ToString();

                    try
                    {
                        WindowsIdentity wi = new WindowsIdentity(userLogin);
                        WindowsPrincipal wp = new WindowsPrincipal(wi);
                        SecurityIdentifier grpSid = new SecurityIdentifier(sid);

                        flag = wp.IsInRole(grpSid);
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                    }
                //}

                return flag;
            }
        }
    }
}