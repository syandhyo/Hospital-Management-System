using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class LABORATORY_lab_patient_search : System.Web.UI.Page
{
    SqlDataReader dr;
    DataMathods OBJ_METHOD = new DataMathods();

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


            DataSet DS2 = OBJ_METHOD.Get_DataSet("SELECT DISTINCT BEDNO FROM BED_TABLE", false, false);

            //dropbedno.SelectedIndex = 0;
            DropDownList1.DataSource = DS2;
            DropDownList1.DataTextField = "BEDNO";
            DropDownList1.DataValueField = "BEDNO";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }



    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtspid.Text == "")
            {
                string message = "alert('* Please Select Patient OPD Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtspid.Text != "")
            {
                DataSet DS2 = OBJ_METHOD.Get_DataSet("select * from ADMISSION_TABLE where ID='" + txtspid.Text + "'", false, false);
                if (DS2.Tables[0].Rows.Count > 0)
                {

                }
                else
                {
                    string message = "alert('Invalid OPD Number.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }

            }


            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_OP");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtspid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("SP_OT_OP_PatientSrch", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            else
            {

                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }

            #region oldcode
            // SqlDataAdapter da = new SqlDataAdapter("SELECT ID,PNAME,TELPHNO,MOBNO,DATETIME FROM REGISTRATION_TBL WHERE ID= '" + txtspid.Text + "'  ORDER BY ID DESC", con);
            //using (SqlCommand cmd = new SqlCommand("SP_OT_OP_PatientSrch", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_OP";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtspid.Text;
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
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
            //        GridView1.DataKeyNames = new string[] { "ID" };
            //        GridView1.DataBind();
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
    protected void btnshowph_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtsmobile.Text == "")
            {
                string message = "alert('* Please Select Patient Mobile Number.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }


            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "Select_Phn");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@TELPHNO", SqlDbType.VarChar, 500, txtsmobile.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("SP_OT_OP_PatientSrch", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            else
            {

                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }

            #region oldcode
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,PNAME,TELPHNO,MOBNO,DATETIME FROM REGISTRATION_TBL WHERE TELPHNO= '" + txtsmobile.Text + "' ORDER BY ID DESC", con);

            //using (SqlCommand cmd = new SqlCommand("SP_OT_OP_PatientSrch", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "Select_Phn";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = txtsmobile.Text;
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        dr.Close();
            //        DataTable dt = new DataTable();
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        da.Fill(dt);
            //        if (dt.Rows.Count == 0)
            //        {

            //            string message = "alert(' No Records is there..')";
            //            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        }
            //        else
            //        {
            //            GridView1.SelectedIndex = 0;
            //            GridView1.DataSource = dt;
            //            GridView1.DataKeyNames = new string[] { "ID" };
            //            GridView1.DataBind();
            //        }
            //    }
            //    else
            //    {
            //        string message = "alert('No Record Found..')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //        return;
            //    }
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnshowip_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtip.Text == "")
            {
                string message = "alert('* Please Select Patient IP No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtip.Text != "")
            {
                DataSet DS = OBJ_METHOD.Get_DataSet("select * from BED_TABLE where VN='" + txtip.Text + "'", false, false);

                if (DS.Tables[0].Rows.Count > 0)
                {

                }
                else
                {
                    string message = "alert('Invalid IPD Number.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }

            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_IP");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, txtip.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_OT_IP_PatientSrch", false, true, SQL_PARAMS);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = DS1;
                GridView2.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            #region oldcode
            //using (SqlCommand cmd = new SqlCommand("SP_OT_IP_PatientSrch", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_IP";
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtip.Text;
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = DropDownList1.SelectedValue;
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    if (dt.Rows.Count == 0)
            //    {

            //        string message = "alert(' No Record Found')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    }
            //    else
            //    {
            //        GridView2.SelectedIndex = 0;
            //        GridView2.DataSource = dt;
            //        GridView2.DataBind();
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
    protected void btnshowbed_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('* Please Select BED NO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BED");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, DropDownList1.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_OT_IP_PatientSrch", false, true, SQL_PARAMS);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = DS1;
                GridView2.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            #region oldcode
            //using (SqlCommand cmd = new SqlCommand("SP_OT_IP_PatientSrch", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "";
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = DropDownList1.Text;
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    if (dt.Rows.Count == 0)
            //    {
            //        string message = "alert('No Record Found')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    }
            //    else
            //    {
            //        GridView2.SelectedIndex = 0;
            //        GridView2.DataSource = dt;
            //        GridView2.DataBind();
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
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet DS1 = OBJ_METHOD.Get_DataSet("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN", false, false);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView2.SelectedIndex = 0;
                GridView2.DataSource = DS1;
                GridView2.PageIndex = e.NewPageIndex;
                GridView2.DataBind();
            }

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    
}