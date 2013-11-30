using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemAsset.Domain.Data;
using SystemAsset.Domain.BizDac;
using SystemAsset.Domain.Entities;

namespace SystemAsset.WebUI.Controllers
{
	public class CodeController : BaseController
    {
		CodeBiz codeBiz = new CodeBiz();
        public ActionResult Index()
        {
            return View();
        }


		#region asset_code 조회
		/// <summary>메뉴 -- code
        /// asset_code 조회
        /// </summary>
        /// <param name="asset_group_type"> 자산그룹코드</param>
        /// <param name="type">자산구분코드</param>
        /// <returns></returns>
        public ActionResult CommonCodeList(string asset_group_type, string type)
        {
            if (string.IsNullOrEmpty(type) == true)
            {
                //에러 처리
            }

            string strTitle = string.Empty;

            //페이지상단에 메뉴바 보여지는 텍스트
            switch (type)
            {
                case "BUSINESS": strTitle = "server : 업무"; break;
                case "SERVICE": strTitle = "서비스"; break;
                case "OS": strTitle = "OS"; break;
                case "Hardware": strTitle = "장비구분"; break;
                case "IDC": strTitle = "IDC"; break;
                case "SITE": strTitle = "site : 사이트"; break;

                default: strTitle = "Common Code"; break;
            }


            ViewData["subject"] = strTitle;//상단 타이틀
            ViewData["type"] = type;//소분류
            ViewData["asset_group_type"] = asset_group_type;//그룹분류 예 : NETWORK,COMMON,SERVER,STORAGE.등
            return View();
        }

        #endregion

        #region CODE메뉴_COMMON_IDC
        /// <summary>메뉴 --CODE IDC
        /// ASSET_IDC_CODE조회
        /// </summary>
        /// <returns></returns>
        public ActionResult CommonAssetIdcCodeList()
        {
            string strTitle = "IDC";
            
            ViewData["subject"] = strTitle;//상단 타이틀
            
            return View();
        }
        #endregion

        #region CODE메뉴 RACK_CODE조회
        /// <summary>
        /// 메뉴 Code Rack
        /// ASSET_RACK_CODE조회
        /// </summary>
        /// <returns></returns>
        public ActionResult CommonAssetRackCodeList()
        {
            string strTitle = "RACK";
            ViewData["subject"] = strTitle;
            return View();
        }
        #endregion

        #region Code 메뉴 ASSET_VENDER_CODE 조회
        /// <summary>
        ///  메뉴 Code_Server_Vender_Code조회
        ///  ASSET_VENDER_CODE조회
        /// </summary>
        /// <returns></returns>
        public ActionResult CommonAssetServerVenderCodeList()
        {
            string strTitle = "SERVER_VENDER";
            ViewData["subject"] = strTitle;
            return View();
        }
        #endregion

        #region Code 메뉴 ASSET_NETWORK_VENDER_CODE 조회
        /// <summary>
        ///  메뉴 Code_Network_Vender_Code조회
        ///  ASSET_NETWORK_VENDER_CODE조회
        /// </summary>
        /// <returns></returns>
        public ActionResult CommonAssetNetworkVenderCodeList()
        {
            string strTitle = "NETWORK_VENDER";
            ViewData["subject"] = strTitle;
            return View();
        }
        #endregion

        #region Code 메뉴 ASSET_STORAGE_VENDER_CODE 조회
        /// <summary>
        /// 메뉴 Code_Storage_Vender_Code조회
        ///  ASSET_STORAGE_VENDER_CODE조회
        /// </summary>
        /// <returns></returns>
        public ActionResult CommonAssetStorageVenderCodeList()
        {
            string strTitle = "STORAGE_VENDER";
            ViewData["subject"] = strTitle;
            return View();
        }
        #endregion

        #region Code메뉴 ASSET_ETC_VENDER_CODE 조회
        /// <summary>
        /// 메뉴 Code_Etc_Vender_Code조회
        ///  ASSET_Etc_VENDER_CODE조회
        /// </summary>
        /// <returns></returns>
        public ActionResult CommonAssetEtcVenderCodeList()
        {
            string strTitle = "ETC_VENDER";
            ViewData["subject"] = strTitle;
            return View();
        }
        #endregion

        #region Code 메뉴 ASSET_IP_TYPE 리스트
        public ActionResult CommonAssetIpTypeList()
        {
            string strTitle = "IP_TYPE";
            ViewData["subject"] = strTitle;
            return View();
        }
        #endregion
        //**********************************************************************************************************************************************

        #region 공통 코드 리턴 JSON

        /// <summary>
		/// 공통 코드 리턴 JSON 
		/// </summary>
		/// <param name="type">ASSET_TYPE</param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult GetCommonCodeList(string asset_group_type, string type)
		{
            
			List<AssetCommonCodeT> assetCommonCodeList = new List<AssetCommonCodeT>();
			using (CodeBiz codeBiz = new CodeBiz())
			{
				assetCommonCodeList = codeBiz.GetCommonCodeByAssetType(asset_group_type, type);
			}

			Models.JqGridModel model = new Models.JqGridModel
			{
				page = 0,
				total = assetCommonCodeList.Count,
				records = assetCommonCodeList.Count,
				rows = assetCommonCodeList
			};

			return Json(model);
		}

		#endregion


        #region ASSET_IDC_CODE ( IDC 코드) 리턴 JSON
        /// <summary>
        /// idc_code 리턴JSON
        /// </summary>
        /// <returns>ASSET_IDC_CODE리스트</returns>
        [HttpPost]
        public JsonResult GetAssetIdcCodeList()
        {
            List<AssetIdcCodeT> assetIdcCodeList = new List<AssetIdcCodeT>();
            using (CodeBiz codeBiz = new CodeBiz())
            {
                assetIdcCodeList = codeBiz.GetAssetIdcCode();
            }

            Models.JqGridModel model = new Models.JqGridModel
            {
                page = 0,
                total = assetIdcCodeList.Count,
                records = assetIdcCodeList.Count,
                rows = assetIdcCodeList
            };
            return Json(model);
        }
        #endregion

        #region ASSET_RACK_CODE (RACK CODE) 리턴 JSON
        /// <summary>
        /// Rack_code 리턴 Json
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAssetRackCodeList() 
        {
            List<AssetRackCodeT> assetRackCodeList = new List<AssetRackCodeT>();
            using (CodeBiz codebiz = new CodeBiz())
            {
                assetRackCodeList = codebiz.GetAssetRackCode();
            }
            Models.JqGridModel model = new Models.JqGridModel
            {
                page = 0,
                total = assetRackCodeList.Count,
                records = assetRackCodeList.Count,
                rows = assetRackCodeList
            };
            return Json(model);
        }
        #endregion

        #region ASSET_SERVER_VENDER_CODE리턴 JSON
        /// <summary>
        /// ASSET_SERVER_VENDER_CODE코드 리턴json
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		public JsonResult GetAssetServerVenderCodeList(int? page, int? rows)
		{
			int paramPageNo = page == null ? 1 : page.Value;
			int paramPageSize = rows == null ? 10 : rows.Value;

			int nTotalCount = codeBiz.GetAssetServerVenderCode().Count;
			int nRecord = nTotalCount <= paramPageSize ? 1 : (int)Math.Ceiling((double)(nTotalCount / paramPageSize));

            List<AssetServerVenderCodeT> assetServerVenderCodeList = new List<AssetServerVenderCodeT>();
            using(CodeBiz codebiz = new CodeBiz()){
                assetServerVenderCodeList = codebiz.GetAssetServerVenderCode();
            }
            Models.JqGridModel model = new Models.JqGridModel
			{
				page = paramPageNo,
				total = nRecord,
				records = nTotalCount,
				rows = codeBiz.GetAssetServerVenderCode()
            };
            return Json(model);
        }
        #endregion

        #region ASSET_NETWORK_VENDER_CODE리턴 JSON
        /// <summary>
        /// ASSET_NETWORK_VENDER_CODE 코드 리턴json
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAssetNetworkVenderCodeList()
        {
            List<AssetNetworkVenderCodeT> assetNetworkVenderCodeList = new List<AssetNetworkVenderCodeT>();
            using (CodeBiz codebiz = new CodeBiz())
            {
                assetNetworkVenderCodeList = codebiz.GetAssetNetworkVenderCode();
            }
            Models.JqGridModel model = new Models.JqGridModel
            {
                page = 0,
                total = assetNetworkVenderCodeList.Count,
                records = assetNetworkVenderCodeList.Count,
                rows = assetNetworkVenderCodeList
            };
            return Json(model);
        }
        #endregion

        #region ASSET_STORAGE_VENDER_CODE 코드 리턴json
        /// <summary>
        /// ASSET_STORAGE_VENDER_CODE 코드 리턴json
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAssetStorageVenderCodeList()
        {
            List<AssetStorageVenderCodeT> assetStorageVenderCodeList = new List<AssetStorageVenderCodeT>();
            using (CodeBiz codebiz = new CodeBiz())
            {
                assetStorageVenderCodeList = codebiz.GetAssetStorageVenderCode();

            }
            Models.JqGridModel model = new Models.JqGridModel
            {
                page = 0,
                total = assetStorageVenderCodeList.Count,
                records = assetStorageVenderCodeList.Count,
                rows = assetStorageVenderCodeList
            };
            return Json(model);
        }
        #endregion

        #region ASSET_ETC_VENDER_CODE 코드 리턴json
        /// <summary>
        /// ASSET_ETC_VENDER_CODE 코드 리턴json
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAssetEtcVenderCodeList()
        {
            List<AssetEtcVenderCodeT> assetEtcVenderCodeList = new List<AssetEtcVenderCodeT>();
            using (CodeBiz codebiz = new CodeBiz())
            {
                assetEtcVenderCodeList = codebiz.GetAssetEtcVenderCode();

            }
            Models.JqGridModel model = new Models.JqGridModel
            {
                page = 0,
                total = assetEtcVenderCodeList.Count,
                records = assetEtcVenderCodeList.Count,
                rows = assetEtcVenderCodeList
            };
            return Json(model);
        }
        #endregion

		#region [POST] IDC별 RACK 리스트 리턴 JSON

		/// <summary>
		/// IDC별 RACK 리스트 리턴 JSON
		/// </summary>
		/// <param name="IdcSeq"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult GetAssetRackListByIdcCode(string IdcCode)
		{
			List<AssetRackCodeT> model = new CodeBiz().GetAssetRackListByIdcCode(IdcCode);

			return Json(model);
		}

		#endregion

        #region [POST] IP코드별 IP네임 리턴 JSON

        /// <summary>
        /// IP코드별 IP네임 리턴 JSON
        /// </summary>
        /// <param name="Ip_Class_Code"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetAssetSelectIpTypeList()
        {
            List<AssetIpTypeT> assetIpTypeList = new List<AssetIpTypeT>();
            using (CodeBiz codeBiz = new CodeBiz())
            {
                assetIpTypeList = codeBiz.GetAssetSelectIpTypeList();
            }

            Models.JqGridModel model = new Models.JqGridModel
            {
                page = 0,
                total = assetIpTypeList.Count,
                records = assetIpTypeList.Count,
                rows = assetIpTypeList
            };
            return Json(model);
        }

        #endregion

	}
}
