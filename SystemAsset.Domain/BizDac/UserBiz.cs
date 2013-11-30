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
    public class UserBiz : DacBase
    {
        #region 서버 리스트 정보를 가져온다.
        public List<UserT> GetUserList(string searchType, string searchText, int pageNo, int pageSize)
        {
            return new UserDac().SelectUserList(searchType, searchText, pageNo, pageSize);
        }


        public int CountUserList(string searchType, string searchText)
        {
            return new UserDac().CountUserList(searchType, searchText);
        }

        #endregion
    }
}