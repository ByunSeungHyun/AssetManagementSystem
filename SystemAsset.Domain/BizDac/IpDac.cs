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
    public class IpDac :DacBase
    {
        #region IP 리스트 정보를 가져온다.

        /// <summary>
        /// IP 리스트 정보를 가져온다.
        /// 2013-11-28 김두성
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<AssetIpInfoEX1T> SelectIpList(string searchType, string searchText, int pageNo, int pageSize)
        {
            List < AssetIpInfoEX1T > list =  new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<AssetIpInfoEX1T>(
                                            typeof(AssetIpInfoEX1T),
                                            SystemAssetMappingCache.GetDataMappings("AssetIpInfoEX1T"),
                                            CommandType.StoredProcedure,
                                            "dbo.UPAR_ASSETDB_Asset_SelectAssetIPList",
                                            SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
                                            SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30),
                                            SqlParameterHelper.CreateParameter("@PAGE_NO", pageNo, SqlDbType.Int),
                                            SqlParameterHelper.CreateParameter("@PAGE_SIZE", pageSize, SqlDbType.Int)
                                            );
            return list;
        }

        /// <summary>
        /// </summary>
        /// IP 리스트 총 카운트를 가져온다.
        /// 2013-11-28 김두성
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int CountIpList(string searchType, string searchText)
        {
            object obj = new DacHelper(DbHelper, "intradb_read").SelectScalar(
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_SelectAssetIPList",
                    SqlParameterHelper.CreateParameter("@SEARCH_TYPE", searchType, SqlDbType.Char, 1),
                    SqlParameterHelper.CreateParameter("@SEARCH_TEXT", searchText, SqlDbType.VarChar, 30)
                    );

            if (obj != null)
                return (int)obj;
            else
                return 0;
        }

        #endregion
    }
}
