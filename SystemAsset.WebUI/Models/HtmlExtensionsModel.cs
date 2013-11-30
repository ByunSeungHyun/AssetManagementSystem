using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemAsset.WebUI.Models
{
	public static class HtmlExtensionsModel
	{

		public static DateTime AddYears(this DateTime? dt, int year)
		{
			if (dt != null && dt.Value != DateTime.MinValue && dt.Value != DateTime.MaxValue)
				return dt.Value.AddYears(year);
			else
				return DateTime.Now;
		}

		public static string ToFormatString(this DateTime? dt, string format)
		{
			if (dt != null && dt.Value != DateTime.MinValue && dt.Value != DateTime.MaxValue)
				return dt.Value.ToString(format);
			else
				return string.Empty;
		}
	}
}