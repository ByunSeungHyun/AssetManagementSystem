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
    public class UserDac : DacBase
    {
        [Cache(IsRefreshNeeded = true, CachedObjectName = "User")]
        public void InsertUser(UserT user)
        {
            new DacHelper(DbHelper, "intradb_write").Execute(
                CommandType.StoredProcedure,
                 "dbo.UPAR_ASSETDB_User_Insert",
                SqlParameterHelper.CreateParameter("@LOGIN_ID", user.LoginId, SqlDbType.VarChar),
                 SqlParameterHelper.CreateParameter("@INS_OPRT", user.InsertUser, SqlDbType.VarChar));
        }

        //[Cache(IsRefreshNeeded = true, CachedObjectName = "ProjectResources")]
        //public void UpdateHRMSProjectResource(HRMSProjectResourceT resource)
        //{
        //    new DacHelper(DbHelper, "HRMS").Execute(
        //        CommandType.StoredProcedure,
        //         "dbo.UPAR_HRMS_PROJECTRESOURCE_UpdateHRMSProjectResource",
        //        SqlParameterHelper.CreateParameter("@START_DATE", resource.StartDate, SqlDbType.DateTime),
        //         SqlParameterHelper.CreateParameter("@END_DATE", resource.EndDate, SqlDbType.DateTime),
        //         SqlParameterHelper.CreateParameter("@DESC", resource.Description, SqlDbType.VarChar),
        //         SqlParameterHelper.CreateParameter("@PROJECT_RESOURCE_ID", resource.ID, SqlDbType.Int),
        //         SqlParameterHelper.CreateParameter("@PL_YN", DataUtil.BoolToYN(resource.IsPL), SqlDbType.Char),
        //         SqlParameterHelper.CreateParameter("@DD_YN", DataUtil.BoolToYN(resource.IsDD), SqlDbType.Char),
        //         SqlParameterHelper.CreateParameter("@BUSE_CODE", resource.BuseCode, SqlDbType.Char),
        //        //SqlParameterHelper.CreateParameter("@BUSE_NAME", resource.BuseName, SqlDbType.VarChar),
        //         SqlParameterHelper.CreateParameter("@USER_ID", resource.UserID, SqlDbType.VarChar),
        //         SqlParameterHelper.CreateParameter("@RESOURCE", resource.MD, SqlDbType.Float),
        //         SqlParameterHelper.CreateParameter("@PHASE", resource.Phase, SqlDbType.Int)
        //        //SqlParameterHelper.CreateParameter("@UPD_USER_ID", userID, SqlDbType.VarChar)

        //        //SqlParameterHelper.CreateParameter("@USER_NAME", resource.UserName, SqlDbType.VarChar)
        //         );
        //}

        public UserT SelectUserByLoginId(string loginID)
        {
            return (UserT)new DacHelper(DbHelper, "intradb_read").SelectSingleEntity(
                typeof(UserT),
                SystemAssetMappingCache.GetDataMappings("UserT"),
                CommandType.StoredProcedure,
                "dbo.UPAR_ASSETDB_User_SelectByLoginId",
                SqlParameterHelper.CreateParameter("@LOGIN_ID", loginID, SqlDbType.VarChar)
                );
        }

        #region etc리스트 정보를 가져온다

        public List<UserT> SelectUserList(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new DacHelper(DbHelper, "intradb_read").SelectMultipleEntities<UserT>(
                    typeof(UserT),
                    SystemAssetMappingCache.GetDataMappings("UserT"),
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_User_SelectList"
                    );
        }

        public int CountUserList(string searchType, string searchText)
        {
            var obj = new DacHelper(DbHelper, "intradb_read").SelectScalar(
                    CommandType.StoredProcedure,
                    "dbo.UPAR_ASSETDB_Asset_CountAssetUserList"
                    );

            if (obj != null)
                //return (int)obj;
                return Convert.ToInt32(obj);
            else
                return 0;
        }

        #endregion
    }
}
