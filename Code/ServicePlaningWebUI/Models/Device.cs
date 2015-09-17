using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using ServicePlaningWebUI.Helpers;
using ServicePlaningWebUI.Objects.Interfaces;

namespace ServicePlaningWebUI.Models
{
    public class Device : Db.Db, IDbObject<int>
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int IdModel { get; set; }
        public string SerialNum { get; set; }
        public string InvNum { get; set; }
        public int? Counter { get; set; }
        public int? CounterColour { get; set; }
        public int? Age { get; set; }
        public DateTime? InstalationDate { get; set; }
        public int? IdCreator { get; set; }

        public int[] OptIds { get; set; }

        //public DeviceOption[] Options { get; set; }  

        public Device()
        {
            if (Id > 0)
            {
                Get(Id);
            }
        }

        public Device(int id)
        {
            Get(id);
        }

        public void Get(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device", Value = id, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "getDevice", pId);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                Id = (int)dr["id_device"];
                Model = dr["model"].ToString();
                IdModel = (int)dr["id_device_model"];
                SerialNum = dr["serial_num"].ToString();
                InvNum = dr["inv_num"].ToString();
                Counter = GetValueIntOrNull(dr["counter"].ToString());
                CounterColour = GetValueIntOrNull(dr["counter_colour"].ToString());
                Age = GetValueIntOrNull(dr["age"].ToString());
                InstalationDate = GetValueDateTimeOrNull(dr["instalation_date"].ToString());
                IdCreator = GetValueIntOrNull(dr["id_creator"].ToString());


                //<DeviceOptions>
                pId = new SqlParameter() { ParameterName = "id_device", Value = Id, DbType = DbType.Int32 };
                DataTable dtOpt = ExecuteQueryStoredProcedure(Srvpl.sp, "getDevice2OptionsList", pId);
                List<int> optIds = new List<int>();

                if (dtOpt.Rows.Count > 0)
                {
                    foreach (DataRow drOpt in dtOpt.Rows)
                    {
                        int optId = (int)drOpt["id_device_option"];
                        optIds.Add(optId);
                    }
                }

                OptIds = optIds.ToArray();
                //</DeviceOptions>

                //ScheduleDates

            }
        }

        public void Save()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device", Value = Id, DbType = DbType.Int32 };
            //SqlParameter pModel = new SqlParameter() { ParameterName = "model", Value = Model, DbType = DbType.AnsiString };
            SqlParameter pIdModel = new SqlParameter() { ParameterName = "id_device_model", Value = IdModel, DbType = DbType.Int32 };
            SqlParameter pSerialNum = new SqlParameter() { ParameterName = "serial_num", Value = SerialNum, DbType = DbType.AnsiString };
            SqlParameter pInvNum = new SqlParameter() { ParameterName = "inv_num", Value = InvNum, DbType = DbType.AnsiString };
           
            SqlParameter pCounter = new SqlParameter() { ParameterName = "counter", Value = Counter, DbType = DbType.Int32 };
            SqlParameter pCounterColour = new SqlParameter() { ParameterName = "counter_colour", Value = CounterColour, DbType = DbType.Int32 };
            SqlParameter pAge = new SqlParameter() { ParameterName = "age", Value = Age, DbType = DbType.Int32 };
            SqlParameter pInstalationDate = new SqlParameter() { ParameterName = "instalation_date", Value = InstalationDate, DbType = DbType.DateTime };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            DataTable dt = ExecuteQueryStoredProcedure(Srvpl.sp, "saveDevice", pId, /*pModel,*/ pIdModel, pSerialNum, pCounter, pAge, pInstalationDate, pIdCreator, pInvNum, pCounterColour);


            //TODO: Перенести исключения на уровень БД!!!

            if (dt.Rows.Count > 0)
            {
                Id = (int)dt.Rows[0]["id_device"];
            }
            else
            {
                throw new Exception("При сохранении аппарата не вернулся его ID.");
            }

            //DeviceOptions
            ClearOptions();//Скрываем существующие опции чтобы сохранить отмеченные

            foreach (int optId in OptIds)
            {
                Device2Options d2o = new Device2Options() {IdDevice = Id, IdOption = optId, IdCreator = IdCreator};
                d2o.Save();
            }
            //</DeviceOptions>
        }

        public void Delete(int id)
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device", Value = id, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Srvpl.sp, "closeDevice", pId);
        }

        public void ClearOptions()
        {
            SqlParameter pId = new SqlParameter() { ParameterName = "id_device", Value = Id, DbType = DbType.Int32 };
            SqlParameter pIdCreator = new SqlParameter() { ParameterName = "id_creator", Value = IdCreator, DbType = DbType.Int32 };

            ExecuteStoredProcedure(Srvpl.sp, "closeAllOptions4Device", pId, pIdCreator);
        }

        public void Delete(int id, int idCreator)
        {
            throw new NotImplementedException();
        }
    }
}