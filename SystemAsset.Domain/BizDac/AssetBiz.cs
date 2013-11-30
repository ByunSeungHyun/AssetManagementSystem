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
	public class AssetBiz : DacBase
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
		public List<AssetServerInfoEx1T> GetServerList(string searchType, string searchText, int pageNo, int pageSize)
		{
			return new AssetDac().SelectServerList(searchType, searchText, pageNo, pageSize);
		}

		/// <summary>
		/// Asset 서버 리스트 총 카운트를 가져온다.
		/// 2013-11-22  문태중
		/// </summary>
		/// <param name="searchType"></param>
		/// <param name="searchText"></param>
		/// <returns></returns>
		public int CountServerList(string searchType, string searchText)
		{
			return new AssetDac().CountServerList(searchType, searchText);
		}

		#endregion

        #region Asset 서버 정보 조회

        /// <summary>
		/// Asset 서버 정보를 조회한다.
		/// 2013-11-25 문태중
		/// </summary>
		/// <param name="Seq"></param>
		/// <returns></returns>
		public AssetServerInfoEx1T GetServerInfo(Int64 Seq)
		{
			return new AssetDac().SelectServerInfo(Seq);
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
			return new AssetDac().UpdateServerInfo(model);
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
			new AssetDac().DeleteServerInfo(seq, delOprt);
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
			new AssetDac().DeleteServerList(seqList, delOprt);
		}

		#endregion

        /******************************** 네트워크 *************************************************/

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
        public List<AssetNetworkInfoEx1T> GetNetworkList(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new AssetDac().SelectNetworkList(searchType, searchText, pageNo, pageSize);
        }

        public List<AssetNetworkInfoEx2T> GetNetworkList2(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new AssetDac().SelectNetworkList2(searchType, searchText, pageNo, pageSize);
        }

        /// <summary>
        /// Asset 네트워크 리스트 총 카운트를 가져온다.
        /// 2013-11-27  김두성
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int CountNetworkList(string searchType, string searchText)
        {
            return new AssetDac().CountNetworkList(searchType, searchText);
        }

        #endregion

        #region Asset 네트워크 정보 조회

        /// <summary>
        /// Network 서버 정보를 조회한다.
        /// 2013-11-27 김두성
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public AssetNetworkInfoEx1T GetNetworkInfo(Int64 Seq)
        {
            return new AssetDac().SelectNetworkInfo(Seq);
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
            return new AssetDac().UpdateNetworkInfo(model);
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
            new AssetDac().DeleteNetworkInfo(seq, delOprt);
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
            new AssetDac().DeleteNetworkList(seqList, delOprt);
        }

        #endregion

        /***********************************Storage**************************************************/
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
        public List<AssetStorageInfoEx1T> GetStorageList(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new AssetDac().SelectStorageList(searchType, searchText, pageNo, pageSize);
        }

        /// <summary>
        /// Asset 서버 리스트 총 카운트를 가져온다.
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int CountStorageList(string searchType, string searchText)
        {
            return new AssetDac().CountStorageList(searchType, searchText);
        }

        #endregion

        #region Asset Storage 정보 조회

        /// <summary>
        /// Asset Storage 정보를 조회한다.
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public AssetStorageInfoEx1T GetStorageInfo(Int64 Seq)
        {
            return new AssetDac().SelectStorageInfo(Seq);
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
            return new AssetDac().UpdateStorageInfo(model);
        }

        #endregion

        #region Asset Storage 정보 삭제

        /// <summary>
        /// Asset Storage 정보 삭제
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="delOprt"></param>
        public void DeleteStorageInfo(Int64 seq, string delOprt)
        {
            new AssetDac().DeleteStorageInfo(seq, delOprt);
        }

        #endregion

        #region Asset Storage 정보 삭제 리스트

        /// <summary>
        /// Asset Storage 정보 삭제 리스트
        /// 2013-11-27 변승현
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="delOprt"></param>
        public void DeleteStorageList(string seqList, string delOprt)
        {
            new AssetDac().DeleteStorageList(seqList, delOprt);
        }

        #endregion

        /******************************************ETC**************************/
        #region etc 정보 리스트
        public List<AssetEtcInfoEx1T> GetEtcList(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new AssetDac().SelectEtcList(searchType, searchText, pageNo, pageSize);
        }

        public int CountEtcList(string searchType, string searchText)
        {
            return new AssetDac().CountEtcList(searchType, searchText);
        }


        #endregion

        #region Asset etc 정보 조회


        public AssetEtcInfoEx1T GetEtcInfo(Int64 Seq)
        {
            return new AssetDac().SelectEtcInfo(Seq);
        }

        #endregion

        #region Asset 기타 정보 업데이트

        /// <summary>
        /// Asset 서버 정보 추가 및 업데이트
        /// 2013-11-25 문태중
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateEtcInfo(AssetEtcInfoT model)
        {
            return new AssetDac().UpdateEtcInfo(model);
        }

        #endregion

        #region Asset etc 정보 삭제


        public void DeleteEtcInfo(Int64 seq, string delOprt)
        {
            new AssetDac().DeleteEtcInfo(seq, delOprt);
        }

        #endregion

        #region Asset tec 정보 삭제 리스트


        public void DeleteEtcList(string seqList, string delOprt)
        {
            new AssetDac().DeleteEtcList(seqList, delOprt);
        }

        #endregion
	}
}
