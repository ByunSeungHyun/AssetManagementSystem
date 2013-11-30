using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemAsset.WebUI.Models;

namespace SystemAsset.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "ASP.NET MVC 응용 프로그램을 바로 시작하려면 이 템플릿을 수정하십시오.";

			ViewBag.ProcStatus = new SelectList(new ProcStatusModel().GetProcStatusList(), "Value", "Text", 2);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "응용 프로그램 설명 페이지입니다.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "연락처 페이지입니다.";

            return View();
        }

		public ActionResult TopMenu()
		{
			return PartialView();
		}
    }
}
