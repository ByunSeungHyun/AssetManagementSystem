using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemAsset.Domain.Data;
using SystemAsset.Domain.BizDac;
using SystemAsset.Domain.Entities;
using SystemAsset.WebUI.Models;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace SystemAsset.WebUI.Controllers
{
    public class AssetController : BaseController
    {
        //
        // GET: /Asset/


        public ActionResult Index()
        {
            return View();
        }

		#region Asset 서버 리스트

		public ActionResult ServerList()
		{
			return View();
		}

		#endregion

		#region Asset 서버 등록 및 업데이트

		public ActionResult ServerUpdate(long? Seq)
		{
			AssetServerInfoEx1T model = new AssetServerInfoEx1T();
			string strTitle = "Asset Server 신규추가";

			if (Seq != null && Seq.Value > 0)
			{
				strTitle = "Asset Server 수정";
				model = new AssetBiz().GetServerInfo(Seq.Value);				
			}
			else
			{ 
				//기본값 설정
				model.Qty = 1;
				model.RackStartLocationValue = 0;
			}

			ViewData["Tiele"] = strTitle;

			#region 공통 코드 리스트 설정
			AssetCommonCodeT CommonDefault = new AssetCommonCodeT();
			CommonDefault.AssetCode = "";
			CommonDefault.CodeName = "선택하세요";

			List<AssetCommonCodeT> SiteCodeList = new CodeBiz().GetCommonCodeByAssetType("COMMON", "SITE");
			List<AssetCommonCodeT> ServiceCodeList = new CodeBiz().GetCommonCodeByAssetType("SERVER", "SERVICE");
			List<AssetCommonCodeT> OsCodeList = new CodeBiz().GetCommonCodeByAssetType("SERVER", "OS");
			List<AssetCommonCodeT> BusinessCodeList = new CodeBiz().GetCommonCodeByAssetType("SERVER", "BUSINESS");
			List<AssetCommonCodeT> HardwareCodeList = new CodeBiz().GetCommonCodeByAssetType("SERVER", "Hardware");

			AssetServerVenderCodeT VenderDefault = new AssetServerVenderCodeT();
            VenderDefault.VenderCode = "";
			VenderDefault.VenderName = "선택하세요";
			
			List<AssetServerVenderCodeT> VenderCodeList = new CodeBiz().GetAssetServerVenderCode();

			AssetIdcCodeT IdcDefault = new AssetIdcCodeT();
			IdcDefault.IdcCode = "";
			IdcDefault.LClassName = "선택하세요";

			List<AssetIdcCodeT> IdcCodeList = new CodeBiz().GetAssetIdcCode();

			AssetRackCodeT RackDefault = new AssetRackCodeT();
            RackDefault.RackCode = "";
			RackDefault.RackName = "선택하세요";

			List<AssetRackCodeT> RackCodeList = new List<AssetRackCodeT>();
            //.Value > 0
			if (model.IdcCode != null && model.IdcCode != "")
			{
				RackCodeList = new CodeBiz().GetAssetRackListByIdcCode(model.IdcCode);
			}
				

			//기본값 설정
			SiteCodeList.Insert(0, CommonDefault);
			ServiceCodeList.Insert(0, CommonDefault);
			OsCodeList.Insert(0, CommonDefault);
			BusinessCodeList.Insert(0, CommonDefault);
			HardwareCodeList.Insert(0, CommonDefault);
			VenderCodeList.Insert(0, VenderDefault);
			IdcCodeList.Insert(0, IdcDefault);
			RackCodeList.Insert(0, RackDefault);

			ViewBag.SiteCodeList = SiteCodeList;
			ViewBag.ServiceCodeList = ServiceCodeList;
			ViewBag.OsCodeList = OsCodeList;
			ViewBag.TaskCodeList = BusinessCodeList;
			ViewBag.HardwareCodeList = HardwareCodeList;
			ViewBag.VenderCodeList = VenderCodeList;
			ViewBag.IdcCodeList = IdcCodeList;
			ViewBag.RackCodeList = RackCodeList;
			

			#endregion

			return View(model);
		}

		#endregion

		#region Asset 서버 정보 엑셀 다운로드

		public ActionResult GetServerExcelList(string searchType, string searchText, int? page, int? rows)
		{
			var grid = new System.Web.UI.WebControls.GridView();
			List<AssetServerInfoEx1T> model = new AssetBiz().GetServerList(searchType, searchText, page.Value, rows.Value);

			grid.DataSource = from m in model
							  select new
							  {
								  Seq = m.Seq,
								  SapNo = m.SapNo,
								  ServerName = m.ServerName,
								  EquipTypeName = m.EquipTypeName,
								  BuyDate = m.BuyDate.Value.ToString("yyyy-MM-dd"),
								  ExpireDate = m.ExpireDate.Value.ToString("yyyy-MM-dd"),
								  RemainDay = m.RemainDay.Value,
								  SiteName = m.SiteName,
								  TaskName = m.TaskName,
								  ServiceName = m.ServiceName,
								  IdcName = m.IdcName,
								  RackLocationCode = m.RackLocationCode,
								  OsName = m.OsName,
								  VenderName = m.VenderName,
								  ProductName = m.ProductName,
								  SerialNo = m.SerialNo,
								  ProcessCnt = m.ProcessCnt,
								  CpuTypeName = m.CpuTypeName,
								  TotalCoreCnt = m.TotalCoreCnt,
								  MemorySizeName = m.MemorySizeName,
								  ServerIp = m.ServerIp,
								  Content = m.Content
							  };
			

			grid.DataBind();

			Response.ClearContent();

			Response.AddHeader("content-disposition", string.Format("attachment; filename=Excel_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss")));
			Response.Write("<meta http-equiv='Content-Type' content='text/html; charset=" + Request.ContentEncoding.HeaderName + "'>");
			Response.Buffer = true;

			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);

			grid.RenderControl(htw);			
			Response.Write(sw.ToString());
			Response.End();

			return View("Index"); 
		}

		#endregion

		#region [POST] Asset 서버 리스트

		[HttpPost]
		public JsonResult GetServerList(string searchType, string searchText, int? page, int? rows)
		{
			string paramSearchType = string.IsNullOrEmpty(searchType) ? string.Empty : searchType;
			string paramSearchText = string.IsNullOrEmpty(searchText) ? string.Empty : searchText;
			int paramPageNo = page == null ? 1 : page.Value;
			int paramPageSize = rows == null ? 10 : rows.Value;

			AssetBiz assetBiz = new AssetBiz();
			
			int nTotalCount = assetBiz.CountServerList(paramSearchType, paramSearchText);
			int nRecord = nTotalCount <= paramPageSize ? 1 : (int)Math.Ceiling((double)(nTotalCount / paramPageSize));

			Models.JqGridModel model = new Models.JqGridModel
			{
				page = paramPageNo,
				total = nRecord,
				records = nTotalCount,
				rows = assetBiz.GetServerList(paramSearchType, paramSearchText, paramPageNo, paramPageSize)
			};
			
			return Json(model);
		}

		#endregion

		#region [POST] Asset 서버 정보 업데이트

		/// <summary>
		/// [POST] Asset 서버 정보 업데이트
		/// 2013-11-25 문태중
		/// </summary>
		/// <param name="Seq"></param>
		/// <param name="SapNo"></param>
		/// <param name="ServerName"></param>
		/// <param name="EquipTypeCode"></param>
		/// <param name="SiteCode"></param>
		/// <param name="TaskCode"></param>
		/// <param name="ServiceCode"></param>
		/// <param name="IdcCode"></param>
		/// <param name="RackCode"></param>
		/// <param name="BuyDate"></param>
		/// <param name="OsCode"></param>
		/// <param name="VenderCode"></param>
		/// <param name="Content"></param>
		/// <returns></returns>
		public JsonResult SetServerUpdate(Int64? Seq, string SapNo, string ServerName, string SerialNo, int Qty, string EquipTypeCode, string SiteCode, string TaskCode,
			string ServiceCode, String IdcCode, String RackCode, int RackStartLocationValue, DateTime BuyDate, string OsCode, string VenderCode, string Content)
		{
			ResultModel returnModel = new ResultModel();

			AssetServerInfoT model = new AssetServerInfoT();
			model.Seq = (Seq == null) ? 0 : Seq.Value;
			model.SapNo = SapNo;
			model.ServerName = ServerName;
			model.SerialNo = SerialNo;
			model.Qty = Qty;
			model.EquipTypeCode = EquipTypeCode;
			model.SiteCode = SiteCode;
			model.TaskCode = TaskCode;
			model.ServiceCode = ServiceCode;
			model.IdcCode = IdcCode;
			model.RackCode = RackCode;
			model.RackStartLocationValue = RackStartLocationValue;
			model.BuyDate = BuyDate;
			model.OsCode = OsCode;
			model.VenderCode = VenderCode;
			model.Content = Content;
			model.InsOprt = this.UserID;

			int nResult = new AssetBiz().UpdateServerInfo(model);

			returnModel.statusCode = nResult > 0 ? StatusCodeEnum.Success : StatusCodeEnum.Fail;

			return Json(returnModel);
		}

		#endregion

		#region [POST] Asset 서버 정보 삭제

		/// <summary>
		/// [POST] Asset 서버 정보 삭제
		/// 2013-11-25 문태중
		/// </summary>
		/// <param name="Seq"></param>
		/// <returns></returns>
		public JsonResult DelServerUpdate(Int64 Seq)
		{
			new AssetBiz().DeleteServerInfo(Seq, this.UserID);

			ResultModel returnModel = new ResultModel();
			returnModel.statusCode = StatusCodeEnum.Success;

			return Json(returnModel);
		}

		/// <summary>
		/// [POST] Asset 서버 정보 삭제 리스트
		/// 2013-11-25 문태중
		/// </summary>
		/// <param name="Seq"></param>
		/// <returns></returns>
		public JsonResult DelServerUpdateList(string SeqList)
		{
			new AssetBiz().DeleteServerList(SeqList, this.UserID);

			ResultModel returnModel = new ResultModel();
			returnModel.statusCode = StatusCodeEnum.Success;

			return Json(returnModel);
		}

		#endregion

        /*************************네트워크***************************************/

        #region Asset 네트워크 리스트
        public ActionResult NetworkList()
        {
            return View();
        }
        #endregion

        #region Asset 네트워크 등록 및 업데이트

        public ActionResult NetworkUpdate(long? Seq)
        {
            AssetNetworkInfoEx1T model = new AssetNetworkInfoEx1T();
            string strTitle = "Asset Network 신규추가";

            if (Seq != null && Seq.Value > 0)
            {
                strTitle = "Asset Network 수정";
                model = new AssetBiz().GetNetworkInfo(Seq.Value);
            }
            else
            {
                //기본값 설정
                model.Qty = 1;
                model.RackStartLocationValue = 0;
            }

            ViewData["Tiele"] = strTitle;

            #region 공통 코드 리스트 설정
            AssetCommonCodeT CommonDefault = new AssetCommonCodeT();
            CommonDefault.AssetCode = "";
            CommonDefault.CodeName = "선택하세요";

            List<AssetCommonCodeT> SiteCodeList = new CodeBiz().GetCommonCodeByAssetType("COMMON", "SITE");
            List<AssetCommonCodeT> BusinessCodeList = new CodeBiz().GetCommonCodeByAssetType("NETWORK", "BUSINESS");
            List<AssetCommonCodeT> HardwareCodeList = new CodeBiz().GetCommonCodeByAssetType("NETWORK", "Hardware");

            AssetNetworkVenderCodeT VenderDefault = new AssetNetworkVenderCodeT();
            VenderDefault.VenderCode = "";
            VenderDefault.VenderName = "선택하세요";

            List<AssetNetworkVenderCodeT> VenderCodeList = new CodeBiz().GetAssetNetworkVenderCode();

            AssetIdcCodeT IdcDefault = new AssetIdcCodeT();
            IdcDefault.IdcCode = "";
            IdcDefault.LClassName = "선택하세요";

            List<AssetIdcCodeT> IdcCodeList = new CodeBiz().GetAssetIdcCode();

            AssetRackCodeT RackDefault = new AssetRackCodeT();
            RackDefault.RackCode = "";
            RackDefault.RackName = "선택하세요";

            List<AssetRackCodeT> RackCodeList = new List<AssetRackCodeT>();
            if (model.IdcCode != null && model.IdcCode != "")
            {
                RackCodeList = new CodeBiz().GetAssetRackListByIdcCode(model.IdcCode);
            }


            //기본값 설정
            SiteCodeList.Insert(0, CommonDefault);
            BusinessCodeList.Insert(0, CommonDefault);
            HardwareCodeList.Insert(0, CommonDefault);
            VenderCodeList.Insert(0, VenderDefault);
            IdcCodeList.Insert(0, IdcDefault);
            RackCodeList.Insert(0, RackDefault);

            ViewBag.SiteCodeList = SiteCodeList;
            ViewBag.HardwareCodeList = HardwareCodeList;
            ViewBag.VenderCodeList = VenderCodeList;
            ViewBag.IdcCodeList = IdcCodeList;
            ViewBag.RackCodeList = RackCodeList;


            #endregion

            return View(model);
        }

        #endregion

        #region Asset 네트워크 정보 엑셀 다운로드

        public ActionResult GetNetworkExcelList(string searchType, string searchText, int? page, int? rows)
        {
            var grid = new System.Web.UI.WebControls.GridView();
            List<AssetNetworkInfoEx1T> model = new AssetBiz().GetNetworkList(searchType, searchText, page.Value, rows.Value);

            grid.DataSource = from m in model
                              select new
                              {
                                  Seq = m.Seq,
                                  SapNo = m.SapNo,
                                  ServerName = m.ServerName,
                                  EquipTypeName = m.EquipTypeName,
                                  BuyDate = m.BuyDate.Value.ToString("yyyy-MM-dd"),
                                  ExpireDate = m.ExpireDate.Value.ToString("yyyy-MM-dd"),
                                  RemainDay = m.RemainDay.Value,
                                  SiteName = m.SiteName,
                                  IdcName = m.IdcName,
                                  RackLocationCode = m.RackLocationCode,
                                  VenderName = m.VenderName,
                                  ProductName = m.ProductName,
                                  SerialNo = m.SerialNo,
                                  ProcessCnt = m.ProcessCnt,
                                  CpuTypeName = m.CpuTypeName,
                                  TotalCoreCnt = m.TotalCoreCnt,
                                  MemorySizeName = m.MemorySizeName,
                                  ServerIp = m.ServerIp,
                                  Content = m.Content
                              };


            grid.DataBind();

            Response.ClearContent();

            Response.AddHeader("content-disposition", string.Format("attachment; filename=Excel_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss")));
            Response.Write("<meta http-equiv='Content-Type' content='text/html; charset=" + Request.ContentEncoding.HeaderName + "'>");
            Response.Buffer = true;

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

            return View("Index");
        }

        #endregion

        #region [POST] Asset 네트워크 리스트

        //[HttpPost]
        public JsonResult GetNetworkList(string searchType, string searchText, int? page, int? rows)
        {
            string paramSearchType = string.IsNullOrEmpty(searchType) ? string.Empty : searchType;
            string paramSearchText = string.IsNullOrEmpty(searchText) ? string.Empty : searchText;
            int paramPageNo = page == null ? 1 : page.Value;
            int paramPageSize = rows == null ? 10 : rows.Value;

            AssetBiz assetBiz = new AssetBiz();

            int nTotalCount = assetBiz.CountNetworkList(paramSearchType, paramSearchText);
            int nRecord = nTotalCount <= paramPageSize ? 1 : (int)Math.Ceiling((double)(nTotalCount / paramPageSize));
            Models.JqGridModel model = new Models.JqGridModel
            {
                page = paramPageNo,
                total = nRecord,
                records = nTotalCount,
                rows = assetBiz.GetNetworkList(paramSearchType, paramSearchText, paramPageNo, paramPageSize)
            };

            return Json(model);
        }

        #endregion

        #region [POST] Asset 네트워크 정보 업데이트

        /// <summary>
        /// [POST] Asset 네트워크 정보 업데이트
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="Seq"></param>
        /// <param name="SapNo"></param>
        /// <param name="ServerName"></param>
        /// <param name="EquipTypeCode"></param>
        /// <param name="SiteCode"></param>
        /// <param name="TaskCode"></param>
        /// <param name="ServiceCode"></param>
        /// <param name="IdcCode"></param>
        /// <param name="RackCode"></param>
        /// <param name="BuyDate"></param>
        /// <param name="OsCode"></param>
        /// <param name="VenderCode"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public JsonResult SetNetworkUpdate(Int64? Seq, string SapNo, string ServerName, string SerialNo, int Qty, string EquipTypeCode, string SiteCode,
             String IdcCode, String RackCode, int RackStartLocationValue, DateTime BuyDate, string VenderCode, string Content)
        {
            ResultModel returnModel = new ResultModel();

            AssetNetworkInfoT model = new AssetNetworkInfoT();
            model.Seq = (Seq == null) ? 0 : Seq.Value;
            model.SapNo = SapNo;
            model.ServerName = ServerName;
            model.SerialNo = SerialNo;
            model.Qty = Qty;
            model.EquipTypeCode = EquipTypeCode;
            model.SiteCode = SiteCode;
            model.IdcCode = IdcCode;
            model.RackCode = RackCode;
            model.RackStartLocationValue = RackStartLocationValue;
            model.BuyDate = BuyDate;
            model.VenderCode = VenderCode;
            model.Content = Content;
            model.InsOprt = this.UserID;

            int nResult = new AssetBiz().UpdateNetworkInfo(model);

            returnModel.statusCode = nResult > 0 ? StatusCodeEnum.Success : StatusCodeEnum.Fail;

            return Json(returnModel);
        }

        #endregion

        #region [POST] Asset 네트워크 정보 삭제

        /// <summary>
        /// [POST] Asset 네트워크 정보 삭제
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public JsonResult DelNetworkUpdate(Int64 Seq)
        {
            new AssetBiz().DeleteNetworkInfo(Seq, this.UserID);

            ResultModel returnModel = new ResultModel();
            returnModel.statusCode = StatusCodeEnum.Success;

            return Json(returnModel);
        }

        /// <summary>
        /// [POST] Asset 네트워크 정보 삭제 리스트
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public JsonResult DelNetworkUpdateList(string SeqList)
        {
            new AssetBiz().DeleteNetworkList(SeqList, this.UserID);

            ResultModel returnModel = new ResultModel();
            returnModel.statusCode = StatusCodeEnum.Success;

            return Json(returnModel);
        }

        #endregion

        /*****************************Storage*************************************/

        #region Asset Storage 리스트

        public ActionResult StorageList()
        {
            return View();
        }

        #endregion

        #region [POST] Asset Storage 리스트

        [HttpPost]
        public JsonResult GetStorageList(string searchType, string searchText, int? page, int? rows)
        {
            string paramSearchType = string.IsNullOrEmpty(searchType) ? string.Empty : searchType;
            string paramSearchText = string.IsNullOrEmpty(searchText) ? string.Empty : searchText;
            int paramPageNo = page == null ? 1 : page.Value;
            int paramPageSize = rows == null ? 10 : rows.Value;

            AssetBiz assetBiz = new AssetBiz();

            int nTotalCount = assetBiz.CountStorageList(paramSearchType, paramSearchText);
            int nRecord = nTotalCount <= paramPageSize ? 1 : (int)Math.Ceiling((double)(nTotalCount / paramPageSize));

            Models.JqGridModel model = new Models.JqGridModel
            {
                page = paramPageNo,
                total = nRecord,
                records = nTotalCount,
                rows = assetBiz.GetStorageList(paramSearchType, paramSearchText, paramPageNo, paramPageSize)
            };

            return Json(model);
        }

        #endregion

        #region [POST] Asset Storage 정보 업데이트

        /// <summary>
        /// [POST] Asset Storage 정보 업데이트
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="Seq"></param>
        /// <param name="SapNo"></param>
        /// <param name="ServerName"></param>
        /// <param name="EquipTypeCode"></param>
        /// <param name="SiteCode"></param>
        /// <param name="TaskCode"></param>
        /// <param name="ServiceCode"></param>
        /// <param name="IdcCode"></param>
        /// <param name="RackCode"></param>
        /// <param name="BuyDate"></param>
        /// <param name="OsCode"></param>
        /// <param name="VenderCode"></param>
        /// <param name="Content"></param>
        /// <param name="InsOprt"></param>
        /// <param name="DelDate"></param>
        /// <param name="InsDate"></param>
        /// <returns></returns>
        public JsonResult SetStorageUpdate(Int64? Seq, string SapNo, string ServerName, string SerialNo, int Qty, string EquipTypeCode, string SiteCode, string TaskCode,
            string ServiceCode, String IdcCode, String RackCode, int RackStartLocationValue, DateTime BuyDate, string OsCode, string VenderCode, string Content)
        {
            ResultModel returnModel = new ResultModel();

            AssetStorageInfoT model = new AssetStorageInfoT();
            model.Seq = (Seq == null) ? 0 : Seq.Value;
            model.SapNo = SapNo;
            model.ServerName = ServerName;
            model.SerialNo = SerialNo;
            model.Qty = Qty;
            model.EquipTypeCode = EquipTypeCode;
            model.SiteCode = SiteCode;
            model.TaskCode = TaskCode;
            model.ServiceCode = ServiceCode;
            model.IdcCode = IdcCode;
            model.RackCode = RackCode;
            model.RackStartLocationValue = RackStartLocationValue;
            model.BuyDate = BuyDate;
            model.OsCode = OsCode;
            model.VenderCode = VenderCode;
            model.Content = Content;
            model.InsOprt = this.UserID;

            int nResult = new AssetBiz().UpdateStorageInfo(model);

            returnModel.statusCode = nResult > 0 ? StatusCodeEnum.Success : StatusCodeEnum.Fail;
            return Json(returnModel);
        }

        #endregion

        #region [POST] Asset Storage 정보 삭제

        /// <summary>
        /// [POST] Asset Storage 정보 삭제
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public JsonResult DelStorageUpdate(Int64 Seq)
        {
            new AssetBiz().DeleteStorageInfo(Seq, this.UserID);

            ResultModel returnModel = new ResultModel();
            returnModel.statusCode = StatusCodeEnum.Success;

            return Json(returnModel);
        }

        /// <summary>
        /// [POST] Asset Storage 정보 삭제 리스트
        /// 2013-11-25 문태중
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public JsonResult DelStorageUpdateList(string SeqList)
        {
            new AssetBiz().DeleteStorageList(SeqList, this.UserID);

            ResultModel returnModel = new ResultModel();
            returnModel.statusCode = StatusCodeEnum.Success;

            return Json(returnModel);
        }
        #endregion

        #region Asset Storage 등록 및 업데이트

        public ActionResult StorageUpdate(long? Seq)
        {
            AssetStorageInfoEx1T model = new AssetStorageInfoEx1T();
            string strTitle = "Asset Storage 신규추가";

            if (Seq != null && Seq.Value > 0)
            {
                strTitle = "Asset Storage 수정";
                model = new AssetBiz().GetStorageInfo(Seq.Value);
            }
            else
            {
                //기본값 설정
                model.Qty = 1;
                model.RackStartLocationValue = 0;
            }

            ViewData["Tiele"] = strTitle;

            #region 공통 코드 리스트 설정
            AssetCommonCodeT CommonDefault = new AssetCommonCodeT();
            CommonDefault.AssetCode = "";
            CommonDefault.CodeName = "선택하세요";

            List<AssetCommonCodeT> SiteCodeList = new CodeBiz().GetCommonCodeByAssetType("COMMON", "SITE");
            //List<AssetCommonCodeT> ServiceCodeList = new CodeBiz().GetCommonCodeByAssetType("STORAGE", "SERVICE");
            //List<AssetCommonCodeT> OsCodeList = new CodeBiz().GetCommonCodeByAssetType("STORAGE", "OS");
            List<AssetCommonCodeT> BusinessCodeList = new CodeBiz().GetCommonCodeByAssetType("STORAGE", "BUSINESS");
            List<AssetCommonCodeT> HardwareCodeList = new CodeBiz().GetCommonCodeByAssetType("STORAGE", "Hardware");

            AssetStorageVenderCodeT VenderDefault = new AssetStorageVenderCodeT();
            VenderDefault.VenderCode = "";
            VenderDefault.VenderName = "선택하세요";

            List<AssetStorageVenderCodeT> VenderCodeList = new CodeBiz().GetAssetStorageVenderCode();

            AssetIdcCodeT IdcDefault = new AssetIdcCodeT();
            IdcDefault.IdcCode = "";
            IdcDefault.LClassName = "선택하세요";

            List<AssetIdcCodeT> IdcCodeList = new CodeBiz().GetAssetIdcCode();

            AssetRackCodeT RackDefault = new AssetRackCodeT();
            RackDefault.RackCode = "";
            RackDefault.RackName = "선택하세요";

            List<AssetRackCodeT> RackCodeList = new List<AssetRackCodeT>();
            if (model.IdcCode != null && model.IdcCode != "")
            {
                RackCodeList = new CodeBiz().GetAssetRackListByIdcCode(model.IdcCode);
            }


            //기본값 설정
            SiteCodeList.Insert(0, CommonDefault);
            //ServiceCodeList.Insert(0, CommonDefault);
            //OsCodeList.Insert(0, CommonDefault);
            BusinessCodeList.Insert(0, CommonDefault);
            HardwareCodeList.Insert(0, CommonDefault);
            VenderCodeList.Insert(0, VenderDefault);
            IdcCodeList.Insert(0, IdcDefault);
            RackCodeList.Insert(0, RackDefault);

            ViewBag.SiteCodeList = SiteCodeList;
            //ViewBag.ServiceCodeList = ServiceCodeList;
            //ViewBag.OsCodeList = OsCodeList;
            ViewBag.TaskCodeList = BusinessCodeList;
            ViewBag.HardwareCodeList = HardwareCodeList;
            ViewBag.VenderCodeList = VenderCodeList;
            ViewBag.IdcCodeList = IdcCodeList;
            ViewBag.RackCodeList = RackCodeList;


            #endregion

            return View(model);
        }

        #endregion

        #region Asset Storage 정보 엑셀 다운로드

        public ActionResult GetStorageExcelList(string searchType, string searchText, int? page, int? rows)
        {
            var grid = new System.Web.UI.WebControls.GridView();
            List<AssetStorageInfoEx1T> model = new AssetBiz().GetStorageList(searchType, searchText, page.Value, rows.Value);

            grid.DataSource = from m in model
                              select new
                              {
                                  Seq = m.Seq,
                                  SapNo = m.SapNo,
                                  ServerName = m.ServerName,
                                  EquipTypeName = m.EquipTypeName,
                                  BuyDate = m.BuyDate.Value.ToString("yyyy-MM-dd"),
                                  ExpireDate = m.ExpireDate.Value.ToString("yyyy-MM-dd"),
                                  RemainDay = m.RemainDay.Value,
                                  SiteName = m.SiteName,
                                  TaskCode = m.TaskCode,
                                  IdcName = m.IdcName,
                                  RackLocationCode = m.RackLocationCode,
                                  VenderName = m.VenderName,
                                  ProductName = m.ProductName,
                                  SerialNo = m.SerialNo,
                                  ProcessCnt = m.ProcessCnt,
                                  ServerIp = m.ServerIp,
                                  Content = m.Content,
                                  DelYn = m.DelYn,
                                  DelDate = m.DelDate,
                                  InsOprt = m.InsOprt,
                                  InsDate = m.InsDate

                              };


            grid.DataBind();

            Response.ClearContent();

            Response.AddHeader("content-disposition", string.Format("attachment; filename=Excel_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss")));
            Response.Write("<meta http-equiv='Content-Type' content='text/html; charset=" + Request.ContentEncoding.HeaderName + "'>");
            Response.Buffer = true;

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

            return View("Index");
        }

        #endregion
        /******************etc*************/


        #region asset etc 리스트

        public ActionResult EtcList()
        {
            return View();
        }
        #endregion

        #region Asset etc 등록 및 업데이트

        public ActionResult EtcUpdate(long? Seq)
        {
            AssetEtcInfoEx1T model = new AssetEtcInfoEx1T();
            string strTitle = "Asset 기타 신규추가";

            if (Seq != null && Seq.Value > 0)
            {
                strTitle = "Asset 기타 수정";
                model = new AssetBiz().GetEtcInfo(Seq.Value);
            }
            else
            {
                //기본값 설정

                model.RackStartLocationValue = 0;
            }

            ViewData["Tiele"] = strTitle;

            #region 공통 코드 리스트 설정
            AssetCommonCodeT CommonDefault = new AssetCommonCodeT();
            CommonDefault.AssetCode = "";
            CommonDefault.CodeName = "선택하세요";


            List<AssetCommonCodeT> HardwareCodeList = new CodeBiz().GetCommonCodeByAssetType("Etc", "Hardware");

            AssetEtcVenderCodeT VenderDefault = new AssetEtcVenderCodeT();
            VenderDefault.VenderCode = "";
            VenderDefault.VenderName = "선택하세요";

            List<AssetEtcVenderCodeT> VenderCodeList = new CodeBiz().GetAssetEtcVenderCode();

            AssetIdcCodeT IdcDefault = new AssetIdcCodeT();
            IdcDefault.IdcCode = "";
            IdcDefault.LClassName = "선택하세요";

            List<AssetIdcCodeT> IdcCodeList = new CodeBiz().GetAssetIdcCode();

            AssetRackCodeT RackDefault = new AssetRackCodeT();
            RackDefault.RackCode = "";
            RackDefault.RackName = "선택하세요";

            List<AssetRackCodeT> RackCodeList = new List<AssetRackCodeT>();
            if (model.IdcCode != null && model.IdcCode !="")
            {
                RackCodeList = new CodeBiz().GetAssetRackListByIdcCode(model.IdcCode);
            }


            //기본값 설정

            HardwareCodeList.Insert(0, CommonDefault);
            VenderCodeList.Insert(0, VenderDefault);
            IdcCodeList.Insert(0, IdcDefault);
            RackCodeList.Insert(0, RackDefault);

            ViewBag.HardwareCodeList = HardwareCodeList;
            ViewBag.VenderCodeList = VenderCodeList;
            ViewBag.IdcCodeList = IdcCodeList;
            ViewBag.RackCodeList = RackCodeList;

            #endregion

            return View(model);
        }
        #endregion

        #region Asset etc 정보 엑셀 다운로드

        public ActionResult GetEtcExcelList(string searchType, string searchText, int? page, int? rows)
        {
            var grid = new System.Web.UI.WebControls.GridView();
            List<AssetEtcInfoEx1T> model = new AssetBiz().GetEtcList(searchType, searchText, page.Value, rows.Value);

            grid.DataSource = from m in model
                              select new
                              {
                                  Seq = m.Seq,
                                  ServerName = m.ServerName,
                                  EquipTypeName = m.EquipTypeName,
                                  IdcName = m.IdcName,
                                  RackLocationCode = m.RackLocationCode,
                                  VenderName = m.VenderName,
                                  ProductName = m.ProductName,
                                  Content = m.Content
                              };


            grid.DataBind();

            Response.ClearContent();

            Response.AddHeader("content-disposition", string.Format("attachment; filename=Excel_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss")));
            Response.Write("<meta http-equiv='Content-Type' content='text/html; charset=" + Request.ContentEncoding.HeaderName + "'>");
            Response.Buffer = true;

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

            return View("Index");
        }

        #endregion

        #region [POST] Asset etc 리스트

        [HttpPost]
        public JsonResult GetEtcList(string searchType, string searchText, int? page, int? rows)
        {
            string paramSearchType = string.IsNullOrEmpty(searchType) ? string.Empty : searchType;
            string paramSearchText = string.IsNullOrEmpty(searchText) ? string.Empty : searchText;
            int paramPageNo = page == null ? 1 : page.Value;
            int paramPageSize = rows == null ? 10 : rows.Value;

            AssetBiz assetBiz = new AssetBiz();

            int nTotalCount = assetBiz.CountEtcList(paramSearchType, paramSearchText);
            int nRecord = nTotalCount <= paramPageSize ? 1 : (int)Math.Ceiling((double)(nTotalCount / paramPageSize));

            Models.JqGridModel model = new Models.JqGridModel
            {
                page = paramPageNo,
                total = nRecord,
                records = nTotalCount,
                rows = assetBiz.GetEtcList(paramSearchType, paramSearchText, paramPageNo, paramPageSize)
            };

            return Json(model);
        }

        #endregion

        #region etc 정보 업데이트
        [HttpPost]
        public JsonResult SetEtcUpdate(Int64? Seq, string ServerName, string EquipTypeCode,
                String IdcCode, String RackCode, int RackStartLocationValue, string VenderCode, string Content)
        {
            ResultModel returnModel = new ResultModel();

            AssetEtcInfoT model = new AssetEtcInfoT();
            model.Seq = (Seq == null) ? 0 : Seq.Value;
            model.ServerName = ServerName;
            model.EquipTypeCode = EquipTypeCode;
            model.IdcCode = IdcCode;
            model.RackCode = RackCode;
            model.RackStartLocationValue = RackStartLocationValue;
            model.VenderCode = VenderCode;
            model.Content = Content;


            int nResult = new AssetBiz().UpdateEtcInfo(model);

            returnModel.statusCode = nResult > 0 ? StatusCodeEnum.Success : StatusCodeEnum.Fail;

            return Json(returnModel);
        }


        #endregion

        #region [POST] Asset etc 정보 삭제

        /// <summary>
        /// [POST] Asset 서버 정보 삭제
        /// 2013-11-25 문태중
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public JsonResult DelEtcUpdate(Int64 Seq)
        {
            new AssetBiz().DeleteEtcInfo(Seq, this.UserID);

            ResultModel returnModel = new ResultModel();
            returnModel.statusCode = StatusCodeEnum.Success;

            return Json(returnModel);
        }


        /// <summary>
        /// [POST] Asset 서버 정보 삭제 리스트
        /// 2013-11-25 문태중
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public JsonResult DelEtcUpdateList(string SeqList)
        {
            new AssetBiz().DeleteEtcList(SeqList, this.UserID);

            ResultModel returnModel = new ResultModel();
            returnModel.statusCode = StatusCodeEnum.Success;

            return Json(returnModel);
        }

        #endregion
    }
}
