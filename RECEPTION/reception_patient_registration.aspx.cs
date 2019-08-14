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

public partial class RECEPTION_reception_patient_registration : System.Web.UI.Page
{
    string num1 = "SJ000";
    string num2 = "SJ00000000000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    string AMOUNT;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select max(ID) as ID from REGISTRATION_TBL";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        if (dr.Read() && dr["ID"].ToString() != "")
        {
            num1 = dr["ID"].ToString();
            num2 = dr["ID"].ToString();
            num1 = string.Format("OP{0}", (Convert.ToUInt64(num1.Substring(2)) + 1).ToString("D4"));
            num2 = string.Format("VVKH-{0}", (Convert.ToUInt32(num2.Substring(2)) + 1).ToString("D10"));

        }
        else
        {
            string qry2 = "select OPNO from IDGENERATE_TABLE";
            cmd = new SqlCommand(qry2, con);
            dr1 = null;
            dr.Close();
            dr1 = cmd.ExecuteReader();

            if (dr1.Read())
            {
                num1 = dr1["OPNO"].ToString();
                num2 = dr1["OPNO"].ToString();
                num1 = string.Format("OP{0}", (Convert.ToUInt64(num1.Substring(2)) + 1).ToString("D4"));
                num2 = string.Format("VVKH-{0}", (Convert.ToUInt32(num2.Substring(2)) + 1).ToString("D10"));
            }
            else
            {
                num1 = string.Format("OP{0}", (Convert.ToUInt64(num1.Substring(2)) + 1).ToString("D4"));
                num2 = string.Format("VVKH-{0}", (Convert.ToUInt32(num2.Substring(2)) + 1).ToString("D10"));
            }
        }



        lbluhid.Text = num2;
        // num1 = string.Format("OP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        LBLSLNO.Text = num1;

        dr.Close();
        con.Close();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["PID"] = gr.Cells[0].Text;
        Response.Redirect("~/RECEPTION/reception_preg_reciept.aspx");

    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
            txtregMade.Text = Session["NAME"].ToString();
            if (!IsPostBack)
            {
                binddata();
            }
          
            txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            txtDate.Enabled = false;
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


            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_REGISTRATION");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                grdRegtyp.DataSource = DS1;
                grdRegtyp.DataBind();
            }

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TESTCOMPONENT");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                dropselfcorp.DataSource = DS2;
                dropselfcorp.DataTextField = "CNAME";
                dropselfcorp.DataValueField = "ID";
                dropselfcorp.DataBind();
                dropselfcorp.Items.Insert(0, new ListItem("Please Select","0"));
            }

            SqlParameter[] SQL_PARAMS3 = new SqlParameter[2];

            SQL_PARAMS3[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SHOW_BROKER");
            SQL_PARAMS3[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS3 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS3);
            if (DS3.Tables[0].Rows.Count > 0)
            {
                dropreffname.DataSource = DS3;
                dropreffname.DataTextField = "NAME";
                dropreffname.DataValueField = "ID";
                dropreffname.DataBind();
                dropreffname.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            SqlParameter[] SQL_PARAMS4 = new SqlParameter[2];
            SQL_PARAMS4[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SHOW_DEPT");
            SQL_PARAMS4[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS65 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS4);
            if (DS65.Tables[0].Rows.Count > 0)
            {
                ddldept.DataSource = DS65;
                ddldept.DataTextField = "DeptName";
                ddldept.DataValueField = "id";
                ddldept.DataBind();
                ddldept.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            SqlParameter[] SQL_PARAMS5 = new SqlParameter[2];
            SQL_PARAMS5[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SHOW_DOCT");
            SQL_PARAMS5[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS6 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS5);
            if (DS6.Tables[0].Rows.Count > 0)
            {
                ddldoctor.DataSource = DS6;
                ddldoctor.DataTextField = "Sname";
                ddldoctor.DataValueField = "id";
                ddldoctor.DataBind();
                ddldoctor.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region old code
        //using (SqlCommand cmd = new SqlCommand("RECP_REGISTRATION", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_REGISTRATION";
        //    cmd.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = "";
        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        //    //SqlDataAdapter da1 = new SqlDataAdapter("select slno,ID,FNAME,DOB,DATETIME FROM REGISTRATION_TBL ORDER BY ID desc", con);
        //    DataTable dt1 = new DataTable();
        //    da1.Fill(dt1);
        //    grdRegtyp.SelectedIndex = 0;
        //    grdRegtyp.DataSource = dt1;
        //    // grdRegtyp.DataKeyNames = new string[] { "ID" };  
        //    grdRegtyp.DataBind();
        //}

        //using (SqlCommand cmd1 = new SqlCommand("RECP_REGISTRATION", con))
        //{
        //    cmd1.CommandType = CommandType.StoredProcedure;
        //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TESTCOMPONENT";
        //    cmd1.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = "";
        //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //    //SqlDataAdapter da = new SqlDataAdapter("select distinct b.C_ID as ID,a.CNAME as CNAME from  Corporate_Table a,TEST_COMPONENT_TABLE b where a.ID=b.C_ID and a.ISACTIVE='true'", con);
        //    DataTable ds = new DataTable();
        //    da.Fill(ds);
        //    dropselfcorp.DataSource = ds;
        //    dropselfcorp.DataTextField = "CNAME";
        //    dropselfcorp.DataValueField = "ID";
        //    dropselfcorp.DataBind();
        //    dropselfcorp.Items.Insert(0, "---Select---");
        //}

        //using (SqlCommand cmd2 = new SqlCommand("RECP_REGISTRATION", con))
        //{
        //    cmd2.CommandType = CommandType.StoredProcedure;
        //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOW_BROKER";
        //    cmd2.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = "";
        //    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        //    //SqlDataAdapter da2 = new SqlDataAdapter("select * from Broker_Table ", con);
        //    DataTable ds1 = new DataTable();
        //    da2.Fill(ds1);
        //    dropreffname.DataSource = ds1;
        //    dropreffname.DataTextField = "NAME";
        //    dropreffname.DataValueField = "ID";
        //    dropreffname.DataBind();
        //    dropreffname.Items.Insert(0, "---Select---");
        //}
        #endregion

    }
    protected void chkMlc_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkMlc.Checked)
            {
                mlcDiv.Visible = true;
            }
            else
            {
                mlcDiv.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void chkMinrPatent_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkMinrPatent.Checked)
            {
                gurdndiv.Visible = true;
            }
            else
            {
                gurdndiv.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {            
            if (chkMlc.Checked)
            {
                if (txtcase.Text == "")
                {
                    string message = "alert('* Please!! Enter The Case..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtcase.Focus();
                    return;
                }
                else if (txtdateMlc.Text == "")
                {
                    string message = "alert('* Please!! Enter The MLC Date..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtdateMlc.Focus();
                    return;
                }
                else if (txtPolicStaion.Text == "")
                {
                    string message = "alert('* Please!! Enter The Name Of The Police Station..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtPolicStaion.Focus();
                    return;
                }
            }
            else
            {
                txtdateMlc.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }


            if (ddlaginCard.SelectedIndex == 0)
            {
                if (txtCardno.Text == "")
                {
                    string message = "alert('* Please!! Enter The Card Number..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtCardno.Focus();
                    return;
                }
                else if (txtfirstname.Text == "")
                {
                    string message = "alert('* Please!! Enter The Card Name..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtfirstname.Focus();
                    return;
                }
            }
            if (txtpname.Text == "")
            {
                string message = "alert('* Please!! Enter The Name Of The Patient..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpname.Focus();
                return;
            }

            if (dropreffname.SelectedIndex != 0)
            {
                if (txtfrom.Text == "")
                {
                    string message = "alert('* Please!! Enter The Refferal From..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtfrom.Focus();
                    return;
                    //txtfrom.Focus();
                }
            }
            if (txtpcompl.Text == "")
            {
                string message = "alert('* Please!! Enter The Present Complaint..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpcompl.Focus();
                return;
            }
            if (txtnation.Text == "")
            {
                string message = "alert('* Please!! Enter The Nationality..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtnation.Focus();
                return;
            }
            if (dropgender.SelectedIndex == 0)
            {
                string message = "alert('* Please!! Select The Gender..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtdob.Text == "")
            {
                string message = "alert('* Please!! Enter The Date Of Birth..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdob.Focus();
                return;
            }
           
            if (txtAddress.Text == "")
            {
                string message = "alert('* Please!! Enter The Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtAddress.Focus();
                return;
            }
            if (txtdistrict.Text == "")
            {
                string message = "alert('* Please!! Enter The District..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdistrict.Focus();
                return;
            }
            if (txtstate.Text == "")
            {
                string message = "alert('* Please!! Enter The State..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtstate.Focus();
                return;
            }
            if (txtcharge.Text == "" || txtcharge.Text == "0")
            {
                string message = "alert('* Please!! Enter The Charges..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcharge.Focus();
                return;
            }
            if (chkMinrPatent.Checked)
            {
                if (txtguardname.Text == "")
                {
                    string message = "alert('* Please!! Enter The Name Of The Guardian Name If Patient Is Minor..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtguardname.Focus();
                    return;
                }
                else if (txtrelpatnt.Text == "")
                {
                    string message = "alert('* Please!! Enter The Relation With Patient..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtrelpatnt.Focus();
                    return;
                }
                else if (txtAdressInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The Address Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtAdressInf.Focus();
                    return;
                }
                else if (txtdistrictInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The District Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtdistrictInf.Focus();
                    return;
                }
                else if (txtstateInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The State Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtstateInf.Focus();
                    return;
                }
                else if (txtmobnoInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The Mobile Number Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtmobnoInf.Focus();
                    return;
                }
            }
            if (txtbookdate.Text == "")
            {
                txtbookdate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            
            auto();
            //autouhid();
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[8];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, Session["UID"].ToString());
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@Date", SqlDbType.Date, 0, DateTime.Now.ToString());
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@Collection", SqlDbType.Decimal, 0, Convert.ToDecimal(txtcharge.Text));
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@Paid", SqlDbType.Decimal, 0, "0.00");
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, LBLSLNO.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("usp_UserCollection", "", "", SqlDbType.VarChar, SQL_PARAMS2, true);
            
            if (OBJ_METHOD._RESULT > 0)
            {

                SqlParameter[] SQL_PARAMS1 = new SqlParameter[12];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm"));
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, LBLSLNO.Text);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 200, LBLSLNO.Text);
                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 200, "PATIENT REGISTRATION");
                SQL_PARAMS1[5] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 0, txtcharge.Text);
                SQL_PARAMS1[6] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 0, "OUTPATIENT");
                SQL_PARAMS1[7] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 0, txtcharge.Text);
                SQL_PARAMS1[8] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 0, "0.00");
                SQL_PARAMS1[9] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 0, LBLSLNO.Text);
                SQL_PARAMS1[10] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS1[11] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                   
                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_REGISTRATION", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);

                //string QRCODE=ddldept.SelectedItem.Text+txtfirstname.Text+
                if (OBJ_METHOD._RESULT > 0)
                {
                    if (txtbookno.Text != "")
                    {
                        SqlParameter[] SQL_PARAMS = new SqlParameter[58];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, LBLSLNO.Text);

                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, txtDate.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@REGISNO", SqlDbType.VarChar, 500, txtRegstno.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@REGISTYPE", SqlDbType.VarChar, 500, txtRegType.Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@AGINSTBOOK", SqlDbType.VarChar, 500, txtaginstbook.Text);
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@BOOKINGNO", SqlDbType.VarChar, 500, txtbookno.Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@BOOKDATE", SqlDbType.DateTime, 0, txtbookdate.Text);
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@BOOKFOR", SqlDbType.VarChar, 500, txtbookfor.Text);
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@MLCASE", SqlDbType.VarChar, 500, txtcase.Text);
                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@MLCCHECK", SqlDbType.Bit, 0, chkMlc.Checked);

                        if (chkMlc.Checked)
                        {
                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@MLCDATE", SqlDbType.DateTime, 0, txtdateMlc.Text);
                        }
                        else
                        {
                            //txtdateMlc.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@MLCDATE", SqlDbType.DateTime, 0, txtdateMlc.Text);
                        }


                        SQL_PARAMS[12] = OBJ_METHOD.createParams("@POLICEST", SqlDbType.VarChar, 500, txtPolicStaion.Text);
                        SQL_PARAMS[13] = OBJ_METHOD.createParams("@AGAINSTCARD", SqlDbType.VarChar, 500, ddlaginCard.SelectedItem.Text);
                        SQL_PARAMS[14] = OBJ_METHOD.createParams("@CARDNO", SqlDbType.VarChar, 500, txtCardno.Text);
                        SQL_PARAMS[15] = OBJ_METHOD.createParams("@TITLE", SqlDbType.VarChar, 500, txtTitle.Text);
                        SQL_PARAMS[16] = OBJ_METHOD.createParams("@FNAME", SqlDbType.VarChar, 500, txtfirstname.Text);
                        SQL_PARAMS[17] = OBJ_METHOD.createParams("@PATIENTCOND", SqlDbType.VarChar, 500, txtpatintcond.Text);
                        SQL_PARAMS[18] = OBJ_METHOD.createParams("@BILLOPTN", SqlDbType.VarChar, 500, txtbillopt.Text);
                        SQL_PARAMS[19] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 500, txtpname.Text);
                        SQL_PARAMS[20] = OBJ_METHOD.createParams("@INSURACECO", SqlDbType.VarChar, 500, txtinsurce.Text);

                        if (dropselfcorp.SelectedIndex != 0)
                        {
                            SQL_PARAMS[21] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropselfcorp.SelectedValue);
                        }
                        else
                        {
                            SQL_PARAMS[21] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, "0");
                            //cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = 0;
                        }
                        
                        SQL_PARAMS[22] = OBJ_METHOD.createParams("@BPLCNO", SqlDbType.VarChar, 500, txtbplno.Text);
                        SQL_PARAMS[23] = OBJ_METHOD.createParams("@FROMM", SqlDbType.VarChar, 500, txtfrom.Text);
                        SQL_PARAMS[24] = OBJ_METHOD.createParams("@SOURCENM", SqlDbType.VarChar, 500, txtotSocenm.Text);
                        SQL_PARAMS[25] = OBJ_METHOD.createParams("@REFFRALNAME", SqlDbType.VarChar, 500, dropreffname.SelectedItem.Text);
                        SQL_PARAMS[26] = OBJ_METHOD.createParams("@PRESNTCOMPL", SqlDbType.VarChar, 500, txtpcompl.Text);
                        //cm.Parameters.Add("@REFFNAMEF2", SqlDbType.VarChar).Value = txtreff2.Text;
                        SQL_PARAMS[27] = OBJ_METHOD.createParams("@REVIEW", SqlDbType.VarChar, 500, txtreviw.Text);
                        SQL_PARAMS[28] = OBJ_METHOD.createParams("@DEPTNAME", SqlDbType.VarChar, 500, ddldept.SelectedItem.Text);
                        SQL_PARAMS[29] = OBJ_METHOD.createParams("@DOCTORNM", SqlDbType.VarChar, 500, ddldoctor.SelectedItem.Text);
                        SQL_PARAMS[30] = OBJ_METHOD.createParams("@NATIONLITY", SqlDbType.VarChar, 500, txtnation.Text);
                        SQL_PARAMS[31] = OBJ_METHOD.createParams("@PASSPT", SqlDbType.VarChar, 500, txtpasspt.Text);
                        SQL_PARAMS[32] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, dropgender.SelectedItem.Text);
                        SQL_PARAMS[33] = OBJ_METHOD.createParams("@BLOODGP", SqlDbType.VarChar, 500, dropblogrp.SelectedItem.Text);
                        SQL_PARAMS[34] = OBJ_METHOD.createParams("@DOB", SqlDbType.Date, 0, Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd"));
                        SQL_PARAMS[35] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 100, txtage.Text);
                        SQL_PARAMS[36] = OBJ_METHOD.createParams("@ADDRES", SqlDbType.VarChar, 500, txtAddress.Text);
                        SQL_PARAMS[37] = OBJ_METHOD.createParams("@DIST", SqlDbType.VarChar, 500, txtdistrict.Text);
                        SQL_PARAMS[38] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
                        SQL_PARAMS[39] = OBJ_METHOD.createParams("@TELPHNO", SqlDbType.VarChar, 500, txttelphone.Text);
                        SQL_PARAMS[40] = OBJ_METHOD.createParams("@MOBNO", SqlDbType.VarChar, 500, txtmobno.Text);
                        SQL_PARAMS[41] = OBJ_METHOD.createParams("@EMAILID", SqlDbType.VarChar, 500, txtemailid.Text);

                        SQL_PARAMS[42] = OBJ_METHOD.createParams("@MINORCHECK", SqlDbType.Bit, 500, chkMinrPatent.Checked);
                        SQL_PARAMS[43] = OBJ_METHOD.createParams("@GURDIANAME", SqlDbType.VarChar, 500, txtguardname.Text);
                        SQL_PARAMS[44] = OBJ_METHOD.createParams("@RELTNPATIENT", SqlDbType.VarChar, 500, txtrelpatnt.Text);
                        SQL_PARAMS[45] = OBJ_METHOD.createParams("@GURDINADDRES", SqlDbType.VarChar, 500, txtAdressInf.Text);
                        SQL_PARAMS[46] = OBJ_METHOD.createParams("@GURDINDIST", SqlDbType.VarChar, 500, txtdistrictInf.Text);
                        SQL_PARAMS[47] = OBJ_METHOD.createParams("@GURDINSATE", SqlDbType.VarChar, 500, txtstateInf.Text);
                        SQL_PARAMS[48] = OBJ_METHOD.createParams("@GURDINTELPHNO", SqlDbType.VarChar, 500, txtphInf.Text);
                        SQL_PARAMS[49] = OBJ_METHOD.createParams("@GURDINMOBLE", SqlDbType.VarChar, 500, txtmobnoInf.Text);
                        SQL_PARAMS[50] = OBJ_METHOD.createParams("@GURDINEMAILID", SqlDbType.VarChar, 500, txtemailInf.Text);
                        SQL_PARAMS[51] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, txtcharge.Text);
                        SQL_PARAMS[52] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 500, lbluhid.Text);
                        SQL_PARAMS[53] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[54] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[55] = OBJ_METHOD.createParams("@REGSTMADE", SqlDbType.VarChar, 500, txtregMade.Text);
                        SQL_PARAMS[56] = OBJ_METHOD.createParams("@P_LAST_NAME", SqlDbType.VarChar, 50, txtplastnm.Text);
                        SQL_PARAMS[57] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 50, Session["ORGID"]);

                        OBJ_METHOD.ExecuteProceedure("USP_REGISTRATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            OBJ_METHOD.commitOrRollbackTran("commit");
                            binddata();
                            //reset();
                            message1 = "alert('" + OBJ_METHOD._objOut + "')";
                            
                            
                        }
                        else
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            message1 = "alert('" + OBJ_METHOD._objOut + "')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                            return;
                            //message1 = "alert('Due to some issues, Data not saved.')";
                        }
                    }
                    else
                    {
                        SqlParameter[] SQL_PARAMS = new SqlParameter[58];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT1");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, LBLSLNO.Text);

                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, txtDate.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@REGISNO", SqlDbType.VarChar, 500, txtRegstno.Text);
                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@REGISTYPE", SqlDbType.VarChar, 500, txtRegType.Text);
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@AGINSTBOOK", SqlDbType.VarChar, 500, txtaginstbook.Text);
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@BOOKINGNO", SqlDbType.VarChar, 500, txtbookno.Text);
                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@BOOKDATE", SqlDbType.DateTime, 0, txtbookdate.Text);
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@BOOKFOR", SqlDbType.VarChar, 500, txtbookfor.Text);
                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@MLCASE", SqlDbType.VarChar, 500, txtcase.Text);
                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@MLCCHECK", SqlDbType.Bit, 0, chkMlc.Checked);

                        if (chkMlc.Checked)
                        {
                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@MLCDATE", SqlDbType.DateTime, 0, txtdateMlc.Text);
                        }
                        else
                        {
                            //txtdateMlc.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@MLCDATE", SqlDbType.DateTime, 0, txtdateMlc.Text);
                        }


                        SQL_PARAMS[12] = OBJ_METHOD.createParams("@POLICEST", SqlDbType.VarChar, 500, txtPolicStaion.Text);
                        SQL_PARAMS[13] = OBJ_METHOD.createParams("@AGAINSTCARD", SqlDbType.VarChar, 500, ddlaginCard.SelectedItem.Text);
                        SQL_PARAMS[14] = OBJ_METHOD.createParams("@CARDNO", SqlDbType.VarChar, 500, txtCardno.Text);
                        SQL_PARAMS[15] = OBJ_METHOD.createParams("@TITLE", SqlDbType.VarChar, 500, txtTitle.Text);
                        SQL_PARAMS[16] = OBJ_METHOD.createParams("@FNAME", SqlDbType.VarChar, 500, txtfirstname.Text);
                        SQL_PARAMS[17] = OBJ_METHOD.createParams("@PATIENTCOND", SqlDbType.VarChar, 500, txtpatintcond.Text);
                        SQL_PARAMS[18] = OBJ_METHOD.createParams("@BILLOPTN", SqlDbType.VarChar, 500, txtbillopt.Text);
                        SQL_PARAMS[19] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 500, txtpname.Text);
                        SQL_PARAMS[20] = OBJ_METHOD.createParams("@INSURACECO", SqlDbType.VarChar, 500, txtinsurce.Text);

                        if (dropselfcorp.SelectedIndex != 0)
                        {
                            SQL_PARAMS[21] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropselfcorp.SelectedValue);
                        }
                        else
                        {
                            SQL_PARAMS[21] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, "0");
                            //cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = 0;
                        }
                        SQL_PARAMS[22] = OBJ_METHOD.createParams("@BPLCNO", SqlDbType.VarChar, 500, txtbplno.Text);
                        SQL_PARAMS[23] = OBJ_METHOD.createParams("@FROMM", SqlDbType.VarChar, 500, txtfrom.Text);
                        SQL_PARAMS[24] = OBJ_METHOD.createParams("@SOURCENM", SqlDbType.VarChar, 500, txtotSocenm.Text);
                        SQL_PARAMS[25] = OBJ_METHOD.createParams("@REFFRALNAME", SqlDbType.VarChar, 500, dropreffname.SelectedItem.Text);
                        SQL_PARAMS[26] = OBJ_METHOD.createParams("@PRESNTCOMPL", SqlDbType.VarChar, 500, txtpcompl.Text);
                        //cm.Parameters.Add("@REFFNAMEF2", SqlDbType.VarChar).Value = txtreff2.Text;
                        SQL_PARAMS[27] = OBJ_METHOD.createParams("@REVIEW", SqlDbType.VarChar, 500, txtreviw.Text);
                        SQL_PARAMS[28] = OBJ_METHOD.createParams("@DEPTNAME", SqlDbType.VarChar, 500, ddldept.SelectedItem.Text);
                        SQL_PARAMS[29] = OBJ_METHOD.createParams("@DOCTORNM", SqlDbType.VarChar, 500, ddldoctor.SelectedItem.Text);
                        SQL_PARAMS[30] = OBJ_METHOD.createParams("@NATIONLITY", SqlDbType.VarChar, 500, txtnation.Text);
                        SQL_PARAMS[31] = OBJ_METHOD.createParams("@PASSPT", SqlDbType.VarChar, 500, txtpasspt.Text);
                        SQL_PARAMS[32] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, dropgender.SelectedItem.Text);
                        SQL_PARAMS[33] = OBJ_METHOD.createParams("@BLOODGP", SqlDbType.VarChar, 500, dropblogrp.SelectedItem.Text);
                        SQL_PARAMS[34] = OBJ_METHOD.createParams("@DOB", SqlDbType.Date, 0, Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd"));
                        SQL_PARAMS[35] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 100, txtage.Text);
                        SQL_PARAMS[36] = OBJ_METHOD.createParams("@ADDRES", SqlDbType.VarChar, 500, txtAddress.Text);
                        SQL_PARAMS[37] = OBJ_METHOD.createParams("@DIST", SqlDbType.VarChar, 500, txtdistrict.Text);
                        SQL_PARAMS[38] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
                        SQL_PARAMS[39] = OBJ_METHOD.createParams("@TELPHNO", SqlDbType.VarChar, 500, txttelphone.Text);
                        SQL_PARAMS[40] = OBJ_METHOD.createParams("@MOBNO", SqlDbType.VarChar, 500, txtmobno.Text);
                        SQL_PARAMS[41] = OBJ_METHOD.createParams("@EMAILID", SqlDbType.VarChar, 500, txtemailid.Text);

                        SQL_PARAMS[42] = OBJ_METHOD.createParams("@MINORCHECK", SqlDbType.Bit, 500, chkMinrPatent.Checked);
                        SQL_PARAMS[43] = OBJ_METHOD.createParams("@GURDIANAME", SqlDbType.VarChar, 500, txtguardname.Text);
                        SQL_PARAMS[44] = OBJ_METHOD.createParams("@RELTNPATIENT", SqlDbType.VarChar, 500, txtrelpatnt.Text);
                        SQL_PARAMS[45] = OBJ_METHOD.createParams("@GURDINADDRES", SqlDbType.VarChar, 500, txtAdressInf.Text);
                        SQL_PARAMS[46] = OBJ_METHOD.createParams("@GURDINDIST", SqlDbType.VarChar, 500, txtdistrictInf.Text);
                        SQL_PARAMS[47] = OBJ_METHOD.createParams("@GURDINSATE", SqlDbType.VarChar, 500, txtstateInf.Text);
                        SQL_PARAMS[48] = OBJ_METHOD.createParams("@GURDINTELPHNO", SqlDbType.VarChar, 500, txtphInf.Text);
                        SQL_PARAMS[49] = OBJ_METHOD.createParams("@GURDINMOBLE", SqlDbType.VarChar, 500, txtmobnoInf.Text);
                        SQL_PARAMS[50] = OBJ_METHOD.createParams("@GURDINEMAILID", SqlDbType.VarChar, 500, txtemailInf.Text);
                        SQL_PARAMS[51] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, txtcharge.Text);
                        SQL_PARAMS[52] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 500, lbluhid.Text);
                        SQL_PARAMS[53] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[54] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[55] = OBJ_METHOD.createParams("@REGSTMADE", SqlDbType.VarChar, 500, txtregMade.Text);
                        SQL_PARAMS[56] = OBJ_METHOD.createParams("@P_LAST_NAME", SqlDbType.VarChar, 50, txtplastnm.Text);
                        SQL_PARAMS[57] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 50, Session["ORGID"]);
                        OBJ_METHOD.ExecuteProceedure("USP_REGISTRATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                        if (OBJ_METHOD._RESULT > 0)
                        {
                            OBJ_METHOD.commitOrRollbackTran("commit");
                            binddata();
                            //reset();
                            //message1 = "alert('" + OBJ_METHOD._objOut + "')";
                            
                            
                        }
                        else
                        {
                            OBJ_METHOD.commitOrRollbackTran("rollback");
                            message1 = "alert('Due to some issues, Data not saved.')";

                        }
                    }
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Due to some issues, Data not saved.')";
                }
                //clearcontrol();
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not saved.')";
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            Session["PID"] = LBLSLNO.Text;
            #region old code
            //using (SqlCommand cm = new SqlCommand("USP_REGISTRATION", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = LBLSLNO.Text;

            //    //cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
            //    cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@REGISNO", SqlDbType.VarChar).Value = txtRegstno.Text;
            //    cm.Parameters.Add("@REGISTYPE", SqlDbType.VarChar).Value = txtRegType.Text;
            //    cm.Parameters.Add("@AGINSTBOOK", SqlDbType.VarChar).Value = txtaginstbook.Text;
            //    cm.Parameters.Add("@BOOKINGNO", SqlDbType.VarChar).Value = txtbookno.Text;
            //    cm.Parameters.Add("@BOOKDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtbookdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@BOOKFOR", SqlDbType.VarChar).Value = txtbookfor.Text;
            //    cm.Parameters.Add("@MLCASE", SqlDbType.VarChar).Value = txtcase.Text;
            //    cm.Parameters.Add("@MLCCHECK", SqlDbType.Bit).Value = chkMlc.Checked;
            //    if (chkMlc.Checked)
            //    {
            //        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm");
            //    }
            //    else
            //    {
            //        txtdateMlc.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            //        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm"); ;
            //    }
            //    cm.Parameters.Add("@POLICEST", SqlDbType.VarChar).Value = txtPolicStaion.Text;
            //    cm.Parameters.Add("@AGAINSTCARD", SqlDbType.VarChar).Value = ddlaginCard.SelectedItem.Text;
            //    cm.Parameters.Add("@CARDNO", SqlDbType.VarChar).Value = txtCardno.Text;
            //    cm.Parameters.Add("@TITLE", SqlDbType.VarChar).Value = txtTitle.Text;
            //    cm.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = txtfirstname.Text;
            //    //cm.Parameters.Add("@MIDDLENAME", SqlDbType.VarChar).Value = txtmiddlnm.Text;
            //    //cm.Parameters.Add("@LASTNAME", SqlDbType.VarChar).Value = txtLastname.Text;
            //    cm.Parameters.Add("@PATIENTCOND", SqlDbType.VarChar).Value = txtpatintcond.Text;
            //    cm.Parameters.Add("@BILLOPTN", SqlDbType.VarChar).Value = txtbillopt.Text;
            //    cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtpname.Text;

            //    cm.Parameters.Add("@INSURACECO", SqlDbType.VarChar).Value = txtinsurce.Text;
            //    if (dropselfcorp.SelectedIndex != 0)
            //    {
            //        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropselfcorp.SelectedValue;
            //    }
            //    else
            //    {
            //        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = 0;
            //    }
            //    cm.Parameters.Add("@BPLCNO", SqlDbType.VarChar).Value = txtbplno.Text;
            //    cm.Parameters.Add("@FROMM", SqlDbType.VarChar).Value = txtfrom.Text;
            //    cm.Parameters.Add("@SOURCENM", SqlDbType.VarChar).Value = txtotSocenm.Text;
            //    cm.Parameters.Add("@REFFRALNAME", SqlDbType.VarChar).Value = dropreffname.SelectedItem.Text;
            //    //cm.Parameters.Add("@REFFNAMEF2", SqlDbType.VarChar).Value = txtreff2.Text;
            //    cm.Parameters.Add("@PRESNTCOMPL", SqlDbType.VarChar).Value = txtpcompl.Text;
            //    cm.Parameters.Add("@REVIEW", SqlDbType.VarChar).Value = txtreviw.Text;
            //    cm.Parameters.Add("@DEPTNAME", SqlDbType.VarChar).Value = txtdeptnm.Text;
            //    cm.Parameters.Add("@DOCTORNM", SqlDbType.VarChar).Value = txtdoctnm.Text;
            //    cm.Parameters.Add("@NATIONLITY", SqlDbType.VarChar).Value = txtnation.Text;
            //    cm.Parameters.Add("@PASSPT", SqlDbType.VarChar).Value = txtpasspt.Text;
            //    cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
            //    cm.Parameters.Add("@BLOODGP", SqlDbType.VarChar).Value = dropblogrp.SelectedItem.Text;
            //    cm.Parameters.Add("@DOB", SqlDbType.Date).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
            //    cm.Parameters.Add("@ADDRES", SqlDbType.VarChar).Value = txtAddress.Text;
            //    cm.Parameters.Add("@DIST", SqlDbType.VarChar).Value = txtdistrict.Text;
            //    cm.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
            //    cm.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = txttelphone.Text;
            //    cm.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = txtmobno.Text;
            //    cm.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = txtemailid.Text;

            //    cm.Parameters.Add("@MINORCHECK", SqlDbType.Bit).Value = chkMinrPatent.Checked;
            //    cm.Parameters.Add("@GURDIANAME", SqlDbType.VarChar).Value = txtguardname.Text;
            //    cm.Parameters.Add("@RELTNPATIENT", SqlDbType.VarChar).Value = txtrelpatnt.Text;
            //    cm.Parameters.Add("@GURDINADDRES", SqlDbType.VarChar).Value = txtAdressInf.Text;
            //    //cm.Parameters.Add("@GURDINAREA", SqlDbType.VarChar).Value = txtAreaInf.Text;
            //    cm.Parameters.Add("@GURDINDIST", SqlDbType.VarChar).Value = txtdistrictInf.Text;
            //    cm.Parameters.Add("@GURDINSATE", SqlDbType.VarChar).Value = txtstateInf.Text;
            //    cm.Parameters.Add("@GURDINTELPHNO", SqlDbType.VarChar).Value = txtphInf.Text;
            //    cm.Parameters.Add("@GURDINMOBLE", SqlDbType.VarChar).Value = txtmobnoInf.Text;
            //    cm.Parameters.Add("@GURDINEMAILID", SqlDbType.VarChar).Value = txtemailInf.Text;
            //    cm.Parameters.Add("@REGSTMADE", SqlDbType.VarChar).Value = txtregMade.Text;
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
            //    cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = lbluhid.Text;
            //    cm.ExecuteNonQuery();

            //}
               

            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_REGISTRATION", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT REGISTRATION";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtcharge.Text;
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0.00";
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cm.ExecuteNonQuery();
            //}

              

            //using (SqlCommand cmd = new SqlCommand("usp_UserCollection", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
            //    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
            //    cmd.Parameters.Add("@Collection", SqlDbType.Decimal).Value = txtcharge.Text;
            //    cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cmd.ExecuteNonQuery();
            //}

            ////string message1 = "alert('Registration Saved Successfully .')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            //reset();
            //con.Close();
            //Response.Redirect("~/RECEPTION/Registrationpage.aspx");
                #endregion
            
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
           
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            
        }
        Response.Redirect("~/RECEPTION/reception_preg_reciept.aspx");
    }
    protected void grdRegtyp_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = grdRegtyp.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
       
        try
        {
            DataSet DS1 = OBJ_METHOD.Get_DataSet("SELECT * FROM REGISTRATION_TBL where ID='" + slno + "'", false, false);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                LBLSLNO.Text = DS1.Tables[0].Rows[0]["ID"].ToString();
                txtDate.Text = Convert.ToDateTime(DS1.Tables[0].Rows[0]["DATETIME"]).ToString();
                txtbookno.Text = DS1.Tables[0].Rows[0]["BOOKINGNO"].ToString();
                txtbookdate.Text = Convert.ToDateTime(DS1.Tables[0].Rows[0]["BOOKDATE"].ToString()).ToString("yyyy-MM-dd HH:mm");
                txtbookfor.Text = DS1.Tables[0].Rows[0]["BOOKFOR"].ToString();
                txtcase.Text = DS1.Tables[0].Rows[0]["MLCASE"].ToString();
                chkMlc.Checked = Convert.ToBoolean(DS1.Tables[0].Rows[0]["MLCCHECK"]);
                if (chkMlc.Checked)
                {
                    mlcDiv.Visible = true;
                }
                else
                {
                    mlcDiv.Visible = false;
                }
                txtdateMlc.Text = Convert.ToDateTime(DS1.Tables[0].Rows[0]["MLCDATE"].ToString()).ToString("dd-MM-yyyy");

                txtPolicStaion.Text = DS1.Tables[0].Rows[0]["POLICEST"].ToString();
                ddlaginCard.Text = DS1.Tables[0].Rows[0]["AGAINSTCARD"].ToString();
                if (ddlaginCard.Text == "Yes")
                {
                    txtCardno.Visible = true;
                    txtCardno.Visible = true;
                }
                else
                {
                    txtCardno.Visible = false;
                    txtCardno.Visible = false;
                }
                txtCardno.Text = DS1.Tables[0].Rows[0]["CARDNO"].ToString();
                txtfirstname.Text = DS1.Tables[0].Rows[0]["FNAME"].ToString();
                txtpatintcond.Text = DS1.Tables[0].Rows[0]["PATIENTCOND"].ToString();
                txtbillopt.Text = DS1.Tables[0].Rows[0]["BILLOPTN"].ToString();
                txtpname.Text = DS1.Tables[0].Rows[0]["PNAME"].ToString();

                txtinsurce.Text = DS1.Tables[0].Rows[0]["INSURACECO"].ToString();
                Session["Corpo"] = DS1.Tables[0].Rows[0]["CORPORATE"].ToString();
                lblcorpo.Text = DS1.Tables[0].Rows[0]["CORPORATE"].ToString();
                txtbplno.Text = DS1.Tables[0].Rows[0]["BPLCNO"].ToString();
                txtfrom.Text = DS1.Tables[0].Rows[0]["FROMM"].ToString();
                txtotSocenm.Text = DS1.Tables[0].Rows[0]["SOURCENM"].ToString();
                dropreffname.SelectedItem.Text = DS1.Tables[0].Rows[0]["REFFRALNAME"].ToString();
                txtpcompl.Text = DS1.Tables[0].Rows[0]["PRESNTCOMPL"].ToString();
                txtreviw.Text = DS1.Tables[0].Rows[0]["REVIEW"].ToString();
                ddldept.SelectedItem.Text = DS1.Tables[0].Rows[0]["DEPTNAME"].ToString();
                ddldoctor.SelectedItem.Text = DS1.Tables[0].Rows[0]["DOCTORNM"].ToString();
                txtnation.Text = DS1.Tables[0].Rows[0]["NATIONLITY"].ToString();
                txtpasspt.Text = DS1.Tables[0].Rows[0]["PASSPT"].ToString();
                dropgender.Text = DS1.Tables[0].Rows[0]["GENDER"].ToString(); ;
                dropblogrp.Text = DS1.Tables[0].Rows[0]["BLOODGP"].ToString();
                txtdob.Text = Convert.ToDateTime(DS1.Tables[0].Rows[0]["DOB"]).ToString("yyyy-MM-dd HH:mm");
                txtage.Text = DS1.Tables[0].Rows[0]["AGE"].ToString();
                txtAddress.Text = DS1.Tables[0].Rows[0]["ADDRES"].ToString();
                txtdistrict.Text = DS1.Tables[0].Rows[0]["DIST"].ToString();
                txtstate.Text = DS1.Tables[0].Rows[0]["STATE"].ToString();
                txttelphone.Text = DS1.Tables[0].Rows[0]["TELPHNO"].ToString();
                txtmobno.Text = DS1.Tables[0].Rows[0]["MOBNO"].ToString();
                txtemailid.Text = DS1.Tables[0].Rows[0]["EMAILID"].ToString();
                

                chkMinrPatent.Checked = Convert.ToBoolean(DS1.Tables[0].Rows[0]["MINORCHECK"].ToString());
                if (chkMinrPatent.Checked)
                {
                    gurdndiv.Visible = true;
                }
                else
                {
                    gurdndiv.Visible = false;
                }
                txtguardname.Text = DS1.Tables[0].Rows[0]["GURDIANAME"].ToString();
                txtrelpatnt.Text = DS1.Tables[0].Rows[0]["RELTNPATIENT"].ToString();
                txtAdressInf.Text = DS1.Tables[0].Rows[0]["GURDINADDRES"].ToString();
                txtdistrictInf.Text = DS1.Tables[0].Rows[0]["GURDINDIST"].ToString();
                txtstateInf.Text = DS1.Tables[0].Rows[0]["GURDINSATE"].ToString();
                txtphInf.Text = DS1.Tables[0].Rows[0]["GURDINTELPHNO"].ToString();
                txtmobnoInf.Text = DS1.Tables[0].Rows[0]["GURDINMOBLE"].ToString();
                txtemailInf.Text = DS1.Tables[0].Rows[0]["GURDINEMAILID"].ToString();
                txtregMade.Text = DS1.Tables[0].Rows[0]["REGSTMADE"].ToString();
                txtcharge.Text = DS1.Tables[0].Rows[0]["CHARGES"].ToString();
                lbluhid.Text = DS1.Tables[0].Rows[0]["UHID"].ToString();

                if (lblcorpo.Text != "0")
                {
                    SqlParameter[] SQL_PARAMS2 = new SqlParameter[1];

                    SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TESTCOMPONENT");

                    DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS2);
                    if (DS2.Tables[0].Rows.Count > 0)
                    {
                        dropselfcorp.DataSource = DS2;
                        dropselfcorp.DataTextField = "CNAME";
                        dropselfcorp.DataValueField = "ID";
                        dropselfcorp.DataBind();
                        dropselfcorp.Items.Insert(0, new ListItem("Please Select", "0"));
                        dropselfcorp.SelectedValue = Session["Corpo"].ToString();
                    }
                }
                else
                {
                    SqlParameter[] SQL_PARAMS2 = new SqlParameter[1];

                    SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_TESTCOMPONENT");

                    DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS2);
                    if (DS2.Tables[0].Rows.Count > 0)
                    {
                        dropselfcorp.DataSource = DS2;
                        dropselfcorp.DataTextField = "CNAME";
                        dropselfcorp.DataValueField = "ID";
                        dropselfcorp.DataBind();
                        dropselfcorp.Items.Insert(0, new ListItem("Please Select", "0"));
                        
                    }
                }
                
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void reset()
    {
        txtcase.Text = "";
        txtDate.Text = "";
        txtRegstno.Text = "";
        txtRegType.Text = "";
        txtaginstbook.Text = "";
        txtbookno.Text = "";
        txtbookdate.Text = "";
        txtbookfor.Text = "";
        txtcase.Text = "";
        txtdateMlc.Text = "";
        txtPolicStaion.Text = "";
        ddlaginCard.SelectedIndex = 0;
        txtCardno.Text = "";
        txtTitle.Text = "";
        txtfirstname.Text = "";
      
        txtpatintcond.Text = "";
        txtbillopt.Text = "";
        txtpname.Text = "";
        txtplastnm.Text = "";
        txtinsurce.Text = "";
        dropselfcorp.SelectedIndex = 0;
        txtbplno.Text = "";
        txtfrom.Text = "";
        txtotSocenm.Text = "";
        dropreffname.SelectedIndex = 0;
       
        txtpcompl.Text = "";
        txtreviw.Text = "";
        ddldept.SelectedIndex = 0;
        ddldoctor.SelectedIndex = 0;
        txtnation.Text = "";
        txtpasspt.Text = "";
        dropgender.SelectedIndex = 0;
        dropblogrp.SelectedIndex = 0;
        txtdob.Text = "";
        txtage.Text = "";
       
        txtAddress.Text = "";
        txtdistrict.Text = "";
        txtstate.Text = "";
        txttelphone.Text = "";
        txtmobno.Text = "";
        txtemailid.Text = "";
        txtguardname.Text = "";
        txtrelpatnt.Text = "";
        txtAdressInf.Text = "";
        //txtAreaInf.Text="";
        txtdistrictInf.Text = "";
        txtstateInf.Text = "";
        txtphInf.Text = "";
        txtmobnoInf.Text = "";
        txtemailInf.Text = "";
        txtregMade.Text = "";
        txtcharge.Text = "";
        chkMlc.Checked = false;
        chkMinrPatent.Checked = false;
        txtCardno.Visible = true;
        txtfirstname.Visible = true;
        ddlaginCard.SelectedIndex = 0;
    }
    protected void txtbookno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[3];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECTED_EVENT");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@BOOKING_NO", SqlDbType.VarChar, 500,  txtbookno.Text.Trim());
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"].ToString());

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS2);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                txtbookfor.Text = DS1.Tables[0].Rows[0]["BOOKING_FOR"].ToString();
                txtbookdate.Text = DS1.Tables[0].Rows[0]["BOOKING_DATE"].ToString();
                txtfrom.Text = DS1.Tables[0].Rows[0]["FROM"].ToString();
                txtpcompl.Text = DS1.Tables[0].Rows[0]["COMPLAINT"].ToString();
                txtpname.Text = DS1.Tables[0].Rows[0]["NAME"].ToString();
                dropgender.Text = DS1.Tables[0].Rows[0]["GENDER"].ToString();
                dropblogrp.Text = DS1.Tables[0].Rows[0]["BLOODGROUP"].ToString();
                txtdob.Text = Convert.ToDateTime(DS1.Tables[0].Rows[0]["DATEOFBIRTH"]).ToString("dd-MM-yyyy");
                txtAddress.Text = DS1.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtdistrict.Text = DS1.Tables[0].Rows[0]["DISTRICT"].ToString();
                txtstate.Text = DS1.Tables[0].Rows[0]["STATE"].ToString();
                txttelphone.Text = DS1.Tables[0].Rows[0]["TELEPHONE"].ToString();
                txtmobno.Text = DS1.Tables[0].Rows[0]["MOBILE"].ToString();
                txtemailid.Text = DS1.Tables[0].Rows[0]["EMAIL"].ToString();
                string dtVal = txtdob.Text.Trim();
                DateTime Dob = Convert.ToDateTime(dtVal);
                txtage.Text = CalculateAge(Dob);
            }
            else
            {
                string message = "alert('* Please!! Enter A valid Bookong Number')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbookno.Text = "";
                txtbookno.Focus();
                return;
            }

            #region old code
            //using (SqlCommand cmd2 = new SqlCommand("RECP_REGISTRATION", con))
           // {
           //     cmd2.CommandType = CommandType.StoredProcedure;
           //     cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTED_EVENT";
           //     cmd2.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = txtbookno.Text;
           //     //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
           //     //SqlCommand COM = new SqlCommand("SELECT * FROM RESERVATION_TABLE where BOOKING_NO='" + txtbookno.Text + "' ", con);
           //     dr = cmd2.ExecuteReader();
           //     if (dr.Read())
           //     {
           //         //lblminno.Text = dr["MRNO"].ToString();  
           //         txtbookfor.Text = dr["BOOKING_FOR"].ToString();
           //         txtbookdate.Text = dr["BOOKING_DATE"].ToString();
           //         txtfrom.Text = dr["FROM"].ToString();
           //         // txtrefname.Text = dr["REFERALNAME"].ToString();
           //         txtpcompl.Text = dr["COMPLAINT"].ToString();
           //         txtpname.Text = dr["NAME"].ToString();
           //         //txtdoctnm.Text = dr["DOCTORNAME"].ToString();
           //         dropgender.Text = dr["GENDER"].ToString();
           //         dropblogrp.Text = dr["BLOODGROUP"].ToString();
           //         txtdob.Text = Convert.ToDateTime(dr["DATEOFBIRTH"]).ToString("dd-MM-yyyy");
           //         txtAddress.Text = dr["ADDRESS"].ToString();
           //         txtdistrict.Text = dr["DISTRICT"].ToString();
           //         txtstate.Text = dr["STATE"].ToString();
           //         txttelphone.Text = dr["TELEPHONE"].ToString();
           //         txtmobno.Text = dr["MOBILE"].ToString();
           //         txtemailid.Text = dr["EMAIL"].ToString();
           //         //txtbookfor.Text = dr["BOOKING_FOR"].ToString();
           //         //txtbookfor.Text = dr["BOOKING_FOR"].ToString();
           //     }
           //     dr.Close();

           // }
           // con.Close();
           // string dtVal = txtdob.Text.Trim();
           // DateTime Dob = Convert.ToDateTime(dtVal);
            // txtage.Text = CalculateAge(Dob);
            #endregion
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void grdRegtyp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_REGISTRATION");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"].ToString());

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                grdRegtyp.DataSource = DS1;
                grdRegtyp.PageIndex = e.NewPageIndex;
                grdRegtyp.DataKeyNames = new string[] { "ID" };
                grdRegtyp.DataBind();
            }
           
           
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void ddlaginCard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlaginCard.SelectedIndex == 0)
            {
                txtCardno.Visible = true;
                txtfirstname.Visible = true;
            }
            else
            {
                txtCardno.Visible = false;
                txtfirstname.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    private string CalculateAge(DateTime Dob)
    {
        DateTime Now = DateTime.Now;
        int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
        DateTime PastYearDate = Dob.AddYears(Years);
        int Months = 0;
        for (int i = 1; i <= 12; i++)
        {
            if (PastYearDate.AddMonths(i) == Now)
            {
                Months = i;
                break;
            }
            else if (PastYearDate.AddMonths(i) >= Now)
            {
                Months = i - 1;
                break;
            }
        }
        int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
        return String.Format("{0} Year {1} Month {2} Day", Years, Months, Days);
    }
    protected void txtdob_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string dtVal = txtdob.Text.Trim();
            DateTime Dob = Convert.ToDateTime(dtVal);
            txtage.Text = CalculateAge(Dob);
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void grdRegtyp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
    }
    protected void grdRegtyp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string message = string.Empty;
        try
        {
            string slno = grdRegtyp.DataKeys[e.RowIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 100, slno);
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_REGISTRATION", "", "", SqlDbType.VarChar, SQL_PARAMS2, true);
            

            if (OBJ_METHOD._RESULT > 0)
            {
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 100, slno);
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 100, Session["UID"].ToString());

                OBJ_METHOD.ExecuteProceedure("usp_UserCollection", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);
                if (OBJ_METHOD._RESULT > 0)
                {
                    SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 100, slno);
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");

                    OBJ_METHOD.ExecuteProceedure("USP_REGISTRATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        binddata();
                        reset();
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message = "alert('" + OBJ_METHOD._objOut + "')";
                    }
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message = "alert('You Can Not deleted the Data.')";
                }
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message = "alert('Due to some issues, Data not Deleted.')";
            }
            message = "alert('" + OBJ_METHOD._objOut + "')";
            #region oldcode
            //using (SqlCommand cm = new SqlCommand("USP_REGISTRATION", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno.ToString();

            //    //cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
            //    cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@REGISNO", SqlDbType.VarChar).Value = txtRegstno.Text;
            //    cm.Parameters.Add("@REGISTYPE", SqlDbType.VarChar).Value = txtRegType.Text;
            //    cm.Parameters.Add("@AGINSTBOOK", SqlDbType.VarChar).Value = txtaginstbook.Text;
            //    cm.Parameters.Add("@BOOKINGNO", SqlDbType.VarChar).Value = txtbookno.Text;
            //    cm.Parameters.Add("@BOOKDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtbookdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@BOOKFOR", SqlDbType.VarChar).Value = txtbookfor.Text;
            //    cm.Parameters.Add("@MLCASE", SqlDbType.VarChar).Value = txtcase.Text;
            //    cm.Parameters.Add("@MLCCHECK", SqlDbType.Bit).Value = chkMlc.Checked;
            //    if (chkMlc.Checked)
            //    {

            //        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm");
            //    }
            //    else
            //    {

            //        txtdateMlc.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            //        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm"); ;
            //    }
            //    cm.Parameters.Add("@POLICEST", SqlDbType.VarChar).Value = txtPolicStaion.Text;
            //    cm.Parameters.Add("@AGAINSTCARD", SqlDbType.VarChar).Value = ddlaginCard.SelectedItem.Text;
            //    cm.Parameters.Add("@CARDNO", SqlDbType.VarChar).Value = txtCardno.Text;
            //    cm.Parameters.Add("@TITLE", SqlDbType.VarChar).Value = txtTitle.Text;
            //    cm.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = txtfirstname.Text;
            //    //cm.Parameters.Add("@MIDDLENAME", SqlDbType.VarChar).Value = txtmiddlnm.Text;
            //    //cm.Parameters.Add("@LASTNAME", SqlDbType.VarChar).Value = txtLastname.Text;
            //    cm.Parameters.Add("@PATIENTCOND", SqlDbType.VarChar).Value = txtpatintcond.Text;
            //    cm.Parameters.Add("@BILLOPTN", SqlDbType.VarChar).Value = txtbillopt.Text;
            //    cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtpname.Text;

            //    cm.Parameters.Add("@INSURACECO", SqlDbType.VarChar).Value = txtinsurce.Text;
            //    if (dropselfcorp.SelectedIndex != 0)
            //    {
            //        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropselfcorp.SelectedValue;
            //    }
            //    else
            //    {
            //        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = 0;
            //    }
            //    cm.Parameters.Add("@BPLCNO", SqlDbType.VarChar).Value = txtbplno.Text;
            //    cm.Parameters.Add("@FROMM", SqlDbType.VarChar).Value = txtfrom.Text;
            //    cm.Parameters.Add("@SOURCENM", SqlDbType.VarChar).Value = txtotSocenm.Text;
            //    cm.Parameters.Add("@REFFRALNAME", SqlDbType.VarChar).Value = dropreffname.SelectedItem.Text;
            //    //cm.Parameters.Add("@REFFNAMEF2", SqlDbType.VarChar).Value = txtreff2.Text;
            //    cm.Parameters.Add("@PRESNTCOMPL", SqlDbType.VarChar).Value = txtpcompl.Text;
            //    cm.Parameters.Add("@REVIEW", SqlDbType.VarChar).Value = txtreviw.Text;
            //    cm.Parameters.Add("@DEPTNAME", SqlDbType.VarChar).Value = txtdeptnm.Text;
            //    cm.Parameters.Add("@DOCTORNM", SqlDbType.VarChar).Value = txtdoctnm.Text;
            //    cm.Parameters.Add("@NATIONLITY", SqlDbType.VarChar).Value = txtnation.Text;
            //    cm.Parameters.Add("@PASSPT", SqlDbType.VarChar).Value = txtpasspt.Text;
            //    cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
            //    cm.Parameters.Add("@BLOODGP", SqlDbType.VarChar).Value = dropblogrp.SelectedItem.Text;
            //    cm.Parameters.Add("@DOB", SqlDbType.Date).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
            //    cm.Parameters.Add("@ADDRES", SqlDbType.VarChar).Value = txtAddress.Text;
            //    cm.Parameters.Add("@DIST", SqlDbType.VarChar).Value = txtdistrict.Text;
            //    cm.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
            //    cm.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = txttelphone.Text;
            //    cm.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = txtmobno.Text;
            //    cm.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = txtemailid.Text;

            //    cm.Parameters.Add("@MINORCHECK", SqlDbType.Bit).Value = chkMinrPatent.Checked;
            //    cm.Parameters.Add("@GURDIANAME", SqlDbType.VarChar).Value = txtguardname.Text;
            //    cm.Parameters.Add("@RELTNPATIENT", SqlDbType.VarChar).Value = txtrelpatnt.Text;
            //    cm.Parameters.Add("@GURDINADDRES", SqlDbType.VarChar).Value = txtAdressInf.Text;
            //    //cm.Parameters.Add("@GURDINAREA", SqlDbType.VarChar).Value = txtAreaInf.Text;
            //    cm.Parameters.Add("@GURDINDIST", SqlDbType.VarChar).Value = txtdistrictInf.Text;
            //    cm.Parameters.Add("@GURDINSATE", SqlDbType.VarChar).Value = txtstateInf.Text;
            //    cm.Parameters.Add("@GURDINTELPHNO", SqlDbType.VarChar).Value = txtphInf.Text;
            //    cm.Parameters.Add("@GURDINMOBLE", SqlDbType.VarChar).Value = txtmobnoInf.Text;
            //    cm.Parameters.Add("@GURDINEMAILID", SqlDbType.VarChar).Value = txtemailInf.Text;
            //    cm.Parameters.Add("@REGSTMADE", SqlDbType.VarChar).Value = txtregMade.Text;
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
            //    cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = lbluhid.Text;
            //    cm.ExecuteNonQuery();

            //}
            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_REGISTRATION", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = slno.ToString();
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT REGISTRATION";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtcharge.Text;
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0.00";
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cm.ExecuteNonQuery();
            //}
            //using (SqlCommand cmd = new SqlCommand("usp_UserCollection", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno.ToString();
            //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
            //    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
            //    cmd.Parameters.Add("@Collection", SqlDbType.Decimal).Value = txtcharge.Text;
            //    cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno.ToString();
            //    cmd.ExecuteNonQuery();
            //}
            #endregion

          
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        reset();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (chkMlc.Checked)
            {
                if (txtcase.Text == "")
                {
                    string message = "alert('* Please!! Enter The Case..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtcase.Focus();
                    return;
                }
                else if (txtdateMlc.Text == "")
                {
                    string message = "alert('* Please!! Enter The MLC Date..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtdateMlc.Focus();
                    return;
                }
                else if (txtPolicStaion.Text == "")
                {
                    string message = "alert('* Please!! Enter The Name Of The Police Station..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtPolicStaion.Focus();
                    return;
                }
            }
            else
            {
                txtdateMlc.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }


            if (ddlaginCard.SelectedIndex == 0)
            {
                if (txtCardno.Text == "")
                {
                    string message = "alert('* Please!! Enter The Card Number..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtCardno.Focus();
                    return;
                }
                else if (txtfirstname.Text == "")
                {
                    string message = "alert('* Please!! Enter The Card Name..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtfirstname.Focus();
                    return;
                }
            }
            if (txtpname.Text == "")
            {
                string message = "alert('* Please!! Enter The Name Of The Patient..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpname.Focus();
                return;
            }

            if (dropreffname.SelectedIndex != 0)
            {
                if (txtfrom.Text == "")
                {
                    string message = "alert('* Please!! Enter The Refferal From..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtfrom.Focus();
                    return;
                    //txtfrom.Focus();
                }
            }
            if (txtpcompl.Text == "")
            {
                string message = "alert('* Please!! Enter The Present Complaint..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpcompl.Focus();
                return;
            }
            if (txtnation.Text == "")
            {
                string message = "alert('* Please!! Enter The Nationality..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtnation.Focus();
                return;
            }
            if (dropgender.SelectedIndex == 0)
            {
                string message = "alert('* Please!! Select The Gender..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropgender.Focus();
                return;
            }
            if (txtdob.Text == "")
            {
                string message = "alert('* Please!! Enter The Date Of Birth..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdob.Focus();
                return;
            }
            //if (Convert.ToInt32(txtage.Text) >= 120)
            //{
            //    string message = "alert('* Please!! Age Is Exceed The Format..')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    txtage.Focus();
            //    return;
            //}
            if (txtAddress.Text == "")
            {
                string message = "alert('* Please!! Enter The Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtAddress.Focus();
                return;
            }
            if (txtdistrict.Text == "")
            {
                string message = "alert('* Please!! Enter The District..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdistrict.Focus();
                return;
            }
            if (txtstate.Text == "")
            {
                string message = "alert('* Please!! Enter The State..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtstate.Focus();
                return;
            }
            if (txtcharge.Text == "" || txtcharge.Text == "0")
            {
                string message = "alert('* Please!! Enter The Charges..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcharge.Focus();
                return;
            }
            if (chkMinrPatent.Checked)
            {
                if (txtguardname.Text == "")
                {
                    string message = "alert('* Please!! Enter The Name Of The Guardian Name If Patient Is Minor..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtguardname.Focus();
                    return;
                }
                else if (txtrelpatnt.Text == "")
                {
                    string message = "alert('* Please!! Enter The Relation With Patient..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtrelpatnt.Focus();
                    return;
                }
                else if (txtAdressInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The Address Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtAdressInf.Focus();
                    return;
                }
                else if (txtdistrictInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The District Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtdistrictInf.Focus();
                    return;
                }
                else if (txtstateInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The State Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtstateInf.Focus();
                    return;
                }
                else if (txtmobnoInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The Mobile Number Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    txtmobnoInf.Focus();
                    return;
                }
            }
            if (txtbookdate.Text == "")
            {
                txtbookdate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            
            #region oldcode
            //autouhid();
            //using (SqlCommand cm = new SqlCommand("USP_REGISTRATION", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = LBLSLNO.Text;

            //    //cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
            //    cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@REGISNO", SqlDbType.VarChar).Value = txtRegstno.Text;
            //    cm.Parameters.Add("@REGISTYPE", SqlDbType.VarChar).Value = txtRegType.Text;
            //    cm.Parameters.Add("@AGINSTBOOK", SqlDbType.VarChar).Value = txtaginstbook.Text;
            //    cm.Parameters.Add("@BOOKINGNO", SqlDbType.VarChar).Value = txtbookno.Text;
            //    cm.Parameters.Add("@BOOKDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtbookdate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@BOOKFOR", SqlDbType.VarChar).Value = txtbookfor.Text;
            //    cm.Parameters.Add("@MLCASE", SqlDbType.VarChar).Value = txtcase.Text;
            //    cm.Parameters.Add("@MLCCHECK", SqlDbType.Bit).Value = chkMlc.Checked;
            //    if (chkMlc.Checked)
            //    {

            //        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm");
            //    }
            //    else
            //    {

            //        txtdateMlc.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            //        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm"); ;
            //    }
            //    cm.Parameters.Add("@POLICEST", SqlDbType.VarChar).Value = txtPolicStaion.Text;
            //    cm.Parameters.Add("@AGAINSTCARD", SqlDbType.VarChar).Value = ddlaginCard.SelectedItem.Text;
            //    cm.Parameters.Add("@CARDNO", SqlDbType.VarChar).Value = txtCardno.Text;
            //    cm.Parameters.Add("@TITLE", SqlDbType.VarChar).Value = txtTitle.Text;
            //    cm.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = txtfirstname.Text;
            //    //cm.Parameters.Add("@MIDDLENAME", SqlDbType.VarChar).Value = txtmiddlnm.Text;
            //    //cm.Parameters.Add("@LASTNAME", SqlDbType.VarChar).Value = txtLastname.Text;
            //    cm.Parameters.Add("@PATIENTCOND", SqlDbType.VarChar).Value = txtpatintcond.Text;
            //    cm.Parameters.Add("@BILLOPTN", SqlDbType.VarChar).Value = txtbillopt.Text;
            //    cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtpname.Text;

            //    cm.Parameters.Add("@INSURACECO", SqlDbType.VarChar).Value = txtinsurce.Text;
            //    if (dropselfcorp.SelectedIndex != 0)
            //    {
            //        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropselfcorp.SelectedValue;
            //    }
            //    else
            //    {
            //        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = 0;
            //    }
            //    cm.Parameters.Add("@BPLCNO", SqlDbType.VarChar).Value = txtbplno.Text;
            //    cm.Parameters.Add("@FROMM", SqlDbType.VarChar).Value = txtfrom.Text;
            //    cm.Parameters.Add("@SOURCENM", SqlDbType.VarChar).Value = txtotSocenm.Text;
            //    cm.Parameters.Add("@REFFRALNAME", SqlDbType.VarChar).Value = dropreffname.SelectedItem.Text;
            //    //cm.Parameters.Add("@REFFNAMEF2", SqlDbType.VarChar).Value = txtreff2.Text;
            //    cm.Parameters.Add("@PRESNTCOMPL", SqlDbType.VarChar).Value = txtpcompl.Text;
            //    cm.Parameters.Add("@REVIEW", SqlDbType.VarChar).Value = txtreviw.Text;
            //    cm.Parameters.Add("@DEPTNAME", SqlDbType.VarChar).Value = txtdeptnm.Text;
            //    cm.Parameters.Add("@DOCTORNM", SqlDbType.VarChar).Value = txtdoctnm.Text;
            //    cm.Parameters.Add("@NATIONLITY", SqlDbType.VarChar).Value = txtnation.Text;
            //    cm.Parameters.Add("@PASSPT", SqlDbType.VarChar).Value = txtpasspt.Text;
            //    cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
            //    cm.Parameters.Add("@BLOODGP", SqlDbType.VarChar).Value = dropblogrp.SelectedItem.Text;
            //    cm.Parameters.Add("@DOB", SqlDbType.Date).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
            //    cm.Parameters.Add("@ADDRES", SqlDbType.VarChar).Value = txtAddress.Text;
            //    cm.Parameters.Add("@DIST", SqlDbType.VarChar).Value = txtdistrict.Text;
            //    cm.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
            //    cm.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = txttelphone.Text;
            //    cm.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = txtmobno.Text;
            //    cm.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = txtemailid.Text;

            //    cm.Parameters.Add("@MINORCHECK", SqlDbType.Bit).Value = chkMinrPatent.Checked;
            //    cm.Parameters.Add("@GURDIANAME", SqlDbType.VarChar).Value = txtguardname.Text;
            //    cm.Parameters.Add("@RELTNPATIENT", SqlDbType.VarChar).Value = txtrelpatnt.Text;
            //    cm.Parameters.Add("@GURDINADDRES", SqlDbType.VarChar).Value = txtAdressInf.Text;
            //    //cm.Parameters.Add("@GURDINAREA", SqlDbType.VarChar).Value = txtAreaInf.Text;
            //    cm.Parameters.Add("@GURDINDIST", SqlDbType.VarChar).Value = txtdistrictInf.Text;
            //    cm.Parameters.Add("@GURDINSATE", SqlDbType.VarChar).Value = txtstateInf.Text;
            //    cm.Parameters.Add("@GURDINTELPHNO", SqlDbType.VarChar).Value = txtphInf.Text;
            //    cm.Parameters.Add("@GURDINMOBLE", SqlDbType.VarChar).Value = txtmobnoInf.Text;
            //    cm.Parameters.Add("@GURDINEMAILID", SqlDbType.VarChar).Value = txtemailInf.Text;
            //    cm.Parameters.Add("@REGSTMADE", SqlDbType.VarChar).Value = txtregMade.Text;
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
            //    cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = lbluhid.Text;
            //    cm.ExecuteNonQuery();

            //}
            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_REGISTRATION", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT REGISTRATION";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtcharge.Text;
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0.00";
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cm.ExecuteNonQuery();
            //}
            //using (SqlCommand cmd = new SqlCommand("usp_UserCollection", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
            //    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
            //    cmd.Parameters.Add("@Collection", SqlDbType.Decimal).Value = txtcharge.Text;
            //    cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = LBLSLNO.Text;
            //    cmd.ExecuteNonQuery();
            //}

            //string message1 = "alert('Registration Saved Successfully .')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            //reset();
            //con.Close();
            #endregion

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[8];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, Session["UID"].ToString());
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@Date", SqlDbType.Date, 0, DateTime.Now.ToString());
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@Collection", SqlDbType.Decimal, 0, Convert.ToDecimal(txtcharge.Text));
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@Paid", SqlDbType.Decimal, 0, "0.00");
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, LBLSLNO.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            

            OBJ_METHOD.ExecuteProceedure("usp_UserCollection", "", "", SqlDbType.VarChar, SQL_PARAMS2, true);
            

            if (OBJ_METHOD._RESULT > 0)
            {

                SqlParameter[] SQL_PARAMS1 = new SqlParameter[12];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm"));
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, LBLSLNO.Text);
                SQL_PARAMS1[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 200, LBLSLNO.Text);
                SQL_PARAMS1[4] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 200, "PATIENT REGISTRATION");
                SQL_PARAMS1[5] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 0, txtcharge.Text);
                SQL_PARAMS1[6] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 0, "OUTPATIENT");
                SQL_PARAMS1[7] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 0, txtcharge.Text);
                SQL_PARAMS1[8] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 0, "0.00");
                SQL_PARAMS1[9] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 0, LBLSLNO.Text);
                SQL_PARAMS1[10] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS1[11] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_REGISTRATION", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    /////
                    SqlParameter[] SQL_PARAMS = new SqlParameter[56];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, LBLSLNO.Text);

                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, txtDate.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@REGISNO", SqlDbType.VarChar, 500, txtRegstno.Text);
                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@REGISTYPE", SqlDbType.VarChar, 500, txtRegType.Text);
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@AGINSTBOOK", SqlDbType.VarChar, 500, txtaginstbook.Text);
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@BOOKINGNO", SqlDbType.VarChar, 500, txtbookno.Text);
                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@BOOKDATE", SqlDbType.DateTime, 0, txtbookdate.Text);
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@BOOKFOR", SqlDbType.VarChar, 500, txtbookfor.Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@MLCASE", SqlDbType.VarChar, 500, txtcase.Text);
                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@MLCCHECK", SqlDbType.Bit, 0, chkMlc.Checked);

                    if (chkMlc.Checked)
                    {
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@MLCDATE", SqlDbType.DateTime, 0, txtdateMlc.Text);
                    }
                    else
                    {
                        //txtdateMlc.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@MLCDATE", SqlDbType.DateTime, 0, txtdateMlc.Text);
                    }


                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@POLICEST", SqlDbType.VarChar, 500, txtPolicStaion.Text);
                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@AGAINSTCARD", SqlDbType.VarChar, 500, ddlaginCard.SelectedItem.Text);
                    SQL_PARAMS[14] = OBJ_METHOD.createParams("@CARDNO", SqlDbType.VarChar, 500, txtCardno.Text);
                    SQL_PARAMS[15] = OBJ_METHOD.createParams("@TITLE", SqlDbType.VarChar, 500, txtTitle.Text);
                    SQL_PARAMS[16] = OBJ_METHOD.createParams("@FNAME", SqlDbType.VarChar, 500, txtfirstname.Text);
                    SQL_PARAMS[17] = OBJ_METHOD.createParams("@PATIENTCOND", SqlDbType.VarChar, 500, txtpatintcond.Text);
                    SQL_PARAMS[18] = OBJ_METHOD.createParams("@BILLOPTN", SqlDbType.VarChar, 500, txtbillopt.Text);
                    SQL_PARAMS[19] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 500, txtpname.Text);
                    SQL_PARAMS[20] = OBJ_METHOD.createParams("@INSURACECO", SqlDbType.VarChar, 500, txtinsurce.Text);

                    if (dropselfcorp.SelectedIndex != 0)
                    {
                        SQL_PARAMS[21] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, dropselfcorp.SelectedValue);
                    }
                    else
                    {
                        SQL_PARAMS[21] = OBJ_METHOD.createParams("@CORPORATE", SqlDbType.VarChar, 500, "0");
                        //cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = 0;
                    }
                    SQL_PARAMS[22] = OBJ_METHOD.createParams("@BPLCNO", SqlDbType.VarChar, 500, txtbplno.Text);
                    SQL_PARAMS[23] = OBJ_METHOD.createParams("@FROMM", SqlDbType.VarChar, 500, txtfrom.Text);
                    SQL_PARAMS[24] = OBJ_METHOD.createParams("@SOURCENM", SqlDbType.VarChar, 500, txtotSocenm.Text);
                    SQL_PARAMS[25] = OBJ_METHOD.createParams("@REFFRALNAME", SqlDbType.VarChar, 500, dropreffname.SelectedItem.Text);
                    SQL_PARAMS[26] = OBJ_METHOD.createParams("@PRESNTCOMPL", SqlDbType.VarChar, 500, txtpcompl.Text);
                    //cm.Parameters.Add("@REFFNAMEF2", SqlDbType.VarChar).Value = txtreff2.Text;
                    SQL_PARAMS[27] = OBJ_METHOD.createParams("@REVIEW", SqlDbType.VarChar, 500, txtreviw.Text);
                    SQL_PARAMS[28] = OBJ_METHOD.createParams("@DEPTNAME", SqlDbType.VarChar, 500, ddldept.SelectedItem.Text);
                    SQL_PARAMS[29] = OBJ_METHOD.createParams("@DOCTORNM", SqlDbType.VarChar, 500, ddldoctor.SelectedItem.Text);
                    SQL_PARAMS[30] = OBJ_METHOD.createParams("@NATIONLITY", SqlDbType.VarChar, 500, txtnation.Text);
                    SQL_PARAMS[31] = OBJ_METHOD.createParams("@PASSPT", SqlDbType.VarChar, 500, txtpasspt.Text);
                    SQL_PARAMS[32] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, dropgender.SelectedItem.Text);
                    SQL_PARAMS[33] = OBJ_METHOD.createParams("@BLOODGP", SqlDbType.VarChar, 500, dropblogrp.SelectedItem.Text);
                    SQL_PARAMS[34] = OBJ_METHOD.createParams("@DOB", SqlDbType.Date, 0, Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd"));
                    SQL_PARAMS[35] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 100, txtage.Text);
                    SQL_PARAMS[36] = OBJ_METHOD.createParams("@ADDRES", SqlDbType.VarChar, 500, txtAddress.Text);
                    SQL_PARAMS[37] = OBJ_METHOD.createParams("@DIST", SqlDbType.VarChar, 500, txtdistrict.Text);
                    SQL_PARAMS[38] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
                    SQL_PARAMS[39] = OBJ_METHOD.createParams("@TELPHNO", SqlDbType.VarChar, 500, txttelphone.Text);
                    SQL_PARAMS[40] = OBJ_METHOD.createParams("@MOBNO", SqlDbType.VarChar, 500, txtmobno.Text);
                    SQL_PARAMS[41] = OBJ_METHOD.createParams("@EMAILID", SqlDbType.VarChar, 500, txtemailid.Text);

                    SQL_PARAMS[42] = OBJ_METHOD.createParams("@MINORCHECK", SqlDbType.Bit, 500, chkMinrPatent.Checked);
                    SQL_PARAMS[43] = OBJ_METHOD.createParams("@GURDIANAME", SqlDbType.VarChar, 500, txtguardname.Text);
                    SQL_PARAMS[44] = OBJ_METHOD.createParams("@RELTNPATIENT", SqlDbType.VarChar, 500, txtrelpatnt.Text);
                    SQL_PARAMS[45] = OBJ_METHOD.createParams("@GURDINADDRES", SqlDbType.VarChar, 500, txtAdressInf.Text);
                    SQL_PARAMS[46] = OBJ_METHOD.createParams("@GURDINDIST", SqlDbType.VarChar, 500, txtdistrictInf.Text);
                    SQL_PARAMS[47] = OBJ_METHOD.createParams("@GURDINSATE", SqlDbType.VarChar, 500, txtstateInf.Text);
                    SQL_PARAMS[48] = OBJ_METHOD.createParams("@GURDINTELPHNO", SqlDbType.VarChar, 500, txtphInf.Text);
                    SQL_PARAMS[49] = OBJ_METHOD.createParams("@GURDINMOBLE", SqlDbType.VarChar, 500, txtmobnoInf.Text);
                    SQL_PARAMS[50] = OBJ_METHOD.createParams("@GURDINEMAILID", SqlDbType.VarChar, 500, txtemailInf.Text);
                    SQL_PARAMS[51] = OBJ_METHOD.createParams("@REGSTMADE", SqlDbType.VarChar, 500, txtregMade.Text);
                    SQL_PARAMS[52] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, txtcharge.Text);
                    SQL_PARAMS[53] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 500, lbluhid.Text);
                    SQL_PARAMS[54] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[55] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


                    OBJ_METHOD.ExecuteProceedure("USP_REGISTRATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                    if (OBJ_METHOD._RESULT > 0)
                    {
                        OBJ_METHOD.commitOrRollbackTran("commit");
                        binddata();
                        
                    }
                    else
                    {
                        OBJ_METHOD.commitOrRollbackTran("rollback");
                        message1 = "alert('Due to some issues, Data not saved.')";
                    }
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Due to some issues, Data not saved.')";
                }
            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not saved.')";
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            Session["PID"] = LBLSLNO.Text;
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
           
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        Response.Redirect("~/RECEPTION/reception_preg_reciept.aspx");
    }
    protected void grdRegtyp_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";

        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";

        }
        DataView sortedView = new DataView(getdata());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        grdRegtyp.DataSource = sortedView;
        grdRegtyp.DataBind();
    }
    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();
        SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_REGISTRATION");
        SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

        DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_REGISTRATION", false, true, SQL_PARAMS1);

        //DataSet Ds = OBJ_METHOD.Get_DataSet("select id,DeptName from tblDepartment WHERE Branch_ID = " + Session["Branch"] + " order by id desc", false, false);

        dt = DS1.Tables[0];
        return dt;

    }
}