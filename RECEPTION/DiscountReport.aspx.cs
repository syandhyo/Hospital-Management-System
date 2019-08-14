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

public partial class RECEPTION_DiscountReport : System.Web.UI.Page
{
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtfdate.Text == "")
            {
                string message = "alert('*Please!!Enter Both dates')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txttdate.Text == "")
            {
                string message = "alert('*Please!!Enter Both Dates..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            using (SqlCommand cmd = new SqlCommand("RECP_REPORT_DISCOUNT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                cmd.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@LDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,DATE,PID,NAME,BEDNO,TOTALAMT,BALANCEAMT,AMOUNT FROM DISCOUNT_TABLE where DATE>='" + Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd") + "' and DATE<='" + Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd") + "'", con);
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DiscountReport.xls"));
            Response.ContentType = "application/ms-excel";

            //Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_REPORT_DISCOUNT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                cmd.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@LDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd");
                SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlDataAdapter Adp = new SqlDataAdapter("SELECT ID,DATE,PID,NAME,BEDNO,TOTALAMT,BALANCEAMT,AMOUNT FROM DISCOUNT_TABLE where DATE>='" + Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd") + "' and DATE<='" + Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd") + "'", con);
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
            using (SqlCommand cmd = new SqlCommand("RECP_REPORT_DISCOUNT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT";
                cmd.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@LDATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,DATE,PID,NAME,BEDNO,TOTALAMT,BALANCEAMT,AMOUNT FROM DISCOUNT_TABLE where DATE>='" + Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd") + "' and DATE<='" + Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd") + "'", con);
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