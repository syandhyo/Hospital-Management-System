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
using System.Web.Services;

public partial class RECEPTION_reception_patient_discharge : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl, F;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN, DATE;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;
    DataMathods OBJ_METHOD = new DataMathods();

    [WebMethod]
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from DISCHARGE_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("DS{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;

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


            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DISCHARGE_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_DISCHARGE_SELECT", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS1;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }


           SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

           SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BEDTABLE");
           SQL_PARAMS2[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 100, lblorgid.Text);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_DISCHARGE_SELECT", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                dropbedno.DataSource = DS2;
                dropbedno.DataTextField = "BEDNO";
                dropbedno.DataValueField = "BEDNO";
                dropbedno.DataBind();
                dropbedno.Items.Insert(0, new ListItem("Please Select","0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        #region oldcode
        //using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISCHARGE_PAGE";
        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
        //    //DataTable Dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,DISCHARGE AS DISCHARGE,CONVERT(VARCHAR(10),DATE,105) AS DATE,PID AS PID,NAME AS NAME,BEDNO AS BEDNO FROM DISCHARGE_TABLE ORDER BY ID DESC", con);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    GridView1.SelectedIndex = 0;
        //    GridView1.DataSource = dt;
        //    GridView1.DataKeyNames = new string[] { "ID" };
        //    GridView1.DataBind();
        //}

       //using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
       //{
       //    cmd.CommandType = CommandType.StoredProcedure;
       //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDTABLE";
       //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
       //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
       //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
       //    //DataTable Dt = new DataTable();
       //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
       //    //da1 = new SqlDataAdapter("select BEDNO FROM BED_TABLE where ORGID='" + lblorgid.Text + "'", con);
       //    DataTable ds1 = new DataTable();
       //    da1.Fill(ds1);
       //    dropbedno.DataSource = ds1;
       //    dropbedno.DataTextField = "BEDNO";
       //    dropbedno.DataValueField = "BEDNO";
       //    dropbedno.DataBind();
       //    dropbedno.Items.Insert(0, "Please Select");
        //}
        #endregion
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
            lblorgid.Text = Session["ORGID"].ToString();
            lbluid.Text = Session["UID"].ToString();
            //txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            if (!IsPostBack)
            {
                binddata();
               
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropbedno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROPDOWN");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 100, dropbedno.SelectedItem.Text);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_DISCHARGE_SELECT", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                lblpid.Text = DS2.Tables[0].Rows[0]["VN"].ToString();
                lblpname.Text = DS2.Tables[0].Rows[0]["PNAME"].ToString();
                lblbedno.Text = DS2.Tables[0].Rows[0]["BEDNO"].ToString();
                lblward.Text = DS2.Tables[0].Rows[0]["WARD"].ToString();
                LBBALANCEAMT.Text = DS2.Tables[0].Rows[0]["CREDIT"].ToString();
                LBPAIDAMT.Text = DS2.Tables[0].Rows[0]["DEBIT"].ToString();
                lblins.Text = DS2.Tables[0].Rows[0]["INSURANCE"].ToString();
            }
            else
            {
                string message = "alert('No Data Founds..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                clear();
                return;
            }
            //using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROPDOWN";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.SelectedItem.Text;
            //    //DataTable Dt = new DataTable();
            //    //SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //    //SqlCommand cm = new SqlCommand("select A.VN,A.PNAME,A.BEDNO,A.WARD,B.CREDIT,B.DEBIT,A.INSURANCE FROM BED_TABLE A,PA_MASTER B WHERE A.BEDNO='" + dropbedno.SelectedItem.Text + "' AND B.VN=A.VN", con);
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        lblpid.Text = dr["VN"].ToString();
            //        lblpname.Text = dr["PNAME"].ToString();
            //        lblbedno.Text = dr["BEDNO"].ToString();
            //        lblward.Text = dr["WARD"].ToString();
            //        LBBALANCEAMT.Text = dr["CREDIT"].ToString();
            //        LBPAIDAMT.Text = dr["DEBIT"].ToString();
            //        lblins.Text = dr["INSURANCE"].ToString();
            //    }
            //    else
            //    {
            //        string message = "alert('No Data Founds..')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        clear();
            //        return;
            //    }
            //}
            //dr.Close();
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropbedno.SelectedIndex == 0)
            {

                string message = "alert('* Please!!Select The Bed..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtdate.Text == "")
            {
                string message = "alert('*Please!!Enter The Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (Convert.ToDouble(LBBALANCEAMT.Text) > Convert.ToDouble(LBPAIDAMT.Text) && lblins.Text != "")
            {
                string message = "alert('* Patient has not cleared the dues.You can not discharge now. ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
           
            auto();

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[25];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 200, TXTID.Text);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 200, lblpid.Text);
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 200, lblpname.Text.ToUpper());
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 200, lblbedno.Text);
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtdate.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@WARD", SqlDbType.VarChar, 200, lblward.Text);
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@STATUS", SqlDbType.VarChar, 200, dropstatus.Text);
            SQL_PARAMS2[8] = OBJ_METHOD.createParams("@DISCHARGE", SqlDbType.VarChar, 200, dropdischarge.Text);
            SQL_PARAMS2[9] = OBJ_METHOD.createParams("@NOTE", SqlDbType.VarChar, 200, TXTdoctor.Text);
            SQL_PARAMS2[10] = OBJ_METHOD.createParams("@USERNAME", SqlDbType.VarChar, 200, lblid.Text);
            SQL_PARAMS2[11] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 200, lbluid.Text);
            SQL_PARAMS2[12] = OBJ_METHOD.createParams("@DIAG", SqlDbType.VarChar, 200, TXTdiagnosis.Text);
            SQL_PARAMS2[13] = OBJ_METHOD.createParams("@CSUMM", SqlDbType.VarChar, 200, TXTclinicalsummary.Text);
            SQL_PARAMS2[14] = OBJ_METHOD.createParams("@INVS", SqlDbType.VarChar, 200, TXTinvestigation.Text);
            SQL_PARAMS2[15] = OBJ_METHOD.createParams("@TREAT", SqlDbType.VarChar, 200, TXTtraetment.Text);
            SQL_PARAMS2[16] = OBJ_METHOD.createParams("@DISCU", SqlDbType.VarChar, 200, TXTdiscussion.Text);
            SQL_PARAMS2[17] = OBJ_METHOD.createParams("@ADVI", SqlDbType.VarChar, 200, TXTadvice.Text);
            SQL_PARAMS2[18] = OBJ_METHOD.createParams("@COMPLAINS", SqlDbType.VarChar, 200, TXTcomplains.Text);
            SQL_PARAMS2[19] = OBJ_METHOD.createParams("@PAST", SqlDbType.VarChar, 200, TXTpast.Text);
            SQL_PARAMS2[20] = OBJ_METHOD.createParams("@INSURANCE", SqlDbType.VarChar, 200, lblins.Text);
            SQL_PARAMS2[21] = OBJ_METHOD.createParams("@REMARKS", SqlDbType.VarChar, 200, TXTRemarks.Text);
            SQL_PARAMS2[22] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 200, lblpid.Text);
            SQL_PARAMS2[23] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS2[24] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("RECP_DISCHARGE_INSERT1", "", "", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";

            }

            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not saved.')";
            }
            #region oldcode
            //using (SqlCommand cmd1 = new SqlCommand("RECP_DISCHARGE_INSERT1", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    //SqlCommand cmd1 = new SqlCommand("insert into DISCHARGE_TABLE (ID,PID,NAME,BEDNO,DATE,WARD,STATUS,DISCHARGE,NOTE,USERNAME,USERID,DIAG,CSUMM,INVS,TREAT,DISCU,ADVI,COMPLAINS,PAST,INSURANCE,remarks)values(@ID,@PID,@NAME,@BEDNO,@DATE,@WARD,@STATUS,@DISCHARGE,@NOTE,@USERNAME,@USERID,@DIAG,@CSUMM,@INVS,@TREAT,@DISCU,@ADVI,@COMPLAINS,@PAST,@INSURANCE,@remarks)", con);
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
            //    cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
            //    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblpname.Text.ToUpper();
            //    cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
            //    cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
            //    cmd1.Parameters.Add("@WARD", SqlDbType.VarChar).Value = lblward.Text;
            //    cmd1.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = dropstatus.Text;
            //    cmd1.Parameters.Add("@DISCHARGE", SqlDbType.VarChar).Value = dropdischarge.Text;
            //    cmd1.Parameters.Add("@NOTE", SqlDbType.VarChar).Value = TXTdoctor.Text;
            //    cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
            //    cmd1.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
            //    cmd1.Parameters.Add("@DIAG", SqlDbType.VarChar).Value = TXTdiagnosis.Text;
            //    cmd1.Parameters.Add("@CSUMM", SqlDbType.VarChar).Value = TXTclinicalsummary.Text;
            //    cmd1.Parameters.Add("@INVS", SqlDbType.VarChar).Value = TXTinvestigation.Text;
            //    cmd1.Parameters.Add("@TREAT", SqlDbType.VarChar).Value = TXTtraetment.Text;
            //    cmd1.Parameters.Add("@DISCU", SqlDbType.VarChar).Value = TXTdiscussion.Text;
            //    cmd1.Parameters.Add("@ADVI", SqlDbType.VarChar).Value = TXTadvice.Text;
            //    cmd1.Parameters.Add("@COMPLAINS", SqlDbType.VarChar).Value = TXTcomplains.Text;
            //    cmd1.Parameters.Add("@PAST", SqlDbType.VarChar).Value = TXTpast.Text;
            //    cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = lblins.Text;
            //    cmd1.Parameters.Add("@REMARKS", SqlDbType.VarChar).Value = TXTRemarks.Text;
            //    cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
            //    cmd1.ExecuteNonQuery();
            //    //SqlDataAdapter da1 = new SqlDataAdapter("UPDATE BED_MATRIX_TABLE SET STATUS ='AVAILABLE' WHERE BEDNO='" + lblbedno.Text + "'", con);
            //    //DataSet ds1 = new DataSet();
            //    //da1.Fill(ds1);
            //    //SqlDataAdapter da = new SqlDataAdapter("delete from BED_TABLE where VN='" + lblpid.Text + "'", con);
            //    //ds = new DataSet();
            //    //da.Fill(ds);
            //    Session["DID"] = TXTID.Text;
            //}
            //binddata();
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
            //reset();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        if (dropdischarge.SelectedIndex == 2)
        {
            Response.Redirect("~/RECEPTION/reception_Dischargebillunblock.aspx");
        }
        else
        {
            Response.Redirect("~/RECEPTION/reception_Dischargebill.aspx");
        }


    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["DID"] = gr.Cells[0].Text;

        var UB = gr.Cells[1].Text;

        if (UB == "UnBlock")
        {
            Response.Redirect("~/RECEPTION/reception_Dischargebillunblock.aspx");
        }
        else
        {
            Response.Redirect("~/RECEPTION/reception_Dischargebill.aspx");
        }


    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
           
             SqlParameter[] SQL_PARAMS = new SqlParameter[2];

             SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_DISCHARGE_SELECT", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                TXTID.Text = Ds.Tables[0].Rows[0]["ID"].ToString();
                btncreate.Visible = false;
                btnupdate.Visible = true;
                lblpid.Text = Ds.Tables[0].Rows[0]["PID"].ToString();
                lblpname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                lblbedno.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                //Session["bed"] = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                dropbedno.SelectedItem.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                txtdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
                lblward.Text = Ds.Tables[0].Rows[0]["WARD"].ToString();
                dropstatus.Text = Ds.Tables[0].Rows[0]["STATUS"].ToString();
                dropdischarge.Text = Ds.Tables[0].Rows[0]["DISCHARGE"].ToString();
                TXTdoctor.Text = Ds.Tables[0].Rows[0]["NOTE"].ToString();
                TXTadvice.Text = Ds.Tables[0].Rows[0]["ADVI"].ToString();
                TXTclinicalsummary.Text = Ds.Tables[0].Rows[0]["CSUMM"].ToString();
                TXTdiagnosis.Text = Ds.Tables[0].Rows[0]["DIAG"].ToString();
                TXTdiscussion.Text = Ds.Tables[0].Rows[0]["DISCU"].ToString();
                TXTinvestigation.Text = Ds.Tables[0].Rows[0]["INVS"].ToString();
                TXTtraetment.Text = Ds.Tables[0].Rows[0]["TREAT"].ToString();
                TXTcomplains.Text = Ds.Tables[0].Rows[0]["COMPLAINS"].ToString();
                TXTpast.Text = Ds.Tables[0].Rows[0]["PAST"].ToString();
                lblins.Text = Ds.Tables[0].Rows[0]["INSURANCE"].ToString();
                TXTRemarks.Text = Ds.Tables[0].Rows[0]["REMARKS"].ToString();
            }

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BED");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, Session["bed"].ToString());
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_DISCHARGE_SELECT", false, true, SQL_PARAMS1);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                dropbedno.DataSource = Ds1;
                dropbedno.DataTextField = "BEDNO";
                dropbedno.DataValueField = "BEDNO";
                dropbedno.DataBind();
                dropbedno.Items.Insert(0, new ListItem("Please Select"));
                dropbedno.SelectedValue = Session["bed"].ToString();
            }
         

            //using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = Session["bed"].ToString();
            //    //DataTable Dt = new DataTable();
            //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //    //da1 = new SqlDataAdapter("select BEDNO FROM BED_TABLE where ORGID='" + lblorgid.Text + "'", con);
            //    DataTable ds1 = new DataTable();
            //    da1.Fill(ds1);
            //    dropbedno.DataSource = ds1;
            //    dropbedno.DataTextField = "BEDNO";
            //    dropbedno.DataValueField = "BEDNO";
            //    dropbedno.DataBind();
            //    dropbedno.Items.Insert(0, "Please Select");
            //    dropbedno.SelectedValue = Session["bed"].ToString();
            //}
           
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void clear()
    {
        txtdate.Text = lblpid.Text = lblpname.Text = lblward.Text = lblbedno.Text = "";

    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DISCHARGE_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_DISCHARGE_SELECT", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = Ds;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
           
            //using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISCHARGE_PAGE";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //    //DataTable Dt = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,DISCHARGE AS DISCHARGE,CONVERT(VARCHAR(10),DATE,105) AS DATE,PID AS PID,NAME AS NAME,BEDNO AS BEDNO FROM DISCHARGE_TABLE ORDER BY ID DESC", con);
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
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void clear_data()
    {
        dropbedno.SelectedIndex = 0;
        txtdate.Text = "";
        lblbedno.Text = "";
        lbldate.Text = "";
        lblins.Text = "";
        lblpid.Text = "";
        lblpname.Text = "";
        TXTcomplains.Text = "";
        TXTclinicalsummary.Text = "";
        TXTadvice.Text = "";
        TXTdiagnosis.Text="";
        TXTdiscussion.Text = "";
        TXTdoctor.Text = "";
        TXTinvestigation.Text = "";
        TXTpast.Text = "";
        TXTRemarks.Text = "";
        TXTtraetment.Text = "";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        clear_data();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[8];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 200, TXTID.Text);
            SQL_PARAMS2[2] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 200, lblpid.Text);
            SQL_PARAMS2[3] = OBJ_METHOD.createParams("@PNAME", SqlDbType.VarChar, 200, lblpname.Text.ToUpper());
            SQL_PARAMS2[4] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 200, lblbedno.Text);
            SQL_PARAMS2[5] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS2[6] = OBJ_METHOD.createParams("@WARD", SqlDbType.VarChar, 200, lblward.Text);
            SQL_PARAMS2[7] = OBJ_METHOD.createParams("@INSURANCE", SqlDbType.VarChar, 200, lblins.Text);
           
            OBJ_METHOD.ExecuteProceedure("RECP_DISCHARGE_INSERT2", "", "@msg", SqlDbType.VarChar, SQL_PARAMS2, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                btncreate.Visible = true;
                btnupdate.Visible = false;

            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not saved.')";
            }
           
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clear_data();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        //Response.Redirect("~/RECEPTION/reception_patient_discharge.aspx");

    }
}