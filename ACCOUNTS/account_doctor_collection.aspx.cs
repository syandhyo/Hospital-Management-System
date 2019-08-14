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

public partial class ACCOUNTS_account_doctor_collection : System.Web.UI.Page
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

            DataSet Dt = OBJ_METHOD.Get_DataSet("select EMPID,Sname from tblStaff", false,false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                dropdoctor.DataSource = Dt;
                dropdoctor.DataTextField = "Sname";
                dropdoctor.DataValueField = "EMPID";
                dropdoctor.DataBind();
                dropdoctor.Items.Insert(0, new ListItem("Please Select", "0"));
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
            if (CheckBox1.Checked == true)
            {

                if (dropdoctor.SelectedIndex == 0)
                {
                    string message = "alert('* Fields are mandatory.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    dropdoctor.Focus();
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
                ////SqlDataAdapter da = new SqlDataAdapter("select A.id,A.OPNo,CONVERT(VARCHAR(10),A.CDate,105) as DATE,A.fee,B.NAME,C.Sname,A.Followup from tblOPConsultancy A, PATIENT_REG_TABLE B,tblStaff C WHERE A.OPNo= B.ID and C.id=A.Staffid and c.Sname='" + dropdoctor.SelectedItem.Text + "' and a.CDate between '" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);

                //using (SqlCommand cm = new SqlCommand("SP_DOCTORWise_RPT", con))
                //{
                //    cm.CommandType = CommandType.StoredProcedure;
                //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
                //    cm.Parameters.Add("@Sname", SqlDbType.VarChar).Value = dropdoctor.SelectedItem.Text;
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

                Session["DBOPERATION"] = "DISPLAY_DOCTOR";
                DBOPERATION = Session["DBOPERATION"].ToString();

                SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, dropdoctor.SelectedItem.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                DataSet DS = OBJ_METHOD.Get_DataSet("SP_DOCTORWise_RPT", false, true, SQL_PARAMS);
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
            else
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
              
                Session["DBOPERATION"] = "DISPLAY_DATE";
                DBOPERATION = Session["DBOPERATION"].ToString();
                #region oldcode
                // SqlDataAdapter da = new SqlDataAdapter("select A.id,A.OPNo,CONVERT(VARCHAR(10),A.CDate,105) as DATE,A.fee,B.NAME,C.Sname,A.Followup from tblOPConsultancy A, PATIENT_REG_TABLE B,tblStaff C WHERE A.OPNo= B.ID and C.id=A.Staffid and a.CDate between '" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
               
                //using (SqlCommand cm = new SqlCommand("SP_DOCTORWise_RPT", con))
                //{
                //    cm.CommandType = CommandType.StoredProcedure;
                //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
                //    cm.Parameters.Add("@Sname", SqlDbType.VarChar).Value = "";
                //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
                //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
                //    SqlDataAdapter da = new SqlDataAdapter(cm);
                //    DataTable dt = new DataTable();
                //    da.Fill(dt);
                //    GridView1.DataSource = dt;
                //    // GridView1.Columns[4].Visible = false;
                //    //GridView1.PageIndex = e.NewPageIndex;
                //    GridView1.DataKeyNames = new string[] { "id" };
                //    GridView1.DataBind();


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

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, "");
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                DataSet DS = OBJ_METHOD.Get_DataSet("SP_DOCTORWise_RPT", false, true, SQL_PARAMS);
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
        }

        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }
    protected void CheckBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            datacorp();
            dropdoctor.Visible = true;
        }
        else
        {
            dropdoctor.Visible = false;
            txtfrom.Text = "";
            txtto.Text = "";
            GridView1.Visible = false;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Doctor_Consultwise.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
          

            if (CheckBox1.Checked == true)
            {
                #region oldcode
                //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                //SqlDataAdapter da = new SqlDataAdapter("select A.id,A.OPNo,CONVERT(VARCHAR(10),A.CDate,105) as DATE,A.fee,B.NAME,C.Sname,A.Followup from tblOPConsultancy A, PATIENT_REG_TABLE B,tblStaff C WHERE A.OPNo= B.ID and C.id=A.Staffid and c.Sname='" + dropdoctor.SelectedItem.Text + "' and a.CDate between '" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
                //using (SqlCommand cm = new SqlCommand("SP_DOCTORWise_RPT", con))
                //{
                //    cm.CommandType = CommandType.StoredProcedure;
                //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
                //    cm.Parameters.Add("@Sname", SqlDbType.VarChar).Value = dropdoctor.SelectedItem.Text;
                //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
                //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
                //    SqlDataAdapter da = new SqlDataAdapter(cm);
                //    DataTable dt = new DataTable();
                //    da.Fill(dt);
                //    GridView1.DataSource = dt;
                //    GridView1.Columns[4].Visible = false;
                //    GridView1.DataBind();
                //}
                #endregion
                try
                {
                    DBOPERATION = Session["DBOPERATION"].ToString();
                    SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, dropdoctor.SelectedItem.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                    DataSet DS = OBJ_METHOD.Get_DataSet("SP_DOCTORWise_RPT", false, true, SQL_PARAMS);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                       
                        GridView1.DataSource = DS;
                        GridView1.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: '{0}'", ex);
                }
            }
            else
            {
                //  bind_data2();
                if (txtfrom.Text == "")
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
                try
                {
                    #region oldcode
                    //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
                    //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";
                    // SqlDataAdapter da = new SqlDataAdapter("select A.id,A.OPNo,CONVERT(VARCHAR(10),A.CDate,105) as DATE,A.fee,B.NAME,C.Sname,A.Followup from tblOPConsultancy A, PATIENT_REG_TABLE B,tblStaff C WHERE A.OPNo= B.ID and C.id=A.Staffid and a.CDate between '" + Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 00:01:00' AND '" + Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00'", con);
                    //using (SqlCommand cm = new SqlCommand("SP_DOCTORWise_RPT", con))
                    //{
                    //    cm.CommandType = CommandType.StoredProcedure;
                    //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
                    //    cm.Parameters.Add("@Sname", SqlDbType.VarChar).Value = "";
                    //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
                    //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
                    //    SqlDataAdapter da = new SqlDataAdapter(cm);
                    //    DataTable dt = new DataTable();
                    //    da.Fill(dt);
                    //    GridView1.DataSource = dt;
                    //    // GridView1.Columns[4].Visible = false;
                    //    //GridView1.PageIndex = e.NewPageIndex;
                    //    GridView1.DataKeyNames = new string[] { "id" };
                    //    GridView1.DataBind();
                    //}
                    #endregion

                    DBOPERATION = Session["DBOPERATION"].ToString();
                    SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                    //SQL_PARAMS[1] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, dropdoctor.SelectedItem.Text);
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, "");
                    DataSet DS = OBJ_METHOD.Get_DataSet("SP_DOCTORWise_RPT", false, true, SQL_PARAMS);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        //GridView1.Columns[4].Visible = false;
                        GridView1.DataSource = DS;
                        GridView1.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.ToString();
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
        #region oldcode
        //String FromDate = Convert.ToDateTime(txtfrom.Text).ToString("yyyy-MM-dd") + " 01:00:00";
        //String ToDate = Convert.ToDateTime(txtto.Text).ToString("yyyy-MM-dd") + " 23:59:00";

        //using (SqlCommand cm = new SqlCommand("SP_DOCTORWise_RPT", con))
        //{
        //    cm.CommandType = CommandType.StoredProcedure;
        //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = DBOPERATION.ToString();
        //    cm.Parameters.Add("@Sname", SqlDbType.VarChar).Value = dropdoctor.SelectedItem.Text;
        //    cm.Parameters.Add("@FDATE", SqlDbType.DateTime).Value = txtfrom.Text;
        //    cm.Parameters.Add("@TDATE", SqlDbType.DateTime).Value = txtto.Text;
        //    SqlDataAdapter da = new SqlDataAdapter(cm);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    GridView1.DataSource = dt;
        //    GridView1.PageIndex = e.NewPageIndex;
        //    GridView1.DataKeyNames = new string[] { "id" };
        //    GridView1.DataBind();
        //}
        #endregion


        try
        {
            DBOPERATION = Session["DBOPERATION"].ToString();
            
            if (CheckBox1.Checked == true)
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, dropdoctor.SelectedItem.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                DataSet DS = OBJ_METHOD.Get_DataSet("SP_DOCTORWise_RPT", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = DS;
                    GridView1.PageIndex = e.NewPageIndex;
                    GridView1.DataKeyNames = new string[] { "id" };
                    GridView1.DataBind();
                }
            }
            else
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[4];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, DBOPERATION.ToString());
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@FDATE", SqlDbType.DateTime, 0, txtfrom.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@TDATE", SqlDbType.DateTime, 0, txtto.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, "");
                DataSet DS = OBJ_METHOD.Get_DataSet("SP_DOCTORWise_RPT", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = DS;
                    GridView1.PageIndex = e.NewPageIndex;
                    GridView1.DataKeyNames = new string[] { "id" };
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
       
    }
}