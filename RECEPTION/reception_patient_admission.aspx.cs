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

public partial class RECEPTION_reception_patient_admission : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2, rr;
    DataTable dt;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select max(VN) as VN from ADMISSION_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        if (dr.Read() && dr["VN"].ToString() != "")
        {
            num1 = dr["VN"].ToString();
            num1 = string.Format("IP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        }
        else
        {
            string qry2 = "select IPNO from IDGENERATE_TABLE";
            cmd = new SqlCommand(qry2, con);
            dr1 = null;
            dr.Close();
            dr1 = cmd.ExecuteReader();

            if (dr1.Read())
            {
                num1 = dr1["IPNO"].ToString();
                num1 = string.Format("IP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            }
            else
            {
                num1 = string.Format("IP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            }

        }
        //num1 = string.Format("IP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

        dr.Close();
        con.Close();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["AID"] = gr.Cells[0].Text;
        Response.Redirect("~/RECEPTION/reception_admission_reciept.aspx");

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
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
                div1.Visible = true;
                div2.Visible = false;
                txtcard.Visible = false;
                txtinsname.Visible = false;
                txtinsnumber.Visible = false;
                txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            con.Close();
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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ADMISSION_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            //using (SqlCommand cmd = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_PAGE";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da = new SqlDataAdapter("SELECT VN AS ID,NAME,PHONE,BEDNO FROM ADMISSION_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    // GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = dt;
            //    GridView1.DataKeyNames = new string[] { "ID" };
            //    GridView1.DataBind();
            //}
            SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BEDMATRIX");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropward.DataSource = Ds;
                dropward.DataTextField = "NAME";
                dropward.DataValueField = "ID";
                dropward.DataBind();
                dropward.Items.Insert(0, new ListItem("Please Select","0"));
            }
            //using (SqlCommand cmd1 = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDMATRIX";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT NAME FROM BED_MATRIX_TABLE WHERE ORGID='" + lblorgid.Text + "'", con);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    //dropbedno.SelectedIndex = 0;
            //    dropward.DataSource = dt1;
            //    dropward.DataTextField = "NAME";
            //    dropward.DataBind();
            //    dropward.Items.Insert(0, "Please Select");
            //}
            SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PROCEDURE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS1);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                droppackage.DataSource = Ds2;
                droppackage.DataTextField = "ProcedureName";
                droppackage.DataValueField = "Tariff";
                droppackage.DataBind();
                droppackage.Items.Insert(0, new ListItem("NO PACKAGE","0"));
            }
            //using (SqlCommand cmd2 = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    cmd2.CommandType = CommandType.StoredProcedure;
            //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PROCEDURE";
            //    cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd2.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    cmd2.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd2.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd2.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            //    //SqlDataAdapter da2 = new SqlDataAdapter("SELECT Tariff,ProcedureName FROM tblProcedureCost WHERE ORGID='" + lblorgid.Text + "' order by id desc", con);
            //    DataTable dt2 = new DataTable();
            //    da2.Fill(dt2);
            //    //dropbedno.SelectedIndex = 0;
            //    droppackage.DataSource = dt2;
            //    droppackage.DataTextField = "ProcedureName";
            //    droppackage.DataValueField = "Tariff";
            //    droppackage.DataBind();
            //    droppackage.Items.Insert(0, "NO PACKAGE");
            //}
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[1];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds3 = OBJ_METHOD.Get_DataSet("USP_empbroker", false, true, SQL_PARAMS2);
            if (Ds3.Tables[0].Rows.Count > 0)
            {
                dropreferedby.DataSource = Ds3;
                dropreferedby.DataTextField = "NAME";
                dropreferedby.DataValueField = "ID";
                dropreferedby.DataBind();
                dropreferedby.Items.Insert(0, new ListItem("NONE","0"));
            }
            //using (SqlCommand com = new SqlCommand("USP_empbroker", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    SqlDataAdapter da3 = new SqlDataAdapter(com);
            //    DataTable dt3 = new DataTable();
            //    da3.Fill(dt3);
            //    //dropbedno.SelectedIndex = 0;
            //    dropreferedby.DataSource = dt3;
            //    dropreferedby.DataTextField = "NAME";
            //    dropreferedby.DataValueField = "ID";
            //    dropreferedby.DataBind();
            //    dropreferedby.Items.Insert(0, "NONE");
            //}


            //con.Open();
            SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CORPO");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds4 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS1);
            if (Ds4.Tables[0].Rows.Count > 0)
            {
                dropinsurance.DataSource = Ds4;
                dropinsurance.DataTextField = "CNAME";
                dropinsurance.DataValueField = "ID";
                dropinsurance.DataBind();
                // dropinsurance.Items.Insert(0, "No Corporate");
                dropinsurance.Items.Insert(0,new ListItem("Please Select","0"));
            }
            //using (SqlCommand cmd4 = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    cmd4.CommandType = CommandType.StoredProcedure;
            //    cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CORPO";
            //    cmd4.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd4.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    cmd4.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd4.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd4.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            //    //SqlDataAdapter da4 = new SqlDataAdapter("select  ID,CNAME from Corporate_Table where ISACTIVE=1", con);
            //    DataTable dt4 = new DataTable();
            //    da4.Fill(dt4);
            //    //dropbedno.selectedindex = 0;
            //    dropinsurance.DataSource = dt4;
            //    dropinsurance.DataTextField = "CNAME";
            //    dropinsurance.DataValueField = "ID";
            //    dropinsurance.DataBind();
            //    // dropinsurance.Items.Insert(0, "No Corporate");
            //    dropinsurance.Items.Insert(0, "Please Select");
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }

    public void clear_control()
    {
        // txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        txtdate.Text = "";
        lblpid.Text = "";
        txtname.Text = "";
        txtphone.Text = "0";
        txtecontact.Text = "0";
        txtage.Text = "0";
        txtdate.Text = "0";
        Txtperad.Text = "0";
        //txtpref.Text = "0";
        txtregfee.Text = "0";
        txttempad.Text = "0";
        // txtdiease.Text = "0";
        btncreate.Visible = true;
        btnupdate.Visible = false;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        string message1 = string.Empty;
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtage.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            else if (txtregfee.Text == "")
            {
                txtregfee.Text = "0";

            }
            else if (Convert.ToDecimal(txtregfee.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropinsurance.SelectedIndex != 0 && txtinsname.Text == "")
            {
                string message = "alert('* Please!! Enter The Insurance Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropinsurance.SelectedIndex != 0 && txtinsnumber.Text == "")
            {
                string message = "alert('* Please!! Enter Insurance Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (droppayment.SelectedIndex != 0 && txtcard.Text == "")
            {
                string message = "alert('* Please Enter Card No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropward.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            else if (txtdate.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CORPO");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                auto();
                OBJ_METHOD = new DataMathods();
                SQL_PARAMS = new SqlParameter[32];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblpid.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, lbloutpatient.Text);
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());

                SQL_PARAMS[5] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 500, txtage.Text);
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, droprtype.Text);
                SQL_PARAMS[7] = OBJ_METHOD.createParams("@PHONE", SqlDbType.VarChar, 500, txtphone.Text);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@ECONTACT", SqlDbType.VarChar, 500, txtecontact.Text);
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@PREF", SqlDbType.VarChar, 500, dropreferedby.SelectedValue);

                SQL_PARAMS[10] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtdate.Text);
                SQL_PARAMS[11] = OBJ_METHOD.createParams("@PADDRESS", SqlDbType.VarChar, 500, Txtperad.Text);
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@TADDRESS", SqlDbType.VarChar, 500, txttempad.Text);
                SQL_PARAMS[13] = OBJ_METHOD.createParams("@RGFEE", SqlDbType.Decimal, 0, txtregfee.Text);
                SQL_PARAMS[14] = OBJ_METHOD.createParams("@DISEASE", SqlDbType.VarChar, 500, txtdiease.Text);

                SQL_PARAMS[15] = OBJ_METHOD.createParams("@USERNAME", SqlDbType.VarChar, 500, lblid.Text);
                SQL_PARAMS[16] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 500, lbluid.Text);
                SQL_PARAMS[17] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 5000, dropbedno.Text);
                SQL_PARAMS[18] = OBJ_METHOD.createParams("@WARD", SqlDbType.VarChar, 500, dropward.Text);
                SQL_PARAMS[19] = OBJ_METHOD.createParams("@FAMILY", SqlDbType.VarChar, 500, txtfalmilyhead.Text);

                SQL_PARAMS[20] = OBJ_METHOD.createParams("@INSURANCE", SqlDbType.VarChar, 500, dropinsurance.Text);
                SQL_PARAMS[21] = OBJ_METHOD.createParams("@PAC", SqlDbType.VarChar, 500, droppackage.SelectedValue);
                SQL_PARAMS[22] = OBJ_METHOD.createParams("@PACNAME", SqlDbType.VarChar, 500, droppackage.SelectedItem.Text);
                SQL_PARAMS[23] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, TXTID.Text);
                SQL_PARAMS[24] = OBJ_METHOD.createParams("@INSURANCENAME", SqlDbType.VarChar, 500, txtinsname.Text);

                SQL_PARAMS[25] = OBJ_METHOD.createParams("@INSURANCENO", SqlDbType.VarChar, 500, txtinsnumber.Text);
                SQL_PARAMS[26] = OBJ_METHOD.createParams("@PMODE", SqlDbType.VarChar, 500, droppayment.Text);
                SQL_PARAMS[27] = OBJ_METHOD.createParams("@PNO", SqlDbType.VarChar, 0, txtcard.Text);
                SQL_PARAMS[28] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[29] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                SQL_PARAMS[30] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 0, LBLUHID.Text);
                if (dropinsurance.SelectedIndex != 0)
                {
                    SQL_PARAMS[31] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 0, dropinsurance.SelectedValue);
                }
                else 
                {
                    SQL_PARAMS[31] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 0, "0");
                }

                OBJ_METHOD.ExecuteProceedure("usp_admission", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                { 
                    SQL_PARAMS = new SqlParameter[15];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@REF", SqlDbType.VarChar, 500, dropreferedby.SelectedValue);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 500, LBLUHID.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@PCHARGE", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@PP", SqlDbType.Decimal, 0, "0.00");

                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@LCHARGE", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@LP", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@BCHARGE", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@BP", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@RCHARGE", SqlDbType.Decimal, 0, "0.00");

                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@RP", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@MCHARGE", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@MP", SqlDbType.Decimal, 0, "0.00");
                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[14] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                    OBJ_METHOD.ExecuteProceedure("usp_ref_tran", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    { 
                        SQL_PARAMS = new SqlParameter[8];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, Session["UID"].ToString());
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@Date", SqlDbType.Date, 500, DateTime.Now.ToString());
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@Collection", SqlDbType.Decimal, 0, txtregfee.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@Paid", SqlDbType.Decimal, 0, "0.00");

                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, "");
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                        OBJ_METHOD.ExecuteProceedure("usp_UserCollection", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            if (droppackage.SelectedIndex != 0)
                            {
                                SQL_PARAMS = new SqlParameter[14];

                                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 500, txtdate.Text);
                                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, TXTID.Text);
                                SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblpid.Text);
                                SQL_PARAMS[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);

                                SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "PATIENT ADMISSION");
                                SQL_PARAMS[6] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, '-' + txtregfee.Text);
                                SQL_PARAMS[7] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                                SQL_PARAMS[8] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, txtregfee.Text);
                                SQL_PARAMS[9] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, "0");

                                SQL_PARAMS[10] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, TXTID.Text);
                                SQL_PARAMS[11] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "ADVANCE DETAILS");
                                SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_ADMISSION", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                                if (OBJ_METHOD._RESULT > 0)
                                {
                                    SQL_PARAMS = new SqlParameter[14];

                                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 500, txtdate.Text);
                                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, TXTID.Text);
                                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblpid.Text);
                                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);

                                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "PACKAGE COST" + '-' + droppackage.SelectedItem.Text);
                                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, droppackage.SelectedValue);
                                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, "0");
                                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, droppackage.SelectedValue);

                                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, TXTID.Text);
                                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "OTHER CHARGES");
                                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                                    OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                                    if (OBJ_METHOD._RESULT > 0)
                                    {
                                        OBJ_METHOD.commitOrRollbackTran("commit");
                                        clear_control();
                                    }
                                }
                            }
                            else
                            {
                                SQL_PARAMS = new SqlParameter[14];

                                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 500, txtdate.Text);
                                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, TXTID.Text);
                                SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblpid.Text);
                                SQL_PARAMS[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);

                                SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "PATIENT ADMISSION");
                                SQL_PARAMS[6] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, '-' + txtregfee.Text);
                                SQL_PARAMS[7] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                                SQL_PARAMS[8] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, txtregfee.Text);
                                SQL_PARAMS[9] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, "0");

                                SQL_PARAMS[10] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, TXTID.Text);
                                SQL_PARAMS[11] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "ADVANCE DETAILS");
                                SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_ADMISSION", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                                if (OBJ_METHOD._RESULT > 0)
                                {
                                    OBJ_METHOD.commitOrRollbackTran("commit");
                                    Session["AID"] = TXTID.Text;
                                    clear_control();
                                }
                            }
                        }
                    }
                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
            else
            {
                
                string message = "alert('The selected bed is not available. Try new bed.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;

            }
            
            #region OLDCODE
            //using (SqlCommand com1 = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    com1.CommandType = CommandType.StoredProcedure;
            //    com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED_STATUS";
            //    com1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    com1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    com1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    com1.Parameters.Add("@VN", SqlDbType.VarChar).Value = "";
            //    com1.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //    //SqlCommand com1 = new SqlCommand("select * from BED_MATRIX_TABLE where BEDNO='" + dropbedno.Text + "' AND STATUS='AVAILABLE' and ORGID='" + lblorgid.Text + "'", con);
            //    dr = com1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        dr.Close();
            //        auto();
            //        using (SqlCommand cmd1 = new SqlCommand("usp_admission", con))
            //        {
            //            cmd1.CommandType = CommandType.StoredProcedure;
            //            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblpid.Text;
            //            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //            cmd1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = lbloutpatient.Text;
            //            cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();

            //            cmd1.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
            //            cmd1.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = droprtype.Text;
            //            cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtphone.Text;
            //            cmd1.Parameters.Add("@ECONTACT", SqlDbType.VarChar).Value = txtecontact.Text;
            //            cmd1.Parameters.Add("@PREF", SqlDbType.VarChar).Value = dropreferedby.SelectedValue;

            //            cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
            //            cmd1.Parameters.Add("@PADDRESS", SqlDbType.VarChar).Value = Txtperad.Text;
            //            cmd1.Parameters.Add("@TADDRESS", SqlDbType.VarChar).Value = txttempad.Text;
            //            cmd1.Parameters.Add("@RGFEE", SqlDbType.Decimal).Value = txtregfee.Text;
            //            cmd1.Parameters.Add("@DISEASE", SqlDbType.VarChar).Value = txtdiease.Text;

            //            cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
            //            cmd1.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
            //            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //            cmd1.Parameters.Add("@WARD", SqlDbType.VarChar).Value = dropward.Text;
            //            cmd1.Parameters.Add("@FAMILY", SqlDbType.VarChar).Value = txtfalmilyhead.Text;

            //            cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = dropinsurance.Text;
            //            cmd1.Parameters.Add("@PAC", SqlDbType.VarChar).Value = droppackage.SelectedValue.ToString();
            //            cmd1.Parameters.Add("@PACNAME", SqlDbType.VarChar).Value = droppackage.SelectedItem.Text.ToString();
            //            cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = TXTID.Text;
            //            cmd1.Parameters.Add("@INSURANCENAME", SqlDbType.VarChar).Value = txtinsname.Text.ToUpper();

            //            cmd1.Parameters.Add("@INSURANCENO", SqlDbType.VarChar).Value = txtinsnumber.Text.ToUpper();
            //            cmd1.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.Text;
            //            cmd1.Parameters.Add("@PNO", SqlDbType.VarChar).Value = txtcard.Text;
            //            cmd1.Parameters.Add("@UHID", SqlDbType.VarChar).Value = LBLUHID.Text;
            //            if (dropinsurance.SelectedIndex != 0)
            //            {
            //                cmd1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropinsurance.SelectedValue;
            //            }
            //            else
            //            {
            //                cmd1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "0";
            //            }
            //            cmd1.ExecuteNonQuery();
            //            using (SqlCommand cm = new SqlCommand("usp_ref_tran", con))
            //            {
            //                cm.CommandType = CommandType.StoredProcedure;
            //                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //                cm.Parameters.Add("@REF", SqlDbType.VarChar).Value = dropreferedby.SelectedValue;
            //                cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = LBLUHID.Text;
            //                cm.Parameters.Add("@PCHARGE", SqlDbType.Decimal).Value = "0.00";
            //                cm.Parameters.Add("@PP", SqlDbType.Decimal).Value = "0.00";
            //                cm.Parameters.Add("@LCHARGE", SqlDbType.Decimal).Value = "0.00";
            //                cm.Parameters.Add("@LP", SqlDbType.Decimal).Value = "0.00";
            //                cm.Parameters.Add("@BCHARGE", SqlDbType.Decimal).Value = "0.00";
            //                cm.Parameters.Add("@BP", SqlDbType.Decimal).Value = "0.00";
            //                cm.Parameters.Add("@RCHARGE", SqlDbType.Decimal).Value = "0.00";
            //                cm.Parameters.Add("@RP", SqlDbType.Decimal).Value = "0.00";
            //                cm.Parameters.Add("@MCHARGE", SqlDbType.Decimal).Value = "0.00";
            //                cm.Parameters.Add("@MP", SqlDbType.Decimal).Value = "0.00";
            //                cm.ExecuteNonQuery();
            //                using (SqlCommand cmd = new SqlCommand("usp_UserCollection", con))
            //                {
            //                    cmd.CommandType = CommandType.StoredProcedure;
            //                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
            //                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
            //                    cmd.Parameters.Add("@Collection", SqlDbType.Decimal).Value = txtregfee.Text;
            //                    cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
            //                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
            //                    cmd.ExecuteNonQuery();
            //                }
            //            }

            //            if (droppackage.SelectedIndex != 0)
            //            {
            //                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_ADMISSION", con))
            //                {
            //                    cm.CommandType = CommandType.StoredProcedure;
            //                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
            //                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
            //                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT ADMISSION";
            //                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = '-' + txtregfee.Text;
            //                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtregfee.Text;
            //                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
            //                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = TXTID.Text;
            //                    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
            //                    cm.ExecuteNonQuery();
            //                }
            //                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
            //                {
            //                    cm.CommandType = CommandType.StoredProcedure;
            //                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
            //                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
            //                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PACKAGE COST" + '-' + droppackage.SelectedItem.Text;
            //                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = droppackage.SelectedValue;
            //                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
            //                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = droppackage.SelectedValue;
            //                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = TXTID.Text;
            //                    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //                    cm.ExecuteNonQuery();
            //                }
            //            }
            //            else
            //            {
            //                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_ADMISSION", con))
            //                {
            //                    cm.CommandType = CommandType.StoredProcedure;
            //                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
            //                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
            //                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT ADMISSION";
            //                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = '-' + txtregfee.Text;
            //                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtregfee.Text;
            //                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
            //                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = TXTID.Text;
            //                    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
            //                    cm.ExecuteNonQuery();
            //                }
            //            }
            //            binddata();
            //            Session["AID"] = TXTID.Text;
            //        }

            //    }
            //    else
            //    {
            //        dr.Close();
            //        string message = "alert('The selected bed is not available. Try new bed.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;

            //    }
            //}
            //con.Close();
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
        Response.Redirect("~/RECEPTION/reception_admission_reciept.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_VN");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID",SqlDbType.VarChar,500,lblorgid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VN",SqlDbType.VarChar,500,slno);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID",SqlDbType.Int,0,Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                div2.Visible = true;
                div1.Visible = false;
                btncreate.Visible = false;
                btnupdate.Visible = true;
                lblpid.Text = DS.Tables[0].Rows[0]["ID"].ToString();
                lbloutpatient.Text = DS.Tables[0].Rows[0]["PTYPE"].ToString();
                txtname.Text = DS.Tables[0].Rows[0]["NAME"].ToString();
                txtage.Text = DS.Tables[0].Rows[0]["AGE"].ToString();
                droprtype.Text = DS.Tables[0].Rows[0]["GENDER"].ToString();
                txtphone.Text = DS.Tables[0].Rows[0]["PHONE"].ToString();
                txtecontact.Text = DS.Tables[0].Rows[0]["ECONTACT"].ToString();
                txtdate.Text = DS.Tables[0].Rows[0]["DATE"].ToString();
                Txtperad.Text = DS.Tables[0].Rows[0]["PADDRESS"].ToString();
                txttempad.Text = DS.Tables[0].Rows[0]["TADDRESS"].ToString();
                txtregfee.Text = DS.Tables[0].Rows[0]["RGFEE"].ToString();
                txtdiease.Text = DS.Tables[0].Rows[0]["DISEASE"].ToString();
                dropward.SelectedItem.Value = (string)DS.Tables[0].Rows[0]["WARD"];
                dropbedno.SelectedItem.Value = DS.Tables[0].Rows[0]["BEDNO"].ToString();
                txtfalmilyhead.Text = DS.Tables[0].Rows[0]["FAMILY"].ToString();
                dropinsurance.SelectedValue = DS.Tables[0].Rows[0]["INSURANCE"].ToString();
                txtinsname.Text = DS.Tables[0].Rows[0]["INSURANCENAME"].ToString();
                txtinsnumber.Text = DS.Tables[0].Rows[0]["INSURANCENO"].ToString();
                droppayment.Text = DS.Tables[0].Rows[0]["PMODE"].ToString();
                txtcard.Text = DS.Tables[0].Rows[0]["PNO"].ToString();
                LBLUHID.Text = DS.Tables[0].Rows[0]["UHID"].ToString();
                TXTID.Text = DS.Tables[0].Rows[0]["VN"].ToString();
                droppackage.SelectedItem.Text = DS.Tables[0].Rows[0]["PAC"].ToString();
                droppackage.SelectedItem.Text = DS.Tables[0].Rows[0]["PACNAME"].ToString();
            }
            if (dropinsurance.SelectedIndex != 0)
            {
                txtinsname.Visible = true;
                txtinsnumber.Visible = true;
            }
            else
            {
                txtinsname.Visible = false;
                txtinsnumber.Visible = false;
            }
            //using (SqlCommand com = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_VN";
            //    com.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    com.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    com.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    com.Parameters.Add("@VN", SqlDbType.VarChar).Value = slno;
            //    com.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //    //SqlCommand com = new SqlCommand("select * from ADMISSION_TABLE where VN='" + slno + "'", con);
            //    dr = com.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        div2.Visible = true;
            //        div1.Visible = false;
            //        btncreate.Visible = false;
            //        btnupdate.Visible = true;
            //        lblpid.Text = dr["ID"].ToString();
            //        lbloutpatient.Text = dr["PTYPE"].ToString();
            //        txtname.Text = dr["NAME"].ToString();
            //        txtage.Text = dr["AGE"].ToString();
            //        droprtype.Text = dr["GENDER"].ToString();
            //        txtphone.Text = dr["PHONE"].ToString();
            //        txtecontact.Text = dr["ECONTACT"].ToString();
            //        // txtpref.Text = dr["PREF"].ToString();
            //        txtdate.Text = dr["DATE"].ToString();
            //        Txtperad.Text = dr["PADDRESS"].ToString();
            //        txttempad.Text = dr["TADDRESS"].ToString();
            //        txtregfee.Text = dr["RGFEE"].ToString();
            //        txtdiease.Text = dr["DISEASE"].ToString();
            //        dropward.Text = dr["WARD"].ToString();
            //        dropbedno.Text = dr["BEDNO"].ToString();
            //        txtfalmilyhead.Text = dr["FAMILY"].ToString();
            //        dropinsurance.SelectedValue = dr["INSURANCE"].ToString();
            //        txtinsname.Text = dr["INSURANCENAME"].ToString();
            //        txtinsnumber.Text = dr["INSURANCENO"].ToString();
            //        droppayment.Text = dr["PMODE"].ToString();
            //        txtcard.Text = dr["PNO"].ToString();
            //        LBLUHID.Text = dr["UHID"].ToString();
            //        TXTID.Text = dr["VN"].ToString();
            //        droppackage.SelectedValue = dr["PAC"].ToString();
            //        droppackage.SelectedItem.Text = dr["PACNAME"].ToString();

            //    }
            //    dr.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            
            string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, slno);

            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_ADMISSION", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, slno);

                OBJ_METHOD.ExecuteProceedure("usp_admission", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clear_control();
                }
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            //using (SqlCommand cmd1 = new SqlCommand("usp_admission", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = lbloutpatient.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd1.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
            //    cmd1.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = droprtype.Text;
            //    cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtphone.Text;
            //    cmd1.Parameters.Add("@ECONTACT", SqlDbType.VarChar).Value = txtecontact.Text;
            //    cmd1.Parameters.Add("@PREF", SqlDbType.VarChar).Value = dropreferedby.SelectedValue;
            //    cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
            //    cmd1.Parameters.Add("@PADDRESS", SqlDbType.VarChar).Value = Txtperad.Text;
            //    cmd1.Parameters.Add("@TADDRESS", SqlDbType.VarChar).Value = txttempad.Text;
            //    cmd1.Parameters.Add("@RGFEE", SqlDbType.Decimal).Value = 0;
            //    cmd1.Parameters.Add("@DISEASE", SqlDbType.VarChar).Value = txtdiease.Text;
            //    cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
            //    cmd1.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
            //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cmd1.Parameters.Add("@WARD", SqlDbType.VarChar).Value = dropward.Text;
            //    cmd1.Parameters.Add("@FAMILY", SqlDbType.VarChar).Value = txtfalmilyhead.Text;
            //    cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = dropinsurance.Text;
            //    cmd1.Parameters.Add("@PAC", SqlDbType.VarChar).Value = droppackage.SelectedValue.ToString();
            //    cmd1.Parameters.Add("@PACNAME", SqlDbType.VarChar).Value = droppackage.SelectedItem.Text.ToString();
            //    cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = slno;
            //    cmd1.Parameters.Add("@INSURANCENAME", SqlDbType.VarChar).Value = txtinsname.Text.ToUpper();
            //    cmd1.Parameters.Add("@INSURANCENO", SqlDbType.VarChar).Value = txtinsnumber.Text.ToUpper();
            //    cmd1.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.Text;
            //    cmd1.Parameters.Add("@PNO", SqlDbType.VarChar).Value = txtcard.Text;
            //    cmd1.Parameters.Add("@UHID", SqlDbType.VarChar).Value = LBLUHID.Text;
            //    cmd1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = "";
            //    cmd1.ExecuteNonQuery();


            //}
            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_ADMISSION", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT ADMISSION";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@CREDIT", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = slno;
            //    cm.ExecuteNonQuery();
            //}
            ////SqlDataAdapter da = new SqlDataAdapter("delete from PA_TRANS where VOUCHERNO='" + TXTID.Text + "'", con);
            ////ds = new DataSet();
            ////da.Fill(ds);
            //binddata();
            //con.Close();

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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ADMISSION_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            //using (SqlCommand cmd = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_PAGE";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da = new SqlDataAdapter("SELECT VN AS ID,NAME,PHONE,BEDNO FROM ADMISSION_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView1.SelectedIndex = 0;
            //    GridView1.DataSource = dt;
            //    GridView1.PageIndex = e.NewPageIndex;
            //    GridView1.DataKeyNames = new string[] { "ID" };
            //    GridView1.DataBind();
            //}
            
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
                string message4 = "alert(' Please!! Enter The OPD No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message4, true);
                return;
            }

            if (txtspid.Text != "")
            {
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_RESRV_ID");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtspid.Text);

                DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_RESERAVATION", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    string message4 = "alert(' Patient With OPD Number-  " + txtspid.Text + " Is already Admitted.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message4, true);
                    return;
                }
                //using (SqlCommand cmd = new SqlCommand("RECP_RESERAVATION", con))
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RESRV_ID";
                //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtspid.Text;
                //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                //    cmd.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "NULL";
                //    SqlDataAdapter Adp2 = new SqlDataAdapter(cmd);
                //    //SqlCommand cmd = con.CreateCommand();
                //    //cmd.CommandText = "  Select * from [ADMISSION_TABLE] where ID='" + txtpatientid.Text + "'";
                //    rr = cmd.ExecuteReader();
                //    if (rr.Read() == true)
                //    {

                //        string message4 = "alert(' Patient With OPD Number-  " + txtspid.Text + " Is already Admitted.')";
                //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message4, true);
                //        return;
                //    }
                //    rr.Close();
                //}
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ID");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, txtspid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("[RECP_ADMISSION_SELECT]", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                div2.Visible = true;
                div1.Visible = false;
                lblpid.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                LBLUHID.Text = Ds.Tables[0].Rows[0]["UHID"].ToString();
                txtname.Text = Ds.Tables[0].Rows[0]["PNAME"].ToString();
                txtage.Text = Ds.Tables[0].Rows[0]["AGE"].ToString();
                droprtype.Text = Ds.Tables[0].Rows[0]["GENDER"].ToString();
                txtphone.Text = Ds.Tables[0].Rows[0]["MOBNO"].ToString();
                txtecontact.Text = Ds.Tables[0].Rows[0]["GURDINMOBLE"].ToString();
                Txtperad.Text = Ds.Tables[0].Rows[0]["ADDRES"].ToString();
                txttempad.Text = Ds.Tables[0].Rows[0]["ADDRES"].ToString();
                txtdiease.Text = Ds.Tables[0].Rows[0]["PRESNTCOMPL"].ToString();
                Session["D_ID"] = Ds.Tables[0].Rows[0]["CORPORATE"].ToString();
            }
            else
            {

                string message = "alert('* No Data Found.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            #region oldcode
            //using (SqlCommand com = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ID";
            //    com.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    com.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    com.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    com.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtspid.Text;
            //    com.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //    //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlCommand com = new SqlCommand("select * from  REGISTRATION_TBL where ID='" + txtspid.Text + "'", con);
            //    dr = com.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        div2.Visible = true;
            //        div1.Visible = false;
            //        //btncreate.Visible = false;
            //        //btnupdate.Visible = true;
            //        lblpid.Text = dr["ID"].ToString();
            //        LBLUHID.Text = dr["UHID"].ToString();
            //        //   lbloutpatient.Text = dr["PTYPE"].ToString();
            //        txtname.Text = dr["PNAME"].ToString();
            //        txtage.Text = dr["AGE"].ToString();
            //        droprtype.Text = dr["GENDER"].ToString();
            //        txtphone.Text = dr["MOBNO"].ToString();
            //        txtecontact.Text = dr["GURDINMOBLE"].ToString();
            //        //txtpref.Text = dr["PREF"].ToString();
            //        // txtdate.Text = dr["DATE"].ToString();
            //        Txtperad.Text = dr["ADDRES"].ToString();
            //        //  txttempad.Text = dr["TADDRESS"].ToString();
            //        // txtregfee.Text = dr["RGFEE"].ToString();
            //        txtdiease.Text = dr["PRESNTCOMPL"].ToString();
            //        Session["D_ID"] = dr["CORPORATE"].ToString();
            //        dr.Close();
            //    }

            //    else
            //    {

            //        string message = "alert('* No Data Found.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //        return;
            //    }
            #endregion
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CORPO");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropinsurance.DataSource = Ds1;
                dropinsurance.DataTextField = "CNAME";
                dropinsurance.DataValueField = "ID";
                dropinsurance.DataBind();
                dropinsurance.Items.Insert(0,new ListItem("Please Select","0"));
                dropinsurance.SelectedValue = Session["D_ID"].ToString();
            }
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[1];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds3 = OBJ_METHOD.Get_DataSet("USP_empbroker", false, true, SQL_PARAMS2);
            if (Ds3.Tables[0].Rows.Count > 0)
            {
                dropreferedby.DataSource = Ds3;
                dropreferedby.DataTextField = "NAME";
                dropreferedby.DataValueField = "ID";
                dropreferedby.DataBind();
                dropreferedby.Items.Insert(0, new ListItem("NONE", "0"));
            }
            //using (SqlCommand cmd4 = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    cmd4.CommandType = CommandType.StoredProcedure;
            //    cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CORPO";
            //    cmd4.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd4.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    cmd4.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd4.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd4.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            //    //SqlDataAdapter da4 = new SqlDataAdapter("select  ID,CNAME from Corporate_Table where ISACTIVE=1", con);
            //    DataTable dt4 = new DataTable();
            //    da4.Fill(dt4);
            //    //dropbedno.selectedindex = 0;
            //    dropinsurance.DataSource = dt4;
            //    dropinsurance.DataTextField = "CNAME";
            //    dropinsurance.DataValueField = "ID";
            //    dropinsurance.DataBind();
            //    dropinsurance.Items.Insert(0, "-SELECT--");
            //    dropinsurance.SelectedValue = Session["D_ID"].ToString();
            //}
            if (dropinsurance.SelectedIndex != 0)
            {
                txtinsname.Visible = true;
                txtinsnumber.Visible = true;
            }
            else
            {
                txtinsname.Visible = false;
                txtinsnumber.Visible = false;
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }


    }
    protected void btnshowph_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_MOBNO");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@MOBNO", SqlDbType.VarChar, 500, txtsmobile.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                div2.Visible = true;
                div1.Visible = false;
                lblpid.Text = DS1.Tables[0].Rows[0]["ID"].ToString();
                LBLUHID.Text = DS1.Tables[0].Rows[0]["UHID"].ToString();
                txtname.Text = DS1.Tables[0].Rows[0]["PNAME"].ToString();
                txtage.Text = DS1.Tables[0].Rows[0]["AGE"].ToString();
                droprtype.Text = DS1.Tables[0].Rows[0]["GENDER"].ToString();
                txtphone.Text = DS1.Tables[0].Rows[0]["MOBNO"].ToString();
                txtecontact.Text = DS1.Tables[0].Rows[0]["GURDINMOBLE"].ToString();
                Txtperad.Text = DS1.Tables[0].Rows[0]["ADDRES"].ToString();
                txtdiease.Text = DS1.Tables[0].Rows[0]["PRESNTCOMPL"].ToString();
                Session["D_ID"] = DS1.Tables[0].Rows[0]["CORPORATE"].ToString();
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CORPO");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtspid.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropinsurance.DataSource = Ds1;
                dropinsurance.DataTextField = "CNAME";
                dropinsurance.DataValueField = "ID";
                dropinsurance.DataBind();
                dropinsurance.Items.Insert(0,new ListItem("Please select","0"));
                dropinsurance.SelectedValue = Session["D_ID"].ToString();
            }
            
            if (dropinsurance.SelectedIndex != 0)
            {
                txtinsname.Visible = true;
                txtinsnumber.Visible = true;
            }
            else
            {
                txtinsname.Visible = false;
                txtinsnumber.Visible = false;
            }
            
            #region Oldcode

            //using (SqlCommand com = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_MOBNO";
            //    com.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    com.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    com.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    com.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtspid.Text;
            //    com.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = txtsmobile.Text;
            //    //SqlCommand com = new SqlCommand("select * from REGISTRATION_TBL where MOBNO='" + txtsmobile.Text + "'order by ID DESC", con);
            //    dr = com.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        div2.Visible = true;
            //        div1.Visible = false;
            //        //btncreate.Visible = false;
            //        //btnupdate.Visible = true;
            //        lblpid.Text = dr["ID"].ToString();
            //        LBLUHID.Text = dr["UHID"].ToString();
            //        // lbloutpatient.Text = dr["PTYPE"].ToString();
            //        txtname.Text = dr["PNAME"].ToString();
            //        txtage.Text = dr["AGE"].ToString();
            //        droprtype.Text = dr["GENDER"].ToString();
            //        txtphone.Text = dr["MOBNO"].ToString();
            //        txtecontact.Text = dr["GURDINMOBLE"].ToString();
            //        //txtpref.Text = dr["PREF"].ToString();
            //        // txtdate.Text = dr["DATE"].ToString();
            //        Txtperad.Text = dr["ADDRES"].ToString();
            //        //  txttempad.Text = dr["TADDRESS"].ToString();
            //        // txtregfee.Text = dr["RGFEE"].ToString();
            //        txtdiease.Text = dr["PRESNTCOMPL"].ToString();
            //        Session["D_ID"] = dr["CORPORATE"].ToString();
            //        dr.Close();
            //    }

            //    else
            //    {

            //        string message = "alert('* No Data Found.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //        return;
            //    }
                //using (SqlCommand cmd4 = new SqlCommand("RECP_ADMISSION_SELECT", con))
                //{
                //    cmd4.CommandType = CommandType.StoredProcedure;
                //    cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CORPO";
                //    cmd4.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //    cmd4.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                //    cmd4.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                //    cmd4.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                //    cmd4.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
                //    SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                //    //SqlDataAdapter da4 = new SqlDataAdapter("select  ID,CNAME from Corporate_Table where ISACTIVE=1", con);
                //    DataTable dt4 = new DataTable();
                //    da4.Fill(dt4);
                //    //dropbedno.selectedindex = 0;
                //    dropinsurance.DataSource = dt4;
                //    dropinsurance.DataTextField = "CNAME";
                //    dropinsurance.DataValueField = "ID";
                //    dropinsurance.DataBind();
                //    dropinsurance.Items.Insert(0, "--SELECT--");
                //    dropinsurance.SelectedValue = Session["D_ID"].ToString();
                //}
            #endregion
                
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnUHID_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_UHID");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 500, TXTUHID.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                div2.Visible = true;
                div1.Visible = false;
                lblpid.Text = DS1.Tables[0].Rows[0]["ID"].ToString();
                LBLUHID.Text = DS1.Tables[0].Rows[0]["UHID"].ToString();
                txtname.Text = DS1.Tables[0].Rows[0]["PNAME"].ToString();
                txtage.Text = DS1.Tables[0].Rows[0]["AGE"].ToString();
                droprtype.Text = DS1.Tables[0].Rows[0]["GENDER"].ToString();
                txtphone.Text = DS1.Tables[0].Rows[0]["MOBNO"].ToString();
                txtecontact.Text = DS1.Tables[0].Rows[0]["GURDINMOBLE"].ToString();
                Txtperad.Text = DS1.Tables[0].Rows[0]["ADDRES"].ToString();
                txtdiease.Text = DS1.Tables[0].Rows[0]["PRESNTCOMPL"].ToString();
                Session["D_ID"] = DS1.Tables[0].Rows[0]["CORPORATE"].ToString();
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_CORPO");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtspid.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropinsurance.DataSource = Ds1;
                dropinsurance.DataTextField = "CNAME";
                dropinsurance.DataValueField = "ID";
                dropinsurance.DataBind();
                dropinsurance.Items.Insert(0, new ListItem("Please select", "0"));
                dropinsurance.SelectedValue = Session["D_ID"].ToString();
            }
            
            #region old code
            //using (SqlCommand com = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_UHID";
            //    com.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    com.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //    com.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    com.Parameters.Add("@VN", SqlDbType.VarChar).Value = TXTUHID.Text;
            //    com.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = txtsmobile.Text;
            //    //SqlCommand com = new SqlCommand("select * from PATIENT_REG_TABLE where UHID='" + TXTUHID.Text + "'order by ID DESC", con);
            //    dr = com.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        div2.Visible = true;
            //        div1.Visible = false;
            //        //btncreate.Visible = false;
            //        //btnupdate.Visible = true;
            //        lblpid.Text = dr["ID"].ToString();
            //        LBLUHID.Text = dr["UHID"].ToString();
            //        // lbloutpatient.Text = dr["PTYPE"].ToString();
            //        txtname.Text = dr["PNAME"].ToString();
            //        txtage.Text = dr["AGE"].ToString();
            //        droprtype.Text = dr["GENDER"].ToString();
            //        txtphone.Text = dr["MOBNO"].ToString();
            //        txtecontact.Text = dr["GURDINMOBLE"].ToString();
            //        //txtpref.Text = dr["PREF"].ToString();
            //        // txtdate.Text = dr["DATE"].ToString();
            //        Txtperad.Text = dr["ADDRES"].ToString();
            //        //  txttempad.Text = dr["TADDRESS"].ToString();
            //        // txtregfee.Text = dr["RGFEE"].ToString();
            //        txtdiease.Text = dr["PRESNTCOMPL"].ToString();
            //        Session["D_ID"] = dr["CORPORATE"].ToString();
            //        dr.Close();
            //    }

            //    else
            //    {

            //        string message = "alert('* No Data Found.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //        return;
            //    }
            //    using (SqlCommand cmd4 = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //    {
            //        cmd4.CommandType = CommandType.StoredProcedure;
            //        cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CORPO";
            //        cmd4.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //        cmd4.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            //        cmd4.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //        cmd4.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //        cmd4.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "NULL";
            //        SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            //        //SqlDataAdapter da4 = new SqlDataAdapter("select  ID,CNAME from Corporate_Table where ISACTIVE=1", con);
            //        DataTable dt4 = new DataTable();
            //        da4.Fill(dt4);
            //        //dropbedno.selectedindex = 0;
            //        dropinsurance.DataSource = dt4;
            //        dropinsurance.DataTextField = "CNAME";
            //        dropinsurance.DataValueField = "ID";
            //        dropinsurance.DataBind();
            //        dropinsurance.Items.Insert(0, "--SELECT--");
            //        dropinsurance.SelectedValue = Session["D_ID"].ToString();
            //    }
            #endregion
            if (dropinsurance.SelectedIndex != 0)
                {
                    txtinsname.Visible = true;
                    txtinsnumber.Visible = true;
                }
                else
                {
                    txtinsname.Visible = false;
                    txtinsnumber.Visible = false;
                }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (lblpid.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtname.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtage.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            

            else if (txtregfee.Text == "")
            {
                txtregfee.Text = "0";

            }
            else if (Convert.ToDecimal(txtregfee.Text) < 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            
            else if (droppayment.SelectedIndex != 0 && txtcard.Text == "")
            {
                string message = "alert('* Please Enter Card No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropward.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[32];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblpid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, lbloutpatient.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text.ToUpper());

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 500, txtage.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, droprtype.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@PHONE", SqlDbType.VarChar, 500, txtphone.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@ECONTACT", SqlDbType.VarChar, 500, txtecontact.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@PREF", SqlDbType.VarChar, 500, dropreferedby.SelectedValue);

            SQL_PARAMS[10] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtdate.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@PADDRESS", SqlDbType.VarChar, 500, Txtperad.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@TADDRESS", SqlDbType.VarChar, 500, txttempad.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@RGFEE", SqlDbType.Decimal, 0, txtregfee.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@DISEASE", SqlDbType.VarChar, 500, txtdiease.Text);

            SQL_PARAMS[15] = OBJ_METHOD.createParams("@USERNAME", SqlDbType.VarChar, 500, lblid.Text);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 500, lbluid.Text);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 5000, dropbedno.Text);
            SQL_PARAMS[18] = OBJ_METHOD.createParams("@WARD", SqlDbType.VarChar, 500, dropward.Text);
            SQL_PARAMS[19] = OBJ_METHOD.createParams("@FAMILY", SqlDbType.VarChar, 500, txtfalmilyhead.Text);

            SQL_PARAMS[20] = OBJ_METHOD.createParams("@INSURANCE", SqlDbType.VarChar, 500, dropinsurance.Text);
            SQL_PARAMS[21] = OBJ_METHOD.createParams("@PAC", SqlDbType.VarChar, 500, droppackage.SelectedValue);
            SQL_PARAMS[22] = OBJ_METHOD.createParams("@PACNAME", SqlDbType.VarChar, 500, droppackage.SelectedItem.Text);
            SQL_PARAMS[23] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, TXTID.Text);
            SQL_PARAMS[24] = OBJ_METHOD.createParams("@INSURANCENAME", SqlDbType.VarChar, 500, txtinsname.Text);

            SQL_PARAMS[25] = OBJ_METHOD.createParams("@INSURANCENO", SqlDbType.VarChar, 500, txtinsnumber.Text);
            SQL_PARAMS[26] = OBJ_METHOD.createParams("@PMODE", SqlDbType.VarChar, 500, droppayment.Text);
            SQL_PARAMS[27] = OBJ_METHOD.createParams("@PNO", SqlDbType.VarChar, 0, txtcard.Text);
            SQL_PARAMS[28] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[29] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            SQL_PARAMS[30] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 0, LBLUHID.Text);
            if (dropinsurance.SelectedIndex != 0)
            {
                SQL_PARAMS[31] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 0, dropinsurance.SelectedValue);
            }
            else 
            {
                SQL_PARAMS[31] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 0, "0");
            }

            OBJ_METHOD.ExecuteProceedure("usp_admission", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                if (droppackage.SelectedIndex != 0)
                {
                    SQL_PARAMS = new SqlParameter[14];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 500, txtdate.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblpid.Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);

                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "PATIENT ADMISSION");
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, '-' + txtregfee.Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, txtregfee.Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, "0");

                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "ADVANCE DETAILS");
                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                    OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_ADMISSION", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        SQL_PARAMS = new SqlParameter[5];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "PACKAGE COST" + '-' + droppackage.SelectedItem.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, droppackage.SelectedValue);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "OTHER CHARGES");

                        OBJ_METHOD.ExecuteProceedure("RECP_ADMISSION_UPDATE", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            OBJ_METHOD.commitOrRollbackTran("commit");
                            clear_control();
                        }
                    }
                }
                else
                {
                    SQL_PARAMS = new SqlParameter[14];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 500, txtdate.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblpid.Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, dropbedno.Text);

                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "PATIENT ADMISSION");
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, '-' + txtregfee.Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, txtregfee.Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, "0");

                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, TXTID.Text);
                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "ADVANCE DETAILS");
                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                    OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_ADMISSION", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        Session["AID"] = TXTID.Text;
                        clear_control();
                    }
                }
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
        
            //using (SqlCommand cmd1 = new SqlCommand("usp_admission", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblpid.Text;
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = lbloutpatient.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text.ToUpper();
            //    cmd1.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
            //    cmd1.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = droprtype.Text;
            //    cmd1.Parameters.Add("@PHONE", SqlDbType.VarChar).Value = txtphone.Text;
            //    cmd1.Parameters.Add("@ECONTACT", SqlDbType.VarChar).Value = txtecontact.Text;
            //    cmd1.Parameters.Add("@PREF", SqlDbType.VarChar).Value = dropreferedby.SelectedValue;
            //    cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
            //    cmd1.Parameters.Add("@PADDRESS", SqlDbType.VarChar).Value = Txtperad.Text;
            //    cmd1.Parameters.Add("@TADDRESS", SqlDbType.VarChar).Value = txttempad.Text;
            //    cmd1.Parameters.Add("@RGFEE", SqlDbType.Decimal).Value = txtregfee.Text;
            //    cmd1.Parameters.Add("@DISEASE", SqlDbType.VarChar).Value = txtdiease.Text;
            //    cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
            //    cmd1.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
            //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //    cmd1.Parameters.Add("@WARD", SqlDbType.VarChar).Value = dropward.Text;
            //    cmd1.Parameters.Add("@FAMILY", SqlDbType.VarChar).Value = txtfalmilyhead.Text;
            //    cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = dropinsurance.SelectedValue;
            //    cmd1.Parameters.Add("@PAC", SqlDbType.VarChar).Value = droppackage.SelectedValue.ToString();
            //    cmd1.Parameters.Add("@PACNAME", SqlDbType.VarChar).Value = droppackage.SelectedItem.Text.ToString();
            //    cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd1.Parameters.Add("@INSURANCENAME", SqlDbType.VarChar).Value = txtinsname.Text.ToUpper();
            //    cmd1.Parameters.Add("@INSURANCENO", SqlDbType.VarChar).Value = txtinsnumber.Text.ToUpper();
            //    cmd1.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.Text;
            //    cmd1.Parameters.Add("@PNO", SqlDbType.VarChar).Value = txtcard.Text;
            //    cmd1.Parameters.Add("@UHID", SqlDbType.VarChar).Value = LBLUHID.Text;
            //    cmd1.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropinsurance.SelectedValue;
            //    cmd1.ExecuteNonQuery();

            //    if (droppackage.SelectedIndex != 0)
            //    {
            //        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_ADMISSION", con))
            //        {
            //            cm.CommandType = CommandType.StoredProcedure;
            //            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
            //            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
            //            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT ADMISSION";
            //            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = '-' + txtregfee.Text;
            //            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtregfee.Text;
            //            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
            //            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = TXTID.Text;
            //            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
            //            cm.ExecuteNonQuery();
            //        }
            //        using (SqlCommand cmd = new SqlCommand("RECP_ADMISSION_UPDATE", con))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;
            //            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //            //SqlCommand cmd = new SqlCommand("update PA_TRANS SET BEDNO=@BEDNO,DESCRIPTION=@DESCRIPTION,CHARGES=@CHARGES WHERE CATEGORY=@CATEGORY", con);
            //            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //            cmd.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PACKAGE COST" + '-' + droppackage.SelectedItem.Text;
            //            cmd.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = droppackage.SelectedValue;
            //            cmd.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
            //            cmd.ExecuteNonQuery();
            //        }
            //    }

            //    else
            //    {
            //        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_ADMISSION", con))
            //        {
            //            cm.CommandType = CommandType.StoredProcedure;
            //            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = TXTID.Text;
            //            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
            //            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            //            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT ADMISSION";
            //            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = '-' + txtregfee.Text;
            //            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtregfee.Text;
            //            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
            //            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = TXTID.Text;
            //            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
            //            cm.ExecuteNonQuery();
            //        }
            //    }
            //}
            //binddata();
            
            

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
        Response.Redirect("~/RECEPTION/reception_admission_reciept.aspx");

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        div1.Visible = true;
        div2.Visible = false;
        binddata();
        clear_control();
    }
    protected void dropward_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BED_NAME");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, dropward.SelectedValue);
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_ADMISSION_SELECT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                dropbedno.DataSource = DS1;
                dropbedno.DataTextField = "BEDNO";
                dropbedno.DataValueField = "BEDNO";
                dropbedno.DataBind();
            }

            //using (SqlCommand com = new SqlCommand("RECP_ADMISSION_SELECT", con))
            //{
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED_NAME";
            //    com.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    com.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropward.SelectedItem.Text;
            //    com.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    com.Parameters.Add("@VN", SqlDbType.VarChar).Value = "";
            //    com.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = "";
            //    SqlDataAdapter da1 = new SqlDataAdapter(com);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("SELECT BEDNO FROM BED_MATRIX_TABLE WHERE STATUS='AVAILABLE' AND NAME='" + dropward.SelectedItem.Text + "' AND ORGID='" + lblorgid.Text + "' ORDER BY ID ASC", con);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    //dropbedno.SelectedIndex = 0;
            //    dropbedno.DataSource = dt1;
            //    dropbedno.DataTextField = "BEDNO";
            //    dropbedno.DataValueField = "BEDNO";
            //    dropbedno.DataBind();
            //}
            //con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropinsurance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (dropinsurance.SelectedIndex == 0)
            {
                txtinsname.Visible = false;
                txtinsnumber.Visible = false;
                txtinsname.Text = "";
                txtinsnumber.Text = "";
            }
            else
            {
                txtinsname.Visible = true;
                txtinsnumber.Visible = true;
                txtinsname.Text = "";
                txtinsnumber.Text = "";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void droppayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (droppayment.SelectedIndex == 0)
            {
                txtcard.Visible = false;
                txtcard.Text = "";
            }
            else
            {
                txtcard.Visible = true;
                txtcard.Text = "";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}