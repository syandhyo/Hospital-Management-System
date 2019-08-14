
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


public partial class RECEPTION_Ipcharge : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    public string id_hist,PRICE;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j, F;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;


    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select id from tblAllCharge";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["id"].ToString();
        }
        num1 = string.Format("CH{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

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
                LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
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
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();
        con.Close();

        SqlDataAdapter da = new SqlDataAdapter("SELECT tblAllCharge.id as id,ADMISSION_TABLE.VN AS PID,ADMISSION_TABLE.NAME, ADMISSION_TABLE.GENDER, ADMISSION_TABLE.BEDNO, ADMISSION_TABLE.WARD, tblAllCharge.Bdate FROM ADMISSION_TABLE INNER JOIN tblAllCharge ON ADMISSION_TABLE.VN = tblAllCharge.PID", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.SelectedIndex = 0;
        GridView1.DataSource = dt;
        GridView1.DataKeyNames = new string[] { "ID" };
        GridView1.DataBind();
        Grvcharge.Visible = true;

        da = new SqlDataAdapter("select * FROM tblChargeMaster", con);
        DataTable ds = new DataTable();
        da.Fill(ds);
        Grvcharge.DataSource = ds;
        Grvcharge.DataBind();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select * from ADMISSION_TABLE where VN='" + txtspid.Text + "' ORDER BY VN DESC", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                div2.Visible = true;
                div1.Visible = false;
                lblname.Text = dr["NAME"].ToString();
                lblbed.Text = dr["BEDNO"].ToString();
                lblip.Text = dr["VN"].ToString();
                lblward.Text = dr["WARD"].ToString();
                lblgender.Text = dr["GENDER"].ToString();
                UHID.Text = dr["UHID"].ToString();
            }
            else
            {
                string message = "alert('* Id is not found.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dr.Close();
            }
            dr.Close();

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
            SqlCommand com = new SqlCommand("select * from ADMISSION_TABLE where bedno='" + txtbedno.Text + "' ORDER BY VN DESC", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                div2.Visible = true;
                div1.Visible = false;
                lblname.Text = dr["NAME"].ToString();
                lblbed.Text = dr["BEDNO"].ToString();
                lblward.Text = dr["WARD"].ToString();
                lblgender.Text = dr["GENDER"].ToString();
                lblip.Text = dr["VN"].ToString();
                UHID.Text = dr["UHID"].ToString();
            }
            else
            {
                string message = "alert('* Bed no is not found.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dr.Close();
            }
            dr.Close();
            con.Close();
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
            //var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblAllCharge  where id='" + slno + "'", con);
            ds = new DataSet();
            da.Fill(ds);
            btnSubmit.Visible = false;
            btndelete.Visible = true;
            btnupdate.Visible = true;
            txtid.Text = ds.Tables[0].Rows[0]["id"].ToString();
            txtdate.Text = ds.Tables[0].Rows[0]["Bdate"].ToString();
            lblip.Text = ds.Tables[0].Rows[0]["PID"].ToString();
            LBPAIDAMT.Text = ds.Tables[0].Rows[0]["PRICE"].ToString();
            Grvchargeupdate.DataSource = ds;
            Grvchargeupdate.DataBind();
            Grvcharge.Visible = false;
            SqlCommand C = new SqlCommand("select * from ADMISSION_TABLE WHERE VN='" + lblip.Text + "'", con);
            dr = C.ExecuteReader();
            if (dr.Read())
            {
                lblname.Text = dr["NAME"].ToString();
                lblward.Text = dr["WARD"].ToString();
                lblgender.Text = dr["GENDER"].ToString();
                lblbed.Text = dr["BEDNO"].ToString();
            }
            dr.Close();

            div1.Visible = false;
            div2.Visible = true;

            con.Close();
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
            SqlDataAdapter da = new SqlDataAdapter("SELECT tblAllCharge.id,ADMISSION_TABLE.VN AS PID,ADMISSION_TABLE.NAME, ADMISSION_TABLE.GENDER, ADMISSION_TABLE.BEDNO, ADMISSION_TABLE.WARD, tblAllCharge.Bdate FROM ADMISSION_TABLE INNER JOIN tblAllCharge ON ADMISSION_TABLE.VN = tblAllCharge.PID", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
     
    }
    public void insert()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        foreach (GridViewRow row in Grvcharge.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
             
                if (chkRow.Checked)
                {
                    string dropcharge = (row.Cells[1].FindControl("lblcharge") as Label).Text;
                    string txtdsc = (row.Cells[2].FindControl("txtdescription") as TextBox).Text;
                    string txtprice = (row.Cells[3].FindControl("txtprice") as TextBox).Text;
                    SqlCommand cmd = new SqlCommand("insert into tblAllCharge(id,ORGID,PID,Bedno,BDate,UserId,CHARGETYPE,DSR,PRICE)values(@id,@ORGID,@PID,@Bedno,@BDate,@UserId,@CHARGETYPE,@DSR,@PRICE)", con);
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
                    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
                    cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = txtdate.Text;
                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
                    cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = dropcharge.ToString();
                    cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = txtdsc.ToString();
                    cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = txtprice.ToString();
                    cmd.ExecuteNonQuery();
                    using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
                    {
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                        cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                        cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                        cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
                        cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
                        cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = txtdsc.ToUpper();
                        cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtprice.ToString();
                        cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                        cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                        cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = txtprice.ToString();
                        cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
                        cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
                        cm.ExecuteNonQuery();
                    }
                    
                    if (dropcharge.ToString() == "BED CHARGE")
                    {
                        using (SqlCommand cm = new SqlCommand("per_tran", con))
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "BEDUPDATE";
                            cm.Parameters.Add("@REF", SqlDbType.VarChar).Value = "";
                            cm.Parameters.Add("@UHID", SqlDbType.VarChar).Value = UHID.Text;
                            cm.Parameters.Add("@PCHARGE", SqlDbType.Decimal).Value ="0.00" ;
                            cm.Parameters.Add("@PP", SqlDbType.Decimal).Value = "0.00";
                            cm.Parameters.Add("@LCHARGE", SqlDbType.Decimal).Value = "0.00";
                            cm.Parameters.Add("@LP", SqlDbType.Decimal).Value = "0.00";
                            cm.Parameters.Add("@BCHARGE", SqlDbType.Decimal).Value = Convert.ToDecimal(txtprice);
                            cm.Parameters.Add("@BP", SqlDbType.Decimal).Value = "0.00";
                            cm.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
      con.Close();
    }
    //public void UPDATE()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("UPDATE tblAllCharge SET CHARGETYPE=@CHARGETYPE,DSR=@DSR,PRICE=@PRICE,PID=@PID,Bedno=@Bedno WHERE id=@id", con);
    //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
    //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
    //    cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = txtdate.Text;
    //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
    //    cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = dropcharge.Text;
    //    cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = txtdsc.Text.ToUpper();
    //    cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = txtprice.Text;
    //    cmd.ExecuteNonQuery();
      
    //        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
    //        {
    //            cm.CommandType = CommandType.StoredProcedure;
    //            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
    //            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
    //            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
    //            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
    //            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = txtdsc.Text.ToUpper();
    //            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
    //            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
    //            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
    //            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
    //            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
    //            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
    //            cm.ExecuteNonQuery();
    //        }
    //        using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
    //        {
    //            cm.CommandType = CommandType.StoredProcedure;
    //            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
    //            cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
    //            cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
    //            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //            cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
    //            cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = txtdsc.Text.ToUpper();
    //            cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtprice.Text;
    //            cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
    //            cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
    //            cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = txtprice.Text;
    //            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
    //            cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
    //            cm.ExecuteNonQuery();
    //        }
    //    con.Close();
    //}
    public void delete()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        foreach (GridViewRow row in Grvchargeupdate.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string dropcharge = (row.Cells[1].FindControl("lblcharge") as Label).Text;
                string txtdsc = (row.Cells[2].FindControl("txtdescription") as TextBox).Text;
                string txtprice = (row.Cells[3].FindControl("txtprice") as TextBox).Text;
                using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
                    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
                    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
                    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
                    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = txtdsc.ToUpper();
                    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = dropcharge;
                    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
                    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
                    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = txtprice;
                    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
                    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
                    cm.ExecuteNonQuery();
                }
            }
        }
       
        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        // Validation

        //if (txtdsc.Text == "")
        //{
        //    string message = "alert('* Fields are mandatory.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

        //    return;
        //}
        //else if (Convert.ToDouble(txtprice.Text) < 0)
        //{
        //    string message = "alert('* Fields are mandatory.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    return;
        //}
        //else if (txtprice.Text == "")
        //{
        //    string message = "alert('* Fields are mandatory.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    txtprice.Text = "0";
        //    return;
        //}
        //else if (dropcharge.SelectedIndex == 0)
        //{
        //    string message = "alert('*SELECT CHARGE')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    txtprice.Text = "0";
        //    return;
        //}

        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            SqlCommand cmd = new SqlCommand("insert into tblallcharge2(id,ORGID,PID,Bedno,BDate,UserId)values(@id,@ORGID,@PID,@Bedno,@BDate,@UserId)", con);
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
            cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = txtdate.Text;
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
            cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "0.00";
            cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "0.00";
            cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "0.00";
            cmd.ExecuteNonQuery();
            insert();
            binddata();
            con.Close();

        
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        Response.Redirect("~/Reception/Copy of Ipcharge.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        // Validation
        //if (txtdsc.Text == "")
        //{
        //    string message = "alert('* Fields are mandatory.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
           
        //    return;
        //}
        //else if (Convert.ToDouble(txtprice.Text) < 0)
        //{
        //    string message = "alert('* Fields are mandatory.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    return;
        //}
        //else if (txtprice.Text == "")
        //{
        //    string message = "alert('* Fields are mandatory.')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    txtprice.Text = "0";
        //    return;
        //}
        //else if (dropcharge.SelectedIndex == 0)
        //{
        //    string message = "alert('*SELECT CHARGE')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    txtprice.Text = "0";
        //    return;
        //}
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string message = "alert('* You Cant Edit.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            dr.Close();
            //UPDATE();
            binddata();
            con.Close();
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/Reception/Copy of Ipcharge.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        Response.Redirect("~/Reception/Copy of Ipcharge.aspx");
    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Delete' and selected='False'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                string message = "alert('* You Cant delete.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            dr.Close();
            SqlCommand cm = new SqlCommand("delete from tblAllCharge where id='" + txtid.Text + "'", con);
            cm.ExecuteNonQuery();
            delete();
            binddata();
         
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/Reception/Copy of Ipcharge.aspx");
    }
}