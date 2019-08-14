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

public partial class RECEPTION_reception_Advance_Bill : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlDataReader dr, dr1;
    SqlConnection con;
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    string AMOUNT;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select id from tblAdvancePayment";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["id"].ToString();
        }
        num1 = string.Format("AP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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

            if (!IsPostBack)
            {
                binddata();


                txtcard.Visible = false;
                dropemp.Visible = false;
            }


            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            binddata();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
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

            string c_id = Session["i_id"].ToString();
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PID");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, c_id);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
            }

            //using (SqlCommand cmd = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PID";
            //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = c_id;
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("SELECT tblAdvancePayment.id,ADMISSION_TABLE.ID as PID,ADMISSION_TABLE.NAME,ADMISSION_TABLE.BEDNO,tblAdvancePayment.Amount,tblAdvancePayment.Adate FROM ADMISSION_TABLE INNER JOIN tblAdvancePayment ON ADMISSION_TABLE.VN = tblAdvancePayment.PID and tblAdvancePayment.PID='" + c_id + "'", con);
            //    DataTable Dt = new DataTable();
            //    da.Fill(Dt);
            //    GridView1.DataSource = Dt;
            //    GridView1.DataBind();
            //}
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_STAFF");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                dropemp.DataSource = DS2;
                dropemp.DataTextField = "Sname";
                dropemp.DataValueField = "EMPID";
                dropemp.DataBind();
                dropemp.Items.Insert(0, new ListItem("Please Select"));
            }
            //using (SqlCommand cmd1 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STAFF";
            //    cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";

            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //    //SqlDataAdapter da1 = new SqlDataAdapter("SELECT EMPID,Sname FROM tblStaff", con);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    //dropbedno.SelectedIndex = 0;
            //    dropemp.DataSource = dt1;
            //    dropemp.DataTextField = "Sname";
            //    dropemp.DataValueField = "EMPID";
            //    dropemp.DataBind();
            //    dropemp.Items.Insert(0, "Please Select");
            //}
            SQL_PARAMS2 = new SqlParameter[3];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ADMISSION");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, c_id);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS2);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                lblname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                lblbed.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                lblip.Text = Ds.Tables[0].Rows[0]["VN"].ToString();
                SQL_PARAMS2 = new SqlParameter[3];

                SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PA_MASTER");
                SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);
                SQL_PARAMS2[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);

                DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS2);
                if (Ds1.Tables[0].Rows.Count > 0)
                {
                    lbltotalamt.Text = Ds1.Tables[0].Rows[0]["CREDIT"].ToString();
                    lblpaidamt.Text = Ds1.Tables[0].Rows[0]["DEBIT"].ToString();
                    lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
                    txtamount.Text = lblremainamt.Text;
                }
            }
            #region old code
            //using (SqlCommand cmd2 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //{
            //    cmd2.CommandType = CommandType.StoredProcedure;
            //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION";
            //    cmd2.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd2.Parameters.Add("@VN", SqlDbType.VarChar).Value = c_id;
            //    cmd2.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";

            //    //SqlDataAdapter da1 = new SqlDataAdapter(cmd2);
            //    //SqlCommand com = new SqlCommand("select * from ADMISSION_TABLE where VN='" + c_id + "'", con);
            //    dr = cmd2.ExecuteReader();
            //    if (dr.Read())
            //    {

            //        lblname.Text = dr["NAME"].ToString();
            //        lblbed.Text = dr["BEDNO"].ToString();
            //        //txtamount.Text = dr["Amount"].ToString();
            //        lblip.Text = dr["VN"].ToString();
            //        dr.Close();
            //        using (SqlCommand cmd3 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //        {
            //            cmd3.CommandType = CommandType.StoredProcedure;
            //            cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PA_MASTER";
            //            cmd3.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //            cmd3.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //            cmd3.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";

            //            //SqlCommand cm1 = new SqlCommand("SELECT CREDIT,DEBIT FROM PA_MASTER WHERE VN='" + lblip.Text + "'", con);
            //            dr = cmd3.ExecuteReader();
            //            if (dr.Read())
            //            {
            //                lbltotalamt.Text = dr["CREDIT"].ToString();
            //                lblpaidamt.Text = dr["DEBIT"].ToString();
            //                lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
            //                txtamount.Text = lblremainamt.Text;
            //            }
            //            dr.Close();
            //        }
            //    }
            //    dr.Close();
            //}
            //con.Close();
        #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            // Validation
            if (txtamount.Text == "")
            {
                string message = "alert('*Please!! Enter The Amount..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0";
                return;
            }
            else if (Convert.ToDecimal(lblremainamt.Text) <= 0)
            {
                string message = "alert('* There Is No Due Amount..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(lblremainamt.Text) > 0 && Convert.ToDecimal(txtamount.Text) == 0 || txtamount.Text == "")
            {
                string message = "alert('Please!! Enter The Amount To Be Paid..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 500, lblid.Text);

            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@CAMOUNT", SqlDbType.VarChar, 500, txtamount.Text);

            SQL_PARAMS[7] = OBJ_METHOD.createParams("@PAMOUNT", SqlDbType.Decimal, 0, "0.00");
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "Collection Against Advance");

            OBJ_METHOD.ExecuteProceedure("USP_COLLECTION", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[14];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, txtdate.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);

                SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, lblbed.Text);

                SQL_PARAMS[7] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, txtamount.Text);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "PAYMENT");
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, '-' + txtamount.Text);

                SQL_PARAMS[11] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, "0");
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);
                SQL_PARAMS[13] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "ADVANCE DETAILS");

                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_PAYMENT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SQL_PARAMS = new SqlParameter[11];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "insert_credit");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, txtdate.Text);

                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Amount", SqlDbType.Decimal, 500, txtamount.Text);

                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, dropemp.SelectedValue);
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@PMODE", SqlDbType.VarChar, 500, droppayment.Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@IPD", SqlDbType.VarChar, 500, lblip.Text);
                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.Decimal, 500, lbltotalamt.Text);

                    OBJ_METHOD.ExecuteProceedure("RECEP_ADVANCEPAY_INSUP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        SQL_PARAMS = new SqlParameter[12];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "insert_advnc");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, txtdate.Text);

                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Amount", SqlDbType.Decimal, 500, txtamount.Text);

                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, dropemp.SelectedValue);
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, lblbed.Text);

                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);
                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@PMODE", SqlDbType.VarChar, 500, droppayment.Text);
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@PNO", SqlDbType.VarChar, 500, txtcard.Text);

                        OBJ_METHOD.ExecuteProceedure("RECEP_ADVANCEPAY_INSUP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            OBJ_METHOD.commitOrRollbackTran("commit");
                            clearcontrol();
                        }
                    }
                }
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            

            Session["ADID"] = txtid.Text;
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
        Response.Redirect("~/RECEPTION/Reception_advancebill.aspx");
        #region Old code
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //    con.Open();
        //    auto();
        //    using (SqlCommand cmd = new SqlCommand("RECEP_ADVANCEPAY_INSUP", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "insert_advnc";
        //        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //        cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
        //        cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
        //        cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
        //        cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtdate.Text;
        //        cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = txtamount.Text;
        //        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
        //        cmd.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@IPD", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = "0.00";
        //        //SqlCommand cmd = new SqlCommand("insert into tblAdvancePayment(id,ORGID,PID,Bedno,ADate,Amount,UserId)values(@id,@ORGID,@PID,@Bedno,@ADate,@Amount,@UserId)", con);

        //        cmd.ExecuteNonQuery();
        //    }

        //    using (SqlCommand cmd1 = new SqlCommand("RECEP_ADVANCEPAY_INSUP", con))
        //    {
        //        cmd1.CommandType = CommandType.StoredProcedure;
        //        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "insert_credit";
        //        cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //        cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
        //        cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //        cmd1.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
        //        cmd1.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtdate.Text;
        //        cmd1.Parameters.Add("@Amount", SqlDbType.Decimal).Value = txtamount.Text;
        //        cmd1.Parameters.Add("@UserId", SqlDbType.VarChar).Value = dropemp.SelectedValue;
        //        cmd1.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.SelectedValue;
        //        cmd1.Parameters.Add("@IPD", SqlDbType.VarChar).Value = lblip.Text;
        //        cmd1.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = lbltotalamt.Text;
        //        //SqlCommand cmd11 = new SqlCommand("insert into CREDIT_TABLE(ID,PMODE,AMOUNT,IPD,DATE,TOTALAMT,EMPID)values(@ID,@PMODE,@AMOUNT,@IPD,@DATE,@TOTALAMT,@EMPID)", con);

        //        //cmd11.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
        //        //cmd11.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.SelectedValue;
        //        //cmd11.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = txtamount.Text;
        //        //cmd11.Parameters.Add("@IPD", SqlDbType.VarChar).Value = lblip.Text;
        //        //cmd11.Parameters.Add("@DATE", SqlDbType.Date).Value = txtdate.Text;
        //        //cmd11.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = lbltotalamt.Text;
        //        //cmd11.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = dropemp.SelectedValue;
        //        cmd1.ExecuteNonQuery();
        //    }

        //    using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
        //    {
        //        cm.CommandType = CommandType.StoredProcedure;
        //        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
        //        cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
        //        cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
        //        cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
        //        cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
        //        cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PAYMENT";
        //        cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtamount.Text;
        //        cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
        //        cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + txtamount.Text;
        //        cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
        //        cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
        //        cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
        //        cm.ExecuteNonQuery();
        //    }
        //    using (SqlCommand cm = new SqlCommand("USP_COLLECTION", con))
        //    {
        //        cm.CommandType = CommandType.StoredProcedure;
        //        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
        //        cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        //        cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
        //        cm.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;
        //        cm.Parameters.Add("@CAMOUNT", SqlDbType.VarChar).Value = txtamount.Text;
        //        cm.Parameters.Add("@PAMOUNT", SqlDbType.Decimal).Value = "0.00";
        //        cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "Collection Against Advance";
        //        cm.ExecuteNonQuery();
        //    }

        //    Session["ADID"] = txtid.Text;
        //    binddata();
        //    con.Close();
        //    string message1 = "alert('Successfully Saved')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
        //Response.Redirect("~/RECEPTION/Reception_advancebill.aspx");
        #endregion
    }
    public void clearcontrol()
    {
        txtamount.Text = lblremainamt.Text;
        
        btnupdate.Visible = false;
        btnSubmit.Visible = true;
        btnDELETE.Visible = false;
    }
    protected void btnDELETE_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE_ID";
                cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd2.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand cmd = new SqlCommand("delete from tblAdvancePayment where id=@id", con);
                //cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd.ExecuteNonQuery();
                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PAYMENT";
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text;
                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
                    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
                    cm.ExecuteNonQuery();
                }
            }
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/reception_Advance_Bill.aspx");
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtamount.Text == "")
            {
                string message = "alert('*Please!! Enter The Amount..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0";
                return;
            }
            else if (Convert.ToDecimal(lblremainamt.Text) <= 0)
            {
                string message = "alert('* There Is No Due Amount..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(lblremainamt.Text) > 0 && Convert.ToDecimal(txtamount.Text) == 0 || txtamount.Text == "")
            {
                string message = "alert('Please!! Enter The Amount To Be Paid..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECEP_ADVANCEPAY_INSUP", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "update";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtdate.Text;
                cmd1.Parameters.Add("@Amount", SqlDbType.Decimal).Value = txtamount.Text;
                cmd1.Parameters.Add("@UserId", SqlDbType.VarChar).Value = dropemp.SelectedValue;
                cmd1.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.SelectedItem.Text;
                cmd1.Parameters.Add("@IPD", SqlDbType.VarChar).Value = lblip.Text;
                cmd1.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = lbltotalamt.Text;
                //SqlCommand cmd1 = new SqlCommand("UPDATE tblAdvancePayment SET Amount=@Amount WHERE id=@id", con);
                //cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                //cmd1.Parameters.Add("@Amount", SqlDbType.VarChar).Value = txtamount.Text;
                cmd1.ExecuteNonQuery();
            }

            
            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PAYMENT";
                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtamount.Text;
                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + txtamount.Text;
                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
                cm.ExecuteNonQuery();
            }

            Session["ADID"] = txtid.Text;
            binddata();
            con.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/Reception_advancebill.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/reception_Advance_Bill.aspx");
    }
    protected void droppayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (droppayment.SelectedIndex == 0)
        {
            txtcard.Visible = false;
            txtcard.Text = "";
        }
        else if (droppayment.SelectedIndex == 3)
        {
            dropemp.Visible = true;
            // txtcard.Visible = false;
            //txtcard.Text = "";
        }
        else
        {
            txtcard.Visible = true;
            dropemp.Visible = true;
            txtcard.Text = "";
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ID";
                cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("SELECT tblAdvancePayment.id, tblAdvancePayment.ORGID, tblAdvancePayment.PID as PID, tblAdvancePayment.Bedno, tblAdvancePayment.Adate, tblAdvancePayment.Amount,tblAdvancePayment.PMODE,tblAdvancePayment.PNO,tblAdvancePayment.UserId,ADMISSION_TABLE.NAME as NAME FROM tblAdvancePayment INNER JOIN ADMISSION_TABLE ON tblAdvancePayment.PID = ADMISSION_TABLE.VN WHERE tblAdvancePayment.id ='" + slno + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btnSubmit.Visible = false;
                    btnupdate.Visible = true;
                    btnDELETE.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    lblname.Text = dr["NAME"].ToString();
                    lblip.Text = dr["PID"].ToString();
                    lblbed.Text = dr["BEDNO"].ToString();
                    txtamount.Text = dr["Amount"].ToString();
                    LBPAIDAMT.Text = dr["Amount"].ToString();
                    droppayment.SelectedItem.Text = dr["PMODE"].ToString();
                    txtcard.Text = dr["PNO"].ToString();
                }
            }
            dr.Close();
            using (SqlCommand cmd3 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            {
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PA_MASTER";
                cmd3.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
                cmd3.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
                cmd3.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";

                //SqlCommand cm1 = new SqlCommand("SELECT CREDIT,DEBIT FROM PA_MASTER WHERE VN='" + lblip.Text + "'", con);
                dr1 = cmd3.ExecuteReader();
                if (dr1.Read())
                {
                    lbltotalamt.Text = dr1["CREDIT"].ToString();
                    lblpaidamt.Text = dr1["DEBIT"].ToString();
                    lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
                    txtamount.Text = lblremainamt.Text;
                }
                else
                {
                    string message = "alert('* No Data Found In Transactions..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                dr1.Close();
            }
            con.Close();
            //div1.Visible = false;
            div2.Visible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PAGE";
            cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT tblAdvancePayment.id,ADMISSION_TABLE.ID as PID,ADMISSION_TABLE.NAME,ADMISSION_TABLE.BEDNO,tblAdvancePayment.Amount,tblAdvancePayment.Adate FROM ADMISSION_TABLE INNER JOIN tblAdvancePayment ON ADMISSION_TABLE.VN = tblAdvancePayment.PID", con);

            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            con.Close();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["ADID"] = gr.Cells[0].Text;
        Response.Redirect("~/RECEPTION/Reception_advancebill.aspx");
    }
}