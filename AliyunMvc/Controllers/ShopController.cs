using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AliyunMvc.Controllers
{
    /// <summary>
    /// 商品
    /// </summary>
    public class ShopController : Controller
    {
        /// <summary>
        /// 商品类别添加显示
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopType()
        {
            return View();
        }
        /// <summary>
        /// 商品添加
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopInsert()
        {
            return View();
        }
    }
}