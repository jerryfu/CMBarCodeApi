using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Web.Http;

namespace BarCodeApi.Controllers
{
    /// <summary>
    /// BarCode回傳
    /// </summary>
    public class GetData12Controller : BaseApiController
    {
        public ReturnInfo Get([FromBody]GetParam md)
        {
            ReturnInfo r = new ReturnInfo();
            try
            {
                string query_from_ip = getUserIP();
                var json_query = Newtonsoft.Json.JsonConvert.SerializeObject(md);
                logger.Info("存放資料，IP:{0}， 參數:{1}。", query_from_ip, json_query);

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
            public DateTime? Key02 { get; set; }
            /// <summary>
            /// 客戶_編號
            /// </summary>
            public int? Key03 { get; set; }
            /// <summary>
            /// 產品_編號
            /// </summary>
            public int? Key04 { get; set; }
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