using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Web.Http;

namespace BarCodeApi.Controllers
{
    /// <summary>
    /// BarCode回傳
    /// </summary>
    public class GetData12Controller : BaseApiController
    {
        public ReturnInfo Get([FromUri]GetParam md)
        {
            ReturnInfo r = new ReturnInfo();
            try
            {
                string query_from_ip = getUserIP();
                var json_query = Newtonsoft.Json.JsonConvert.SerializeObject(md);
                logger.Info("存放資料，IP:{0}， 參數:{1}。", query_from_ip, json_query);

                db = new ChaominEntities();
                var conn = db.Database.Connection as SqlConnection;
                SqlCommand cmd = new SqlCommand("usp_盤點_取得資料12", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var Key01 = md.Key01;
                var Key02 = md.Key02;
                var Key03 = md.Key03;
                var Key04 = md.Key04 ?? "";//(object)DBNull.Value;


                cmd.Parameters.Add(new SqlParameter("@P01", Key01));
                cmd.Parameters.Add(new SqlParameter("@P02", Key02));
                cmd.Parameters.Add(new SqlParameter("@P03", Key03));
                cmd.Parameters.Add(new SqlParameter("@P04", Key04));

                conn.Open();
                var reader = cmd.ExecuteReader();

                IList<PackData> packData = new List<PackData>();

                while (reader.Read())
                {
                    PackData pd = new PackData();
                    pd.Product_SN = (int)reader["產品_編號"];
                    pd.Product_Name = reader["產品_名稱"].ToString();
                    pd.DataCount = (int)reader["資料筆數"];
                    packData.Add(pd);
                }

                reader.Close();
                conn.Close();

                r.Data = packData;
                r.ReturnCode = 0;
                return r;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "儲存資料錯誤");
                r.ReturnCode = ExceptionCode;
                return r;
            }
        }
        public class GetParam
        {
            /// <summary>
            /// 廠別: 1:中壢 / 2:台中
            /// </summary>
            public int Key01 { get; set; }
            /// <summary>
            /// 訂單日期:YYYY-MM-DD HH:MM:SS.000
            /// </summary>
            public DateTime Key02 { get; set; }
            /// <summary>
            /// 產品分類備註	
            /// </summary>
            public int Key03 { get; set; }
            /// <summary>
            /// 產品_名稱
            /// </summary>
            public string Key04 { get; set; }
        }
        public class ReturnInfo
        {
            public int ReturnCode { get; set; }
            public IList<PackData> Data { get; set; }
        }
        public class PackData
        {
            public int Product_SN { get; set; }
            public string Product_Name { get; set; }
            public int DataCount { get; set; }
        }
    }
}