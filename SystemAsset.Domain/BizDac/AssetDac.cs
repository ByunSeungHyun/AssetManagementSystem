using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemAsset.Domain.Framework;
using SystemAsset.Domain.Attributes;
using System.Data.SqlClient;
using System.Data;
using SystemAsset.Domain.Entities;
using SystemAsset.Domain.Data;

namespace SystemAsset.Domain.BizDac
{
	public class AssetDac : DacBase
	{
		#region 서버 리스트 정보를 가져온다.

		/// <summary>
		/// Asset 서버 리스트 정보를 가져온다.
		/// 2013-11-22 문태중
		/// </summary>
		/// <param name="searchType"></param>
		/// <param name="searchText"></param>
		/// <param name="pageNo"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public List<AssetServerInfoEx1T> SelectServerList(string searchType, string searchText, int pageNo, int pageSize)
		{
			return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetServerInfoEx1T>(
					typeof(AssetServerInfoEx1T),
					SystemAssetMappingCache.GetDataMappings("AssetServerInfoEx1T"),
					CommandType.StoredProcedure,
					"dbo.UPAR_ASSETDB_Asset_SelectAssetServerList",
					SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
					SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30),
					SqlParameterHelper.CreateParameter("@PAGE_NO", pageNo, SqlDbType.Int),
					SqlParameterHelper.CreateParameter("@PAGE_SIZE", pageSize, SqlDbType.Int)
					);
		}


		/// <summary>
		/// </summary>
		/// Asset 서버 리스트 총 카운트를 가져온다.
		/// 2013-11-22 문태중
		/// </summary>
		/// <param name="searchType"></param>
		/// <param name="searchText"></param>
		/// <returns></returns>
		public int CountServerList(string searchType, string searchText)
		{
			object obj = new DacHelper(DbHelper, "intradb_read").SelectScalar(
					CommandType.StoredProcedure,
					"dbo.UPAR_ASSETDB_Asset_CountAssetServerList",
					SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
					SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30)
					);

			if (obj != null)
				return (int)obj;
			else
				return 0;
		}

		#endregion

		#region Asset 서버 정보 조회

		/// <summary>
		/// Asset 서버 정보를 조회한다.
		/// 2013-11-25 문태중
		/// </summary>
		/// <param name="Seq"></param>
		/// <returns></returns>
		public AssetServerInfoEx1T SelectServerInfo(Int64 Seq)
		{
			return new DacHelper(DbHelper, "intradb_read").SelectSingleEntity<AssetServerInfoEx1T>(
					typeof(AssetServerInfoEx1T),
					SystemAssetMappingCache.GetDataMappings("AssetServerInfoEx1T"),
					CommandType.StoredProcedure,
					"dbo.UPAR_ASSETDB_Asset_SelectAssetServerInfo",
					SqlParameterHelper.CreateParameter("@SEQ", Seq, SqlDbType.BigInt)
					);
		}

		#endregion

		#region Asset 서버 정보 업데이트

		/// <summary>
		/// Asset 서버 정보 추가 및 업데이트
		/// 2013-11-25 문태중
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int UpdateServerInfo(AssetServerInfoT model)
		{
			var SEQ = new DacHelper(DbHelper, "intradb_write").SelectScalar(
				CommandType.StoredProcedure,
				"dbo.UPAR_ASSETDB_Asset_UpdateAssetServerInfo",
				SqlParameterHelper.CreateParameter("@SEQ", model.Seq == null ? 0 : model.Seq.Value, SqlDbType.BigInt),
				SqlParameterHelper.CreateParameter("@BUY_DATE", model.BuyDate, SqlDbType.DateTime),
				SqlParameterHelper.CreateParameter("@SAP_NO", model.SapNo, SqlDbType.VarChar, 50),
				SqlParameterHelper.CreateParameter("@EQUIP_TYPE_CODE", model.EquipTypeCode, SqlDbType.VarChar, 10),
				SqlParameterHelper.CreateParameter("@SERIAL_NO", model.SerialNo, SqlDbType.VarChar, 50),
				SqlParameterHelper.CreateParameter("@SERVER_NAME", model.ServerName, SqlDbType.VarChar, 20),
				SqlParameterHelper.CreateParameter("@SITE_CODE", model.SiteCode, SqlDbType.VarChar, 10),
				SqlParameterHelper.CreateParameter("@TASK_CODE", model.TaskCode, SqlDbType.VarChar, 10),
				SqlParameterHelper.CreateParameter("@SERVICE_CODE", model.ServiceCode, SqlDbType.VarChar, 10),
				SqlParameterHelper.CreateParameter("@IDC_CODE", model.IdcCode, SqlDbType.VarChar, 20),
				SqlParameterHelper.CreateParameter("@RACK_CODE", model.RackCode, SqlDbType.VarChar, 20),
				SqlParameterHelper.CreateParameter("@RACK_START_LOCATION_VALUE", model.RackStartLocationValue.Value, SqlDbType.Int),
				SqlParameterHelper.CreateParameter("@VENDER_CODE", model.VenderCode, SqlDbType.VarChar, 20),
				SqlParameterHelper.CreateParameter("@QTY", model.Qty.Value, SqlDbType.Int),
				SqlParameterHelper.CreateParameter("@OS_CODE", model.OsCode, SqlDbType.VarChar, 10),
				SqlParameterHelper.CreateParameter("@CONTENT", model.Content, SqlDbType.VarChar, 4000),
				SqlParameterHelper.CreateParameter("@INS_OPRT", model.InsOprt, SqlDbType.VarChar, 30)				
				);
            return Convert.ToInt32(SEQ);
		}

		#endregion

		#region Asset 서버 정보 삭제

		/// <summary>
		/// Asset 서버 정보 삭제
		/// 2013-11-25 문태중
		/// </summary>
		/// <param name="seq"></param>
		/// <param name="delOprt"></param>
		public void DeleteServerInfo(Int64 seq, string delOprt)
		{
			new DacHelper(DbHelper, "intradb_write").Execute(
				CommandType.StoredProcedure,
				"dbo.UPAR_ASSETDB_Asset_DeleteAssetServerInfo",
				SqlParameterHelper.CreateParameter("@SEQ", seq, SqlDbType.BigInt),
				SqlParameterHelper.CreateParameter("@DEL_OPRT", delOprt, SqlDbType.VarChar, 30)
				);
		}

		#endregion

		#region Asset 서버 정보 삭제 리스트

		/// <summary>
		/// Asset 서버 정보 삭제 리스트
		/// 2013-11-25 문태중
		/// </summary>
		/// <param name="seq"></param>
		/// <param name="delOprt"></param>
		public void DeleteServerList(string seqList, string delOprt)
		{
			new DacHelper(DbHelper, "intradb_write").Execute(
				CommandType.StoredProcedure,
				"dbo.UPAR_ASSETDB_Asset_DeleteAssetServerList",
				SqlParameterHelper.CreateParameter("@SEQ_LIST", seqList, SqlDbType.VarChar, 500),
				SqlParameterHelper.CreateParameter("@DEL_OPRT", delOprt, SqlDbType.VarChar, 30)
				);
		}

		#endregion

        /************************************ 네트워크 ******************************************************/

        #region 네트워크 리스트 정보를 가져온다.

        /// <summary>
        /// Asset 네트워크 리스트 정보를 가져온다.
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<AssetNetworkInfoEx1T> SelectNetworkList(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetNetworkInfoEx1T>(
                    typeof(AssetNetworkInfoEx1T),
                    SystemAssetMappingCache.GetDataMappings("AssetNetworkInfoEx1T"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_SelectAssetNetworkList",
                    SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
                    SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30),
                    SqlParameterHelper.CreateParameter("@PAGE_NO", pageNo, SqlDbType.Int),
                    SqlParameterHelper.CreateParameter("@PAGE_SIZE", pageSize, SqlDbType.Int)
                    );
        }

        public List<AssetNetworkInfoEx2T> SelectNetworkList2(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetNetworkInfoEx2T>(
                    typeof(AssetNetworkInfoEx2T),
                    SystemAssetMappingCache.GetDataMappings("AssetNetworkInfoEx2T"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_SelectAssetNetworkList",
                    SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
                    SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30),
                    SqlParameterHelper.CreateParameter("@PAGE_NO", pageNo, SqlDbType.Int),
                    SqlParameterHelper.CreateParameter("@PAGE_SIZE", pageSize, SqlDbType.Int)
                    );
        }
        
        /// <summary>
        /// </summary>
        /// Asset 네트워크 리스트 총 카운트를 가져온다.
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int CountNetworkList(string searchType, string searchText)
        {
            object obj = new DacHelper(DbHelper, "intradb_read").SelectScalar(
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_CountAssetNetworkList",
                    SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
                    SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30)
                    );

            if (obj != null)
                return (int)obj;
            else
                return 0;
        }

        #endregion

        #region Asset 네트워크 정보 조회

        /// <summary>
        /// Network 서버 정보를 조회한다.
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public AssetNetworkInfoEx1T SelectNetworkInfo(Int64 Seq)
        {
            return new DacHelper(DbHelper, "intradb_read").SelectSingleEntity<AssetNetworkInfoEx1T>(
                    typeof(AssetNetworkInfoEx1T),
                    SystemAssetMappingCache.GetDataMappings("AssetNetworkInfoEx1T"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_SelectAssetNetworkInfo",
                    SqlParameterHelper.CreateParameter("@SEQ", Seq, SqlDbType.BigInt)
                    );
        }

        #endregion

        #region Asset 네트워크 정보 업데이트

        /// <summary>
        /// Asset 네트워크 정보 추가 및 업데이트
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateNetworkInfo(AssetNetworkInfoT model)
        {
             var SEQ = new DacHelper(DbHelper, "intradb_write").SelectScalar(
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Asset_UpdateAssetNetworkInfo",
                SqlParameterHelper.CreateParameter("@SEQ", model.Seq == null ? 0 : model.Seq.Value, SqlDbType.BigInt),
                SqlParameterHelper.CreateParameter("@BUY_DATE", model.BuyDate, SqlDbType.DateTime),
                SqlParameterHelper.CreateParameter("@SAP_NO", model.SapNo, SqlDbType.VarChar, 50),
                SqlParameterHelper.CreateParameter("@EQUIP_TYPE_CODE", model.EquipTypeCode, SqlDbType.VarChar, 10),
                SqlParameterHelper.CreateParameter("@SERIAL_NO", model.SerialNo, SqlDbType.VarChar, 50),
                SqlParameterHelper.CreateParameter("@SERVER_NAME", model.ServerName, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@SITE_CODE", model.SiteCode, SqlDbType.VarChar, 10),
                SqlParameterHelper.CreateParameter("@IDC_SEQ", model.IdcCode, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@RACK_SEQ", model.RackCode, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@RACK_START_LOCATION_VALUE", model.RackStartLocationValue.Value, SqlDbType.Int),
                SqlParameterHelper.CreateParameter("@VENDER_CODE", model.VenderCode, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@QTY", model.Qty.Value, SqlDbType.TinyInt),
                SqlParameterHelper.CreateParameter("@CONTENT", model.Content, SqlDbType.VarChar, 4000),
                SqlParameterHelper.CreateParameter("@INS_OPRT", model.InsOprt, SqlDbType.VarChar, 30)
                );
            return Convert.ToInt32(SEQ);
        }

        #endregion

        #region Asset 네트워크 정보 삭제

        /// <summary>
        /// Asset 네트워크 정보 삭제
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="delOprt"></param>
        public void DeleteNetworkInfo(Int64 seq, string delOprt)
        {
            new DacHelper(DbHelper, "intradb_write").Execute(
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Asset_DeleteAssetNetworkInfo",
                SqlParameterHelper.CreateParameter("@SEQ", seq, SqlDbType.BigInt),
                SqlParameterHelper.CreateParameter("@DEL_OPRT", delOprt, SqlDbType.VarChar, 30)
                );
        }

        #endregion

        #region Asset 네트워크 정보 삭제 리스트

        /// <summary>
        /// Asset 네트워크 정보 삭제 리스트
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="delOprt"></param>
        public void DeleteNetworkList(string seqList, string delOprt)
        {
            new DacHelper(DbHelper, "intradb_write").Execute(
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Asset_DeleteAssetNetworkList",
                SqlParameterHelper.CreateParameter("@SEQ_LIST", seqList, SqlDbType.VarChar, 500),
                SqlParameterHelper.CreateParameter("@DEL_OPRT", delOprt, SqlDbType.VarChar, 30)
                );
        }

        #endregion

        /**********************************Storage********************************************************/

        #region Storage 리스트 정보를 가져온다.

        /// <summary>
        /// Asset Storage 리스트 정보를 가져온다.
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<AssetStorageInfoEx1T> SelectStorageList(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetStorageInfoEx1T>(
                    typeof(AssetStorageInfoEx1T),
                    SystemAssetMappingCache.GetDataMappings("AssetStorageInfoEx1T"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_SelectAssetStorageList",
                    SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
                    SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30),
                    SqlParameterHelper.CreateParameter("@PAGE_NO", pageNo, SqlDbType.Int),
                    SqlParameterHelper.CreateParameter("@PAGE_SIZE", pageSize, SqlDbType.Int)
                    );
        }

        /// <summary>
        /// </summary>
        /// Asset 서버 리스트 총 카운트를 가져온다.
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int CountStorageList(string searchType, string searchText)
        {
            object obj = new DacHelper(DbHelper, "intradb_read").SelectScalar(
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_CountAssetStorageList",
                    SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
                    SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30)
                    );

            if (obj != null)
                return (int)obj;
            else
                return 0;
        }

        #endregion

        #region Asset Storage 정보 조회

        /// <summary>
        /// Asset Storage 정보를 조회한다.
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public AssetStorageInfoEx1T SelectStorageInfo(Int64 Seq)
        {
            return new DacHelper(DbHelper, "intradb_read").SelectSingleEntity<AssetStorageInfoEx1T>(
                    typeof(AssetStorageInfoEx1T),
                    SystemAssetMappingCache.GetDataMappings("AssetStorageInfoEx1T"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_SelectAssetStorageInfo",
                    SqlParameterHelper.CreateParameter("@SEQ", Seq, SqlDbType.BigInt)
                    );
        }

        #endregion

        #region Asset Storage 정보 업데이트

        /// <summary>
        /// Asset Storage 정보 추가 및 업데이트
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateStorageInfo(AssetStorageInfoT model)
        {
            var SEQ = new DacHelper(DbHelper, "intradb_write").SelectScalar(
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Asset_UpdateAssetStorageInfo",
                SqlParameterHelper.CreateParameter("@SEQ", model.Seq == null ? 0 : model.Seq.Value, SqlDbType.BigInt),
                SqlParameterHelper.CreateParameter("@BUY_DATE", model.BuyDate, SqlDbType.DateTime),
                SqlParameterHelper.CreateParameter("@SAP_NO", model.SapNo, SqlDbType.VarChar, 50),
                SqlParameterHelper.CreateParameter("@EQUIP_TYPE_CODE", model.EquipTypeCode, SqlDbType.VarChar, 10),
                SqlParameterHelper.CreateParameter("@SERIAL_NO", model.SerialNo, SqlDbType.VarChar, 50),
                SqlParameterHelper.CreateParameter("@SERVER_NAME", model.ServerName, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@SITE_CODE", model.SiteCode, SqlDbType.VarChar, 10),
                SqlParameterHelper.CreateParameter("@IDC_SEQ", model.IdcCode, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@RACK_SEQ", model.RackCode, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@RACK_START_LOCATION_VALUE", model.RackStartLocationValue.Value, SqlDbType.Int),
                SqlParameterHelper.CreateParameter("@VENDER_CODE", model.VenderCode, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@QTY", model.Qty.Value, SqlDbType.Int),
                SqlParameterHelper.CreateParameter("@CONTENT", model.Content, SqlDbType.VarChar, 4000),
                SqlParameterHelper.CreateParameter("@INS_OPRT", model.InsOprt, SqlDbType.VarChar, 30)
                );

            return Convert.ToInt32(SEQ);
        }

        #endregion

        #region Asset Storage 정보 삭제

        /// <summary>
        /// Asset 서버 정보 삭제
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="delOprt"></param>
        public void DeleteStorageInfo(Int64 seq, string delOprt)
        {
            new DacHelper(DbHelper, "intradb_write").Execute(
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Asset_DeleteAssetStorageInfo",
                SqlParameterHelper.CreateParameter("@SEQ", seq, SqlDbType.BigInt),
                SqlParameterHelper.CreateParameter("@DEL_OPRT", delOprt, SqlDbType.VarChar, 30)
                );
        }

        #endregion

        #region Asset Storage 정보 삭제 리스트

        /// <summary>
        /// Asset 서버 정보 삭제 리스트
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="delOprt"></param>
        public void DeleteStorageList(string seqList, string delOprt)
        {
            new DacHelper(DbHelper, "intradb_write").Execute(
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Asset_DeleteAssetStorageList",
                SqlParameterHelper.CreateParameter("@SEQ_LIST", seqList, SqlDbType.VarChar, 500),
                SqlParameterHelper.CreateParameter("@DEL_OPRT", delOprt, SqlDbType.VarChar, 30)
                );
        }

        #endregion

        /*********************************ETC*******************************/

        #region etc리스트 정보를 가져온다

        public List<AssetEtcInfoEx1T> SelectEtcList(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetEtcInfoEx1T>(
                    typeof(AssetEtcInfoEx1T),
                    SystemAssetMappingCache.GetDataMappings("AssetEtcInfoEx1T"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_SelectAssetETCList",
                    SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
                    SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30),
                    SqlParameterHelper.CreateParameter("@PAGE_NO", pageNo, SqlDbType.Int),
                    SqlParameterHelper.CreateParameter("@PAGE_SIZE", pageSize, SqlDbType.Int)
                    );
        }

        public int CountEtcList(string searchType, string searchText)
        {
            object obj = new DacHelper(DbHelper, "intradb_read").SelectScalar(
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_SelectAssetETCList",
                    SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
                    SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30)
                    );

            if (obj != null)
                return (int)obj;
            else
                return 0;
        }

        #endregion

        #region Asset etc 정보 조회

        /// <summary>
        /// Asset 서버 정보를 조회한다.
        /// 2013-11-28 최혁남
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public AssetEtcInfoEx1T SelectEtcInfo(Int64 Seq)
        {
            return new DacHelper(DbHelper, "intradb_read").SelectSingleEntity<AssetEtcInfoEx1T>(
                    typeof(AssetEtcInfoEx1T),
                    SystemAssetMappingCache.GetDataMappings("AssetEtcInfoEx1T"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_SelectAssetETCInfo",
                    SqlParameterHelper.CreateParameter("@SEQ", Seq, SqlDbType.BigInt)
                    );
        }

        #endregion

        #region Asset etc 정보 업데이트

        /// <summary>
        /// Asset 서버 정보 추가 및 업데이트
        /// 2013-11-28 최혁남
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateEtcInfo(AssetEtcInfoT model)
        {
            var SEQ = new DacHelper(DbHelper, "intradb_write").SelectScalar(
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Asset_UpdateAssetEtcInfo",
                SqlParameterHelper.CreateParameter("@SEQ", model.Seq == null ? 0 : model.Seq.Value, SqlDbType.BigInt),
                SqlParameterHelper.CreateParameter("@EQUIP_TYPE_CODE", model.EquipTypeCode, SqlDbType.VarChar, 10),
                SqlParameterHelper.CreateParameter("@SERVER_NAME", model.ServerName, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@IDC_SEQ", model.IdcCode, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@RACK_SEQ", model.RackCode, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@RACK_START_LOCATION_VALUE", model.RackStartLocationValue.Value, SqlDbType.Int),
                SqlParameterHelper.CreateParameter("@VENDER_CODE", model.VenderCode, SqlDbType.VarChar, 20),
                SqlParameterHelper.CreateParameter("@CONTENT", model.Content, SqlDbType.VarChar, 4000),
                SqlParameterHelper.CreateParameter("@INS_OPRT", model.InsOprt, SqlDbType.VarChar, 30)

                );
            //return(int)SEQ;

            return Convert.ToInt32(SEQ);
        }

        #endregion

        #region Asset etc 정보 삭제


        public void DeleteEtcInfo(Int64 seq, string delOprt)
        {
            new DacHelper(DbHelper, "intradb_write").Execute(
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Asset_DeleteAssetETCInfo",
                SqlParameterHelper.CreateParameter("@SEQ", seq, SqlDbType.BigInt),
                SqlParameterHelper.CreateParameter("@DEL_OPRT", delOprt, SqlDbType.VarChar, 30)
                );
        }

        #endregion

        #region Asset etc 정보 삭제 리스트

        /// <summary>
        /// Asset 서버 정보 삭제 리스트
        /// 2013-11-28 최혁남
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="delOprt"></param>
        public void DeleteEtcList(string seqList, string delOprt)
        {
            new DacHelper(DbHelper, "intradb_write").Execute(
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Asset_DeleteAssetETCList",
                SqlParameterHelper.CreateParameter("@SEQ_LIST", seqList, SqlDbType.VarChar, 500),
                SqlParameterHelper.CreateParameter("@DEL_OPRT", delOprt, SqlDbType.VarChar, 30)
                );
        }

        #endregion
    }
}
