using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace BarCodeApi.Controllers
{
    /// <summary>
    /// BarCode回傳
    /// </summary>
    public class GetData14Controller : BaseApiController
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
                SqlCommand cmd = new SqlCommand("usp_盤點_取得資料14", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                var Key01 = md.Key01;
                var Key02 = md.Key02;
                var Key03 = md.Key03;
                var Key04 = md.Key04; // (object)DBNull.Value;
                var Key05 = md.Key05;
                var Key06 = md.Key06 ?? "";
                var Key07 = md.Key07 ?? "";
                var Key08 = md.Key08;

                cmd.Parameters.Add(new SqlParameter("@P01", Key01));
                cmd.Parameters.Add(new SqlParameter("@P02", Key02));
                cmd.Parameters.Add(new SqlParameter("@P03", Key03));
                cmd.Parameters.Add(new SqlParameter("@P04", Key04));
                cmd.Parameters.Add(new SqlParameter("@P05", Key05));
                cmd.Parameters.Add(new SqlParameter("@P06", Key06));
                cmd.Parameters.Add(new SqlParameter("@P07", Key07));
                cmd.Parameters.Add(new SqlParameter("@P08", Key08));

                conn.Open();
                var reader = cmd.ExecuteReader();

                IList<PackData> packData = new List<PackData>();

                while (reader.Read())
                {
                    PackData pd = new PackData();
                    pd.Order_SN_Master = (int)reader["訂單主檔編號"];
                    pd.Order_Date = (DateTime)reader["訂單日期"];
                    pd.Customer_SN = (int)reader["客戶_編號"];
                    pd.Customer_Name1 = reader["客戶_名稱"].ToString();
                    pd.Customer_Name2 = reader["客戶_別名"].ToString();
                    pd.Customer_Name3 = reader["客戶_簡稱"].ToString();
                    pd.Order_SN_Detail = (int)reader["訂單明細編號"];
                    pd.Product_SN = (int)reader["產品_編號"];
                    pd.Product_Name = reader["產品_名稱"].ToString();
                    pd.Product_Unit = reader["產品_單位"].ToString();
                    pd.Product_Qty = (float)reader["產品_訂購數量"];
                    pd.Product_Package = reader["產品_包裝方式"].ToString();
                    pd.Product_Remark = reader["產品_備註說明"].ToString();
                    pd.ProductCat_SN = (int)reader["產品分類_編號"];
                    pd.ProductCat_Name = reader["產品分類_名稱"].ToString();
                    pd.ProductCat_Remark = reader["產品分類_備註"].ToString();
                    pd.ProductCat_Sort = reader["產品分類_排序"].ToString();
                    pd.CustomerArea_SN = (int)reader["客戶區域_編號"];
                    pd.CustomerArea_Name = reader["客戶區域_名稱"].ToString();
                    pd.CustomerArea_Sort = (double)reader["客戶區域_排序"];

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
                logger.Error(ex, "儲存取得錯誤");
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
            /// 訂單主檔編號 0:全部
            /// </summary>
            public int Key03 { get; set; }
            /// <summary>
            /// 產品_編號 0:全部
            /// </summary>
            public int Key04 { get; set; }
            /// <summary>
            /// 產品分類_備註(是否有值)(1:有 / 2:無  /0:全部)
            /// </summary>
            public int Key05 { get; set; }
            /// <summary>
            /// 產品分類_備註
            /// </summary>
            public string Key06 { get; set; }
            /// <summary>
            /// 產品_名稱
            /// </summary>
            public string Key07 { get; set; }
            /// <summary>
            /// 提早印	0: 全部，1:提早印，2:非提早印
            /// </summary>
            public string Key08 { get; set; }
        }
        public class ReturnInfo
        {
            public int ReturnCode { get; set; }
            public IList<PackData> Data { get; set; }
        }
        public class PackData
        {
            public int Order_SN_Master { get; set; }
            public DateTime Order_Date { get; set; }
            public int? Customer_SN { get; set; }
            public string Customer_Name1 { get; set; }
            public string Customer_Name2 { get; set; }
            public string Customer_Name3 { get; set; }
            public int Order_SN_Detail { get; set; }
            public int Product_SN { get; set; }
            public string Product_Name { get; set; }
            public string Product_Unit { get; set; }
            public float Product_Qty { get; set; }
            public string Product_Package { get; set; }
            public string Product_Remark { get; set; }
            public int ProductCat_SN { get; set; }
            public string ProductCat_Name { get; set; }
            public string ProductCat_Remark { get; set; }
            public string ProductCat_Sort { get; set; }
            public int? CustomerArea_SN { get; set; }
            public string CustomerArea_Name { get; set; }
            public double? CustomerArea_Sort { get; set; }
        }
    }
}