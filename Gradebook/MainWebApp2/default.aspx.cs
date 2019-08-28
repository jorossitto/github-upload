using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainWebApp2
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            OutputLabel.Text = "Clicked me";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OutputLabel.Text = DropDownList1.SelectedIndex.ToString();
        }
    }
}