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
public partial class RECEPTION_Discharge : System.Web.UI.Page
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
        using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISCHARGE_PAGE";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            //DataTable Dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,DISCHARGE AS DISCHARGE,CONVERT(VARCHAR(10),DATE,105) AS DATE,PID AS PID,NAME AS NAME,BEDNO AS BEDNO FROM DISCHARGE_TABLE ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
        }
        con.Close();
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
            lblorgid.Text = Session["ORGID"].ToString();
            lbluid.Text = Session["UID"].ToString();
            //txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            if (!IsPostBack)
            {
                binddata();
                using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BEDTABLE";
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                    //DataTable Dt = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                    //da1 = new SqlDataAdapter("select BEDNO FROM BED_TABLE where ORGID='" + lblorgid.Text + "'", con);
                    DataTable ds1 = new DataTable();
                    da1.Fill(ds1);
                    dropbedno.DataSource = ds1;
                    dropbedno.DataTextField = "BEDNO";
                    dropbedno.DataValueField = "BEDNO";
                    dropbedno.DataBind();
                    dropbedno.Items.Insert(0, "Please Select");
                }
            }
            con.Close();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROPDOWN";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.SelectedItem.Text;
                //DataTable Dt = new DataTable();
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                //SqlCommand cm = new SqlCommand("select A.VN,A.PNAME,A.BEDNO,A.WARD,B.CREDIT,B.DEBIT,A.INSURANCE FROM BED_TABLE A,PA_MASTER B WHERE A.BEDNO='" + dropbedno.SelectedItem.Text + "' AND B.VN=A.VN", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblpid.Text = dr["VN"].ToString();
                    lblpname.Text = dr["PNAME"].ToString();
                    lblbedno.Text = dr["BEDNO"].ToString();
                    lblward.Text = dr["WARD"].ToString();
                    LBBALANCEAMT.Text = dr["CREDIT"].ToString();
                    LBPAIDAMT.Text = dr["DEBIT"].ToString();
                    lblins.Text = dr["INSURANCE"].ToString();
                }
                else
                {
                    string message = "alert('No Data Founds..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    clear();
                    return;
                }
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cmd1 = new SqlCommand("RECP_DISCHARGE_INSERT1", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                //SqlCommand cmd1 = new SqlCommand("insert into DISCHARGE_TABLE (ID,PID,NAME,BEDNO,DATE,WARD,STATUS,DISCHARGE,NOTE,USERNAME,USERID,DIAG,CSUMM,INVS,TREAT,DISCU,ADVI,COMPLAINS,PAST,INSURANCE,remarks)values(@ID,@PID,@NAME,@BEDNO,@DATE,@WARD,@STATUS,@DISCHARGE,@NOTE,@USERNAME,@USERID,@DIAG,@CSUMM,@INVS,@TREAT,@DISCU,@ADVI,@COMPLAINS,@PAST,@INSURANCE,@remarks)", con);
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
                cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = lblpname.Text.ToUpper();
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
                cmd1.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                cmd1.Parameters.Add("@WARD", SqlDbType.VarChar).Value = lblward.Text;
                cmd1.Parameters.Add("@STATUS", SqlDbType.VarChar).Value = dropstatus.Text;
                cmd1.Parameters.Add("@DISCHARGE", SqlDbType.VarChar).Value = dropdischarge.Text;
                cmd1.Parameters.Add("@NOTE", SqlDbType.VarChar).Value = TXTdoctor.Text;
                cmd1.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = lblid.Text;
                cmd1.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lbluid.Text;
                cmd1.Parameters.Add("@DIAG", SqlDbType.VarChar).Value = TXTdiagnosis.Text;
                cmd1.Parameters.Add("@CSUMM", SqlDbType.VarChar).Value = TXTclinicalsummary.Text;
                cmd1.Parameters.Add("@INVS", SqlDbType.VarChar).Value = TXTinvestigation.Text;
                cmd1.Parameters.Add("@TREAT", SqlDbType.VarChar).Value = TXTtraetment.Text;
                cmd1.Parameters.Add("@DISCU", SqlDbType.VarChar).Value = TXTdiscussion.Text;
                cmd1.Parameters.Add("@ADVI", SqlDbType.VarChar).Value = TXTadvice.Text;
                cmd1.Parameters.Add("@COMPLAINS", SqlDbType.VarChar).Value = TXTcomplains.Text;
                cmd1.Parameters.Add("@PAST", SqlDbType.VarChar).Value = TXTpast.Text;
                cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = lblins.Text;
                cmd1.Parameters.Add("@REMARKS", SqlDbType.VarChar).Value = TXTRemarks.Text;
                cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
                cmd1.ExecuteNonQuery();
                //SqlDataAdapter da1 = new SqlDataAdapter("UPDATE BED_MATRIX_TABLE SET STATUS ='AVAILABLE' WHERE BEDNO='" + lblbedno.Text + "'", con);
                //DataSet ds1 = new DataSet();
                //da1.Fill(ds1);
                //SqlDataAdapter da = new SqlDataAdapter("delete from BED_TABLE where VN='" + lblpid.Text + "'", con);
                //ds = new DataSet();
                //da.Fill(ds);
                Session["DID"] = TXTID.Text;
            }
            binddata();
            con.Close();


        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
            if (dropdischarge.SelectedIndex == 2)
            {
                Response.Redirect("~/RECEPTION/Dischargebillunblock.aspx");
            }
            else
            {
                Response.Redirect("~/RECEPTION/Dischargebill.aspx");
            }
        
       
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
       
            GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
            Session["DID"] = gr.Cells[0].Text;

            var UB = gr.Cells[1].Text;

            if (UB == "UnBlock")
            {
                Response.Redirect("~/RECEPTION/Dischargebillunblock.aspx");
            }
            else
            {
                Response.Redirect("~/RECEPTION/Dischargebill.aspx");
            }
       
     
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                //DataTable Dt = new DataTable();
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("select * from DISCHARGE_TABLE where ID='" + slno + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    TXTID.Text = dr["ID"].ToString();
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    lblpid.Text = dr["PID"].ToString();
                    lblpname.Text = dr["NAME"].ToString();
                    lblbedno.Text = dr["BEDNO"].ToString();
                    Session["bed"] = dr["BEDNO"].ToString();
                    txtdate.Text = Convert.ToDateTime(dr["DATE"]).ToString("dd-MM-yyyy");
                    lblward.Text = dr["WARD"].ToString();
                    dropstatus.Text = dr["STATUS"].ToString();
                    dropdischarge.Text = dr["DISCHARGE"].ToString();
                    TXTdoctor.Text = dr["NOTE"].ToString();
                    TXTadvice.Text = dr["ADVI"].ToString();
                    TXTclinicalsummary.Text = dr["CSUMM"].ToString();
                    TXTdiagnosis.Text = dr["DIAG"].ToString();
                    TXTdiscussion.Text = dr["DISCU"].ToString();
                    TXTinvestigation.Text = dr["INVS"].ToString();
                    TXTtraetment.Text = dr["TREAT"].ToString();
                    TXTcomplains.Text = dr["COMPLAINS"].ToString();
                    TXTpast.Text = dr["PAST"].ToString();
                    lblins.Text = dr["INSURANCE"].ToString();
                    TXTRemarks.Text = dr["REMARKS"].ToString();
                }
            }
            dr.Close();
            using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = Session["bed"].ToString();
                //DataTable Dt = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                //da1 = new SqlDataAdapter("select BEDNO FROM BED_TABLE where ORGID='" + lblorgid.Text + "'", con);
                DataTable ds1 = new DataTable();
                da1.Fill(ds1);
                dropbedno.DataSource = ds1;
                dropbedno.DataTextField = "BEDNO";
                dropbedno.DataValueField = "BEDNO";
                dropbedno.DataBind();
                dropbedno.Items.Insert(0, "Please Select");
                dropbedno.SelectedValue = Session["bed"].ToString();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clear()
    {
        txtdate.Text =lblpid.Text= lblpname.Text=lblward.Text=lblbedno.Text="";

    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_DISCHARGE_SELECT", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISCHARGE_PAGE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                //DataTable Dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,DISCHARGE AS DISCHARGE,CONVERT(VARCHAR(10),DATE,105) AS DATE,PID AS PID,NAME AS NAME,BEDNO AS BEDNO FROM DISCHARGE_TABLE ORDER BY ID DESC", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
   
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/Discharge.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_DISCHARGE_INSERT2", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = TXTID.Text;
                //SqlDataAdapter da1 = new SqlDataAdapter("delete from DISCHARGE_TABLE where ID='" + TXTID.Text + "'", con);
                //DataSet ds1 = new DataSet();
                //da1.Fill(ds1);
                //SqlCommand cmd1 = new SqlCommand("insert into BED_TABLE (PID,PNAME,BEDNO,WARD,ORGID,INSURANCE,VN)values(@PID,@PNAME,@BEDNO,@WARD,@ORGID,@INSURANCE,@VN)", con);
                cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblpid.Text;
                cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblpid.Text;
                cmd1.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = lblpname.Text.ToUpper();
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbedno.Text;
                cmd1.Parameters.Add("@WARD", SqlDbType.VarChar).Value = lblward.Text;
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@INSURANCE", SqlDbType.VarChar).Value = lblins.Text;

                cmd1.ExecuteNonQuery();
            }
            //SqlDataAdapter da = new SqlDataAdapter("UPDATE BED_MATRIX_TABLE SET STATUS ='OCCUPIED' WHERE BEDNO='" + lblbedno.Text + "'", con);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
            
        
        Response.Redirect("~/RECEPTION/Discharge.aspx");
       
    }
}