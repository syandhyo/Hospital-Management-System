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

public partial class RECEPTION_reception_nicu_admission : System.Web.UI.Page
{
    string num1 = "SJ000";
    String num2 = "SJ000";
    SqlDataReader dr;
    SqlConnection con;
    SqlCommand com;
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
            auto();
            
            DataSet Ds2 = OBJ_METHOD.Get_DataSet("select * from tblDepartment where Branch_ID='" + Session["Branch"] + "'", false, false);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                dropdept.DataSource = Ds2;
                dropdept.DataTextField = "DeptName";
                dropdept.DataValueField = "id";
                dropdept.DataBind();
                dropdept.Items.Insert(0, new ListItem("Please Select","0"));
            }
            //SqlDataAdapter Adp2 = new SqlDataAdapter("select * from tblDepartment", con);
            //DataTable Dt2 = new DataTable();
            //Adp2.Fill(Dt2);
            //dropdept.DataSource = Dt2;
            //dropdept.DataTextField = "DeptName";
            //dropdept.DataValueField = "id";
            //dropdept.DataBind();
            //dropdept.Items.Insert(0, "---Please Select---");
        }


        
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from NICU_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        //num1 = string.Format("PU{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtipno.Text = "IP-" + num1 + 1 + "-" + lblfyear.Text;
        dr.Close();

        string qry2 = "select MAX(REGN_NO) as ID from NICU_TABLE";

        com = new SqlCommand(qry2, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num2 = dr["ID"].ToString();
        }
        num2 = string.Format("NICU{0}", (Convert.ToUInt32(num2.Substring(4)) + 1).ToString("D3"));
        txtregn.Text = num2;
        dr.Close();
        con.Close();
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
        SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_NICU_PAGE");
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

        DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_NICU", false, true, SQL_PARAMS1);
        if (DS1.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = DS1;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
        //using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_NICU_PAGE";
        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";

        //    SqlDataAdapter Adp = new SqlDataAdapter(cmd);
        //    //SqlDataAdapter Adp = new SqlDataAdapter("SELECT * from NICU_TABLE", con);
        //    DataTable Dt = new DataTable();
        //    Adp.Fill(Dt);
        //    GridView1.DataSource = Dt;
        //    GridView1.DataKeyNames = new string[] { "id" };
        //    GridView1.DataBind();
        //}
        //con.Close();
    }
    public void clearcontrol()
    {

        txtid.Text = "";
        txtregn.Text = "";
        txtname.Text = "";
        txtpatientid.Text = "";
        txtminorpatient.Text = "";
        txtpatientcond.Text = "";
        dropgender.Text = "";
        dropbloodgroup.Text = "";
        txtdateofbirth.Text = "";
        txtaddress.Text = "";
        txtpin.Text = "";
        txtdistrict.Text = "";
        txtstate.Text = "";
        txtmobileno.Text = "";
        txtemailid.Text = "";
        txtfrom.Text = "";
        txtrefname.Text = "";
        txtcomplaint.Text = "";
        dropdept.SelectedIndex = 0;
        dropdoctor.SelectedIndex = 0;
        txttreatment.Text = "";
        droproomtype.Text = "";
        txtoperationdate.Text = "";
        txtbookingdate.Text = "";
        txtdays.Text = "";
        btnSubmit.Visible = true;
        btnupdate.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            // Validation
            if (txtpatientid.Text == "")
            {
                string message = "alert('* Patient Id is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('* Name mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropgender.Text == "")
            {
                string message = "alert('* Gender is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('* Address is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtmobileno.Text == "")
            {
                string message = "alert('* Mobile No. mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (droproomtype.Text == "")
            {
                string message = "alert('* Room Type is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtbookingdate.Text == "")
            {
                string message = "alert('* Booking Date is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[32];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@REGN_NO", SqlDbType.VarChar, 500, txtregn.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, txtipno.Text);

            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@MOTHER_EXIT", SqlDbType.VarChar, 500, dropmotherexit.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@COMBINE_BILL", SqlDbType.VarChar, 500, dropcombinebill.Text);

            SQL_PARAMS[9] = OBJ_METHOD.createParams("@MINOR_PATIENT", SqlDbType.VarChar, 500, txtminorpatient.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@EMERGENCY", SqlDbType.VarChar, 500, txtemergency.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@PATIENT_CONDITION", SqlDbType.VarChar, 500, txtpatientcond.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@PATIENT_ID", SqlDbType.VarChar, 500, txtpatientid.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, dropgender.Text);

            SQL_PARAMS[14] = OBJ_METHOD.createParams("@BLOODGROUP", SqlDbType.VarChar, 500, dropbloodgroup.Text);
            SQL_PARAMS[15] = OBJ_METHOD.createParams("@DATEOFBIRTH", SqlDbType.Date, 0, Convert.ToDateTime(txtdateofbirth.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@PIN", SqlDbType.VarChar, 500, txtpin.Text);
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@DISTRICT", SqlDbType.VarChar, 500, txtdistrict.Text);

            SQL_PARAMS[19] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
            SQL_PARAMS[20] = OBJ_METHOD.createParams("@MOBILE", SqlDbType.VarChar, 0, txtmobileno.Text);
            SQL_PARAMS[21] = OBJ_METHOD.createParams("@EMAIL", SqlDbType.VarChar, 500, txtemailid.Text);
            SQL_PARAMS[22] = OBJ_METHOD.createParams("@FROM", SqlDbType.VarChar, 500, txtfrom.Text);
            SQL_PARAMS[23] = OBJ_METHOD.createParams("@REFERALNAME", SqlDbType.VarChar, 500, txtrefname.Text);

            SQL_PARAMS[24] = OBJ_METHOD.createParams("@COMPLAINT", SqlDbType.VarChar, 500, txtcomplaint.Text);
            SQL_PARAMS[25] = OBJ_METHOD.createParams("@CONSULTINGDEPT", SqlDbType.VarChar, 0, dropdept.SelectedValue);
            SQL_PARAMS[26] = OBJ_METHOD.createParams("@DOCTORNAME", SqlDbType.VarChar, 500, dropdoctor.SelectedValue);
            SQL_PARAMS[27] = OBJ_METHOD.createParams("@TREATEMENT", SqlDbType.VarChar, 500, txttreatment.Text);
            SQL_PARAMS[28] = OBJ_METHOD.createParams("@ROOMTYPE", SqlDbType.VarChar, 500, droproomtype.Text);

            SQL_PARAMS[29] = OBJ_METHOD.createParams("@OPERATIONDATE", SqlDbType.Date, 500, Convert.ToDateTime(txtoperationdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[30] = OBJ_METHOD.createParams("@BOOKINGFORDATE", SqlDbType.Date, 0, Convert.ToDateTime(txtbookingdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[31] = OBJ_METHOD.createParams("@DAYS", SqlDbType.VarChar, 500, txtdays.Text);

            OBJ_METHOD.ExecuteProceedure("USP_NICU", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
            }
            #region old code
            //using (SqlCommand cmd = new SqlCommand("USP_NICU", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    // cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txtid.Text;
            //    cmd.Parameters.Add("@REGN_NO", SqlDbType.VarChar).Value = txtregn.Text;
            //    cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtipno.Text;
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
            //    cmd.Parameters.Add("@MOTHER_EXIT", SqlDbType.VarChar).Value = dropmotherexit.Text;
            //    cmd.Parameters.Add("@COMBINE_BILL", SqlDbType.VarChar).Value = dropcombinebill.Text;
            //    cmd.Parameters.Add("@MINOR_PATIENT", SqlDbType.VarChar).Value = txtminorpatient.Text;
            //    cmd.Parameters.Add("@EMERGENCY", SqlDbType.VarChar).Value = txtemergency.Text;

            //    cmd.Parameters.Add("@PATIENT_CONDITION", SqlDbType.VarChar).Value = txtpatientcond.Text;
            //    cmd.Parameters.Add("@PATIENT_ID", SqlDbType.VarChar).Value = txtpatientid.Text;
            //    cmd.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.Text;

            //    cmd.Parameters.Add("@BLOODGROUP", SqlDbType.VarChar).Value = dropbloodgroup.Text;
            //    cmd.Parameters.Add("@DATEOFBIRTH", SqlDbType.Date).Value = txtdateofbirth.Text;
            //    cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
            //    cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
            //    cmd.Parameters.Add("@DISTRICT", SqlDbType.VarChar).Value = txtdistrict.Text;
            //    cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;

            //    cmd.Parameters.Add("@MOBILE", SqlDbType.VarChar).Value = txtmobileno.Text;
            //    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemailid.Text;
            //    cmd.Parameters.Add("@FROM", SqlDbType.VarChar).Value = txtfrom.Text;
            //    cmd.Parameters.Add("@REFERALNAME", SqlDbType.VarChar).Value = txtrefname.Text;

            //    cmd.Parameters.Add("@COMPLAINT", SqlDbType.VarChar).Value = txtcomplaint.Text;
            //    cmd.Parameters.Add("@CONSULTINGDEPT", SqlDbType.VarChar).Value = dropdept.SelectedValue;
            //    cmd.Parameters.Add("@DOCTORNAME", SqlDbType.VarChar).Value = dropdoctor.SelectedValue;
            //    cmd.Parameters.Add("@TREATEMENT", SqlDbType.VarChar).Value = txttreatment.Text;
            //    cmd.Parameters.Add("@ROOMTYPE", SqlDbType.VarChar).Value = droproomtype.Text;

            //    cmd.Parameters.Add("@OPERATIONDATE", SqlDbType.Date).Value = txtoperationdate.Text;
            //    cmd.Parameters.Add("@BOOKINGFORDATE", SqlDbType.Date).Value = txtbookingdate.Text;
            //    cmd.Parameters.Add("@DAYS", SqlDbType.VarChar).Value = txtdays.Text;
            //    cmd.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();
            //clearcontrol();
            //string message1 = "alert('Successfully Saved.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            // Validation
            if (txtpatientid.Text == "")
            {
                string message = "alert('* Patient Id is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('* Name mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropgender.Text == "")
            {
                string message = "alert('* Gender is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtdateofbirth.Text == "")
            {
                string message = "alert('* Date Of Birth is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('* Address is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtmobileno.Text == "")
            {
                string message = "alert('* Mobile No. mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (droproomtype.Text == "")
            {
                string message = "alert('* Room Type is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtbookingdate.Text == "")
            {
                string message = "alert('* Booking Date is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[32];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@REGN_NO", SqlDbType.VarChar, 500, txtregn.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATE", SqlDbType.Date, 0, Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd"));
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@IPNO", SqlDbType.VarChar, 500, txtipno.Text);

            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@MOTHER_EXIT", SqlDbType.VarChar, 500, dropmotherexit.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@COMBINE_BILL", SqlDbType.VarChar, 500, dropcombinebill.Text);

            SQL_PARAMS[9] = OBJ_METHOD.createParams("@MINOR_PATIENT", SqlDbType.VarChar, 500, txtminorpatient.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@EMERGENCY", SqlDbType.VarChar, 500, txtemergency.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@PATIENT_CONDITION", SqlDbType.VarChar, 500, txtpatientcond.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, dropgender.Text);

            SQL_PARAMS[14] = OBJ_METHOD.createParams("@BLOODGROUP", SqlDbType.VarChar, 500, dropbloodgroup.Text);
            SQL_PARAMS[15] = OBJ_METHOD.createParams("@DATEOFBIRTH", SqlDbType.Date, 0, txtdateofbirth.Text);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@PIN", SqlDbType.VarChar, 500, txtpin.Text);
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@DISTRICT", SqlDbType.VarChar, 500, txtdistrict.Text);

            SQL_PARAMS[19] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
            SQL_PARAMS[20] = OBJ_METHOD.createParams("@MOBILE", SqlDbType.VarChar, 0, txtmobileno.Text);
            SQL_PARAMS[21] = OBJ_METHOD.createParams("@EMAIL", SqlDbType.VarChar, 500, txtemailid.Text);
            SQL_PARAMS[22] = OBJ_METHOD.createParams("@FROM", SqlDbType.VarChar, 500, txtfrom.Text);
            SQL_PARAMS[23] = OBJ_METHOD.createParams("@REFERALNAME", SqlDbType.VarChar, 500, txtrefname.Text);

            SQL_PARAMS[24] = OBJ_METHOD.createParams("@COMPLAINT", SqlDbType.VarChar, 500, txtcomplaint.Text);
            SQL_PARAMS[25] = OBJ_METHOD.createParams("@CONSULTINGDEPT", SqlDbType.VarChar, 0, dropdept.SelectedValue);
            SQL_PARAMS[26] = OBJ_METHOD.createParams("@DOCTORNAME", SqlDbType.VarChar, 500, dropdoctor.SelectedValue);
            SQL_PARAMS[27] = OBJ_METHOD.createParams("@TREATEMENT", SqlDbType.VarChar, 500, txttreatment.Text);
            SQL_PARAMS[28] = OBJ_METHOD.createParams("@ROOMTYPE", SqlDbType.VarChar, 500, droproomtype.Text);

            SQL_PARAMS[29] = OBJ_METHOD.createParams("@OPERATIONDATE", SqlDbType.Date, 500, txtoperationdate.Text);
            SQL_PARAMS[30] = OBJ_METHOD.createParams("@BOOKINGFORDATE", SqlDbType.Date, 0, txtbookingdate.Text);
            SQL_PARAMS[31] = OBJ_METHOD.createParams("@DAYS", SqlDbType.VarChar, 500, txtdays.Text);

            OBJ_METHOD.ExecuteProceedure("USP_NICU", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            //using (SqlCommand cmd = new SqlCommand("USP_NICU", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    // cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txtid.Text;
            //    cmd.Parameters.Add("@REGN_NO", SqlDbType.VarChar).Value = txtregn.Text;
            //    cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
            //    cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = txtipno.Text;
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
            //    cmd.Parameters.Add("@MOTHER_EXIT", SqlDbType.VarChar).Value = dropmotherexit.Text;
            //    cmd.Parameters.Add("@COMBINE_BILL", SqlDbType.VarChar).Value = dropcombinebill.Text;
            //    cmd.Parameters.Add("@MINOR_PATIENT", SqlDbType.VarChar).Value = txtminorpatient.Text;
            //    cmd.Parameters.Add("@EMERGENCY", SqlDbType.VarChar).Value = txtemergency.Text;
            //    cmd.Parameters.Add("@PATIENT_CONDITION", SqlDbType.VarChar).Value = txtpatientcond.Text;
            //    cmd.Parameters.Add("@PATIENT_ID", SqlDbType.VarChar).Value = txtpatientid.Text;
            //    cmd.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.Text;
            //    cmd.Parameters.Add("@BLOODGROUP", SqlDbType.VarChar).Value = dropbloodgroup.Text;
            //    cmd.Parameters.Add("@DATEOFBIRTH", SqlDbType.Date).Value = txtdateofbirth.Text;
            //    cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
            //    cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
            //    cmd.Parameters.Add("@DISTRICT", SqlDbType.VarChar).Value = txtdistrict.Text;
            //    cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
            //    cmd.Parameters.Add("@MOBILE", SqlDbType.VarChar).Value = txtmobileno.Text;
            //    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemailid.Text;
            //    cmd.Parameters.Add("@FROM", SqlDbType.VarChar).Value = txtfrom.Text;
            //    cmd.Parameters.Add("@REFERALNAME", SqlDbType.VarChar).Value = txtrefname.Text;
            //    cmd.Parameters.Add("@COMPLAINT", SqlDbType.VarChar).Value = txtcomplaint.Text;
            //    cmd.Parameters.Add("@CONSULTINGDEPT", SqlDbType.VarChar).Value = dropdept.SelectedValue;
            //    cmd.Parameters.Add("@DOCTORNAME", SqlDbType.VarChar).Value = dropdoctor.SelectedValue;
            //    cmd.Parameters.Add("@TREATEMENT", SqlDbType.VarChar).Value = txttreatment.Text;
            //    cmd.Parameters.Add("@ROOMTYPE", SqlDbType.VarChar).Value = droproomtype.Text;
            //    cmd.Parameters.Add("@OPERATIONDATE", SqlDbType.Date).Value = txtoperationdate.Text;
            //    cmd.Parameters.Add("@BOOKINGFORDATE", SqlDbType.Date).Value = txtbookingdate.Text;
            //    cmd.Parameters.Add("@DAYS", SqlDbType.VarChar).Value = txtdays.Text;
            //    cmd.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();
            //clearcontrol();
            //string message1 = "alert('Successfully Updated.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
        
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);


            OBJ_METHOD.ExecuteProceedure("USP_NICU", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            //using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = slno;

            //    //SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //    //SqlCommand cm = new SqlCommand("delete from NICU_TABLE where ID='" + slno + "'", con);
            //    cmd.ExecuteNonQuery();
            //}
            //binddata();
            //con.Close();
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ID");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RECP_NICU", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = Ds2.Tables[0].Rows[0]["ID"].ToString();
                txtregn.Text = Ds2.Tables[0].Rows[0]["REGN_NO"].ToString();
                txtdate.Text = Ds2.Tables[0].Rows[0]["DATE"].ToString();
                txtipno.Text = Ds2.Tables[0].Rows[0]["IPNO"].ToString();
                txtname.Text = Ds2.Tables[0].Rows[0]["NAME"].ToString();
                dropmotherexit.Text = Ds2.Tables[0].Rows[0]["MOTHER_EXIT"].ToString();
                dropcombinebill.Text = Ds2.Tables[0].Rows[0]["COMBINE_BILL"].ToString();
                txtminorpatient.Text = Ds2.Tables[0].Rows[0]["MINOR_PATIENT"].ToString();
                txtemergency.Text = Ds2.Tables[0].Rows[0]["EMERGENCY"].ToString();
                txtpatientcond.Text = Ds2.Tables[0].Rows[0]["PATIENT_CONDITION"].ToString();
                txtpatientid.Text = Ds2.Tables[0].Rows[0]["PATIENT_ID"].ToString();
                dropgender.Text = Ds2.Tables[0].Rows[0]["GENDER"].ToString();
                dropbloodgroup.Text = Ds2.Tables[0].Rows[0]["BLOODGROUP"].ToString();
                txtdateofbirth.Text = Convert.ToDateTime(Ds2.Tables[0].Rows[0]["DATEOFBIRTH"]).ToString("dd-MM-yyyy");
                txtaddress.Text = Ds2.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtpin.Text = Ds2.Tables[0].Rows[0]["PIN"].ToString();
                txtdistrict.Text = Ds2.Tables[0].Rows[0]["DISTRICT"].ToString();
                txtstate.Text = Ds2.Tables[0].Rows[0]["STATE"].ToString();
                txtmobileno.Text = Ds2.Tables[0].Rows[0]["MOBILE"].ToString();
                txtemailid.Text = Ds2.Tables[0].Rows[0]["EMAIL"].ToString();
                txtfrom.Text = Ds2.Tables[0].Rows[0]["FROM"].ToString();
                txtrefname.Text = Ds2.Tables[0].Rows[0]["REFERALNAME"].ToString();
                txtcomplaint.Text = Ds2.Tables[0].Rows[0]["COMPLAINT"].ToString();
                dropdept.SelectedValue = Ds2.Tables[0].Rows[0]["CONSULTINGDEPT"].ToString();
                dropdoctor.SelectedValue = Ds2.Tables[0].Rows[0]["DOCTORNAME"].ToString();
                txttreatment.Text = Ds2.Tables[0].Rows[0]["TREATEMENT"].ToString();
                droproomtype.Text = Ds2.Tables[0].Rows[0]["ROOMTYPE"].ToString();
                txtoperationdate.Text = Convert.ToDateTime(Ds2.Tables[0].Rows[0]["OPERATIONDATE"]).ToString("dd-MM-yyyy");
                txtbookingdate.Text = Convert.ToDateTime(Ds2.Tables[0].Rows[0]["BOOKINGFORDATE"]).ToString("dd-MM-yyyy");
                txtdays.Text = Ds2.Tables[0].Rows[0]["DAYS"].ToString();
            }
            //using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ID";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;

            //    //SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //    //SqlCommand com = new SqlCommand("SELECT * from NICU_TABLE where id='" + slno + "'", con);
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        btnSubmit.Visible = false;
            //        btnupdate.Visible = true;
            //        txtid.Text = dr["ID"].ToString();
            //        txtregn.Text = dr["REGN_NO"].ToString();
            //        txtdate.Text = dr["DATE"].ToString();
            //        txtipno.Text = dr["IPNO"].ToString();
            //        txtname.Text = dr["NAME"].ToString();
            //        dropmotherexit.Text = dr["MOTHER_EXIT"].ToString();
            //        dropcombinebill.Text = dr["COMBINE_BILL"].ToString();
            //        txtminorpatient.Text = dr["MINOR_PATIENT"].ToString();
            //        txtemergency.Text = dr["EMERGENCY"].ToString();
            //        txtpatientcond.Text = dr["PATIENT_CONDITION"].ToString();
            //        txtpatientid.Text = dr["PATIENT_ID"].ToString();
            //        dropgender.Text = dr["GENDER"].ToString();
            //        dropbloodgroup.Text = dr["BLOODGROUP"].ToString();
            //        txtdateofbirth.Text = dr["DATEOFBIRTH"].ToString();
            //        txtaddress.Text = dr["ADDRESS"].ToString();
            //        txtpin.Text = dr["PIN"].ToString();
            //        txtdistrict.Text = dr["DISTRICT"].ToString();
            //        txtstate.Text = dr["STATE"].ToString();
            //        txtmobileno.Text = dr["MOBILE"].ToString();
            //        txtemailid.Text = dr["EMAIL"].ToString();
            //        txtfrom.Text = dr["FROM"].ToString();
            //        txtrefname.Text = dr["REFERALNAME"].ToString();
            //        txtcomplaint.Text = dr["COMPLAINT"].ToString();
            //        dropdept.SelectedValue = dr["CONSULTINGDEPT"].ToString();
            //        dropdoctor.SelectedValue = dr["DOCTORNAME"].ToString();
            //        txttreatment.Text = dr["TREATEMENT"].ToString();
            //        droproomtype.Text = dr["ROOMTYPE"].ToString();
            //        txtoperationdate.Text = dr["OPERATIONDATE"].ToString();
            //        txtbookingdate.Text = dr["BOOKINGFORDATE"].ToString();
            //        txtdays.Text = dr["DAYS"].ToString();
            //    }
            //}
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

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_NICU_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RECP_NICU", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds2;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
            //using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_NICU_PAGE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";

            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da = new SqlDataAdapter("SELECT * from NICU_TABLE", con);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView1.DataSource = dt;
            //    GridView1.PageIndex = e.NewPageIndex;
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
    protected void dropdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, dropdept.SelectedValue);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RECP_NICU", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                dropdoctor.DataSource = Ds2;
                dropdoctor.DataTextField = "Sname";
                dropdoctor.DataValueField = "id";
                dropdoctor.DataBind();
                dropdoctor.Items.Insert(0, "Please Select");
            }
            //using (SqlCommand cmd = new SqlCommand("RECP_NICU", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = dropdept.SelectedValue;

            //    SqlDataAdapter Adp1 = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter Adp1 = new SqlDataAdapter("select id,Sname from tblStaff where Deptid='" + dropdept.SelectedValue + "'", con);
            //    DataTable Dt1 = new DataTable();
            //    Adp1.Fill(Dt1);
            //    dropdoctor.DataSource = Dt1;
            //    dropdoctor.DataTextField = "Sname";
            //    dropdoctor.DataValueField = "id";
            //    dropdoctor.DataBind();
            //    dropdoctor.Items.Insert(0, "---select---");
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}