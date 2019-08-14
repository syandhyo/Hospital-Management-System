using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class ACCOUNTS_account_op_consultancy_report : System.Web.UI.Page
{
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblid.Text = Session["NAME"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();

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

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

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
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            // Validation
            if (txtfdate.Text == "")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('* Field are mondatary')</script>");
                return;
            }
            else if (txttdate.Text == "")
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('* Field are mondatary')</script>");
                return;
            }
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cmd = new SqlCommand("SP_OPCONSULTANCY_RPT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cmd.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfdate.Text;
            //    cmd.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txttdate.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("SELECT  tblOPConsultancy.id,tblOPConsultancy.OPNo,tblStaff.Sname,tblOPConsultancy.fee,tblOPConsultancy.CDate FROM tblOPConsultancy INNER JOIN tblStaff ON tblOPConsultancy.Staffid = tblStaff.id where tblOPConsultancy.CDate between '" + Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd") + "'", con);
            //    DataTable Dt = new DataTable();
            //    da.Fill(Dt);
            //    GridView1.DataSource = Dt;
            //    GridView1.DataBind();

            //    if (Dt.Rows.Count == 0)
            //    {
            //        Button1.Visible = false;
            //        string message = "alert('* No Record Found !')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;
            //    }
            //    else
            //    {
            //        Button1.Visible = true;
            //    }
            //}
            //con.Close();
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfdate.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txttdate.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_OPCONSULTANCY_RPT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                Button1.Visible = true;
                GridView1.DataSource = DS1;
                GridView1.DataBind();

            }
            else
            {
                Button1.Visible = false;
                string message = "alert('* No Record Found !')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    protected void ExportToExcel(object sender, EventArgs e)
    {

        if (GridView1.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "opconsutancy.xls"));
            Response.ContentType = "application/ms-excel";

            //Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cmd = new SqlCommand("SP_OPCONSULTANCY_RPT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cmd.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfdate.Text;
            //    cmd.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txttdate.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    // SqlDataAdapter Adp = new SqlDataAdapter("SELECT  tblOPConsultancy.id,tblOPConsultancy.OPNo,tblStaff.Sname,tblOPConsultancy.fee,tblOPConsultancy.CDate FROM tblOPConsultancy INNER JOIN tblStaff ON tblOPConsultancy.Staffid = tblStaff.id where CDate between '" + Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd") + "'", con);
            //    DataTable Dt = new DataTable();
            //    da.Fill(ds);
            //    GridView1.DataSource = ds.Tables[0];
            //    GridView1.DataBind();
            //}
            //con.Close();

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfdate.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txttdate.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_OPCONSULTANCY_RPT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1.Tables[0];
                GridView1.DataBind();
            }
            else
            {
              
            }
           
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
            using (SqlCommand cmd = new SqlCommand("SP_OPCONSULTANCY_RPT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
                cmd.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfdate.Text;
                cmd.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txttdate.Text;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT  tblOPConsultancy.id as ID,tblOPConsultancy.OPNo,tblStaff.Sname,tblOPConsultancy.fee,tblOPConsultancy.CDate FROM tblOPConsultancy INNER JOIN tblStaff ON tblOPConsultancy.Staffid = tblStaff.id where tblOPConsultancy.CDate between '" + Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd") + "'", con);
                // DataTable dt = new DataTable();
                //da.Fill(dt);
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
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