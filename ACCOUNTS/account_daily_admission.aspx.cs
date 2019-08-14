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

public partial class ACCOUNTS_account_daily_admission : System.Web.UI.Page
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
            datacorp();
        }
    }


    protected void Show_Click(object sender, EventArgs e)
    {

        if (CheckBox1.Checked == true)
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
                // String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                // SqlDataAdapter da = new SqlDataAdapter("select a.ID,CONVERT(VARCHAR(10),a.DATE,105) as DATE,a.NAME,a.BEDNO,a.RGFEE,c.CNAME FROM ADMISSION_TABLE a join Corporate_Table c on a.CORPORATE=c.ID and  CORPORATE='" + dropinsurance.Text + "' AND DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
                //using (SqlCommand cm = new SqlCommand("SP_Admission_RPT", con))
                //{
                //    cm.CommandType = CommandType.StoredProcedure;
                //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
                //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropinsurance.SelectedValue;
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
                //        GridView1.Columns[4].Visible = true;
                //        GridView1.DataSource = dt;
                //        GridView1.DataBind();

                //        if (dt.Rows.Count == 0)
                //        {
                //            Button2.Visible = false;
                //        }
                //        else
                //        {
                //            Button2.Visible = true;
                //        }
                //    }
                //    //GridView1.Columns[4].Visible = true;
                //    //GridView1.DataSource = dt;
                //    //GridView1.DataBind();
                //}
                #endregion

                Session["DBOPERATION"] = "DISPLAY_CORPORATE";
                DBOPERATION = Session["DBOPERATION"].ToString();

                SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);

                DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_Admission_RPT", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    Button2.Visible = true;
                    GridView1.DataSource = DS1;
                    GridView1.Columns[4].Visible = true;
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
        else
        {

            try
            {
                if (txtfrom.Text == "")
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

                #region old code
                //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                //SqlDataAdapter da = new SqlDataAdapter("select ID,CONVERT(VARCHAR(10),DATE,105) as DATE,NAME,BEDNO,RGFEE FROM ADMISSION_TABLE where DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
              
                //using (SqlCommand cm = new SqlCommand("SP_Admission_RPT", con))
                //{
                //    cm.CommandType = CommandType.StoredProcedure;
                //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
                //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
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
                //        GridView1.Columns[4].Visible = false;
                //        //GridView1.PageIndex = e.NewPageIndex;
                //        GridView1.DataKeyNames = new string[] { "ID" };
                //        GridView1.DataBind();

                //        if (dt.Rows.Count == 0)
                //        {
                //            Button2.Visible = false;
                //        }
                //        else
                //        {
                //            Button2.Visible = true;
                //        }
                //    }
                //}
                #endregion
                
                Session["DBOPERATION"] = "DISPLAY_DATE";
                DBOPERATION = Session["DBOPERATION"].ToString();

               SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
              //  SQL_PARAMS1[3] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);

                DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_Admission_RPT", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    Button2.Visible = true;
                    GridView1.DataSource = DS1;
                    GridView1.Columns[4].Visible = false;
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

            DataSet dt4 = OBJ_METHOD.Get_DataSet("select ID,CNAME from Corporate_Table where ISACTIVE=1", false, false);
            if (dt4.Tables[0].Rows.Count > 0)
            {
                //dropbedno.selectedindex = 0;
                dropinsurance.DataSource = dt4;
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
    protected void CheckBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            datacorp();
            dropinsurance.Visible = true;
        }
        else
        {
            dropinsurance.Visible = false;
            txtfrom.Text = "";
            txtto.Text = "";
            GridView1.Visible = false;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        try
        {
            #region oldcode
            //using (SqlCommand cm = new SqlCommand("SP_Admission_RPT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
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


            DBOPERATION = Session["DBOPERATION"].ToString();
           
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_Admission_RPT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                Button2.Visible = true;
                GridView1.DataSource = DS1;
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
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DailyAdmission.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
           
            try
            {
                if (CheckBox1.Checked == true)
                {
                    #region oldcode
                    //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                    //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                    //SqlDataAdapter da = new SqlDataAdapter("select a.ID,CONVERT(VARCHAR(10),a.DATE,105) as DATE,a.NAME,a.BEDNO,a.RGFEE,c.CNAME FROM ADMISSION_TABLE a join Corporate_Table c on a.CORPORATE=c.ID and  CORPORATE='" + dropinsurance.Text + "' AND DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);


                    //using (SqlCommand cm = new SqlCommand("SP_Admission_RPT", con))
                    //{
                    //    cm.CommandType = CommandType.StoredProcedure;
                    //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
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
                    //        GridView1.Columns[4].Visible = true;
                    //        GridView1.DataSource = dt;
                    //        GridView1.DataBind();

                    //        if (dt.Rows.Count == 0)
                    //        {
                    //            Button2.Visible = false;
                    //        }
                    //        else
                    //        {
                    //            Button2.Visible = true;
                    //        }
                    //    }
                    //}

                    #endregion

                    DBOPERATION = Session["DBOPERATION"].ToString();
                    
                    //---Displaying Corporate ----/////
                    SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

                    SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                    SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                    SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                    SQL_PARAMS1[3] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.Text);

                    DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_Admission_RPT", false, true, SQL_PARAMS1);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        Button2.Visible = true;
                        GridView1.SelectedIndex = 0;
                        GridView1.Columns[4].Visible = true;
                        GridView1.DataSource = DS1;
                        GridView1.DataBind();
                    }
                    else
                    {
                        Button2.Visible = false;
                    }

                }
                else
                {
                    #region oldcode
                    //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                    //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                    //SqlDataAdapter da = new SqlDataAdapter("select ID,CONVERT(VARCHAR(10),DATE,105) as DATE,NAME,BEDNO,RGFEE FROM ADMISSION_TABLE where DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);



                    //using (SqlCommand cm = new SqlCommand("SP_Admission_RPT", con))
                    //{
                    //    cm.CommandType = CommandType.StoredProcedure;
                    //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
                    //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
                    //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
                    //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
                    //    SqlDataAdapter da = new SqlDataAdapter(cm);
                    //    DataTable dt = new DataTable();
                    //    da.Fill(dt);
                    //    GridView1.DataSource = dt;
                    //    GridView1.DataKeyNames = new string[] { "ID" };
                    //    GridView1.DataBind();
                    //}
                    #endregion

                    //--Displaying Date--////
                    DBOPERATION = Session["DBOPERATION"].ToString();

                    SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

                    SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                    SQL_PARAMS1[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                    SQL_PARAMS1[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                   // SQL_PARAMS1[3] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, dropinsurance.Text);

                    DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_Admission_RPT", false, true, SQL_PARAMS1);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        Button2.Visible = true;
                        GridView1.SelectedIndex = 0;
                        GridView1.Columns[4].Visible = false;
                        GridView1.DataSource = DS1;
                        GridView1.DataBind();
                    }
                    else
                    {
                        Button2.Visible = false;
                    }

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