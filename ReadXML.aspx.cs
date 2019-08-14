using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class ReadXML : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.BindGrid();
        }
    }

    private void BindGrid()
    {
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(Server.MapPath("~/Collection.xml"));
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
}