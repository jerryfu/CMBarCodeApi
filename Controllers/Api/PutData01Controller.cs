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
    public class PutData01Controller : ApiController
    {
        private ChaominEntities db = new ChaominEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const int ExceptionCode = 99;

        public ReturnInfo Post([FromBody]postParam md)
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

        public class postParam
        {
            public IList<PostModal> data { get; set; }
        }
        public class PostModal
        {
            public int Order_SN_Detail { get; set; }
            public int Product_SN { get; set; }
            public string Product_Unit { get; set; }
            public int Product_Qty { get; set; }
            public int Product_Qty_New { get; set; }
        }
        public class ReturnInfo
        {
            public int ReturnCode { get; set; }
        }
    }
}