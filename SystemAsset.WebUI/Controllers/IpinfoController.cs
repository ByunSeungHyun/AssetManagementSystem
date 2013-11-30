using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemAsset.Domain.Entities;
using SystemAsset.Domain.Data;
using SystemAsset.Domain.BizDac;
namespace SystemAsset.WebUI.Controllers
{
    public class IpinfoController : Controller
    {
        //
        // GET: /Ipinfo/

        public ActionResult Index()
        {
            return View();
        }
        #region 전체 IP 리스트
        public ActionResult IPList()
        {
            return View();
        }
        #endregion

        #region [POST] IP정보 리턴 JSON

        /// <summary>
        /// IP정보 리턴 JSON
        /// </summary>
        /// 2013-11-28 김두성
        /// <param name="Ip_Class_Code"></param>
        /// <returns></returns>
        //[HttpPost]
        public JsonResult GetIpList(string searchType, string searchText, int? page, int? rows)
        {
            string paramSearchType = string.IsNullOrEmpty(searchType) ? string.Empty : searchType;
            string paramSearchText = string.IsNullOrEmpty(searchText) ? string.Empty : searchText;
            int paramPageNo = page == null ? 1 : page.Value;
            int paramPageSize = rows == null ? 10 : rows.Value;

            IpBiz ipBiz = new IpBiz();

            int nTotalCount = ipBiz.CountIpList(paramSearchType, paramSearchText);
            int nRecord = nTotalCount <= paramPageSize ? 1 : (int)Math.Ceiling((double)(nTotalCount / paramPageSize));
            Models.JqGridModel model = new Models.JqGridModel
            {
                page = paramPageNo,
                total = nRecord,
                records = nTotalCount,
                rows = ipBiz.GetAssetSelectIpInfoList(paramSearchType, paramSearchText, paramPageNo, paramPageSize)
            };

            return Json(model);
        }

        #endregion
    }
}
