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

public partial class RECEPTION_Registrationpage : System.Web.UI.Page
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
            Response.Redirect("~/RECEPTION/preg_reciept.aspx");
      
    }
    //public void autouhid()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    string qry1 = "select UHID as idh from REGISTRATION_TBL";

    //    com = new SqlCommand(qry1, con);
    //    dr = null;

    //    dr = com.ExecuteReader();

    //    while (dr.Read())
    //    {
    //        num2 = dr["idh"].ToString();
    //    }
    //    num2 = string.Format("VH{0}", (Convert.ToUInt32(num2.Substring(2)) + 1).ToString("D4"));
    //    lbluhid.Text = num2;

    //    dr.Close();
    //    con.Close();
    //}
    //public void auto()
    //{   
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    string qry1 = "select ID as id from REGISTRATION_TBL";

    //    com = new SqlCommand(qry1, con);
    //    dr = null;

    //    dr = com.ExecuteReader();

    //    while (dr.Read())
    //    {
    //        num1 = dr["id"].ToString();
    //    }
    //    num1 = string.Format("OP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
    //    LBLSLNO.Text = num1;

    //    dr.Close();
    //    con.Close();
    //}
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
            txtregMade.Text = Session["NAME"].ToString();
            if (!IsPostBack)
            {
                binddata();
            }
            con.Close();
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {

            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        using (SqlCommand cmd = new SqlCommand("RECP_REGISTRATION", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_REGISTRATION";
            cmd.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = "";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //SqlDataAdapter da1 = new SqlDataAdapter("select slno,ID,FNAME,DOB,DATETIME FROM REGISTRATION_TBL ORDER BY ID desc", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            grdRegtyp.SelectedIndex = 0;
            grdRegtyp.DataSource = dt1;
            // grdRegtyp.DataKeyNames = new string[] { "ID" };  
            grdRegtyp.DataBind();
        }
        using (SqlCommand cmd1 = new SqlCommand("RECP_REGISTRATION", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TESTCOMPONENT";
            cmd1.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = "";
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //SqlDataAdapter da = new SqlDataAdapter("select distinct b.C_ID as ID,a.CNAME as CNAME from  Corporate_Table a,TEST_COMPONENT_TABLE b where a.ID=b.C_ID and a.ISACTIVE='true'", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropselfcorp.DataSource = ds;
            dropselfcorp.DataTextField = "CNAME";
            dropselfcorp.DataValueField = "ID";
            dropselfcorp.DataBind();
            dropselfcorp.Items.Insert(0, "---Select---");
        }

        using (SqlCommand cmd2 = new SqlCommand("RECP_REGISTRATION", con))
        {
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOW_BROKER";
            cmd2.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = "";
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            //SqlDataAdapter da2 = new SqlDataAdapter("select * from Broker_Table ", con);
            DataTable ds1 = new DataTable();
            da2.Fill(ds1);
            dropreffname.DataSource = ds1;
            dropreffname.DataTextField = "NAME";
            dropreffname.DataValueField = "ID";
            dropreffname.DataBind();
            dropreffname.Items.Insert(0, "---Select---");
        }

        con.Close();
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            if (txtbookno.Text != "")
            {
                using (SqlCommand cm = new SqlCommand("RECP_REGISTRATION", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BOOKING";
                    cm.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = txtbookno.Text;
                    dr = cm.ExecuteReader();
                    if (dr.Read())
                    {
                        string message = "alert('* This booking No. Is Already Registered..')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                        return;
                    }
                    dr.Close();
                }
            }
            if (chkMlc.Checked)
            {
                if (txtcase.Text == "")
                {
                    string message = "alert('* Please!! Enter The Case..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                }
                else if (txtdateMlc.Text == "")
                {
                    string message = "alert('* Please!! Enter The MLC Date..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                }
                else if (txtPolicStaion.Text == "")
                {
                    string message = "alert('* Please!! Enter The Name Of The Police Station..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

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

                    return;
                }
                else if (txtfirstname.Text == "")
                {
                    string message = "alert('* Please!! Enter The Card Name..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                }
            }
            if (txtpname.Text == "")
            {
                string message = "alert('* Please!! Enter The Name Of The Patient..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            if (dropreffname.SelectedIndex != 0)
            {
                if (txtfrom.Text == "")
                {
                    string message = "alert('* Please!! Enter The Refferal From..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                    //txtfrom.Focus();
                }
            }
            if (txtpcompl.Text == "")
            {
                string message = "alert('* Please!! Enter The Present Complaint..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtnation.Text == "")
            {
                string message = "alert('* Please!! Enter The Nationality..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
                return;
            }
            //if (Convert.ToInt32(txtage.Text) >= 120)
            //{
            //    string message = "alert('* Please!! Age Is Exceed The Format..')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    return;
            //}
            if (txtAddress.Text == "")
            {
                string message = "alert('* Please!! Enter The Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtdistrict.Text == "")
            {
                string message = "alert('* Please!! Enter The District..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtstate.Text == "")
            {
                string message = "alert('* Please!! Enter The State..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtcharge.Text == "" || txtcharge.Text == "0")
            {
                string message = "alert('* Please!! Enter The Charges..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (chkMinrPatent.Checked)
            {
                if (txtguardname.Text == "")
                {
                    string message = "alert('* Please!! Enter The Name Of The Guardian Name If Patient Is Minor..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtrelpatnt.Text == "")
                {
                    string message = "alert('* Please!! Enter The Relation With Patient..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtAdressInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The Address Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtdistrictInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The District Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtstateInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The State Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtmobnoInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The Mobile Number Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
             if (txtbookdate.Text == "")
            {
                txtbookdate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {

                
                auto();
                //autouhid();
                using (SqlCommand cm = new SqlCommand("USP_REGISTRATION", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = LBLSLNO.Text;

                    //cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
                    cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
                    cm.Parameters.Add("@REGISNO", SqlDbType.VarChar).Value = txtRegstno.Text;
                    cm.Parameters.Add("@REGISTYPE", SqlDbType.VarChar).Value = txtRegType.Text;
                    cm.Parameters.Add("@AGINSTBOOK", SqlDbType.VarChar).Value = txtaginstbook.Text;
                    cm.Parameters.Add("@BOOKINGNO", SqlDbType.VarChar).Value = txtbookno.Text;
                    cm.Parameters.Add("@BOOKDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtbookdate.Text).ToString("yyyy-MM-dd HH:mm");
                    cm.Parameters.Add("@BOOKFOR", SqlDbType.VarChar).Value = txtbookfor.Text;
                    cm.Parameters.Add("@MLCASE", SqlDbType.VarChar).Value = txtcase.Text;
                    cm.Parameters.Add("@MLCCHECK", SqlDbType.Bit).Value = chkMlc.Checked;
                    if (chkMlc.Checked)
                    {
                        
                        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        
                        txtdateMlc.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm"); ;
                    }
                    cm.Parameters.Add("@POLICEST", SqlDbType.VarChar).Value = txtPolicStaion.Text;
                    cm.Parameters.Add("@AGAINSTCARD", SqlDbType.VarChar).Value = ddlaginCard.SelectedItem.Text;
                    cm.Parameters.Add("@CARDNO", SqlDbType.VarChar).Value = txtCardno.Text;
                    cm.Parameters.Add("@TITLE", SqlDbType.VarChar).Value = txtTitle.Text;
                    cm.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = txtfirstname.Text;
                    //cm.Parameters.Add("@MIDDLENAME", SqlDbType.VarChar).Value = txtmiddlnm.Text;
                    //cm.Parameters.Add("@LASTNAME", SqlDbType.VarChar).Value = txtLastname.Text;
                    cm.Parameters.Add("@PATIENTCOND", SqlDbType.VarChar).Value = txtpatintcond.Text;
                    cm.Parameters.Add("@BILLOPTN", SqlDbType.VarChar).Value = txtbillopt.Text;
                    cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtpname.Text;

                    cm.Parameters.Add("@INSURACECO", SqlDbType.VarChar).Value = txtinsurce.Text;
                    if (dropselfcorp.SelectedIndex != 0)
                    {
                        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropselfcorp.SelectedValue;
                    }
                    else
                    {
                        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = 0;
                    }
                    cm.Parameters.Add("@BPLCNO", SqlDbType.VarChar).Value = txtbplno.Text;
                    cm.Parameters.Add("@FROMM", SqlDbType.VarChar).Value = txtfrom.Text;
                    cm.Parameters.Add("@SOURCENM", SqlDbType.VarChar).Value = txtotSocenm.Text;
                    cm.Parameters.Add("@REFFRALNAME", SqlDbType.VarChar).Value = dropreffname.SelectedItem.Text;
                    //cm.Parameters.Add("@REFFNAMEF2", SqlDbType.VarChar).Value = txtreff2.Text;
                    cm.Parameters.Add("@PRESNTCOMPL", SqlDbType.VarChar).Value = txtpcompl.Text;
                    cm.Parameters.Add("@REVIEW", SqlDbType.VarChar).Value = txtreviw.Text;
                    cm.Parameters.Add("@DEPTNAME", SqlDbType.VarChar).Value = txtdeptnm.Text;
                    cm.Parameters.Add("@DOCTORNM", SqlDbType.VarChar).Value = txtdoctnm.Text;
                    cm.Parameters.Add("@NATIONLITY", SqlDbType.VarChar).Value = txtnation.Text;
                    cm.Parameters.Add("@PASSPT", SqlDbType.VarChar).Value = txtpasspt.Text;
                    cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
                    cm.Parameters.Add("@BLOODGP", SqlDbType.VarChar).Value = dropblogrp.SelectedItem.Text;
                    cm.Parameters.Add("@DOB", SqlDbType.Date).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");
                    cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
                    cm.Parameters.Add("@ADDRES", SqlDbType.VarChar).Value = txtAddress.Text;
                    cm.Parameters.Add("@DIST", SqlDbType.VarChar).Value = txtdistrict.Text;
                    cm.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                    cm.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = txttelphone.Text;
                    cm.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = txtmobno.Text;
                    cm.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = txtemailid.Text;

                    cm.Parameters.Add("@MINORCHECK", SqlDbType.Bit).Value = chkMinrPatent.Checked;
                    cm.Parameters.Add("@GURDIANAME", SqlDbType.VarChar).Value = txtguardname.Text;
                    cm.Parameters.Add("@RELTNPATIENT", SqlDbType.VarChar).Value = txtrelpatnt.Text;
                    cm.Parameters.Add("@GURDINADDRES", SqlDbType.VarChar).Value = txtAdressInf.Text;
                    //cm.Parameters.Add("@GURDINAREA", SqlDbType.VarChar).Value = txtAreaInf.Text;
                    cm.Parameters.Add("@GURDINDIST", SqlDbType.VarChar).Value = txtdistrictInf.Text;
                    cm.Parameters.Add("@GURDINSATE", SqlDbType.VarChar).Value = txtstateInf.Text;
                    cm.Parameters.Add("@GURDINTELPHNO", SqlDbType.VarChar).Value = txtphInf.Text;
                    cm.Parameters.Add("@GURDINMOBLE", SqlDbType.VarChar).Value = txtmobnoInf.Text;
                    cm.Parameters.Add("@GURDINEMAILID", SqlDbType.VarChar).Value = txtemailInf.Text;
                    cm.Parameters.Add("@REGSTMADE", SqlDbType.VarChar).Value = txtregMade.Text;
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
                    cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = lbluhid.Text;
                    cm.ExecuteNonQuery();

                }
                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_REGISTRATION", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = LBLSLNO.Text;
                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = LBLSLNO.Text;
                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT REGISTRATION";
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtcharge.Text;
                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0.00";
                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = LBLSLNO.Text;
                    cm.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("usp_UserCollection", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
                    cmd.Parameters.Add("@Collection", SqlDbType.Decimal).Value = txtcharge.Text;
                    cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = LBLSLNO.Text;
                    cmd.ExecuteNonQuery();
                }

                string message1 = "alert('Registration Saved Successfully .')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                reset();
                con.Close();
                Session["PID"] = LBLSLNO.Text;
            }

                //Response.Redirect("~/RECEPTION/Registrationpage.aspx");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/preg_reciept.aspx");
    }
     protected void grdRegtyp_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = grdRegtyp.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            SqlCommand com = new SqlCommand("SELECT * FROM REGISTRATION_TBL where ID='" + slno + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                LBLSLNO.Text = dr["ID"].ToString();
                txtDate.Text= Convert.ToDateTime(dr["DATETIME"]).ToString();
                txtbookno.Text = dr["BOOKINGNO"].ToString();
                txtbookdate.Text=Convert.ToDateTime(dr["BOOKDATE"].ToString()).ToString("yyyy-MM-dd HH:mm");
                txtbookfor.Text = dr["BOOKFOR"].ToString();
                txtcase.Text=dr["MLCASE"].ToString();
                chkMlc.Checked = Convert.ToBoolean(dr["MLCCHECK"]);
                txtdateMlc.Text=Convert.ToDateTime(dr["MLCDATE"].ToString()).ToString("dd-MM-yyyy");

                txtPolicStaion.Text=dr["POLICEST"].ToString();
                ddlaginCard.Text = dr["AGAINSTCARD"].ToString();
                txtCardno.Text = dr["CARDNO"].ToString();
                txtfirstname.Text = dr["FNAME"].ToString();
                txtpatintcond.Text=dr["PATIENTCOND"].ToString();
                txtbillopt.Text=dr["BILLOPTN"].ToString();
                txtpname.Text=dr["PNAME"].ToString();

                txtinsurce.Text = dr["INSURACECO"].ToString();
                Session["Corpo"] = dr["CORPORATE"].ToString();
                lblcorpo.Text = dr["CORPORATE"].ToString();
                txtbplno.Text=dr["BPLCNO"].ToString();
                txtfrom.Text=dr["FROMM"].ToString();
                txtotSocenm.Text=dr["SOURCENM"].ToString();
                dropreffname.SelectedItem.Text=dr["REFFRALNAME"].ToString();
                txtpcompl.Text=dr["PRESNTCOMPL"].ToString();
                txtreviw.Text=dr["REVIEW"].ToString();
                txtdeptnm.Text=dr["DEPTNAME"].ToString();
                txtdoctnm.Text=dr["DOCTORNM"].ToString();
                txtnation.Text=dr["NATIONLITY"].ToString();
                txtpasspt.Text=dr["PASSPT"].ToString();
                dropgender.Text = dr["GENDER"].ToString(); ;
                dropblogrp.Text=dr["BLOODGP"].ToString();
                txtdob.Text= Convert.ToDateTime(dr["DOB"]).ToString("yyyy-MM-dd HH:mm");
                txtage.Text=dr["AGE"].ToString();
                txtAddress.Text=dr["ADDRES"].ToString();
                txtdistrict.Text=dr["DIST"].ToString();
                txtstate.Text = dr["STATE"].ToString();
                txttelphone.Text=dr["TELPHNO"].ToString();
                txtmobno.Text=dr["MOBNO"].ToString();
                txtemailid.Text=dr["EMAILID"].ToString();

                chkMinrPatent.Checked = Convert.ToBoolean(dr["MINORCHECK"].ToString());
                txtguardname.Text=dr["GURDIANAME"].ToString();
                txtrelpatnt.Text=dr["RELTNPATIENT"].ToString();
                txtAdressInf.Text=dr["GURDINADDRES"].ToString();
                txtdistrictInf.Text=dr["GURDINDIST"].ToString();
                txtstateInf.Text=dr["GURDINSATE"].ToString();
                txtphInf.Text=dr["GURDINTELPHNO"].ToString();
                txtmobnoInf.Text=dr["GURDINMOBLE"].ToString();
                txtemailInf.Text=dr["GURDINEMAILID"].ToString();
                txtregMade.Text=dr["REGSTMADE"].ToString();
                txtcharge.Text=dr["CHARGES"].ToString();
                lbluhid.Text=dr["UHID"].ToString();
                dr.Close();
                if (lblcorpo.Text != "0")
                {
                    using (SqlCommand cmd1 = new SqlCommand("RECP_REGISTRATION", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TESTCOMPONENT";
                        cmd1.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = "";
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        //SqlDataAdapter da = new SqlDataAdapter("select distinct b.C_ID as ID,a.CNAME as CNAME from  Corporate_Table a,TEST_COMPONENT_TABLE b where a.ID=b.C_ID and a.ISACTIVE='true'", con);
                        DataTable ds = new DataTable();
                        da.Fill(ds);
                        dropselfcorp.DataSource = ds;
                        dropselfcorp.DataTextField = "CNAME";
                        dropselfcorp.DataValueField = "ID";
                        dropselfcorp.DataBind();
                        dropselfcorp.Items.Insert(0, "---Select---");
                        dropselfcorp.SelectedValue = Session["Corpo"].ToString();
                    }
                }
                else
                {
                    using (SqlCommand cmd1 = new SqlCommand("RECP_REGISTRATION", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_TESTCOMPONENT";
                        cmd1.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = "";
                        SqlDataAdapter da = new SqlDataAdapter(cmd1);
                        //SqlDataAdapter da = new SqlDataAdapter("select distinct b.C_ID as ID,a.CNAME as CNAME from  Corporate_Table a,TEST_COMPONENT_TABLE b where a.ID=b.C_ID and a.ISACTIVE='true'", con);
                        DataTable ds = new DataTable();
                        da.Fill(ds);
                        dropselfcorp.DataSource = ds;
                        dropselfcorp.DataTextField = "CNAME";
                        dropselfcorp.DataValueField = "ID";
                        dropselfcorp.DataBind();
                        dropselfcorp.Items.Insert(0, "---Select---");
                    }
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void reset()
    {
        txtDate.Text="";
        txtRegstno.Text="";
        txtRegType.Text="";
        txtaginstbook.Text="";
        txtbookno.Text="";
        txtbookdate.Text=""; 
        txtbookfor.Text="";
        txtcase.Text="";
        txtdateMlc.Text="";
        txtPolicStaion.Text="";
        ddlaginCard.SelectedIndex=0;
        txtCardno.Text="";
        txtTitle.Text="";
        txtfirstname.Text="";
       // txtmiddlnm.Text="";
        //txtLastname.Text="";
        txtpatintcond.Text="";
        txtbillopt.Text="";
        txtpname.Text="";

        txtinsurce.Text="";
        dropselfcorp.SelectedIndex=0;
         txtbplno.Text="";
        txtfrom.Text="";
        txtotSocenm.Text="";
        dropreffname.SelectedIndex=0;
      //  txtreff2.Text="";
        txtpcompl.Text="";
        txtreviw.Text="";
        txtdeptnm.Text="";
        txtdoctnm.Text="";
        txtnation.Text="";
        txtpasspt.Text="";
        dropgender.SelectedIndex=0;
        dropblogrp.SelectedIndex=0;
        txtdob.Text="";
         txtage.Text="";
        //txtyears.Text="";
        //txtmonth.Text="";
        //txtdays.Text="";
        //txtarea.Text="";

        txtAddress.Text="";
       txtdistrict.Text="";
       txtstate.Text="";
        txttelphone.Text="";
        txtmobno.Text="";
         txtemailid.Text="";
        txtguardname.Text="";
        txtrelpatnt.Text="";
        txtAdressInf.Text="";
        //txtAreaInf.Text="";
         txtdistrictInf.Text="";
        txtstateInf.Text="";
        txtphInf.Text="";
        txtmobnoInf.Text="";
        txtemailInf.Text="";
        txtregMade.Text = "";
        txtcharge.Text = "";
        chkMlc.Checked = false;
        chkMinrPatent.Checked = false;
    }
    protected void txtbookno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd2 = new SqlCommand("RECP_REGISTRATION", con))
            {
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECTED_EVENT";
                cmd2.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = txtbookno.Text;
                //SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                //SqlCommand COM = new SqlCommand("SELECT * FROM RESERVATION_TABLE where BOOKING_NO='" + txtbookno.Text + "' ", con);
                dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    //lblminno.Text = dr["MRNO"].ToString();  
                    txtbookfor.Text = dr["BOOKING_FOR"].ToString();
                    txtbookdate.Text = dr["BOOKING_DATE"].ToString();
                    txtfrom.Text = dr["FROM"].ToString();
                    // txtrefname.Text = dr["REFERALNAME"].ToString();
                    txtpcompl.Text = dr["COMPLAINT"].ToString();
                    txtpname.Text = dr["NAME"].ToString();
                    //txtdoctnm.Text = dr["DOCTORNAME"].ToString();
                    dropgender.Text = dr["GENDER"].ToString();
                    dropblogrp.Text = dr["BLOODGROUP"].ToString();
                    txtdob.Text = Convert.ToDateTime(dr["DATEOFBIRTH"]).ToString("dd-MM-yyyy");
                    txtAddress.Text = dr["ADDRESS"].ToString();
                    txtdistrict.Text = dr["DISTRICT"].ToString();
                    txtstate.Text = dr["STATE"].ToString();
                    txttelphone.Text = dr["TELEPHONE"].ToString();
                    txtmobno.Text = dr["MOBILE"].ToString();
                    txtemailid.Text = dr["EMAIL"].ToString();
                    //txtbookfor.Text = dr["BOOKING_FOR"].ToString();
                    //txtbookfor.Text = dr["BOOKING_FOR"].ToString();
                }
                dr.Close();

            }
            con.Close();
            string dtVal = txtdob.Text.Trim();
            DateTime Dob = Convert.ToDateTime(dtVal);
            txtage.Text = CalculateAge(Dob);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdRegtyp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_REGISTRATION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_REGISTRATION";
                cmd.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT VN AS ID,NAME,PHONE,BEDNO FROM ADMISSION_TABLE WHERE  ORGID='" + lblorgid.Text + "' ORDER BY ID DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grdRegtyp.SelectedIndex = 0;
                grdRegtyp.DataSource = dt;
                grdRegtyp.PageIndex = e.NewPageIndex;
                grdRegtyp.DataKeyNames = new string[] { "ID" };
                grdRegtyp.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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
            Console.WriteLine("An error occurred: '{0}'", ex);
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
        //int Hours = Now.Subtract(PastYearDate).Hours;
        //int Minutes = Now.Subtract(PastYearDate).Minutes;
        //int Seconds = Now.Subtract(PastYearDate).Seconds;
        return String.Format("{0} Year {1} Month {2} Day",Years, Months, Days);
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
            Console.WriteLine("An error occurred: '{0}'", ex);
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
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            string slno = grdRegtyp.DataKeys[e.RowIndex].Values["ID"].ToString();

            using (SqlCommand cm = new SqlCommand("USP_REGISTRATION", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno.ToString();

                //cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
                cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
                cm.Parameters.Add("@REGISNO", SqlDbType.VarChar).Value = txtRegstno.Text;
                cm.Parameters.Add("@REGISTYPE", SqlDbType.VarChar).Value = txtRegType.Text;
                cm.Parameters.Add("@AGINSTBOOK", SqlDbType.VarChar).Value = txtaginstbook.Text;
                cm.Parameters.Add("@BOOKINGNO", SqlDbType.VarChar).Value = txtbookno.Text;
                cm.Parameters.Add("@BOOKDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtbookdate.Text).ToString("yyyy-MM-dd HH:mm");
                cm.Parameters.Add("@BOOKFOR", SqlDbType.VarChar).Value = txtbookfor.Text;
                cm.Parameters.Add("@MLCASE", SqlDbType.VarChar).Value = txtcase.Text;
                cm.Parameters.Add("@MLCCHECK", SqlDbType.Bit).Value = chkMlc.Checked;
                if (chkMlc.Checked)
                {
                        
                    cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                        
                    txtdateMlc.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                    cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm"); ;
                }
                cm.Parameters.Add("@POLICEST", SqlDbType.VarChar).Value = txtPolicStaion.Text;
                cm.Parameters.Add("@AGAINSTCARD", SqlDbType.VarChar).Value = ddlaginCard.SelectedItem.Text;
                cm.Parameters.Add("@CARDNO", SqlDbType.VarChar).Value = txtCardno.Text;
                cm.Parameters.Add("@TITLE", SqlDbType.VarChar).Value = txtTitle.Text;
                cm.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = txtfirstname.Text;
                //cm.Parameters.Add("@MIDDLENAME", SqlDbType.VarChar).Value = txtmiddlnm.Text;
                //cm.Parameters.Add("@LASTNAME", SqlDbType.VarChar).Value = txtLastname.Text;
                cm.Parameters.Add("@PATIENTCOND", SqlDbType.VarChar).Value = txtpatintcond.Text;
                cm.Parameters.Add("@BILLOPTN", SqlDbType.VarChar).Value = txtbillopt.Text;
                cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtpname.Text;

                cm.Parameters.Add("@INSURACECO", SqlDbType.VarChar).Value = txtinsurce.Text;
                if (dropselfcorp.SelectedIndex != 0)
                {
                    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropselfcorp.SelectedValue;
                }
                else
                {
                    cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = 0;
                }
                cm.Parameters.Add("@BPLCNO", SqlDbType.VarChar).Value = txtbplno.Text;
                cm.Parameters.Add("@FROMM", SqlDbType.VarChar).Value = txtfrom.Text;
                cm.Parameters.Add("@SOURCENM", SqlDbType.VarChar).Value = txtotSocenm.Text;
                cm.Parameters.Add("@REFFRALNAME", SqlDbType.VarChar).Value = dropreffname.SelectedItem.Text;
                //cm.Parameters.Add("@REFFNAMEF2", SqlDbType.VarChar).Value = txtreff2.Text;
                cm.Parameters.Add("@PRESNTCOMPL", SqlDbType.VarChar).Value = txtpcompl.Text;
                cm.Parameters.Add("@REVIEW", SqlDbType.VarChar).Value = txtreviw.Text;
                cm.Parameters.Add("@DEPTNAME", SqlDbType.VarChar).Value = txtdeptnm.Text;
                cm.Parameters.Add("@DOCTORNM", SqlDbType.VarChar).Value = txtdoctnm.Text;
                cm.Parameters.Add("@NATIONLITY", SqlDbType.VarChar).Value = txtnation.Text;
                cm.Parameters.Add("@PASSPT", SqlDbType.VarChar).Value = txtpasspt.Text;
                cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
                cm.Parameters.Add("@BLOODGP", SqlDbType.VarChar).Value = dropblogrp.SelectedItem.Text;
                cm.Parameters.Add("@DOB", SqlDbType.Date).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");
                cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
                cm.Parameters.Add("@ADDRES", SqlDbType.VarChar).Value = txtAddress.Text;
                cm.Parameters.Add("@DIST", SqlDbType.VarChar).Value = txtdistrict.Text;
                cm.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                cm.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = txttelphone.Text;
                cm.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = txtmobno.Text;
                cm.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = txtemailid.Text;

                cm.Parameters.Add("@MINORCHECK", SqlDbType.Bit).Value = chkMinrPatent.Checked;
                cm.Parameters.Add("@GURDIANAME", SqlDbType.VarChar).Value = txtguardname.Text;
                cm.Parameters.Add("@RELTNPATIENT", SqlDbType.VarChar).Value = txtrelpatnt.Text;
                cm.Parameters.Add("@GURDINADDRES", SqlDbType.VarChar).Value = txtAdressInf.Text;
                //cm.Parameters.Add("@GURDINAREA", SqlDbType.VarChar).Value = txtAreaInf.Text;
                cm.Parameters.Add("@GURDINDIST", SqlDbType.VarChar).Value = txtdistrictInf.Text;
                cm.Parameters.Add("@GURDINSATE", SqlDbType.VarChar).Value = txtstateInf.Text;
                cm.Parameters.Add("@GURDINTELPHNO", SqlDbType.VarChar).Value = txtphInf.Text;
                cm.Parameters.Add("@GURDINMOBLE", SqlDbType.VarChar).Value = txtmobnoInf.Text;
                cm.Parameters.Add("@GURDINEMAILID", SqlDbType.VarChar).Value = txtemailInf.Text;
                cm.Parameters.Add("@REGSTMADE", SqlDbType.VarChar).Value = txtregMade.Text;
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
                cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = lbluhid.Text;
                cm.ExecuteNonQuery();

            }
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_REGISTRATION", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = LBLSLNO.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = slno.ToString();
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT REGISTRATION";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtcharge.Text;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0.00";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = LBLSLNO.Text;
                cm.ExecuteNonQuery();
            }
            using (SqlCommand cmd = new SqlCommand("usp_UserCollection", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno.ToString();
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
                cmd.Parameters.Add("@Collection", SqlDbType.Decimal).Value = txtcharge.Text;
                cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno.ToString();
                cmd.ExecuteNonQuery();
            }

   
            binddata();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
        Response.Redirect("~/RECEPTION/Registrationpage.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/Registrationpage.aspx");
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkMlc.Checked)
            {
                if (txtcase.Text == "")
                {
                    string message = "alert('* Please!! Enter The Case..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                }
                else if (txtdateMlc.Text == "")
                {
                    string message = "alert('* Please!! Enter The MLC Date..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                }
                else if (txtPolicStaion.Text == "")
                {
                    string message = "alert('* Please!! Enter The Name Of The Police Station..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

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

                    return;
                }
                else if (txtfirstname.Text == "")
                {
                    string message = "alert('* Please!! Enter The Card Name..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                }
            }
            if (txtpname.Text == "")
            {
                string message = "alert('* Please!! Enter The Name Of The Patient..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            if (dropreffname.SelectedIndex != 0)
            {
                if (txtfrom.Text == "")
                {
                    string message = "alert('* Please!! Enter The Refferal From..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                    //txtfrom.Focus();
                }
            }
            if (txtpcompl.Text == "")
            {
                string message = "alert('* Please!! Enter The Present Complaint..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtnation.Text == "")
            {
                string message = "alert('* Please!! Enter The Nationality..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
                return;
            }
            if (Convert.ToInt32(txtage.Text) >= 120)
            {
                string message = "alert('* Please!! Age Is Exceed The Format..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtAddress.Text == "")
            {
                string message = "alert('* Please!! Enter The Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtdistrict.Text == "")
            {
                string message = "alert('* Please!! Enter The District..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtstate.Text == "")
            {
                string message = "alert('* Please!! Enter The State..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtcharge.Text == "" || txtcharge.Text == "0")
            {
                string message = "alert('* Please!! Enter The Charges..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (chkMinrPatent.Checked)
            {
                if (txtguardname.Text == "")
                {
                    string message = "alert('* Please!! Enter The Name Of The Guardian Name If Patient Is Minor..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtrelpatnt.Text == "")
                {
                    string message = "alert('* Please!! Enter The Relation With Patient..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtAdressInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The Address Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtdistrictInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The District Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtstateInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The State Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                else if (txtmobnoInf.Text == "")
                {
                    string message = "alert('* Please!! Enter The Mobile Number Of The Guardian..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            if (txtbookdate.Text == "")
            {
                txtbookdate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                con.Open();
                //autouhid();
                using (SqlCommand cm = new SqlCommand("USP_REGISTRATION", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = LBLSLNO.Text;

                    //cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd");
                    cm.Parameters.Add("@DATETIME", SqlDbType.Date).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
                    cm.Parameters.Add("@REGISNO", SqlDbType.VarChar).Value = txtRegstno.Text;
                    cm.Parameters.Add("@REGISTYPE", SqlDbType.VarChar).Value = txtRegType.Text;
                    cm.Parameters.Add("@AGINSTBOOK", SqlDbType.VarChar).Value = txtaginstbook.Text;
                    cm.Parameters.Add("@BOOKINGNO", SqlDbType.VarChar).Value = txtbookno.Text;
                    cm.Parameters.Add("@BOOKDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtbookdate.Text).ToString("yyyy-MM-dd HH:mm");
                    cm.Parameters.Add("@BOOKFOR", SqlDbType.VarChar).Value = txtbookfor.Text;
                    cm.Parameters.Add("@MLCASE", SqlDbType.VarChar).Value = txtcase.Text;
                    cm.Parameters.Add("@MLCCHECK", SqlDbType.Bit).Value = chkMlc.Checked;
                    if (chkMlc.Checked)
                    {

                        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {

                        txtdateMlc.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                        cm.Parameters.Add("@MLCDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdateMlc.Text).ToString("yyyy-MM-dd HH:mm"); ;
                    }
                    cm.Parameters.Add("@POLICEST", SqlDbType.VarChar).Value = txtPolicStaion.Text;
                    cm.Parameters.Add("@AGAINSTCARD", SqlDbType.VarChar).Value = ddlaginCard.SelectedItem.Text;
                    cm.Parameters.Add("@CARDNO", SqlDbType.VarChar).Value = txtCardno.Text;
                    cm.Parameters.Add("@TITLE", SqlDbType.VarChar).Value = txtTitle.Text;
                    cm.Parameters.Add("@FNAME", SqlDbType.VarChar).Value = txtfirstname.Text;
                    //cm.Parameters.Add("@MIDDLENAME", SqlDbType.VarChar).Value = txtmiddlnm.Text;
                    //cm.Parameters.Add("@LASTNAME", SqlDbType.VarChar).Value = txtLastname.Text;
                    cm.Parameters.Add("@PATIENTCOND", SqlDbType.VarChar).Value = txtpatintcond.Text;
                    cm.Parameters.Add("@BILLOPTN", SqlDbType.VarChar).Value = txtbillopt.Text;
                    cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtpname.Text;

                    cm.Parameters.Add("@INSURACECO", SqlDbType.VarChar).Value = txtinsurce.Text;
                    if (dropselfcorp.SelectedIndex != 0)
                    {
                        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = dropselfcorp.SelectedValue;
                    }
                    else
                    {
                        cm.Parameters.Add("@CORPORATE", SqlDbType.VarChar).Value = 0;
                    }
                    cm.Parameters.Add("@BPLCNO", SqlDbType.VarChar).Value = txtbplno.Text;
                    cm.Parameters.Add("@FROMM", SqlDbType.VarChar).Value = txtfrom.Text;
                    cm.Parameters.Add("@SOURCENM", SqlDbType.VarChar).Value = txtotSocenm.Text;
                    cm.Parameters.Add("@REFFRALNAME", SqlDbType.VarChar).Value = dropreffname.SelectedItem.Text;
                    //cm.Parameters.Add("@REFFNAMEF2", SqlDbType.VarChar).Value = txtreff2.Text;
                    cm.Parameters.Add("@PRESNTCOMPL", SqlDbType.VarChar).Value = txtpcompl.Text;
                    cm.Parameters.Add("@REVIEW", SqlDbType.VarChar).Value = txtreviw.Text;
                    cm.Parameters.Add("@DEPTNAME", SqlDbType.VarChar).Value = txtdeptnm.Text;
                    cm.Parameters.Add("@DOCTORNM", SqlDbType.VarChar).Value = txtdoctnm.Text;
                    cm.Parameters.Add("@NATIONLITY", SqlDbType.VarChar).Value = txtnation.Text;
                    cm.Parameters.Add("@PASSPT", SqlDbType.VarChar).Value = txtpasspt.Text;
                    cm.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.SelectedItem.Text;
                    cm.Parameters.Add("@BLOODGP", SqlDbType.VarChar).Value = dropblogrp.SelectedItem.Text;
                    cm.Parameters.Add("@DOB", SqlDbType.Date).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd HH:mm");
                    cm.Parameters.Add("@AGE", SqlDbType.VarChar).Value = txtage.Text;
                    cm.Parameters.Add("@ADDRES", SqlDbType.VarChar).Value = txtAddress.Text;
                    cm.Parameters.Add("@DIST", SqlDbType.VarChar).Value = txtdistrict.Text;
                    cm.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                    cm.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = txttelphone.Text;
                    cm.Parameters.Add("@MOBNO", SqlDbType.VarChar).Value = txtmobno.Text;
                    cm.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = txtemailid.Text;

                    cm.Parameters.Add("@MINORCHECK", SqlDbType.Bit).Value = chkMinrPatent.Checked;
                    cm.Parameters.Add("@GURDIANAME", SqlDbType.VarChar).Value = txtguardname.Text;
                    cm.Parameters.Add("@RELTNPATIENT", SqlDbType.VarChar).Value = txtrelpatnt.Text;
                    cm.Parameters.Add("@GURDINADDRES", SqlDbType.VarChar).Value = txtAdressInf.Text;
                    //cm.Parameters.Add("@GURDINAREA", SqlDbType.VarChar).Value = txtAreaInf.Text;
                    cm.Parameters.Add("@GURDINDIST", SqlDbType.VarChar).Value = txtdistrictInf.Text;
                    cm.Parameters.Add("@GURDINSATE", SqlDbType.VarChar).Value = txtstateInf.Text;
                    cm.Parameters.Add("@GURDINTELPHNO", SqlDbType.VarChar).Value = txtphInf.Text;
                    cm.Parameters.Add("@GURDINMOBLE", SqlDbType.VarChar).Value = txtmobnoInf.Text;
                    cm.Parameters.Add("@GURDINEMAILID", SqlDbType.VarChar).Value = txtemailInf.Text;
                    cm.Parameters.Add("@REGSTMADE", SqlDbType.VarChar).Value = txtregMade.Text;
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
                    cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = lbluhid.Text;
                    cm.ExecuteNonQuery();

                }
                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_REGISTRATION", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd HH:mm");
                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = LBLSLNO.Text;
                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = LBLSLNO.Text;
                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "";
                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PATIENT REGISTRATION";
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtcharge.Text;
                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "OUTPATIENT";
                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = txtcharge.Text;
                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0.00";
                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = LBLSLNO.Text;
                    cm.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("usp_UserCollection", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = Session["UID"].ToString();
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = DateTime.Now.ToString();
                    cmd.Parameters.Add("@Collection", SqlDbType.Decimal).Value = txtcharge.Text;
                    cmd.Parameters.Add("@Paid", SqlDbType.Decimal).Value = 0.00;
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = LBLSLNO.Text;
                    cmd.ExecuteNonQuery();
                }

                string message1 = "alert('Registration Saved Successfully .')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                reset();
                con.Close();
                Session["PID"] = LBLSLNO.Text;
            }

            //Response.Redirect("~/RECEPTION/Registrationpage.aspx");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/preg_reciept.aspx");
    }
}