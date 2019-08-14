using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;


public partial class RECEPTION_CreditReport : System.Web.UI.Page
{
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
        using (SqlCommand cmd = new SqlCommand("RECP_REPORT_CREDIT", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STAFF";
            cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT EMPID,Sname FROM tblStaff", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            //dropbedno.SelectedIndex = 0;
            dropemp.DataSource = dt1;
            dropemp.DataTextField = "Sname";
            dropemp.DataValueField = "EMPID";
            dropemp.DataBind();
        }
        //dropemp.Items.Insert(0, "Please Select");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_REPORT_CREDIT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_3EMPID";
                cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = dropemp.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("Select * from CREDIT_TABLE where EMPID='" + dropemp.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {

                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    GridView1.SelectedIndex = 0;
                    GridView1.DataSource = dt;
                    GridView1.DataKeyNames = new string[] { "ID" };
                    GridView1.DataBind();
                }
                con.Close();

                if (dt.Rows.Count == 0)
                {
                    Button1.Visible = false;
                }
                else
                {
                    Button1.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {

        if (GridView1.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "CreditReport.xls"));
            Response.ContentType = "application/ms-excel";

            //Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_REPORT_CREDIT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_3EMPID";
                cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = dropemp.Text;
                SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlDataAdapter Adp = new SqlDataAdapter("Select * from CREDIT_TABLE where EMPID='" + dropemp.Text + "'", con);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                GridView1.DataSource = Dt;
                GridView1.DataBind();
            }
            con.Close();

            //Change the Header Row back to white color
            GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
            {
                GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_REPORT_CREDIT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_3EMPID";
                cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = dropemp.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("Select * from CREDIT_TABLE where EMPID='" + dropemp.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}