using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;

public partial class PHARMACYSTORE_pharmacy_find_item : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    DataMathods OBJ_METHOD = new DataMathods();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        if (Session["out"] == "INACTIVE")
        {
            Response.Redirect("~/index.aspx");
        }
        Response.Buffer = true;

        Response.CacheControl = "no-cache";
        if (Session["NAME"] == null)
        {
            Response.Redirect("~/index.aspx");
        }
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            binddata();
        }
        con.Close();
    }
    public void binddata()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("Pharmcy_finditem", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                DropDownList1.DataSource = Ds;
                DropDownList1.DataTextField = "NAME";
                DropDownList1.DataValueField = "NAME";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            #region old code
            //using (SqlCommand cmd = new SqlCommand("Pharmcy_finditem", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    DataTable ds1 = new DataTable();
            //    adp.Fill(ds1);
            //    DropDownList1.DataSource = ds1;
            //    DropDownList1.DataTextField = "NAME";
            //    DropDownList1.DataValueField = "NAME";
            //    DropDownList1.DataBind();
            //    DropDownList1.Items.Insert(0,new ListItem("Please Select","0"));
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    public void clearfield()
    {
        
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('Please!! Choose The Item Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SHOWID");
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, DropDownList1.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("Pharmcy_finditem", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;
                GridView1.DataBind();
                show.Visible = true;
            }
            else
            {
                clearfield();
                string message = "alert('No Record found.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
}