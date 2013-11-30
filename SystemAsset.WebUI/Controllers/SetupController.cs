using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemAsset.WebUI.Controllers
{
	public class SetupController : BaseController
    {
        //
        // GET: /Setup/

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult SetupReportList()
		{
			return View();
		}



    }
}
