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
	public class CodeBiz : BizBase
	{
		#region ASSET_TYPE별 공통 코드 리스트 정보를 가져온다.

		/// <summary>
		/// ASSET_TYPE별 공통 코드 리스트 정보를 가져온다.
		/// </summary>
		/// <param name="type">ASSET_TYPE</param>
		/// <returns></returns>
		public List<AssetCommonCodeT> GetCommonCodeByAssetType(string asset_group_type , string type)
		{
			return new CodeDac().SelectCommonCodeByAssetType(asset_group_type,type);
		}

		#endregion

        #region ASSET_IDC_CODE별리스트 가져온다.
        /// <summary>
        /// Asset_idc_code별 리스트를 가져온다.
        /// </summary>
        /// <returns></returns>
        public List<AssetIdcCodeT> GetAssetIdcCode()
        {
            return new CodeDac().SelectAssetIdcCode();
        }
        #endregion

		#region IDC별 RACK 리스트 정보를 가져온다.
		
		/// <summary>
		/// IDC별 RACK 리스트 정보를 가져온다.
		/// </summary>
		/// <param name="IdcCode"></param>
		/// <returns></returns>
		public List<AssetRackCodeT> GetAssetRackListByIdcCode(string IdcCode)
		{
			return new CodeDac().SelectAssetRackListByIdcCode(IdcCode);
		}
		#endregion

		#region ASSET_RACK_CODE별리스트를 가져온다.
		/// <summary>
        /// Asset_Rack_Code별 리스트를 가져온다.
        /// </summary>
        /// <returns></returns>
        public List<AssetRackCodeT> GetAssetRackCode()
        {
            return new CodeDac().SelectAssetRackCode();
        }
        #endregion

        #region ASSET_SERVER_VENDER_CODE별 리스트를 가져온다.
        /// <summary>
        /// ASSET_SERVER_VENDER 정보를 가져온다.
        /// </summary>
        /// <returns>Asset_server_vender_code</returns>
        public List<AssetServerVenderCodeT> GetAssetServerVenderCode()
        {
            return new CodeDac().SelectAssetServerVenderCode();
        }
        #endregion

        #region ASSSET_NETWORK_VENDER별 리스트를 가져온다.
        /// <summary>
        /// ASSSET_NETWORK_VENDER 정보를 가져온다.
        /// </summary>
        /// <returns></returns>
        public List<AssetNetworkVenderCodeT> GetAssetNetworkVenderCode()
        {
            return new CodeDac().SelectAssetNetworkVenderCode();
        }
        #endregion

        #region ASSET_STORAGE_VENDER별 리스트를 가져온다.
        /// <summary>
        /// ASSET_STORAGE_VENDER 정보를 가져온다.
        /// </summary>
        /// <returns></returns>
        public List<AssetStorageVenderCodeT> GetAssetStorageVenderCode()
        {
            return new CodeDac().SelectAssetStorageVenderCode();
        }
        #endregion

        #region ASSET_ETC_VENDER별 리스트를 가져온다.
        /// <summary>
        /// ASSET_ETC_VENDER 정보를 가져온다.
        /// </summary>
        /// <returns></returns>
        public List<AssetEtcVenderCodeT> GetAssetEtcVenderCode()
        {
            return new CodeDac().SelectAssetEtcVenderCode();
        }
        #endregion

        #region ASSET_IP_TYPE 리스트 정보를 가져온다.
        /// <summary>
        /// ASSET_IP_TYPE 리스트 정보를 가져온다.
        /// </summary>
        /// 작성자 김두성
        /// 날자 2013-11-26
        /// <returns></returns>
        public List<AssetIpTypeT> GetAssetSelectIpTypeList()
        {
            return new CodeDac().SelectIpTypeList();
        }
        #endregion
    }
}
