using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
namespace AliyunMvc.Controllers
{
	public class LognController : Controller
	{
		HttpClientHelper Perclien = new HttpClientHelper("https://localhost:44340/Api/MerchantAPI/");
		HttpClientHelper clientHelper = new HttpClientHelper("https://localhost:44340/api/Permission/");
		// GET: Logn
		public ActionResult Login()
		{
			string UserNameCookie = CookieHelper.GetCookieValue("UserNameCookie");
			UserNameCookie = HttpUtility.UrlDecode(UserNameCookie);
			string UserPwdCookie = CookieHelper.GetCookieValue("UserPwdCookie");
			UserPwdCookie = HttpUtility.UrlDecode(UserPwdCookie);
			ViewBag.UserName = UserNameCookie;
			ViewBag.UserPwd = UserPwdCookie;
			return View();
		}
		/// <summary>
		/// 记住密码
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public int Login(Merchant model)
		{
			string json = Perclien.Get("GetLogin?json=" + JsonConvert.SerializeObject(model));
			int n = json == "" ? 0 : Convert.ToInt32(json);
			if (n > 0)
			{
				string UserNameCookie = CookieHelper.GetCookieValue("UserNameCookie");
				string UserPwdCookie = CookieHelper.GetCookieValue("UserPwdCookie");
				Session["UserName"] = model.MerchantName;
				string admin = clientHelper.Get("GetAdministrator?MerchantName=" + model.MerchantName);
				List<Admin> list = JsonConvert.DeserializeObject<List<Admin>>(admin);
				Admin admin1 = list.FirstOrDefault();
				Session["MenuName"] = admin1.MenuName;
				if (model.IsSaveLoginfo)
				{
					string UserName = HttpUtility.UrlEncode(model.MerchantName);
					string UserPwd = HttpUtility.UrlEncode(model.MerchantPwd);
					if (UserNameCookie != UserName)//当输入的用户名与cookie保存的不同，修改cookie的
						CookieHelper.SetCookie("UserNameCookie", UserName, DateTime.MaxValue);
					if (UserPwdCookie != UserPwd)
						CookieHelper.SetCookie("UserPwdCookie", UserPwd, DateTime.MaxValue);
				}
				else
				{
					CookieHelper.ClearCookie("UserNameCookie");
					CookieHelper.ClearCookie("UserNameCookie");
				}
				return 1;
			}
			else
			{
				return 0;
			}
		}
		public ActionResult ShouYe()
		{
			return View();
		}

	}
}
public class Admin
{
	public string AdministratorName { get; set; }
	public string MenuName { get; set; }
}