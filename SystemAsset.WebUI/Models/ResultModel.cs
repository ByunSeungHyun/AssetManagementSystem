using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemAsset.WebUI.Models
{
	public class ResultModel
	{
		/// <summary>
		/// 결과코드
		/// </summary>
		public StatusCodeEnum statusCode { get; set; }

		/// <summary>
		/// 결과값
		/// </summary>
		public string message { get; set; }
	}

	public enum StatusCodeEnum
	{ 
		Success = 0,
		Fail = 1
	}
}