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
    public class IpBiz : BizBase
    {
        #region ASSET_IP_INFO 리스트 정보를 가져온다.
        /// <summary>
        /// ASSET_IP_INFO 리스트 정보를 가져온다.
        /// </summary>
        /// 작성자 김두성
        /// 날자 2013-11-28
        /// <returns></returns>
        public List<AssetIpInfoEX1T> GetAssetSelectIpInfoList(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new IpDac().SelectIpList(searchType, searchText, pageNo, pageSize);
        }
        #endregion

        #region IP Info 카운트.

        /// <summary>
        /// IP Info 리스트 총 카운트를 가져온다.
        /// 2013-11-28  김두성
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int CountIpList(string searchType, string searchText)
        {
            return new IpDac().CountIpList(searchType, searchText);
        }

        #endregion
    }
}
