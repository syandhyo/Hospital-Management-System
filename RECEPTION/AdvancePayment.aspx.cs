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

public partial class RECEPTION_AdvancePayment : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlDataReader dr,dr1;
    SqlConnection con;
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    string AMOUNT;

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
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
       
            GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
            Session["ADID"] = gr.Cells[0].Text;
            Response.Redirect("~/RECEPTION/advancebill.aspx");
      
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
                dropemp.Visible = false;
            }
            con.Close();
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
        con.Close();
        using (SqlCommand cmd = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PAGE";
            cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter Adp = new SqlDataAdapter("SELECT tblAdvancePayment.id,ADMISSION_TABLE.ID as PID,ADMISSION_TABLE.NAME,ADMISSION_TABLE.BEDNO,tblAdvancePayment.Amount,tblAdvancePayment.Adate FROM ADMISSION_TABLE INNER JOIN tblAdvancePayment ON ADMISSION_TABLE.VN = tblAdvancePayment.PID and tblAdvancePayment.PID='" + c_id + "'", con);
            DataTable Dt = new DataTable();
            da.Fill(Dt);
            GridView1.DataSource = Dt;
            GridView1.DataBind();
        }
        using (SqlCommand cmd1 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STAFF";
            cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
            
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT EMPID,Sname FROM tblStaff", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            //dropbedno.SelectedIndex = 0;
            dropemp.DataSource = dt1;
            dropemp.DataTextField = "Sname";
            dropemp.DataValueField = "EMPID";
            dropemp.DataBind();
            dropemp.Items.Insert(0, "Please Select");
        }
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
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
            div1.Visible = false;
            div2.Visible = true;
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
       
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtspid.Text=="")
            {
                string message = "alert('*Please!!Enter The IPD No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            using (SqlCommand cmd2 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            {
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION";
                cmd2.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
                cmd2.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtspid.Text;
                cmd2.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                
                //SqlCommand com = new SqlCommand("select * from ADMISSION_TABLE where VN='" + txtspid.Text + "'", con);
                dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    div2.Visible = true;
                    div1.Visible = false;
                    lblname.Text = dr["NAME"].ToString();
                    lblbed.Text = dr["BEDNO"].ToString();
                    //txtamount.Text = dr["Amount"].ToString();
                    lblip.Text = dr["VN"].ToString();
                    dr.Close();
                    using (SqlCommand cmd3 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
                    {
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PA_MASTER";
                        cmd3.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
                        cmd3.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
                        cmd3.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                        
                        //SqlCommand cm1 = new SqlCommand("SELECT CREDIT,DEBIT FROM PA_MASTER WHERE VN='" + lblip.Text + "'", con);
                        dr = cmd3.ExecuteReader();
                        if (dr.Read())
                        {
                            lbltotalamt.Text = dr["CREDIT"].ToString();
                            lblpaidamt.Text = dr["DEBIT"].ToString();
                            lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
                            txtamount.Text = lblremainamt.Text;
                        }
                        else
                        {
                            string message = "alert('* No Data Found In Transactions..')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            return;
                        }
                        dr.Close();
                    }
                }
                else
                {
                    string message = "alert('*Invalid IPNO.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    dr.Close();
                }
                dr.Close();
            }

            con.Close();
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtbedno.Text == "")
            {
                string message = "alert('*Please!!Enter The Bed No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            using (SqlCommand cmd3 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            {
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
                cmd3.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
                cmd3.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtbedno.Text;
                cmd3.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                //SqlCommand com = new SqlCommand("select B.PNAME AS NAME,B.BEDNO AS BEDNO,B.PID AS ID,B.VN AS VN from BED_TABLE B where B.BEDNO='" + txtbedno.Text + "'", con);
                dr = cmd3.ExecuteReader();
                if (dr.Read())
                {
                    div2.Visible = true;
                    div1.Visible = false;
                    lblname.Text = dr["NAME"].ToString();
                    lblbed.Text = dr["BEDNO"].ToString();
                    //txtamount.Text = dr["Amount"].ToString();
                    lblip.Text = dr["VN"].ToString();
                    dr.Close();
                    using (SqlCommand cm1 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
                    {
                        cm1.CommandType = CommandType.StoredProcedure;
                        cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PA_MASTER";
                        cm1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
                        cm1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
                        cm1.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                        //SqlCommand cm1 = new SqlCommand("SELECT CREDIT,DEBIT FROM PA_MASTER WHERE VN='" + lblip.Text + "'", con);
                        dr = cm1.ExecuteReader();
                        if (dr.Read())
                        {
                            lbltotalamt.Text = dr["CREDIT"].ToString();
                            lblpaidamt.Text = dr["DEBIT"].ToString();
                            lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
                            txtamount.Text = lblremainamt.Text;
                        }
                        else
                        {
                            string message = "alert('* No Data Found..')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            return;
                        }
                        dr.Close();
                    }
                }

                else
                {
                    string message = "alert('*Bed no is not found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                dr.Close();
            }
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
            else if (Convert.ToDecimal(lblremainamt.Text) > 0 && Convert.ToDecimal(txtamount.Text) == 0 || txtamount.Text=="")
            {
                string message = "alert('Please!! Enter The Amount To Be Paid..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cmd = new SqlCommand("RECEP_ADVANCEPAY_INSUP", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "insert_advnc";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
                cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
                cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtdate.Text;
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = txtamount.Text;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
                cmd.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@IPD", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = "0.00";
                //SqlCommand cmd = new SqlCommand("insert into tblAdvancePayment(id,ORGID,PID,Bedno,ADate,Amount,UserId)values(@id,@ORGID,@PID,@Bedno,@ADate,@Amount,@UserId)", con);

                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd1 = new SqlCommand("RECEP_ADVANCEPAY_INSUP", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "insert_credit";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtdate.Text;
                cmd1.Parameters.Add("@Amount", SqlDbType.Decimal).Value = txtamount.Text;
                cmd1.Parameters.Add("@UserId", SqlDbType.VarChar).Value = dropemp.SelectedValue;
                cmd1.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.SelectedValue;
                cmd1.Parameters.Add("@IPD", SqlDbType.VarChar).Value = lblip.Text;
                cmd1.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = lbltotalamt.Text;
                //SqlCommand cmd11 = new SqlCommand("insert into CREDIT_TABLE(ID,PMODE,AMOUNT,IPD,DATE,TOTALAMT,EMPID)values(@ID,@PMODE,@AMOUNT,@IPD,@DATE,@TOTALAMT,@EMPID)", con);

                //cmd11.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                //cmd11.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.SelectedValue;
                //cmd11.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = txtamount.Text;
                //cmd11.Parameters.Add("@IPD", SqlDbType.VarChar).Value = lblip.Text;
                //cmd11.Parameters.Add("@DATE", SqlDbType.Date).Value = txtdate.Text;
                //cmd11.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = lbltotalamt.Text;
                //cmd11.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = dropemp.SelectedValue;
                cmd1.ExecuteNonQuery();
            }

            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
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
            using (SqlCommand cm = new SqlCommand("USP_COLLECTION", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;
                cm.Parameters.Add("@CAMOUNT", SqlDbType.VarChar).Value = txtamount.Text;
                cm.Parameters.Add("@PAMOUNT", SqlDbType.Decimal).Value = "0.00";
                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "Collection Against Advance";
                cm.ExecuteNonQuery();
            }

            Session["ADID"] = txtid.Text;
            binddata();
            con.Close();
            clearcontrol();
            string message1 = "alert('Successfully Saved')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/advancebill.aspx");
    }
    public void clearcontrol()
    {
        txtamount.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
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

            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PAYMENT";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    //cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text;
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value =  LBPAIDAMT.Text;
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
            //    cm.ExecuteNonQuery();
            //}
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
                //cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + txtamount.Text;
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
        Response.Redirect("~/RECEPTION/advancebill.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        Response.Redirect("~/Reception/AdvancePayment.aspx");
    }
    protected void btndelete_Click(object sender, EventArgs e)
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
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;

                //SqlCommand cmd = new SqlCommand("delete from tblAdvancePayment where id=@id", con);
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
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


        Response.Redirect("~/RECEPTION/AdvancePayment.aspx");
        
       
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        
    }
}