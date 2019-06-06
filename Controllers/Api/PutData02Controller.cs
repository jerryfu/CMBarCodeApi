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
    public class PutData02Controller : BaseApiController
    {
        /// <summary>
        /// 上傳盤點資料
        /// </summary>
        /// <param name="md">陣列資料</param>
        /// <returns>
        /// ReturnInfo JSON ReturnCode=> 0:NO Error/1:異常
        /// </returns>
        public ReturnInfo Post([FromBody]PostParam md)
        {
            ReturnInfo r = new ReturnInfo();
            PostModal get_item = null;
            try
            {
                string query_from_ip = getUserIP();
                var json_query = Newtonsoft.Json.JsonConvert.SerializeObject(md);
                logger.Info("存放資料，IP:{0}， 參數:{1}。", query_from_ip, json_query);

                foreach (var item in md.data)
                {
                    get_item = item;
                    ObjectParameter out_value = new ObjectParameter("returnValue01", typeof(int));

                    var i = db.usp_盤點_最後盤點時間_PUT(md.Flag, item.OrderNumber, out_value);
                    var json_detail = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                    logger.Info("儲存JSON:{0} 回傳值:{1}。", json_detail, out_value.Value);
                }
                r.Count = md.data.Length;
                r.ReturnCode = 0;
                return r;
            }
            catch (Exception ex)
            {
                logger.Error("訊息:{0} 錯誤項:{1} 參數:{2}", ex.Message,
                    Newtonsoft.Json.JsonConvert.SerializeObject(get_item),
                    Newtonsoft.Json.JsonConvert.SerializeObject(md));
                r.ReturnCode = ExceptionCode;
                return r;
            }
        }
        public class PostParam
        {
            [Required]
            public PostModal[] data { get; set; }
            public int Flag { get; set; }
        }
        public class PostModal
        {
            //[Required]
            public int OrderNumber { get; set; }

        }
        public class ReturnInfo
        {
            public int ReturnCode { get; set; }
            public int Count { get; set; }
        }
    }
}