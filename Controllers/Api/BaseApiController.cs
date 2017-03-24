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
    public class BaseApiController : ApiController
    {
        protected ChaominEntities db = new ChaominEntities();
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        protected const int ExceptionCode = 99;
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
    }
}