using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

public partial class ACCOUNTS_account_daily_discharge : System.Web.UI.Page
{

    DataMathods OBJ_METHOD = new DataMathods();
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;

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
            datacorp();
        }
    }

    public void datacorp()
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



            DataSet Dt = OBJ_METHOD.Get_DataSet("select ID,CNAME from Corporate_Table where ISACTIVE=1", false, false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                dropinsurance.DataSource = Dt;
                dropinsurance.DataTextField = "CNAME";
                dropinsurance.DataValueField = "ID";
                dropinsurance.DataBind();
                dropinsurance.Items.Insert(0, new ListItem("Please Select", "0"));
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }


    }
    protected void Show_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropinsurance.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropinsurance.Focus();
                return;
            }
            else if (txtfrom.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfrom.Focus();
                return;
            }
            else if (txtto.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtto.Focus();
                return;
            }
            #region oldcode
            //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
            //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
            //SqlDataAdapter da = new SqlDataAdapter("select c.CNAME,r.ID,(CONVERT(VARCHAR(10),a.DATE,105))as DateofAddmission,d.PID,(CONVERT(VARCHAR(10),d.DATE,105)) as Dateofdecharge,a.RGFEE from DISCHARGE_TABLE d join ADMISSION_TABLE a on d.PID=a.VN   join REGISTRATION_TBL r on r.ID=a.ID join Corporate_Table c   on r.CORPORATE=c.ID and r.CORPORATE='" + dropinsurance.Text + "' AND d.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00' AND a.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
            
            
            //using (SqlCommand cm = new SqlCommand("SP_Decharge_RPT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropinsurance.Text;
            //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
            //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cm);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    if (dt.Rows.Count == 0)
            //    {

            //        string message = "alert(' No Record Found')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    }
            //    else
            //    {

            //        GridView1.SelectedIndex = 0;
            //        GridView1.DataSource = dt;
            //        GridView1.DataBind();
            //    }

            //    if (dt.Rows.Count == 0)
            //    {
            //        Button2.Visible = false;
            //    }
            //    else
            //    {
            //        Button2.Visible = true;
            //    }
            //}
            #endregion


            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
            DataSet DS = OBJ_METHOD.Get_DataSet("SP_Decharge_RPT", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Button2.Visible = true;
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS;
                GridView1.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                Button2.Visible = false;
            }


        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        #region oldcode
        //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
        //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";

        //SqlDataAdapter da = new SqlDataAdapter("select d.ID, c.CNAME,r.ID,(CONVERT(VARCHAR(10),a.DATE,105))as DateofAddmission,d.PID,(CONVERT(VARCHAR(10),d.DATE,105)) as Dateofdecharge,a.RGFEE from DISCHARGE_TABLE d join ADMISSION_TABLE aon d.PID=a.VN   join REGISTRATION_TBL r on r.ID=a.ID join Corporate_Table c   on r.CORPORATE=c.ID and r.CORPORATE='" + dropinsurance.Text + "' AND DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
        //SqlDataAdapter da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(10),DATE,105) AS DATE,USERID,CAMOUNT FROM USERCOLLECTION WHERE USERID='" + lblid.Text + "' and DATE BETWEEN'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd HH:mm") + "' AND '" + Convert.ToDateTime(ToDate).ToString("yyyy-MM-dd HH:mm") + "'", con);

        //using (SqlCommand cm = new SqlCommand("SP_Decharge_RPT", con))
        //{
        //    cm.CommandType = CommandType.StoredProcedure;
        //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
        //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropinsurance.Text;
        //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
        //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
        //    SqlDataAdapter da = new SqlDataAdapter(cm);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    GridView1.DataSource = dt;
        //    GridView1.PageIndex = e.NewPageIndex;
        //    GridView1.DataKeyNames = new string[] { "ID" };
        //    GridView1.DataBind();


        //}

        #endregion

        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
            DataSet DS = OBJ_METHOD.Get_DataSet("SP_Decharge_RPT", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
      
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DailyDischarge_RPT.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;

            #region old code
            //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
            //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
            //SqlDataAdapter da = new SqlDataAdapter("select c.CNAME,r.ID,(CONVERT(VARCHAR(10),a.DATE,105))as DateofAddmission,d.PID,(CONVERT(VARCHAR(10),d.DATE,105)) as Dateofdecharge,a.RGFEE from DISCHARGE_TABLE d join ADMISSION_TABLE a on d.PID=a.VN   join REGISTRATION_TBL r on r.ID=a.ID join Corporate_Table c   on r.CORPORATE=c.ID and r.CORPORATE='" + dropinsurance.Text + "' AND d.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00' AND a.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);


            //using (SqlCommand cm = new SqlCommand("SP_Decharge_RPT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropinsurance.Text;
            //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
            //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cm);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView1.DataSource = dt;
            //    GridView1.DataBind();
            //}
            #endregion

            try
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                DataSet DS = OBJ_METHOD.Get_DataSet("SP_Decharge_RPT", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = DS;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
           

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
    int total = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RGFEE"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lblamount = (Label)e.Row.FindControl("lblTotal");
            lblamount.Text = total.ToString();

            //GridView1.FooterRow.Cells[1].Text = "Total";
        }
    }
}