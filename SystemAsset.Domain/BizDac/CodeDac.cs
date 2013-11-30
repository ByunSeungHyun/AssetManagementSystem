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
	public class CodeDac : DacBase
	{
		#region ASSET_TYPE별 공통 코드 리스트 정보를 가져온다.

		/// <summary>
		/// ASSET_TYPE별 공통 코드 리스트 정보를 가져온다.
		/// </summary>
		/// <param name="type">ASSET_TYPE</param>
		/// <returns></returns>
        public List<AssetCommonCodeT> SelectCommonCodeByAssetType(string asset_group_type, string type)
		{
			return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetCommonCodeT>(
				typeof(AssetCommonCodeT),
				SystemAssetMappingCache.GetDataMappings("AssetCommonCodeT"),
				CommandType.StoredProcedure,
				"dbo.UPAR_ASSETDB_Code_SelectCommonCodeListByAssetType",
				SqlParameterHelper.CreateParameter("@ASSET_TYPE", type, SqlDbType.VarChar, 15),
                SqlParameterHelper.CreateParameter("@ASSET_GROUP_CODE", asset_group_type, SqlDbType.VarChar, 15)
				);
        }

        #endregion

        #region ASSET_IDC_CODE별 리스트 정보를 가져온다.

        /// <summary>
        ///ASSET_IDC_CODE별 리스트 정보를 가져온다.
        /// </summary>
        /// 작성자 pingoli05
        /// 날자 : 2013-11-19
        /// <returns></returns>
        public List<AssetIdcCodeT> SelectAssetIdcCode()
        {
            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetIdcCodeT>(
                typeof(AssetIdcCodeT),
                SystemAssetMappingCache.GetDataMappings("AssetIdcCodeT"),
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_Code_SelectAssetIdcCode"
                );
        }

        #endregion

        #region ASSET_RACK별 리스트 정보를 가져온다.
        /// <summary>
        ///ASSET_RACK_CODE별 리스트 정보를 가져온다.
        /// </summary>
        /// 작성자 pingoli05
        /// 날자 : 2013-11-20
        /// <returns></returns>
        
        public List<AssetRackCodeT> SelectAssetRackCode()
        {
            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetRackCodeT>(
                    typeof(AssetRackCodeT),
                    SystemAssetMappingCache.GetDataMappings("AssetRackCodeT"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Code_SelectAssetRackCode"
                );
        }
        #endregion

		#region IDC별 RACK 리스트 정보를 가져온다.
		
		/// <summary>
		/// IDC별 RACK 리스트 정보를 가져온다.
		/// </summary>
		/// <param name="IdcCode"></param>
		/// <returns></returns>
		public List<AssetRackCodeT> SelectAssetRackListByIdcCode(string IdcCode)
		{
			return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetRackCodeT>(
					typeof(AssetRackCodeT),
					SystemAssetMappingCache.GetDataMappings("AssetRackCodeT"),
					CommandType.StoredProcedure,
					"dbo.UPAR_ASSETDB_Code_SelectAssetRackListByIdcCode",
					SqlParameterHelper.CreateParameter("@IDC_CODE", IdcCode, SqlDbType.VarChar)
				);
		}
		#endregion

        #region ASSET_SERVER_VENDER 리스트 정보를 가져온다/
        /// <summary>
        /// asset_Server_Vender 리스트 정보를 가져온다.
        /// </summary>
        /// 작성자 pingoli05
        /// 날자 2013-11-20
        /// <returns></returns>
        public List<AssetServerVenderCodeT> SelectAssetServerVenderCode()
        {

            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetServerVenderCodeT>(
                    typeof(AssetServerVenderCodeT),
                    SystemAssetMappingCache.GetDataMappings("AssetServerVenderCodeT"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Code_SelectAssetServerVenderCode"
                    );
        }
        #endregion

        #region ASSET_NETWORK_VENDER 리스트 정보를 가져온다/
        /// <summary>
        /// asset_network_Vender 리스트 정보를 가져온다.
        /// </summary>
        /// 작성자 pingoli05
        /// 날자 2013-11-20
        /// <returns></returns>
        public List<AssetNetworkVenderCodeT> SelectAssetNetworkVenderCode()
        {

            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetNetworkVenderCodeT>(
                    typeof(AssetNetworkVenderCodeT),
                    SystemAssetMappingCache.GetDataMappings("AssetNetworkVenderCodeT"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Code_SelectAssetNetworkVenderCode"
                    );
        }
        #endregion

        #region ASSET_STORAGE_VENDER 리스트 정보를 가져온다/
        /// <summary>
        /// asset_storage_Vender 리스트 정보를 가져온다.
        /// </summary>
        /// 작성자 pingoli05
        /// 날자 2013-11-20
        /// <returns></returns>
        public List<AssetStorageVenderCodeT> SelectAssetStorageVenderCode()
        {

            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetStorageVenderCodeT>(
                    typeof(AssetStorageVenderCodeT),
                    SystemAssetMappingCache.GetDataMappings("AssetStorageVenderCodeT"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Code_SelectAssetStorageVenderCode"
                    );
        }
        #endregion

        #region ASSET_ETC_VENDER 리스트 정보를 가져온다/
        /// <summary>
        /// asset_etc_Vender 리스트 정보를 가져온다.
        /// </summary>
        /// 작성자 pingoli05
        /// 날자 2013-11-20
        /// <returns></returns>
        public List<AssetEtcVenderCodeT> SelectAssetEtcVenderCode()
        {

            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetEtcVenderCodeT>(
                    typeof(AssetEtcVenderCodeT),
                    SystemAssetMappingCache.GetDataMappings("AssetEtcVenderCodeT"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Code_SelectAssetEtCVenderCode"
                    );
        }
        #endregion

        #region ASSET_IP_TYPE 리스트 정보를 가져온다.
        /// <summary>
        /// ASSET_IP_TYPE 리스트 정보를 가져온다.
        /// </summary>
        /// 작성자 김두성
        /// 날자 2013-11-26
        /// <returns></returns>
        public List<AssetIpTypeT> SelectIpTypeList()
        {

            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetIpTypeT>(
                    typeof(AssetIpTypeT),
                    SystemAssetMappingCache.GetDataMappings("AssetIpTypeT"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Code_SelectIpTypeList"
                    );
        }
        #endregion
    }
}
