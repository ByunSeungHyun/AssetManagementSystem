using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemAsset.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            return View();
        }

        #region LocationList 리스트
        public ActionResult LocationList()
        {
            return View();
        }
        #endregion

        #region NetworkWorkList 리스트
        public ActionResult NetworkWorkList()
        {
            return View();
        }
        #endregion

        #region ServerWorkList 리스트
        public ActionResult ServerWorkList()
        {
            return View();
        }
        #endregion

        #region WareYearList 리스트
        public ActionResult WareYearList()
        {
            return View();
        }
        #endregion

        #region ServerTypeList 리스트
        public ActionResult ServerTypeList()
        {
            return View();
        }
        #endregion

    }
}
