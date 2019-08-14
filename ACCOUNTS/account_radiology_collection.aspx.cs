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
using System.Drawing;
using System.Web.UI.HtmlControls;

public partial class ACCOUNTS_account_radiology_collection : System.Web.UI.Page
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
      
    }
    protected void CheckBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            drppatient.Visible = true;

        }
        else
        {
            drppatient.Visible = false;
            txtfrom.Text = "";
            txtto.Text = "";
            GridView1.Visible = false;
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

            DataSet Dt = OBJ_METHOD.Get_DataSet("select ID,CNAME from Corporate_Table where ISACTIVE=1", false,false);
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
    protected void Show_Click(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true && CheckBox1.Checked == false)  //--For Inpatient
        {
            try
            {
                if (drppatient.Text == "In Patient")
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

              
                    try
                    {
                        #region oldcode
                        //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                        //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                        //SqlDataAdapter da = new SqlDataAdapter("select r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r.OPDNO ,r.NAME,r.TOTAMT,c.CNAME from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.VN=r.OPDNO  and r.OPDNO LIKE '%IP%' join Corporate_Table c on a.CORPORATE=c.ID and a.CORPORATE='"+dropinsurance.Text+"' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
                       
                        //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                        //{
                        //    cm.CommandType = CommandType.StoredProcedure;
                        //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate_Inpatient";
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
                        //        DAta_Clear();
                        //    }
                        //    else
                        //    {

                        //        GridView1.SelectedIndex = 0;
                        //        // GridView1.Columns[5].Visible = true;
                        //        GridView1.Columns[1].Visible = true;
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

                        SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate_Inpatient");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                        DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            Button2.Visible = true;
                            GridView1.Columns[1].Visible = true;
                            GridView1.DataSource = DS;
                            GridView1.DataBind();
                        }
                        else
                        {
                            string message = "alert(' No Record Found')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            Button2.Visible = false;
                        }
                        DAta_Clear();
                    }
                    catch (Exception ex)
                    {
                        string message = ex.ToString();
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                }
                else if (drppatient.Text == "Out Patient")
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
                    //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                    //{
                    //    cm.CommandType = CommandType.StoredProcedure;
                    //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate_Outpatient";
                    //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropinsurance.Text;
                    //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
                    //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
                    //    SqlDataAdapter da = new SqlDataAdapter(cm);
                    //    // SqlDataAdapter da = new SqlDataAdapter("select r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r.OPDNO ,r.NAME,r.TOTAMT,c.CNAME from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.ID=r.OPDNO  and r.OPDNO LIKE '%OP%' join Corporate_Table c on a.CORPORATE=c.ID and a.CORPORATE='"+dropinsurance.Text+"' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

                    //    DataTable dt = new DataTable();
                    //    da.Fill(dt);
                    //    if (dt.Rows.Count == 0)
                    //    {

                    //        string message = "alert(' No Record Found')";
                    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    //        DAta_Clear();
                    //    }
                    //    else
                    //    {

                    //        GridView1.SelectedIndex = 0;
                    //        // GridView1.Columns[5].Visible = true;
                    //        GridView1.Columns[1].Visible = true;
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
                    try
                    {
                        SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate_Outpatient");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                        DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            Button2.Visible = true;
                            GridView1.Columns[1].Visible = true;
                            GridView1.DataSource = DS;
                            GridView1.DataBind();
                        }
                        else
                        {
                            string message = "alert(' No Record Found')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            Button2.Visible = false;
                        }
                        DAta_Clear();
                    }

                    catch (Exception ex)
                    {
                        string message = ex.ToString();
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                }

                #region extra code
                //else if (drppatient.Text == "All Patient")
                //{
                //    if (drppatient.Text == "")
                //    {
                //        string message = "alert('* Fields are mandatory.')";
                //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                //        return;
                //    }
                //    else if (dropinsurance.Text == "")
                //    {
                //        string message = "alert('* Fields are mandatory.')";
                //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                //        return;
                //    }
                //    else if (txtfrom.Text == "")
                //    {
                //        string message = "alert('* Fields are mandatory.')";
                //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                //        return;
                //    }
                //    else if (txtto.Text == "")
                //    {
                //        string message = "alert('* Fields are mandatory.')";
                //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                //        return;
                //    }

                //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                //    con.Open();

                //    String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                //    String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                //    SqlDataAdapter da = new SqlDataAdapter("select ID,CONVERT(VARCHAR(10),DATE,105) as DATE,OPDNO ,NAME,TOTAMT from TBL_RADIOLGYREQ   where DATE  BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

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
                //       // GridView1.Columns[5].Visible = false;
                //        GridView1.DataSource = dt;
                //        GridView1.DataBind();

                //        //decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("RGFEE"));
                //        //GridView1.FooterRow.Cells[2].Text = "Total";
                //        //GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                //        //GridView1.FooterRow.Cells[3].Text = total.ToString("N2");

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
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
        else if (CheckBox2.Checked == false && CheckBox1.Checked == true)// for dropinsurance or corporate
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
            //SqlDataAdapter da = new SqlDataAdapter("select C.CNAME,r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r1.ID AS outid,a.VN,r.NAME,r.TOTAMT from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.ID=r.OPDNO join REGISTRATION_TBL r1 on r1.ID=a.ID join Corporate_Table c on a.CORPORATE=c.ID and  a.CORPORATE='"+dropinsurance.Text+"' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

            //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate";
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
            //        DAta_Clear();
            //    }
            //    else
            //    {

            //        GridView1.SelectedIndex = 0;
            //        // GridView1.Columns[2].Visible = false;
            //        GridView1.Columns[2].Visible = true;
            //        GridView1.Columns[3].Visible = true;
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
            try
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Button2.Visible = true;
                    GridView1.Columns[2].Visible = true;
                    GridView1.Columns[3].Visible = true;
                    GridView1.DataSource = DS;
                    GridView1.DataBind();
                }
                else
                {
                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    Button2.Visible = false;
                }
                DAta_Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }

        }
        else if (CheckBox1.Checked == false && CheckBox2.Checked == false) //--For Date
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
            //SqlDataAdapter da = new SqlDataAdapter("select ID,CONVERT(VARCHAR(10),DATE,105) as DATE,OPDNO ,NAME,TOTAMT from TBL_RADIOLGYREQ   where DATE  BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

            //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_DATE";
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
            //        GridView1.Columns[1].Visible = true;
            //        GridView1.Columns[6].Visible = false;
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
            try
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_DATE");
                //SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedItem.Text);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Button2.Visible = true;
                    GridView1.Columns[1].Visible = true;
                    GridView1.Columns[6].Visible = false;
                    GridView1.DataSource = DS;
                    GridView1.DataBind();
                }
                else
                {
                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    Button2.Visible = false;
                }
               // DAta_Clear();
            }

            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }

        }
        else if (CheckBox1.Checked == true && CheckBox2.Checked == true)//check both checkbox
        {
            try
            {
                if (drppatient.Text == "In Patient")
                {
                    if (dropinsurance.SelectedIndex == 0)
                    {
                        string message = "alert('* Fields are mandatory.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        dropinsurance.Focus();
                        return;
                    }
                    else if (dropinsurance.Text == "")
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
                    // SqlDataAdapter da = new SqlDataAdapter("select r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r.OPDNO ,r.NAME,r.TOTAMT,c.CNAME from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.VN=r.OPDNO  and r.OPDNO LIKE '%IP%'   join Corporate_Table c on a.CORPORATE=c.ID and a.CORPORATE='" + dropinsurance.Text + "' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

                    //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                    //{
                    //    cm.CommandType = CommandType.StoredProcedure;
                    //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate_Inpatient";
                    //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropinsurance.SelectedItem.Text;
                    //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
                    //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
                    //    SqlDataAdapter da = new SqlDataAdapter(cm);
                    //    DataTable dt = new DataTable();
                    //    da.Fill(dt);
                    //    if (dt.Rows.Count == 0)
                    //    {

                    //        string message = "alert(' No Record Found')";
                    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    //        DAta_Clear();
                    //    }
                    //    else
                    //    {

                    //        GridView1.SelectedIndex = 0;
                    //        //GridView1.Columns[4].Visible = false;
                    //        GridView1.Columns[1].Visible = true;
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
                    //        DAta_Clear();
                    //    }
                    //}
                    #endregion

                    SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate_Inpatient");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                    DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        Button2.Visible = true;
                        GridView1.Columns[1].Visible = true;
                        GridView1.DataSource = DS;
                        GridView1.DataBind();
                    }
                    else
                    {
                        string message = "alert(' No Record Found')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        Button2.Visible = false;
                    }
                    DAta_Clear();

                }
                else if (drppatient.Text == "Out Patient")
                {
                    if (dropinsurance.SelectedIndex == 0)
                    {
                        string message = "alert('* Fields are mandatory.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        dropinsurance.Focus();
                        return;
                    }
                    else if (dropinsurance.Text == "")
                    {
                        string message = "alert('* Fields are mandatory.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                        return;
                    }
                    else if (txtfrom.Text == "")
                    {
                        string message = "alert('* Fields are mandatory.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                        return;
                    }
                    else if (txtto.Text == "")
                    {
                        string message = "alert('* Fields are mandatory.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                        return;
                    }

                    #region old code
                    //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                    //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                    //SqlDataAdapter da = new SqlDataAdapter("select r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r.OPDNO ,r.NAME,r.TOTAMT,c.CNAME from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.ID=r.OPDNO  and r.OPDNO LIKE '%OP%' join Corporate_Table c on a.CORPORATE=c.ID and a.CORPORATE='" + dropinsurance.Text + "' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
                   
                    
                    //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                    //{
                    //    cm.CommandType = CommandType.StoredProcedure;
                    //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate_Outpatient";
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
                    //        DAta_Clear();
                    //    }
                    //    else
                    //    {

                    //        GridView1.SelectedIndex = 0;
                    //        GridView1.Columns[1].Visible = true;
                    //        // GridView1.Columns[3].Visible = false;
                    //        //  GridView1.Columns[4].Visible = false;
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
                    //        DAta_Clear();
                    //    }
                    //}
                    #endregion

                    SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate_Outpatient");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                    DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        Button2.Visible = true;
                        GridView1.Columns[1].Visible = true;
                        GridView1.DataSource = DS;
                        GridView1.DataBind();
                    }
                    else
                    {
                        string message = "alert(' No Record Found')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        Button2.Visible = false;
                    }
                    DAta_Clear();

                }
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
    }

    int total = 0;

    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TOTAMT"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lblamount = (Label)e.Row.FindControl("lblTotal");
            lblamount.Text = total.ToString();

            //GridView1.FooterRow.Cells[2].Text = lblamount.Text;
        }
    }

    public void DAta_Clear()
    {
        drppatient.SelectedIndex = 0;
        dropinsurance.SelectedIndex = 0;
        txtfrom.Text = "";
        txtto.Text = "";
    }

    //----------------FOR EXCEL REPORT----------------------//
    protected void Button2_Click(object sender, EventArgs e)
    {

        if (GridView1.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RadiologyCollection.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;

           
            if (CheckBox2.Checked == true && CheckBox1.Checked == false)  //--For Inpatient checkbox
            {
                try
                {
                    if (drppatient.Text == "In Patient")
                    {
                        #region oldcode
                        //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                        //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                        //SqlDataAdapter da = new SqlDataAdapter("select r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r.OPDNO ,r.NAME,r.TOTAMT,c.CNAME from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.VN=r.OPDNO  and r.OPDNO LIKE '%IP%' join Corporate_Table c on a.CORPORATE=c.ID and a.CORPORATE='"+dropinsurance.Text+"' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

                        //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                        //{
                        //    cm.CommandType = CommandType.StoredProcedure;
                        //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate_Inpatient";
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
                        //        GridView1.Columns[1].Visible = true;
                        //        // GridView1.Columns[5].Visible = true;
                        //        GridView1.DataSource = dt;
                        //        GridView1.DataBind();


                        //    }
                        //}
                        #endregion
                        try
                        {
                            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate_Inpatient");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                            DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                GridView1.Columns[1].Visible = true;
                                GridView1.DataSource = DS;
                                GridView1.DataBind();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: '{0}'", ex);
                        }
                    }
                    else if (drppatient.Text == "Out Patient")  // --------- FOR OUT PATIENT CHECKBOX SELECTED...............
                    {
                       #region old code
                        //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                        //{
                        //    cm.CommandType = CommandType.StoredProcedure;
                        //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate_Outpatient";
                        //    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropinsurance.Text;
                        //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
                        //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
                        //    SqlDataAdapter da = new SqlDataAdapter(cm);
                        //    // SqlDataAdapter da = new SqlDataAdapter("select r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r.OPDNO ,r.NAME,r.TOTAMT,c.CNAME from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.ID=r.OPDNO  and r.OPDNO LIKE '%OP%' join Corporate_Table c on a.CORPORATE=c.ID and a.CORPORATE='"+dropinsurance.Text+"' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

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
                        //        GridView1.Columns[1].Visible = true;
                        //        GridView1.DataSource = dt;
                        //        GridView1.DataBind();


                        //    }
                        //}
                        #endregion

                        try
                        {
                            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate_Outpatient");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                            DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                GridView1.Columns[1].Visible = true;
                                GridView1.DataSource = DS;
                                GridView1.DataBind();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: '{0}'", ex);
                        }
                    }

                    #region extracode
                    //else if (drppatient.Text == "All Patient")
                    //{
                    //    if (drppatient.Text == "")
                    //    {
                    //        string message = "alert('* Fields are mandatory.')";
                    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    //        return;
                    //    }
                    //    else if (dropinsurance.Text == "")
                    //    {
                    //        string message = "alert('* Fields are mandatory.')";
                    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    //        return;
                    //    }
                    //    else if (txtfrom.Text == "")
                    //    {
                    //        string message = "alert('* Fields are mandatory.')";
                    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    //        return;
                    //    }
                    //    else if (txtto.Text == "")
                    //    {
                    //        string message = "alert('* Fields are mandatory.')";
                    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    //        return;
                    //    }

                    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                    //    con.Open();

                    //    String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                    //    String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                    //    SqlDataAdapter da = new SqlDataAdapter("select ID,CONVERT(VARCHAR(10),DATE,105) as DATE,OPDNO ,NAME,TOTAMT from TBL_RADIOLGYREQ   where DATE  BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

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
                    //       // GridView1.Columns[5].Visible = false;
                    //        GridView1.DataSource = dt;
                    //        GridView1.DataBind();

                    //        //decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("RGFEE"));
                    //        //GridView1.FooterRow.Cells[2].Text = "Total";
                    //        //GridView1.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    //        //GridView1.FooterRow.Cells[3].Text = total.ToString("N2");

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
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: '{0}'", ex);
                }
            }
            else if (CheckBox2.Checked == false && CheckBox1.Checked == true)// for dropinsurance or corporate....SELECTION
            {
                
                #region oldcode
                //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                //SqlDataAdapter da = new SqlDataAdapter("select C.CNAME,r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r1.ID AS outid,a.VN,r.NAME,r.TOTAMT from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.ID=r.OPDNO join REGISTRATION_TBL r1 on r1.ID=a.ID join Corporate_Table c on a.CORPORATE=c.ID and  a.CORPORATE='"+dropinsurance.Text+"' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

                //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                //{
                //    cm.CommandType = CommandType.StoredProcedure;
                //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate";
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
                //        GridView1.Columns[2].Visible = true;
                //        GridView1.Columns[3].Visible = true;
                //        GridView1.DataSource = dt;
                //        GridView1.DataBind();


                //    }
                //}
                #endregion

                try
                {
                    SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                    DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        GridView1.Columns[2].Visible = true;
                        GridView1.Columns[3].Visible = true;
                        GridView1.DataSource = DS;
                        GridView1.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: '{0}'", ex);
                }



            }
            else if (CheckBox1.Checked == false && CheckBox2.Checked == false) //--For Date  CONDITION----------
            {
                #region old code
                //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                //SqlDataAdapter da = new SqlDataAdapter("select ID,CONVERT(VARCHAR(10),DATE,105) as DATE,OPDNO ,NAME,TOTAMT from TBL_RADIOLGYREQ   where DATE  BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

                //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                //{
                //    cm.CommandType = CommandType.StoredProcedure;
                //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_DATE";
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
                //        GridView1.Columns[1].Visible = true;
                //        GridView1.Columns[6].Visible = false;
                //        GridView1.DataSource = dt;
                //        GridView1.DataBind();


                //    }
                //}
                #endregion
                try
                {
                    SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_DATE");
                   // SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedItem.Text);
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                    DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        GridView1.Columns[1].Visible = true;
                        GridView1.Columns[6].Visible = false;
                        GridView1.DataSource = DS;
                        GridView1.DataBind();
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: '{0}'", ex);
                }

            }
            else if (CheckBox1.Checked == true && CheckBox2.Checked == true)// BOTH CONDITION ARE TRUE    ////////////////////////////////
            {
                try
                {
                    if (drppatient.Text == "In Patient")
                    {
                       
                        #region old code
                        //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                        //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                        // SqlDataAdapter da = new SqlDataAdapter("select r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r.OPDNO ,r.NAME,r.TOTAMT,c.CNAME from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.VN=r.OPDNO  and r.OPDNO LIKE '%IP%'   join Corporate_Table c on a.CORPORATE=c.ID and a.CORPORATE='" + dropinsurance.Text + "' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

                        //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                        //{
                        //    cm.CommandType = CommandType.StoredProcedure;
                        //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate_Inpatient";
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
                        //        GridView1.Columns[1].Visible = true;
                        //        //GridView1.Columns[4].Visible = false;
                        //        GridView1.DataSource = dt;
                        //        GridView1.DataBind();

                        //    }
                        //}
                        #endregion

                        SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate_Inpatient");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                        DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            GridView1.Columns[1].Visible = true;
                            GridView1.DataSource = DS;
                            GridView1.DataBind();
                        }
                    }
                    else if (drppatient.Text == "Out Patient")
                    {                        
                        #region oldcode
                        //SqlDataAdapter da = new SqlDataAdapter("select r.ID,CONVERT(VARCHAR(10),r.DATE,105) as DATE,r.OPDNO ,r.NAME,r.TOTAMT,c.CNAME from TBL_RADIOLGYREQ r join ADMISSION_TABLE a on a.ID=r.OPDNO  and r.OPDNO LIKE '%OP%' join Corporate_Table c on a.CORPORATE=c.ID and a.CORPORATE='" + dropinsurance.Text + "' AND r.DATE BETWEEN'" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
                        //using (SqlCommand cm = new SqlCommand("SP_RadiologyCllction_RPT", con))
                        //{
                        //    cm.CommandType = CommandType.StoredProcedure;
                        //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY_Corporate_Outpatient";
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
                        //        GridView1.Columns[1].Visible = true;
                        //        GridView1.Columns[4].Visible = false;
                        //        GridView1.DataSource = dt;
                        //        GridView1.DataBind();

                        //    }
                        //}
                        #endregion

                        SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_Corporate_Outpatient");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedValue);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                        DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            GridView1.Columns[1].Visible = true;
                            GridView1.Columns[4].Visible = false;
                            GridView1.DataSource = DS;
                            GridView1.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: '{0}'", ex);
                }
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
        if (CheckBox1.Checked == false && CheckBox2.Checked == false)
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY_DATE");
                //SQL_PARAMS[1] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropinsurance.SelectedItem.Text);
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                DataSet DS = OBJ_METHOD.Get_DataSet("SP_RadiologyCllction_RPT", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    GridView1.Columns[1].Visible = true;
                    GridView1.Columns[6].Visible = false;
                    GridView1.DataSource = DS;
                    GridView1.PageIndex = e.NewPageIndex;
                    GridView1.DataBind();
                }
        }
        
    }
}