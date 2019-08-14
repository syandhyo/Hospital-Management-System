using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class STOREKEEPER_store_all_issue_report : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataReader dr;
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            binddata();
        }
        con.Close();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFDAte.Text == "")
            {
                string message = "alert('Please!! Select Both Dates..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtTDate.Text == "")
            {
                string message = "alert('Please!! Select Both Dates..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            // SqlDataAdapter da = new SqlDataAdapter("select a.ID,CONVERT(VARCHAR(10),a.MINDATE,101)AS DATE,c.DeptName from MIN_TABLE a,MINITEM_TABLE b,tblDepartment c where a.ID=b.ID and a.ISSUEDTO=c.id and a.MINDATE BETWEEN '" + Convert.ToDateTime(txtFDAte.Text).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(txtTDate.Text).ToString("yyyy-MM-dd") + "'", con);
            using (SqlCommand cm = new SqlCommand("SP_All_ISSUE_RPT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOW";
                cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtFDAte.Text;
                cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtTDate.Text;
                SqlDataAdapter da = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    // grSearchPatient.SelectedIndex = 0;
                    grdAllMin.DataSource = dt;
                    grdAllMin.DataBind();
                }
            }
            con.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["MINNO"] = gr.Cells[1].Text;

    }
}