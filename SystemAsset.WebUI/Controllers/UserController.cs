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
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            UserDac dac = new UserDac();
            UserT user = new UserT();

            string userLoginID = "testuser_" + new Random().Next(1000000, 9999999);
            user.LoginId = userLoginID;
            user.InsertUser = userLoginID;

            dac.InsertUser(user);

            return View();

        }

        public ActionResult UserList()
        {
            return View();
        }

        #region [POST] user 리스트

        [HttpPost]
        public JsonResult GetUserList(string searchType, string searchText, int? page, int? rows)
        {
            string paramSearchType = string.IsNullOrEmpty(searchType) ? string.Empty : searchType;
            string paramSearchText = string.IsNullOrEmpty(searchText) ? string.Empty : searchText;
            int paramPageNo = page == null ? 1 : page.Value;
            int paramPageSize = rows == null ? 10 : rows.Value;

            UserBiz userBiz = new UserBiz();

            int nTotalCount = userBiz.CountUserList(paramSearchType, paramSearchText);
            int nRecord = nTotalCount <= paramPageSize ? 1 : (int)Math.Ceiling((double)(nTotalCount / paramPageSize));

            Models.JqGridModel model = new Models.JqGridModel
            {
                page = paramPageNo,
                total = nRecord,
                records = nTotalCount,
                rows = userBiz.GetUserList(paramSearchType, paramSearchText, paramPageNo, paramPageSize)
            };

            return Json(model);
        }

        #endregion

        #region user 정보 엑셀 다운로드

        public ActionResult GetUserExcelList(string searchType, string searchText, int? page, int? rows)
        {
            var grid = new System.Web.UI.WebControls.GridView();
            List<UserT> model = new UserBiz().GetUserList(searchType, searchText, page.Value, rows.Value);

            grid.DataSource = from m in model
                              select new
                              {
                                  LoginId = m.LoginId,
                                  LastLoginDate = m.LastLoginDate,
                                  IsDeleted = m.IsDeleted,
                                  DeletedDate = m.DeletedDate,
                                  InsertUser = m.InsertUser,
                                  InsertDate = m.InsertDate,

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

    }
}
