using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace B2B
{
	public partial class SiteMaster : MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//CultureInfo ci = new CultureInfo("ja-JP");
			//DateTime dt = DateTime.Now;
			//lblUtcDate.Text = dt.ToString("D", ci);
			lblUtcDate.Text = "UTC: " + DateTime.UtcNow.ToLongTimeString() + " " + DateTime.UtcNow.ToLongDateString();
			lblLocalDate.Text = "Local: " + DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
		}
	}
}