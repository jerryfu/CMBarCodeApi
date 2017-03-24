using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BarCodeApi.Controllers
{
    /// <summary>
    /// 取得盤點 全部資料
    /// </summary>
    public class GetData01Controller : ApiController
    {
        private ChaominEntities db = new ChaominEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const int ExceptionCode = 99;

        /// <summary>
        /// 取得盤點 全部資料
        /// </summary>
        /// <param name="q">查詢參數</param>
        /// <returns>VW_盤點廠商_客戶訂單資料陣列資料</returns>
        public ReturnInfo Get([FromUri]QueryParam q)
        {
            var json_query = Newtonsoft.Json.JsonConvert.SerializeObject(q);
            string query_from_ip = getUserIP();
            logger.Info("進行查詢，IP:{0}， 參數:{1}。", query_from_ip, json_query);
            ReturnInfo r = new ReturnInfo();

            #region MyRegion
            try
            {
                var items = db.VW_盤點廠商_客戶訂單資料.AsQueryable();
                if (q.Key01 != null)
                    items = items.Where(x => x.訂單日期 == q.Key01);

                if (q.Key02 != null)
                    items = items.Where(x => x.異動時間 >= q.Key02);

                var list = items
                    .OrderBy(x => x.異動時間)
                    .Select(x => new InventoryModal()
                    {
                        Order_SN_Master = (int)x.訂單主檔編號,
                        Order_Date = (DateTime)x.訂單日期,
                        Customer_SN = (int)x.客戶_編號,
                        Customer_Name1 = x.客戶_名稱,
                        Customer_Name2 = x.客戶_別名,
                        Customer_Name3 = x.客戶_簡稱,
                        Order_SN_Detail = (int)x.訂單明細編號,
                        Product_SN = (int)x.產品_編號,
                        Product_Name = x.產品_名稱,
                        Product_Unit = x.產品_單位,
                        Product_Qty = (float)x.產品_訂購數量,
                        Product_Package = x.產品_包裝方式,
                        Product_Remark = x.產品_備註說明,
                        ProductCat_SN = x.產品分類_編號,
                        ProductCat_Name = x.產品分類_名稱,
                        ProductCat_Remark = x.產品分類_備註,
                        ProductCat_Sort = (int)x.產品分類_排序,
                        CustomerArea_SN = x.客戶區域_編號,
                        CustomerArea_Name = x.客戶區域_名稱,
                        CustomerArea_Sort = x.客戶區域_排序,
                        FactoryType = x.廠別,
                        Transaction_Type = x.異動類別,
                        Transaction_Time = (DateTime)x.異動時間,
                    }).ToList();
                r.ReturnCode = 0;
                r.Data = list;
                return r;

            }
            catch (Exception ex)
            {
                r.ReturnCode = ExceptionCode;
                logger.Error(ex, "取得資料錯誤");
                return r;
            }
            #endregion
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        protected string getUserIP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            return VisitorsIPAddr;
        }


        /// <summary>
        /// 參詢參數，異動時間空白時回傳所有資料,異動時間有值時回傳該時間點之後異動資料
        /// </summary>
        public class QueryParam
        {
            /// <summary>
            /// 訂單日期:YYYY-MM-DD HH:MM:SS.000
            /// </summary>
            public DateTime? Key01 { get; set; }
            /// <summary>
            /// 異動時間:YYYY-MM-DD HH:MM:SS.000
            /// </summary>
            public DateTime? Key02 { get; set; }
        }
        public class InventoryModal
        {
            public int Order_SN_Master { get; set; }
            public DateTime Order_Date { get; set; }
            public int Customer_SN { get; set; }
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
            public int ProductCat_Sort { get; set; }
            public int? CustomerArea_SN { get; set; }
            public string CustomerArea_Name { get; set; }
            public double? CustomerArea_Sort { get; set; }
            public int FactoryType { get; set; }
            public string Transaction_Type { get; set; }
            public DateTime Transaction_Time { get; set; }
        }
        public class ReturnInfo
        {
            public int ReturnCode { get; set; }
            //public string message { get; set; }
            public IList<InventoryModal> Data { get; set; }
        }
    }
}