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

public partial class ACCOUNTS_account_ambulance_report : System.Web.UI.Page
{
    string num1 = "SJ000";
    DataMathods OBJ_METHOD = new DataMathods();
    SqlDataAdapter da, da1;
    DataSet ds = new DataSet();
    SqlCommand com, cmd;
    SqlDataReader dr;
    DataTable dt;
    DataRow dtr;

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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            binddata();
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txt_Fdate.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txt_Todate.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_Ambulance_RPT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                btn_Download.Visible = true;
                grd_ambulance.SelectedIndex = 0;
                grd_ambulance.DataSource = DS1;
                grd_ambulance.DataBind();
            }
            else
            {
                btn_Download.Visible = false;
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btn_Download_Click(object sender, EventArgs e)
    {
        if (grd_ambulance.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Ambulance_RPT.xls"));
            Response.ContentType = "application/ms-excel";

            //Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grd_ambulance.AllowPaging = false;
          

          
            //using (SqlCommand cmd = new SqlCommand("SP_Ambulance_RPT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cmd.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txt_Fdate.Text;
            //    cmd.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txt_Todate.Text;
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //    DataTable Dt = new DataTable();
            //    Adp.Fill(ds);
            //    grd_ambulance.DataSource = ds.Tables[0];
            //    grd_ambulance.DataBind();
            //}
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txt_Fdate.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txt_Todate.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_Ambulance_RPT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {

                grd_ambulance.DataSource = DS1;
                grd_ambulance.DataBind();
            }
           
            grd_ambulance.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            for (int i = 0; i < grd_ambulance.HeaderRow.Cells.Count; i++)
            {
                grd_ambulance.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            grd_ambulance.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void grd_ambulance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txt_Fdate.Text);
        SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txt_Todate.Text);

        DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_Ambulance_RPT", false, true, SQL_PARAMS1);
        if (DS1.Tables[0].Rows.Count > 0)
        {

            grd_ambulance.DataSource = DS1;
            grd_ambulance.PageIndex = e.NewPageIndex;
            grd_ambulance.DataBind();
        }
    }
}