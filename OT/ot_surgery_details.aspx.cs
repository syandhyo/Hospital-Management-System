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

public partial class OT_ot_surgery_details : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataMathods OBJ_METHOD = new DataMathods();
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select id from tblSurgeryDetail";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["id"].ToString();
        }
        num1 = string.Format("SG{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }
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
        txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        if (!IsPostBack)
        {
            binddata();
        }

    }

    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        // reference the Delete LinkButton
    //        LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];

    //        db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
    //    }
    //}
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
            DataSet Ds = OBJ_METHOD.Get_DataSet("select BEDNO from BED_TABLE WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropbedno.DataSource = Ds;
                dropbedno.DataTextField = "BEDNO";
                dropbedno.DataValueField = "BEDNO";
                dropbedno.DataBind();
                dropbedno.Items.Insert(0, new ListItem("Please Select","0"));
            }
            //SqlDataAdapter Adp6 = new SqlDataAdapter("select BEDNO from BED_TABLE", con);
            //DataTable Dt6 = new DataTable();
            //Adp6.Fill(Dt6);
            //dropbedno.DataSource = Dt6;
            //dropbedno.DataTextField = "BEDNO";
            //dropbedno.DataValueField = "BEDNO";
            //dropbedno.DataBind();
            //dropbedno.Items.Insert(0, "Please Select");

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select id,SurgeryType from tblSurgeryType WHERE Branch_ID = " + Session["Branch"] + "", false, false);

             if (Ds1.Tables[0].Rows.Count > 0)
             {
                 dropsurgerytype.DataSource = Ds1;
                 dropsurgerytype.DataTextField = "SurgeryType";
                 dropsurgerytype.DataValueField = "id";
                 dropsurgerytype.DataBind();
                 dropsurgerytype.Items.Insert(0, new ListItem("Please Select","0"));
             }
            //SqlDataAdapter Adp = new SqlDataAdapter("select id,SurgeryType from tblSurgeryType", con);
            //DataTable Dt = new DataTable();
            //Adp.Fill(Dt);
            //dropsurgerytype.DataSource = Dt;
            //dropsurgerytype.DataTextField = "SurgeryType";
            //dropsurgerytype.DataValueField = "id";
            //dropsurgerytype.DataBind();
            //dropsurgerytype.Items.Insert(0, "Please Select");
             SQL_PARAMS = new SqlParameter[2];

             SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_INDEX");
             SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
             //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

             DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_Surgery_PAge", false, true, SQL_PARAMS);
             if (Ds2.Tables[0].Rows.Count > 0)
             {
                 GridView1.DataSource = Ds2;
                 GridView1.DataKeyNames = new string[] { "id" };
                 GridView1.DataBind();
             }
             //using (SqlCommand cmd = new SqlCommand("SP_Surgery_PAge", con))
             //{
             //    cmd.CommandType = CommandType.StoredProcedure;
             //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_INDEX";
             //    SqlDataAdapter Adp = new SqlDataAdapter(cmd);
             //    DataTable Dt = new DataTable();
             //    Adp.Fill(Dt);
             //    GridView1.DataSource = Dt;
             //    GridView1.DataKeyNames = new string[] { "id" };
             //    GridView1.DataBind();
             //}
             //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_INDEX");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            //SQL_PARAMS[2] = OBJ_METHOD.createParams("@DeptName", SqlDbType.VarChar, 500, txtdept.Text);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("SP_Surgery_PAge", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds2;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
            //using (SqlCommand cmd = new SqlCommand("SP_Surgery_PAge", con))
            //{

            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_INDEX";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //    DataTable Dt = new DataTable();
            //    Adp.Fill(Dt);
            //    GridView1.DataSource = Dt;
            //    GridView1.PageIndex = e.NewPageIndex;
            //    GridView1.DataKeyNames = new string[] { "id" };
            //    GridView1.DataBind();
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from tblSurgeryDetail where id='" + slno + "'", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btndelete.Visible = true;
                btnupdate.Visible = true;
                txtid.Text = slno.ToString();
                dropbedno.Text = Ds1.Tables[0].Rows[0]["BEDNO"].ToString();
                lblipdno.Text = Ds1.Tables[0].Rows[0]["IPDNo"].ToString();
                lblname.Text = Ds1.Tables[0].Rows[0]["NAME"].ToString();
                dropsurgerytype.SelectedValue = Ds1.Tables[0].Rows[0]["SurgeryType"].ToString();
                txtsurgeon.Text = Ds1.Tables[0].Rows[0]["Surgeon"].ToString();
                txtanaesthesia.Text = Ds1.Tables[0].Rows[0]["Anaesthesia"].ToString();
                txtanaesthesist.Text = Ds1.Tables[0].Rows[0]["Anaesthesist"].ToString();
                txtsn.Text = Ds1.Tables[0].Rows[0]["SNurse"].ToString();
                txtscharge.Text = Ds1.Tables[0].Rows[0]["scharge"].ToString();
                txtpharmacist.Text = Ds1.Tables[0].Rows[0]["Pharmacist"].ToString();
                LBPAIDAMT.Text = Ds1.Tables[0].Rows[0]["scharge"].ToString();
            }
            //SqlCommand com = new SqlCommand("select * from tblSurgeryDetail where id='" + slno + "'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    btncreate.Visible = false;
            //    btndelete.Visible = true;
            //    btnupdate.Visible = true;
            //    txtid.Text = slno.ToString();
            //    dropbedno.SelectedItem.Text = dr["BEDNO"].ToString();
            //    lblipdno.Text = dr["IPDNo"].ToString();
            //    lblname.Text = dr["NAME"].ToString();
            //    dropsurgerytype.SelectedValue = dr["SurgeryType"].ToString();
            //    txtsurgeon.Text = dr["Surgeon"].ToString();
            //    txtanaesthesia.Text = dr["Anaesthesia"].ToString();
            //    txtanaesthesist.Text = dr["Anaesthesist"].ToString();
            //    txtsn.Text = dr["SNurse"].ToString();
            //    txtscharge.Text = dr["scharge"].ToString();
            //    txtpharmacist.Text = dr["Pharmacist"].ToString();
            //    LBPAIDAMT.Text = dr["scharge"].ToString();
            //}
            //dr.Close();
            //con.Close();
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            if (txtanaesthesia.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropbedno.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtscharge.Text == "" || txtscharge.Text == "0")
            {
                string message = "alert('* Surgery charges can't be Zero.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //txtscharge.Text = "0";
                return;
            }
            else if (Convert.ToDouble(txtscharge.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[14];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, txtinvdate.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblipdno.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "OT CHARGE FOR " + dropsurgerytype.SelectedItem.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, txtscharge.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, "0");
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, txtscharge.Text);

            SQL_PARAMS[10] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblipdno.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "OTHER CHAREGES");
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {      
                SQL_PARAMS = new SqlParameter[17];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 500, lbluid.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@IPDNo", SqlDbType.VarChar, 500, lblipdno.Text);

                SQL_PARAMS[5] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd"));
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);
                SQL_PARAMS[7] = OBJ_METHOD.createParams("@SurgeryType", SqlDbType.VarChar, 500, dropsurgerytype.SelectedValue);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@Anaesthesia", SqlDbType.VarChar, 500, txtanaesthesia.Text);
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@Anaesthesist", SqlDbType.VarChar, 500, txtanaesthesist.Text);

                SQL_PARAMS[10] = OBJ_METHOD.createParams("@SNurse", SqlDbType.VarChar, 500, txtsn.Text);
                SQL_PARAMS[11] = OBJ_METHOD.createParams("@Pharmacist", SqlDbType.VarChar, 500, txtpharmacist.Text);
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[14] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, lblname.Text);
                SQL_PARAMS[15] = OBJ_METHOD.createParams("@scharge", SqlDbType.VarChar, 500, txtscharge.Text);
                SQL_PARAMS[16] = OBJ_METHOD.createParams("@Surgeon", SqlDbType.VarChar, 500, txtsurgeon.Text);

                OBJ_METHOD.ExecuteProceedure("SP_Surgery_Details", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            #region OLD CODE
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //auto();
            //// SqlCommand cmd = new SqlCommand("insert into tblSurgeryDetail(id,ORGID,USERID,IPDNo,BEDNO,SurgeryType,Surgeon,Anaesthesia,Anaesthesist,SNurse,Pharmacist,DATE,NAME,scharge)values(@id,@ORGID,@USERID,@IPDNo,@BEDNO,@SurgeryType,@Surgeon,@Anaesthesia,@Anaesthesist,@SNurse,@Pharmacist,@DATE,@NAME,@scharge)", con);
            //using (SqlCommand cmd = new SqlCommand("SP_Surgery_Details", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
            //    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar).Value = lblipdno.Text;

            //    cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtinvdate.Text;
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cmd.Parameters.Add("@SurgeryType", SqlDbType.VarChar).Value = dropsurgerytype.SelectedValue.ToString();
            //    cmd.Parameters.Add("@Surgeon", SqlDbType.VarChar).Value = txtsurgeon.Text.ToString();
            //    cmd.Parameters.Add("@Anaesthesia", SqlDbType.VarChar).Value = txtanaesthesia.Text;
            //    cmd.Parameters.Add("@Anaesthesist", SqlDbType.VarChar).Value = txtanaesthesist.Text.ToString();

            //    cmd.Parameters.Add("@SNurse", SqlDbType.VarChar).Value = txtsn.Text;
            //    cmd.Parameters.Add("@Pharmacist", SqlDbType.VarChar).Value = txtpharmacist.Text.ToString();
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblname.Text;
            //    cmd.Parameters.Add("@scharge", SqlDbType.VarChar).Value = txtscharge.Text;
            //    cmd.ExecuteNonQuery();
            //}
            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipdno.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;

            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "OT CHARGE FOR " + dropsurgerytype.SelectedItem.Text;
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtscharge.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = txtscharge.Text;

            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipdno.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //    cm.ExecuteNonQuery();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Saved.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void dropipdno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT * FROM ADMISSION_TABLE WHERE VN=(SELECT VN FROM BED_TABLE WHERE BEDNO='" + dropbedno.Text + "')", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            { 
                lblname.Text = Ds1.Tables[0].Rows[0]["NAME"].ToString();
                lblipdno.Text = Ds1.Tables[0].Rows[0]["VN"].ToString();
            }
            else
            {
                string message = "alert('No Record Found.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region old code
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //SqlCommand COM = new SqlCommand("SELECT * FROM ADMISSION_TABLE WHERE VN=(SELECT VN FROM BED_TABLE WHERE BEDNO='" + dropbedno.Text + "')", con);
        //dr = COM.ExecuteReader();
        //if (dr.Read())
        //{
        //    lblname.Text = dr["NAME"].ToString();
        //    lblipdno.Text = dr["VN"].ToString();
        //}
        //else
        //{
        //    string message = "alert('No Record Found.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

        //    return;
        //}

        //dr.Close();
        //con.Close();
        #endregion
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            
            if (txtanaesthesia.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropbedno.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtscharge.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtscharge.Text = "0";
                return;
            }
            else if (Convert.ToDouble(txtscharge.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False' and Branch_ID='" + Session["Branch"] + "'");

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Can't Edit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[12];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, txtinvdate.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblipdno.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "OT CHARGE FOR " + dropsurgerytype.SelectedItem.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, txtscharge.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, "0");
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, txtscharge.Text);

            SQL_PARAMS[10] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblipdno.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "OTHER CHAREGES");

            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE1");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, txtscharge.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblipdno.Text);

                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SQL_PARAMS = new SqlParameter[17];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 500, lbluid.Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@IPDNo", SqlDbType.VarChar, 500, lblipdno.Text);

                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtinvdate.Text);
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@SurgeryType", SqlDbType.VarChar, 500, dropsurgerytype.SelectedValue);
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@Anaesthesia", SqlDbType.VarChar, 500, txtanaesthesia.Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@Anaesthesist", SqlDbType.VarChar, 500, txtanaesthesist.Text);

                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@SNurse", SqlDbType.VarChar, 500, txtsn.Text);
                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@Pharmacist", SqlDbType.VarChar, 500, txtpharmacist.Text);
                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[14] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, lblname.Text);
                    SQL_PARAMS[15] = OBJ_METHOD.createParams("@scharge", SqlDbType.VarChar, 500, txtscharge.Text);
                    SQL_PARAMS[16] = OBJ_METHOD.createParams("@Surgeon", SqlDbType.VarChar, 500, txtsurgeon.Text);

                    OBJ_METHOD.ExecuteProceedure("SP_Surgery_Details", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        clearcontrol();
                        binddata();
                    }
                }
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region old code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //SqlCommand com = new SqlCommand("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    string message = "alert('* You Can't Edit.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            //dr.Close();
            // SqlCommand cmd = new SqlCommand("UPDATE tblSurgeryDetail SET scharge=@scharge,NAME=@NAME,SurgeryType=@SurgeryType,Surgeon=@Surgeon,Anaesthesia=@Anaesthesia,Anaesthesist=@Anaesthesist,SNurse=@SNurse,Pharmacist=@Pharmacist WHERE id=@id", con);
            //using (SqlCommand cmd = new SqlCommand("SP_Surgery_Details", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
            //    cmd.Parameters.Add("@IPDNo", SqlDbType.VarChar).Value = lblipdno.Text;
            //    cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtinvdate.Text;
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cmd.Parameters.Add("@SurgeryType", SqlDbType.VarChar).Value = dropsurgerytype.SelectedValue.ToString();
            //    cmd.Parameters.Add("@Surgeon", SqlDbType.VarChar).Value = txtsurgeon.Text.ToString();
            //    cmd.Parameters.Add("@Anaesthesia", SqlDbType.VarChar).Value = txtanaesthesia.Text;
            //    cmd.Parameters.Add("@Anaesthesist", SqlDbType.VarChar).Value = txtanaesthesist.Text.ToString();
            //    cmd.Parameters.Add("@SNurse", SqlDbType.VarChar).Value = txtsn.Text;
            //    cmd.Parameters.Add("@Pharmacist", SqlDbType.VarChar).Value = txtpharmacist.Text.ToString();
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblname.Text;
            //    cmd.Parameters.Add("@scharge", SqlDbType.VarChar).Value = txtscharge.Text;
            //    cmd.ExecuteNonQuery();
            //}
            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipdno.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "OT CHARGE FOR " + dropsurgerytype.SelectedItem.Text;
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipdno.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //    cm.ExecuteNonQuery();
            //}
            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipdno.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "OT CHARGE FOR " + dropsurgerytype.SelectedItem.Text;
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtscharge.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = txtscharge.Text;
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipdno.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //    cm.ExecuteNonQuery();
            //}
            #endregion

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {
            
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }

    protected void Btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False' and Branch_ID='" + Session["Branch"] + "'");

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* You Can't Edit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "OT CHARGE FOR " + dropsurgerytype.SelectedItem.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblipdno.Text);

            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);

                OBJ_METHOD.ExecuteProceedure("SP_Surgery_Details", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
                    binddata();
                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            #region old code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //SqlCommand com = new SqlCommand("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", con);
            //dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    string message = "alert('* You Cant delete.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            //dr.Close();
            //SqlCommand cm1 = new SqlCommand("delete from tblSurgeryDetail where id='" + txtid.Text + "'", con);
            //cm1.ExecuteNonQuery();
            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtinvdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblipdno.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "OT CHARGE FOR " + dropsurgerytype.SelectedItem.Text;
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtscharge.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = txtscharge.Text;
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblipdno.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //    cm.ExecuteNonQuery();
            //}
            //binddata();

            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }
    public void clearcontrol()
    {
        txtanaesthesia.Text = txtanaesthesist.Text=txtpharmacist.Text=txtsurgeon.Text=txtsn.Text= lblname.Text=lblipdno.Text="";
        txtscharge.Text = "0";
        dropbedno.SelectedIndex = dropsurgerytype.SelectedIndex = 0;
        btncreate.Visible = true;
        btnupdate.Visible = false;
        btndelete.Visible = false;
    }
}