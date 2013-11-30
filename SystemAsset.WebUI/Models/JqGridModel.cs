using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemAsset.WebUI.Models
{
	public class JqGridModel
	{
		public int page { get; set; }
		public int records { get; set; }
		public int total { get; set; }
		public System.Collections.IList rows { get; set; }
	}
}