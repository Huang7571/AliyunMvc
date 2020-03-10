using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AliyunMvc.Models;

namespace AliyunMvc.Controllers
{
    public class orderformController : Controller
    {
        /// <summary>
        /// 各项状态处理界面
        /// </summary>
        /// <returns></returns>
        // GET: orderform
        public ActionResult statedispose()
        {
            return View();
        }


        /// <summary>
        /// 详情界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Particulars()
        {
            return View();
        }

        /// <summary>
        /// 发货界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Phipments()
        {
            return View();
        }
    }
}