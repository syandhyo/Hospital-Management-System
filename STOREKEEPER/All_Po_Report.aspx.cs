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

public partial class STOREKEEPER_All_Po_Report : System.Web.UI.Page
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["PONO"] = gr.Cells[1].Text;
        //var UB = gr.Cells[1].Text;
        //string name = Session["IPDNO"].Text.Trim();
        //Session["name"] = name;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text == "")
        {
            string message = "alert('Please!! Select Both Dates..')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        else if (TextBox2.Text == "")
        {
            string message = "alert('Please!! Select Both Dates..')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("STORE_PO_REPORT", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DATE1", SqlDbType.DateTime).Value = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@DATE2", SqlDbType.DateTime).Value = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter da = new SqlDataAdapter("select CONVERT(VARCHAR(10),A.DATEOFISSUE,103)AS DATE,A.PONO AS PONO,C.NAME1 AS NAME from PO_TABLE A,PO_ITEM_TABLE B,VENDER_MASTER_TABLE C WHERE A.PONO=B.ID AND C.ID=A.VENDOR AND A.DATEOFISSUE BETWEEN '" + Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd") + "' order by A.DATEOFISSUE", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            // grSearchPatient.SelectedIndex = 0;
            grSearchPatient.DataSource = dt;
            grSearchPatient.DataBind();
        }
        con.Close();
    }
}