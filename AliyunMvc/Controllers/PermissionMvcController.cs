using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
namespace AliyunMvc.Controllers
{
	public class PermissionMvcController : Controller
	{
		HttpClientHelper clientHelper = new HttpClientHelper("https://localhost:44340/api/Permission/");
		HttpClientHelper Perclien = new HttpClientHelper("https://localhost:44340/Api/MerchantAPI/");
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
			if (sn > 0)
			{
				Response.Write("<script>alert('修改成功!');location.href='/PermissionMvc/QuanXian'</script>");
			}
		}
		/// <summary>
		/// 用户显示
		/// </summary>
		/// <returns></returns>
		public ActionResult QuanXian()
		{

			return View();
		}
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="MerchantId"></param>
		/// <returns></returns>
		public ActionResult PerDeit(int MerchantId)
		{
			string json = Perclien.Get("GetPage?MerchantId=" + MerchantId);
			PageModel pageModel = JsonConvert.DeserializeObject<PageModel>(json);
			List<Merchant> list = pageModel.data;
			Merchant merchant = list.FirstOrDefault();
			return View(merchant);
		}
		/// <summary>
		/// 用户修改
		/// </summary>
		/// <param name="model"></param>
		[HttpPost]
		public void PerDeit(Merchant model)
		{
			string json = Perclien.Put("PutMerchant", JsonConvert.SerializeObject(model));
			int n = int.Parse(json);
			if (n > 0)
			{
				Response.Write("<script>alert('修改成功');location.href='/PermissionMvc/Index'<script>");

			}
		}
		/// <summary>
		/// 添加
		/// </summary>
		/// <returns></returns>
		public ActionResult Add()
		{
			List<Administrator> administrators = JsonConvert.DeserializeObject<List<Administrator>>(clientHelper.Get("GetAdmin"));

			ViewBag.PerTable = administrators;
			return View();
		}
		[HttpPost]
		public void Add(Merchant model)
		{
			string json = Perclien.Post("PostMerchant", JsonConvert.SerializeObject(model));
			int n = Convert.ToInt32(json);
			if (n > 0)
			{
				Response.Write("<script>alert('添加成功');location.href='/PermissionMvc/QuanXian'</script>");
			}
		}

	}
	public class PageModel
	{
		/// <summary>
		/// 规定成功的状态码
		/// </summary>
		public int code { get; set; }
		/// <summary>
		/// 规定状态信息的字段名称
		/// </summary>
		public string msg { get; set; }
		/// <summary>
		/// 规定数据总数的字段名称
		/// </summary>
		public int count { get; set; }
		/// <summary>
		/// 规定数据列表的字段名称 要返回的数据
		/// </summary>
		public List<Merchant> data { get; set; }
	}
	public class Merchant//商家表
	{
		/// <summary>
		/// 商家编号
		/// </summary>
		public int MerchantId { get; set; }
		/// <summary>
		/// 商家名称
		/// </summary>
		public string MerchantName { get; set; }
		/// <summary>
		/// 商家邮箱
		/// </summary>
		public string MerchantEmail { get; set; }
		/// <summary>
		/// 商家密码
		/// </summary>
		public string MerchantPwd { get; set; }
		/// <summary>
		/// 商家备注
		/// </summary>
		public string MerchantContent { get; set; }
		/// <summary>
		/// 权限Id（关联权限表）
		/// </summary>
		public int Aid { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public int MerchantState { get; set; }
		/// <summary>
		/// 管理员名字
		/// </summary>
		public string AdministratorName { get; set; }
		/// <summary>
		/// 状态名称
		/// </summary>
		public string MState { get; set; }
		/// <summary>
		/// 是否记住密码
		/// </summary>
		public bool IsSaveLoginfo { get; set; }
	}
	public class Administrator//管理员表
	{
		/// <summary>
		/// 管理员编号ss
		/// </summary>
		public int Aid { get; set; }
		/// <summary>
		/// 管理员名字
		/// </summary>
		public string AdministratorName { get; set; }
		/// <summary>
		/// 是否启用
		/// </summary>
		public int Menable { get; set; }

	}
}