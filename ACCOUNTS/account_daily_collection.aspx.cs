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

public partial class ACCOUNTS_account_daily_collection : System.Web.UI.Page
{
    DataMathods OBJ_METHOD = new DataMathods();

    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;

    string DBOPERATION;

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


            DataSet dt1 = OBJ_METHOD.Get_DataSet("select Distinct u.UserId, l.NAME from LOGIN_TABLE l join USER_COLLECTION U on l.ID=U.UserId", false,false);
            if (dt1.Tables[0].Rows.Count > 0)
            {
                drpuser.DataSource = dt1;
                drpuser.DataTextField = "NAME";
                drpuser.DataValueField = "UserId";
                drpuser.DataBind();
                drpuser.Items.Insert(0, new ListItem("Please Select", "0"));
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
            if (drpuser.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                drpuser.Focus();
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
            Session["DBOPERATION"] = "DISPLAY";
            DBOPERATION = Session["DBOPERATION"].ToString();
          
            
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, drpuser.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_UserDailyCollect_RPT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                Button2.Visible = true;
                GridView1.DataSource = DS1;
                GridView1.DataBind();
            }
            else
            {
                Button2.Visible = false;
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Dailycollection.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
           
            try
            {
                DBOPERATION = Session["DBOPERATION"].ToString();
                //using (SqlCommand cm = new SqlCommand("SP_UserDailyCollect_RPT", con))
                //{
                //    cm.CommandType = CommandType.StoredProcedure;
                //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
                //    cm.Parameters.Add("@UserId", SqlDbType.VarChar).Value = drpuser.Text;
                //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
                //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;

                //    SqlDataAdapter da = new SqlDataAdapter(cm);
                //    // SqlDataAdapter da = new SqlDataAdapter("select U.UserID,l.NAME,CONVERT(VARCHAR(10),U.Date,105) AS DATE,U.Collection FROM USER_COLLECTION U join LOGIN_TABLE l on l.ID=U.UserId where  U.UserId='" + drpuser.Text + "' and U.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
                //    //sqlDataAdapter da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(10),DATE,105) AS DATE,USERID,CAMOUNT FROM USERCOLLECTION WHERE USERID='" + lblid.Text + "' and DATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "'", con);
                //    DataTable dt = new DataTable();
                //    da.Fill(ds);
                //    GridView1.DataSource = ds.Tables[0];
                //    GridView1.DataBind();
                //}

                SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, drpuser.Text);

                DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_UserDailyCollect_RPT", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    Button2.Visible = true;
                    GridView1.DataSource = DS1;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       try
        {

            DBOPERATION = Session["DBOPERATION"].ToString();
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, drpuser.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_UserDailyCollect_RPT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                Button2.Visible = true;
                GridView1.DataSource = DS1;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpuser.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                drpuser.Focus();
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
            
            Session["DBOPERATION"] = "DISPLAY_ALL";
            DBOPERATION = Session["DBOPERATION"].ToString();

                      
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, drpuser.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_UserDailyCollect_RPT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                Button2.Visible = true;
                GridView1.DataSource = DS1;
                GridView1.DataBind();
            }
            else
            {
                Button2.Visible = true;
            }


        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    int total = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Collection"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblamount = (Label)e.Row.FindControl("lblTotal");
            lblamount.Text = total.ToString();

            //GridView1.FooterRow.Cells[1].Text = "Total";
        }
    }
}