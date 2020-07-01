using ShopeeChat.CoreApplication.Statics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopeeChat.RestClient.Models.Base
{
    public class CommonResultModel
    {
        public CommonResultModel()
        {
            ID = 0;
            Title = "";
            Error = false;
            Obj = null;
            DelayTime = 0;
        }

        /// <summary>
        ///     ID của bản ghi được thêm, sửa, xóa
        /// </summary>
        public int ID { get; set; }

        public string Msg { get; set; }

        /// <summary>
        ///     Thông báo
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Có lỗi hay không có lỗi
        /// </summary>
        public bool Error { get; set; }

        public int NextAction { get; set; } // 0: ko làm gì, 1: Redirect, 2: Mở tab
        public int DelayTime { get; set; } //đơn vị milisecon 

        /// <summary>
        ///     Đối tượng attach kèm theo thông báo
        /// </summary>
        public object Obj { get; set; }

        public string Html { get; set; }

        public void SetError()
        {
            Title = Notify.ExistsErorr;
            Error = true;
        }

        public void SetErrorTitle()
        {
            Title = Notify.ExistsErorr;
        }
    }
}
