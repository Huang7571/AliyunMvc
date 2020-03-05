using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using AliyunMvc.Models;

namespace AliyunMvc.Controllers
{
    /// <summary>
    /// 商品
    /// </summary>
    public class ShopController : Controller
    {
        HttpClientHelper helper = new HttpClientHelper("https://localhost:44346/Shop/");
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
        /// <summary>
        /// 商品展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopShow()
        {
            string json = helper.Get("GetShop");
            List<Shop> list = JsonConvert.DeserializeObject<List<Shop>>(json);
            ViewBag.Shop = list;
            return View();
        }
    }
}