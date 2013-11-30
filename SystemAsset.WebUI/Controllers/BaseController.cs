using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemAsset.Domain.Entities;

namespace SystemAsset.WebUI.Controllers
{
	public class BaseController : Controller
	{
		public string UserID { get; set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			//로그인 확인
		}
	}
}