using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SystemAsset.WebUI.Models
{
	public class ProcStatusModel
	{
		#region 방법1

		//public enum ProcStatus : int
		//{
		//    선택하세요 = 0,
		//    예정 = 1,
		//    진행 = 2,
		//    완료 = 3,
		//    긴급 = 4,
		//    취소 = 5
		//}

		//Dictionary<string, string> list = new Dictionary<string, string>();

		//foreach (int item in Enum.GetValues(typeof(ProcStatusModel.ProcStatus)))
		//{
		//    list.Add(Enum.GetName(typeof(ProcStatusModel.ProcStatus), item), Convert.ToString(item));
		//}

		//Controller
		//ViewBag.ProcStatus = new SelectList(list, "Value", "Key", 2);

		//View
		//@Html.DropDownList("ProcStatus")

		#endregion

		#region 방법2
		public class ProcStatus
		{
			public int Value { get; set; }
			public string Text { get; set; }
		}
		public List<ProcStatus> GetProcStatusList()
		{
			return new List<ProcStatus>(){
				new ProcStatus(){ Value = 0, Text = "선택하세요"},
				new ProcStatus(){ Value = 1, Text = "예정" },
				new ProcStatus(){ Value = 2, Text = "진행" },
				new ProcStatus(){ Value = 3, Text = "완료" },
				new ProcStatus(){ Value = 4, Text = "긴급" },
				new ProcStatus(){ Value = 5, Text = "취소" }
			};
		}
		//Controller
		//ViewBag.ProcStatus = new SelectList(new ProcStatusModel().ProcStatusList(), "Value", "Text", 2);

		//View
		//@Html.DropDownList("ProcStatus")


		#endregion
	}
}