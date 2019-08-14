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

public partial class ACCOUNTS_MonthlyReportForReferal : System.Web.UI.Page
{
    SqlDataReader dr;
    DataSet ds = new DataSet();
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

        using (SqlCommand com = new SqlCommand("USP_empbroker", con))
        {
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //dropbedno.SelectedIndex = 0;
            droprefname.DataSource = dt;
            droprefname.DataTextField = "NAME";
            droprefname.DataValueField = "ID";
            droprefname.DataBind();
            //droprefname.Items.Insert(0, "NONE");
        }

        con.Close();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            using (SqlCommand com = new SqlCommand("USP_Referal", con))
            {
                if (CheckBox1.Checked)
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INS";
                    com.Parameters.Add("@ID", SqlDbType.VarChar).Value = droprefname.SelectedValue;
                    com.Parameters.Add("@FromDate", SqlDbType.Date).Value = Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd");
                    com.Parameters.Add("@ToDate", SqlDbType.Date).Value = Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd");
                }
                else
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPS";
                    com.Parameters.Add("@ID", SqlDbType.VarChar).Value = droprefname.SelectedValue;
                    com.Parameters.Add("@FromDate", SqlDbType.Date).Value = Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd");
                    com.Parameters.Add("@ToDate", SqlDbType.Date).Value = Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd");
                }


                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    btn_Download.Visible = false;
                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    //Session["mode"] = "true";

                    btn_Download.Visible = true;

                    grd_referal.DataSource = dt;
                    grd_referal.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btn_Download_Click(object sender, EventArgs e)
    {
        if (grd_referal.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ReferalReport.xls"));
            Response.ContentType = "application/ms-excel";

            //Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grd_referal.AllowPaging = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM   where Date between '" + txtfdate.Text + "' and '" + txttdate.Text + "' ", con);

            DataTable Dt = new DataTable();
            Adp.Fill(ds);
            grd_referal.DataSource = ds.Tables[0];
            grd_referal.DataBind();

            con.Close();

            grd_referal.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < grd_referal.HeaderRow.Cells.Count; i++)
            {
                grd_referal.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            grd_referal.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            droprefname.Visible = true;
        }
        else
        {
            droprefname.Visible = false;
        }
    }
}