using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;
using AliyunMvc.Models;
using System.IO;
using Amazon.DeviceFarm.Model;

namespace AliyunMvc.Controllers
{
    /// <summary>
    /// 商品
    /// </summary>
    public class ShopController : Controller
    {
        HttpClientHelper helper = new HttpClientHelper("https://localhost:44340/api/Shop/");
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
        public ActionResult ShopShow(int pageIndex = 1, int pageSize = 8)
        {
            Pager pager = new Pager()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
            };
            string json = helper.Post("PostShopShow", JsonConvert.SerializeObject(pager));
            ReturnModel rm = JsonConvert.DeserializeObject<ReturnModel>(json);
            List<Shop> shop = rm.list;
            ViewBag.Shop = shop;
            return View();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Sname"></param>
        /// <returns></returns>
        public ActionResult Pager(int pageIndex = 1, int pageSize = 8, string Sname = null)
        {
            if (pageIndex <= 1)
            {
                pageIndex = 1;
            }
            if (pageIndex >= Convert.ToInt32(Session["pagerLast"]))
            {
                pageIndex = Convert.ToInt32(Session["pagerLast"]);
            }
            Pager pager = new Pager()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                Sname = Sname,
            };
            string json = helper.Post("PostShopShow", JsonConvert.SerializeObject(pager));
            ReturnModel returnMode = JsonConvert.DeserializeObject<ReturnModel>(json);
            int totalCount = returnMode.TotalCount;
            int pagerLast = (totalCount / pageSize) + (totalCount % pageSize == 0 ? 0 : 1);
            Session["pagerLast"] = pagerLast;
            List<Shop> shops = returnMode.list;
            ViewBag.Shop = shops;
            Session["pageIndex"] = pageIndex;
            return PartialView("_ShoplPage1", ViewBag.Shop);
        }
        public ActionResult ShopUpdate()
        {
            return View();
        }
        //上传图片
        [HttpPost]
        public string FileUpLoad()
        {
            HttpPostedFileBase img = Request.Files["file"];
            string p = "";
            //判断是否上传了图片
            if (img != null)
            {
                p = "/Image/" + Path.GetFileName(img.FileName);
                img.SaveAs(Server.MapPath(p));
            }
            return p;
        }
    }
}