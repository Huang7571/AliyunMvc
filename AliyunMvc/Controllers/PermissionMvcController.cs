using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AliyunMvc.Controllers
{
	public class PermissionMvcController : Controller
	{
		HttpClientHelper clientHelper = new HttpClientHelper("https://localhost:44340/api/Permission/");
		// GET: PermissionMvc
		public ActionResult Index()//权限修改界面
		{

			return View();
		}
		[HttpPost]
		public void Index(string Aid, string MenuName)//权限修改
		{
			MenuName = Request["MenuName"];
			string s = clientHelper.Get("GetPer?Permission=" + MenuName + "&&Aid=" + Aid + "");
			int sn = int.Parse(s);
			if (sn>0)
			{
				Response.Write("<script>alert('修改成功!')</script>");
			}
		}
	}
}